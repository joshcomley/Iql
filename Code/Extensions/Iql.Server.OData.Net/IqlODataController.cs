using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using Brandless.AspNetCore.OData.Extensions.Binding;
using Brandless.AspNetCore.OData.Extensions.Configuration;
using Brandless.AspNetCore.OData.Extensions.Controllers;
using Brandless.Data.EntityFramework.Crud;
using Brandless.Data.Models;
using Brandless.Data.Mptt;
using Iql.Data;
using Iql.Data.Evaluation;
using Iql.Entities;
using Iql.Entities.NestedSets;
using Iql.Entities.PropertyGroups.Files;
using Iql.Entities.Services;
using Iql.Extensions;
using Iql.Server.Media;
using Iql.Server.OData.Net.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Iql.Server.OData.Net
{
    public abstract class IqlODataController<
        TService,
        TDbContextSecured,
        TDbContextUnsecured,
        TUser,
        TModel> : ODataCrudController<TDbContextSecured,
        TDbContextUnsecured,
        TUser,
        TModel>
        where TDbContextSecured : DbContext
        where TDbContextUnsecured : DbContext
        where TUser : class
        where TModel : class
    {
        private IEntityConfigurationBuilder _builder;
        private IMediaManager _mediaManager;
        private CrudManager _crudManager;
        private IqlMediaManager<TService> _iqlMediaManager;
        private EntityConfiguration<TModel> _entityConfiguration;

        public virtual EntityConfiguration<TModel> EntityConfiguration =>
            _entityConfiguration = _entityConfiguration ?? Builder.EntityType<TModel>();

        public virtual IEntityConfigurationBuilder Builder => _builder = _builder ??
                                                                         HttpContext.RequestServices
                                                                             .GetService<IEntityConfigurationProvider>()
                                                                             .Get<TService>();

        public virtual IMediaManager MediaManager =>
            _mediaManager = _mediaManager ?? HttpContext.RequestServices.GetService<IMediaManager>();

        public virtual CrudManager CrudManager =>
            _crudManager = _crudManager ?? new CrudManager(Crud.Unsecured.Context);

        public virtual IqlMediaManager<TService> IqlMediaManager => _iqlMediaManager = _iqlMediaManager ??
            new IqlMediaManager<TService>(
                HttpContext.RequestServices.GetService<IEntityConfigurationProvider>(),
                MediaManager,
                Crud.Unsecured.Context);

        protected virtual async Task<TUser> GetLoggedInUserAsync()
        {
            throw new NotImplementedException();
        }

        [ODataGenericAction(ForTypeTypeParameterName = nameof(TModel))]
        [HttpPost]
        public virtual async Task<IActionResult> IncrementVersion(
            [ModelBinder(typeof(KeyValueBinder))] KeyValuePair<string, object>[] key,
            [FromBody] IncrementVersionModel model)
        {
            if (!string.IsNullOrWhiteSpace(model.PropertyName))
            {
                var entity = Crud.Unsecured.Find(key);
                if (entity != null)
                {
                    var propertyFound = entity.GetType().GetProperties()
                        .SingleOrDefault(_ => _.Name == model.PropertyName);
                    if (propertyFound != null)
                    {
                        var versionValue = DateTime.UtcNow.Ticks.ToString();
                        propertyFound.SetValue(entity, versionValue);
                        await Crud.Unsecured.SaveAsync();
                    }
                }
            }

            return Ok();
        }

        protected override async Task OnAfterPostAsync(TModel postedEntity)
        {
            if (EntityConfiguration.NestedSets != null && EntityConfiguration.NestedSets.Any())
            {
                foreach (var nestedSet in EntityConfiguration.NestedSets)
                {
                    await ApplyNewNestedSetItemAsync(nestedSet, postedEntity);
                }
            }

            await base.OnAfterPostAsync(postedEntity);
        }

        protected override async Task OnAfterDeleteAsync(KeyValuePair<string, object>[] key, TModel entity,
            DeleteActionResult result)
        {
            if (result.Success)
            {
                await DeleteAssociatedMediaAsync(key, entity, result);
            }

            await base.OnAfterDeleteAsync(key, entity, result);
        }

        [HttpGet]
        // [EnableQuery]
        public override async Task<IActionResult> Get(
            [ModelBinder(typeof(KeyValueBinder))] KeyValuePair<string, object>[] key)
        {
            IActionResult result;
            var entityQuery = await GetEntityQuery(key);
            if (entityQuery == null || entityQuery.Count() != 1)
            {
                result = NotFound();
            }
            else
            {
                result = Ok(new SingleResult<TModel>(entityQuery));
            }

            return result;
        }

        private readonly Dictionary<DeleteActionResult, List<Func<Task>>> _deleteMediaTasks =
            new Dictionary<DeleteActionResult, List<Func<Task>>>();

        protected override async Task<DeleteActionResult> DeleteEntityAsync(KeyValuePair<string, object>[] key,
            TModel entity)
        {
            DeleteActionResult result = null;
            var serverEvaluator = NewDataEvaluator();
            var deleteMediaTasks =
                await MediaManager.GetDeleteAssociatedMediaTasksAsync(entity, Builder, serverEvaluator);
            var nestedSetDelete = await DeleteNestedSetEntriesAsync(entity);
            if (nestedSetDelete.HasValue)
            {
                result = new DeleteActionResult(entity, nestedSetDelete.Value,
                    nestedSetDelete.Value ? DeleteEntityResult.Success : DeleteEntityResult.Conflict);
            }
            else
            {
                result = await base.DeleteEntityAsync(key, entity);
            }

            if (deleteMediaTasks != null && deleteMediaTasks.Any())
            {
                _deleteMediaTasks.Add(result, deleteMediaTasks);
            }

            return result;
        }

        private async Task<bool?> DeleteNestedSetEntriesAsync(TModel entity)
        {
            bool? success = null;
            if (EntityConfiguration.NestedSets != null && EntityConfiguration.NestedSets.Any())
            {
                foreach (var nestedSet in EntityConfiguration.NestedSets)
                {
                    var result = await ApplyDeleteNestedSetItem(nestedSet, entity);
                    if (result == false)
                    {
                        success = false;
                    }
                    else if (result == true && success == null)
                    {
                        success = true;
                    }
                }
            }

            return success;
        }

        protected override async Task OnAfterPatchAsync(KeyValuePair<string, object>[] id, TModel currentEntity,
            Delta<TModel> patch, TModel prePatchObject)
        {
            if (EntityConfiguration.NestedSets != null && EntityConfiguration.NestedSets.Any())
            {
                foreach (var nestedSet in EntityConfiguration.NestedSets)
                {
                    await ApplyExistingNestedSetItemAsync(nestedSet, currentEntity, prePatchObject, patch);
                }
            }

            await base.OnAfterPatchAsync(id, currentEntity, patch, prePatchObject);
        }

        private async Task<bool?> ApplyDeleteNestedSetItem(INestedSet nestedSet, TModel entity)
        {
            if (Equals(nestedSet.LeftProperty.GetValue(entity), 0) &&
                Equals(nestedSet.RightProperty.GetValue(entity), 0))
            {
                return null;
            }

            var method = typeof(IqlODataController<
                    TService,
                    TDbContextSecured,
                    TDbContextUnsecured,
                    TUser,
                    TModel>).GetMethod(nameof(ApplyDeleteNestedSetItemTyped),
                    BindingFlags.Instance | BindingFlags.NonPublic)
                ;
            var genericMethod = method.MakeGenericMethod(
                nestedSet.IdProperty.TypeDefinition.Type,
                nestedSet.ParentIdProperty.TypeDefinition.Type,
                nestedSet.KeyProperty.TypeDefinition.Type);
            return await (Task<bool>)genericMethod
                .Invoke(this, new object[] { nestedSet, entity, nestedSet.KeyProperty.GetValue(entity) });
        }

        private Task<bool> ApplyDeleteNestedSetItemTyped<TKey, TNullableKey, TTreeKey>(
            INestedSet nestedSet,
            TModel entity,
            TTreeKey key)
        {
            var mptt = new MpttManager<TKey, TNullableKey, TTreeKey>(
                () => Data.Context,
                key,
                nestedSet.LeftProperty.Name,
                nestedSet.RightProperty.Name,
                nestedSet.LeftOfProperty.Name,
                nestedSet.RightOfProperty.Name,
                nestedSet.IdProperty.Name,
                nestedSet.ParentIdProperty.Name,
                nestedSet.LevelProperty.Name,
                nestedSet.KeyProperty.Name,
                Data.Context.Model.FindEntityType(typeof(TModel)).GetTableName()
            );
            var id = nestedSet.IdProperty.GetValue(entity);
            return mptt.DeleteAsync((TKey)id);
        }

        protected virtual Task ApplyNewNestedSetItemAsync(INestedSet nestedSet, TModel postedEntity)
        {
            var method = typeof(IqlODataController<
                    TService,
                    TDbContextSecured,
                    TDbContextUnsecured,
                    TUser,
                    TModel>).GetMethod(nameof(ApplyNewNestedSetItemTypedAsync),
                    BindingFlags.Instance | BindingFlags.NonPublic)
                ;
            var genericMethod = method.MakeGenericMethod(
                nestedSet.IdProperty.TypeDefinition.Type,
                nestedSet.ParentIdProperty.TypeDefinition.Type,
                nestedSet.KeyProperty.TypeDefinition.Type);
            return (Task)genericMethod
                .Invoke(this, new object[] { nestedSet, postedEntity, nestedSet.KeyProperty.GetValue(postedEntity) });
        }

        protected virtual Task ApplyExistingNestedSetItemAsync(INestedSet nestedSet, TModel currentEntity,
            TModel prePatchEntity, Delta<TModel> patch)
        {
            var method = typeof(IqlODataController<
                    TService,
                    TDbContextSecured,
                    TDbContextUnsecured,
                    TUser,
                    TModel>).GetMethod(nameof(ApplyExistingNestedSetItemTypedAsync),
                    BindingFlags.Instance | BindingFlags.NonPublic)
                ;
            var genericMethod = method.MakeGenericMethod(
                nestedSet.IdProperty.TypeDefinition.Type,
                nestedSet.ParentIdProperty.TypeDefinition.Type,
                nestedSet.KeyProperty.TypeDefinition.Type);
            return (Task)genericMethod
                .Invoke(this,
                    new object[]
                    {
                        nestedSet, currentEntity, prePatchEntity, patch, nestedSet.KeyProperty.GetValue(currentEntity)
                    });
        }

        protected virtual async Task ApplyExistingNestedSetItemTypedAsync<TKey, TNullableKey, TTreeKey>(
            INestedSet nestedSet,
            TModel currentEntity,
            TModel prePatchEntity,
            Delta<TModel> patch,
            TTreeKey key)
        {
            var mptt = new MpttManager<TKey, TNullableKey, TTreeKey>(
                () => Data.Context,
                key,
                nestedSet.LeftProperty.Name,
                nestedSet.RightProperty.Name,
                nestedSet.LeftOfProperty.Name,
                nestedSet.RightOfProperty.Name,
                nestedSet.IdProperty.Name,
                nestedSet.ParentIdProperty.Name,
                nestedSet.LevelProperty.Name,
                nestedSet.KeyProperty.Name,
                Data.Context.Model.FindEntityType(typeof(TModel)).GetTableName()
            );
            var id = nestedSet.IdProperty.GetValue(currentEntity);
            var parentId = nestedSet.ParentIdProperty.GetValue(prePatchEntity);
            var leftOf = nestedSet.LeftOfProperty.GetValue(currentEntity);
            var rightOf = nestedSet.RightOfProperty.GetValue(currentEntity);
            if (!Equals(leftOf, null))
            {
                await mptt.MoveToAsync((TKey)id, (TNullableKey)leftOf, NodeMoveKind.Left);
            }
            else if (!Equals(rightOf, null))
            {
                await mptt.MoveToAsync((TKey)id, (TNullableKey)rightOf, NodeMoveKind.Right);
            }
            else if (
                patch.GetChangedPropertyNames().Any(_ => _ == nestedSet.ParentIdProperty.Name) &&
                patch.TryGetPropertyValue(nestedSet.ParentIdProperty.Name, out var value))
            {
                if (!Equals(value, parentId) && !Equals(value, null) && !Equals(value, default(TNullableKey)))
                {
                    await mptt.MoveToAsync((TKey)id, (TNullableKey)value, NodeMoveKind.Beneath);
                }
            }
        }

        protected virtual async Task ApplyNewNestedSetItemTypedAsync<TKey, TNullableKey, TTreeKey>(
            INestedSet nestedSet,
            TModel postedEntity,
            TTreeKey key)
        {
            var mptt = new MpttManager<TKey, TNullableKey, TTreeKey>(
                () => Data.Context,
                key,
                nestedSet.LeftProperty.Name,
                nestedSet.RightProperty.Name,
                nestedSet.LeftOfProperty.Name,
                nestedSet.RightOfProperty.Name,
                nestedSet.IdProperty.Name,
                nestedSet.ParentIdProperty.Name,
                nestedSet.LevelProperty.Name,
                nestedSet.KeyProperty.Name,
                Data.Context.Model.FindEntityType(typeof(TModel)).GetTableName()
            );
            var id = nestedSet.IdProperty.GetValue(postedEntity);
            var parentId = nestedSet.ParentIdProperty.GetValue(postedEntity);
            var leftOf = nestedSet.LeftOfProperty.GetValue(postedEntity);
            var rightOf = nestedSet.RightOfProperty.GetValue(postedEntity);
            if (!Equals(leftOf, null))
            {
                await mptt.InsertToLeftOfAsync((TKey)id, (TKey)leftOf);
            }
            else if (!Equals(rightOf, null))
            {
                await mptt.InsertToRightOfAsync((TKey)id, (TKey)rightOf);
            }
            else
            {
                await mptt.InsertAsChildOfAsync((TKey)id, (TNullableKey)parentId);
            }
        }

        protected virtual IServiceProviderProvider ResolveServiceProviderProvider()
        {
            var context = new IqlHttpServiceProviderContext(
                HttpContext,
                EntityConfiguration.Builder,
                ResolverUserIdByName,
                NewDataEvaluator(),
                null);
            context.CurrentUserService = NewCurrentUserService(context);
            return new IqlHttpServiceProviderProvider<TUser>(context);
        }

        protected virtual IqlCurrentUserService NewCurrentUserService(IqlHttpServiceProviderContext context)
        {
            return new IqlHttpCurrentUserService<TUser>(context);
        }

        protected virtual Task<object> ResolverUserIdByName(string name)
        {
            return Task.FromResult<object>(null);
        }

        protected override async Task OnPatchAsync(Delta<TModel> patch, TModel dbObject)
        {
            var serverEvaluator = NewDataEvaluator();
            //if (EntityConfiguration.Files != null && EntityConfiguration.Files.Count > 0)
            //{
            //    var changedProperties = patch.GetChangedPropertyNames();
            //    foreach (var file in EntityConfiguration.Files)
            //    {
            //        if (file.VersionProperty != null && changedProperties.Contains(file.VersionProperty.Name))
            //        {
            //            OnNewFileReceived
            //        }
            //    }
            //}
            var clone = (TModel)dbObject.Clone(Builder, EntityConfiguration.Type);
            await base.OnPatchAsync(patch, dbObject);
            await new InferredValueEvaluationSession()
                .TrySetInferredValuesCustomAsync(
                    EntityConfiguration,
                    clone,
                    dbObject,
                    false,
                    serverEvaluator,
                    ResolveServiceProviderProvider());
            ClearNestedEntities(dbObject);
        }

        protected virtual IIqlDataEvaluator NewDataEvaluator(params object[] unsavedEntities)
        {
            return new IqlServerEvaluator(CrudManager, () => Crud.NewUnsecuredDb(), unsavedEntities);
        }

        private static bool IsGuid(string expression)
        {
            if (expression != null)
            {
                Regex guidRegEx =
                    new Regex(
                        @"^(\{{0,1}([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}\}{0,1})$");

                return guidRegEx.IsMatch(expression);
            }

            return false;
        }

        protected override async Task OnBeforePostAsync(TModel currentEntity)
        {
            var serverEvaluator = NewDataEvaluator(currentEntity);
            if (EntityConfiguration.PersistenceKeyProperty != null && currentEntity != null)
            {
                var value = EntityConfiguration.PersistenceKeyProperty.GetValue(currentEntity);
                while (true)
                {
                    if (Equals(null, value))
                    {
                        value = Guid.NewGuid();
                    }

                    var str = value.ToString();
                    if (string.IsNullOrWhiteSpace(str))
                    {
                        value = Guid.NewGuid();
                    }

                    str = value.ToString();
                    if (EntityConfiguration.PersistenceKeyProperty.PropertyInfo.PropertyType == typeof(Guid))
                    {
                        if (str == new Guid().ToString() || !IsGuid(str))
                        {
                            value = Guid.NewGuid();
                        }
                    }

                    if (EntityConfiguration.PersistenceKeyProperty.PropertyInfo.PropertyType == typeof(string))
                    {
                        value = value.ToString();
                    }

                    var param = Expression.Parameter(typeof(TModel));
                    var pred = Expression.Lambda(
                        Expression.Equal(
                            Expression.Property(param, EntityConfiguration.PersistenceKeyProperty.Name),
                            Expression.Constant(value)
                        ),
                        param
                    );
                    var compiled = (Func<TModel, bool>)pred.Compile();
                    var result = CrudManager.DbSet<TModel>().Where(compiled).SingleOrDefault();
                    if (result == null)
                    {
                        break;
                    }
                }

                EntityConfiguration.PersistenceKeyProperty.SetValue(currentEntity, value);
            }

            if (currentEntity != null)
            {
                await new InferredValueEvaluationSession().TrySetInferredValuesCustomAsync(
                    EntityConfiguration,
                    null,
                    currentEntity,
                    true,
                    serverEvaluator,
                    ResolveServiceProviderProvider());
                ClearNestedEntities(currentEntity);
            }
        }

        protected override async Task OnBeforePostAndPatchAsync(TModel currentEntity, Delta<TModel>? patch)
        {
            if (EntityConfiguration.Files != null && currentEntity != null)
            {
                foreach (var file in EntityConfiguration.Files)
                {
                    if (file.VersionProperty != null &&
                        (
                            (patch == null && !Equals(null,
                                currentEntity.GetPropertyValueByName(file.VersionProperty.Name)))
                            ||
                            (patch != null && patch.GetChangedPropertyNames().Contains(file.VersionProperty.Name))
                        )
                       )
                    {
                        await UpdateFileUrlWithVersionAsync(currentEntity, patch, (IFileUrl<TModel>)file);
                    }
                }
            }

            await base.OnBeforePostAndPatchAsync(currentEntity, patch);
        }

        protected virtual void ClearNestedEntities(TModel currentEntity)
        {
            // Clear any nested entities
            foreach (var property in EntityConfiguration.Properties)
            {
                property.Relationship?.ThisEnd.Property.SetValue(currentEntity, null);
            }
        }

        protected virtual async Task UpdateFileUrlWithVersionAsync(TModel currentEntity, Delta<TModel> patch,
            IFileUrl<TModel> file)
        {
            // Don't update this if we're setting it ourselves
            if (patch.GetChangedPropertyNames().Any(_ => _ == file.UrlProperty.Name) &&
                patch.TryGetPropertyValue(file.UrlProperty.Name, out var newUrlObj))
            {
                var newUrl = newUrlObj as string;
                if (string.IsNullOrWhiteSpace(newUrl))
                {
                    await MediaManager.DeleteAsync(currentEntity, file);
                    return;
                }
            }

            var url = (string)file.UrlProperty.GetValue(currentEntity);
            if (!string.IsNullOrWhiteSpace(url))
            {
                var uriBuilder = new UriBuilder(url);
                string version = null;
                if (patch == null)
                {
                    version = (string)file.RootFile.VersionProperty.GetValue(currentEntity);
                }
                else
                {
                    if (patch.TryGetPropertyValue(file.RootFile.VersionProperty.Name, out var v))
                    {
                        version = (string)v;
                    }
                }

                var query = HttpUtility.ParseQueryString(uriBuilder.Query);
                query[$"iql_{file.UrlProperty.Name}_{file.RootFile.VersionProperty.Name}"] = version;
                uriBuilder.Query = query.ToString();
                url = uriBuilder.ToString();
                patch?.TrySetPropertyValue(file.UrlProperty.Name, url);
                file.UrlProperty.SetValue(currentEntity, url);
            }
        }

        protected virtual bool HasChangedPropertyValue(TModel entity, Delta<TModel> patch, string propertyName)
        {
            if (patch == null)
            {
                return HasPostedPropertyValue(entity, patch, propertyName);
            }

            if (patch.TryGetPropertyValue(propertyName, out var newValue))
            {
                return !Equals(newValue, entity.GetPropertyValueByName(propertyName));
            }

            return false;
        }

        private bool HasPostedPropertyValue(TModel entity, Delta<TModel> patch, string propertyName)
        {
            return !Equals(null, PostedPropertyValue(entity, patch, propertyName));
        }

        private object PostedPropertyValue(TModel entity, Delta<TModel> patch, string propertyName)
        {
            if (patch == null)
            {
                return entity.GetPropertyValueByName(propertyName);
            }

            if (patch.TryGetPropertyValue(propertyName, out var r))
            {
                return r;
            }

            return null;
        }

        protected override async Task OnAfterPostAndPatchAsync(TModel currentEntity, Delta<TModel> patch,
            TModel prePatchObject)
        {
            // Download the file, generate thumbnail, update preview URL, delete file
            // Enqueue a worker item
            KeyValuePair<string, object>[] entityKey = null;
            var saveChanges = false;
            foreach (var file_ in EntityConfiguration.Files)
            {
                var file = (File<TModel>)file_;
                if (file.MediaKey != null)
                {
                    entityKey = entityKey ?? CrudManager.EntityKey(currentEntity);
                    var populatedEntity = await PreloadMediaKeyDependenciesAsync(entityKey, file);
                    if (HttpContext.Request.Method.ToUpper() == "POST")
                    {
                        var existingUrl = file.UrlProperty.GetValue(populatedEntity) as string;
                        if (!string.IsNullOrWhiteSpace(existingUrl))
                        {
                            var newUrl =
                                await MediaManager.SetMediaUriAsync(populatedEntity, file, MediaAccessKind.Admin);
                            var oldUrl = existingUrl;
                            await MediaManager.CloneUrlAsync(oldUrl, newUrl);
                            file.UrlProperty.SetValue(populatedEntity, newUrl);
                            await IqlMediaManager.Context.SaveChangesAsync();
                        }
                    }

                    if (EnqueueThumbnails)
                    {
                        var revisionKeysForMediaProperty =
                            file.VersionProperty;
                        var requiresNewPreviews =
                            revisionKeysForMediaProperty != null &&
                            HasChangedPropertyValue(currentEntity, patch, revisionKeysForMediaProperty.Name);
                        // TODO: Only refresh previews if file version has changed
                        if (requiresNewPreviews)
                        {
                            await UpdatePreviewsAsync(currentEntity, patch, file, entityKey);
                        }
                    }
                }
            }

            await base.OnAfterPostAndPatchAsync(currentEntity, patch, prePatchObject);
        }

        public virtual bool EnqueueThumbnails => false;

        protected virtual async Task UpdatePreviewsAsync(TModel currentEntity, Delta<TModel> patch, File<TModel> file,
            KeyValuePair<string, object>[] entityKey)
        {
            TModel populatedEntity;
            foreach (var filePreview_ in file.Previews)
            {
                try
                {
                    var filePreview = (IFileUrl<TModel>)filePreview_;
                    var previewProperty = filePreview.UrlProperty;
                    populatedEntity = await PreloadMediaKeyDependenciesAsync(entityKey, file);
                    populatedEntity = await PreloadMediaKeyDependenciesAsync(entityKey, filePreview);
                    var sourceUrl =
                        await MediaManager.SetMediaUriAsync(populatedEntity, file, MediaAccessKind.ReadOnly);
                    // Should be preview
                    var targetUrl =
                        await MediaManager.SetMediaUriAsync(populatedEntity, filePreview, MediaAccessKind.Admin);
                    // Should be preview
                    var targetUrlReadOnly =
                        await MediaManager.SetMediaUriAsync(populatedEntity, filePreview, MediaAccessKind.ReadOnly);
                    previewProperty.SetValue(populatedEntity, targetUrlReadOnly);
                    await IqlMediaManager.Context.SaveChangesAsync();
                    // Should be preview
                    await MediaManager.DeleteAsync(populatedEntity, filePreview);
                    await EnqueueThumbnailRequest(sourceUrl, targetUrl);
                    await UpdateFileUrlWithVersionAsync(currentEntity, patch, filePreview);
                }
                catch (Exception e)
                {
                    // TODO: Log exception message
                }
            }
        }

        protected abstract Task EnqueueThumbnailRequest(string sourceUrl, string targetUrl);

        [ODataGenericFunction(ForTypeTypeParameterName = nameof(TModel))]
        [HttpGet]
        public virtual async Task<ActionResult<MediaUrl>> GetMediaUploadUrl(
            [ModelBinder(typeof(KeyValueBinder))] KeyValuePair<string, object>[] key,
            [FromRoute] string property
        )
        {
            return new JsonResult(await IqlMediaManager.GetAndSetMediaUrlAsync<TModel>(
                key,
                property
            ));
        }

        internal virtual async Task<MediaUrl> GetMediaUrl(KeyValuePair<string, object>[] key,
            IEntityProperty<TModel> propertyMetadata, MediaAccessKind mediaAccessKind,
            TimeSpan? lifetime = null)
        {
            return await IqlMediaManager.GetAndSetMediaUrlAsync(
                key,
                propertyMetadata,
                mediaAccessKind,
                lifetime
            );
        }
        
        protected virtual async Task DeleteAssociatedMediaAsync(
            KeyValuePair<string, object>[] key,
            TModel entity,
            DeleteActionResult result)
        {
            if (_deleteMediaTasks.ContainsKey(result))
            {
                var tasks = _deleteMediaTasks[result];
                await Task.WhenAll(tasks.Select(_ => _()));
                _deleteMediaTasks.Remove(result);
            }
        }

        protected virtual Task<TModel> PreloadMediaKeyDependenciesAsync(KeyValuePair<string, object>[] key,
            IFileUrl<TModel> file)
        {
            return IqlMediaManager.PreloadMediaKeyDependenciesAsync<TModel>(key, file);
        }

        protected virtual Task<TModel> PreloadPropertyPathAsync(KeyValuePair<string, object>[] key,
            IqlPropertyPath path)
        {
            var query = CrudManager.FindQuery<TModel>(key);
            var relationshipPath = path.GetRelationshipPathToHere(".");
            if (!string.IsNullOrWhiteSpace(relationshipPath))
            {
                query = query.Include(relationshipPath);
            }

            var where = CrudManager.KeyEqualsExpression<TModel>(key);
            var finalQuery = query.Where(where);
            return finalQuery.SingleOrDefaultAsync();
        }

        protected override async Task<bool> ValidateEntityAsync<TEntity>(TEntity entity, string path, bool isValid,
            string accessor)
        {
            var modelConfiguration = Builder.EntityType<TEntity>();
            if (modelConfiguration != null)
            {
                if (modelConfiguration.EntityValidation?.All?.Any() == true)
                {
                    foreach (var entityValidation in modelConfiguration.EntityValidation.All)
                    {
                        var iqlValidationResult = entityValidation.Run(entity);
                        isValid = isValid && iqlValidationResult;
                        if (!iqlValidationResult)
                        {
                            AddModelError<TEntity>(path, entityValidation.Message, entityValidation.Key);
                        }
                    }
                }

                foreach (var property in modelConfiguration.Properties)
                {
                    if (property.ValidationRules?.All?.Any() == true)
                    {
                        foreach (var propertyValidation in property.ValidationRules.All)
                        {
                            var iqlValidationResult = propertyValidation.Run(entity);
                            isValid = isValid && iqlValidationResult;
                            if (!iqlValidationResult)
                            {
                                AddModelError<TEntity>($"{path}{accessor}{property.Name}",
                                    propertyValidation.Message, propertyValidation.Key);
                            }
                        }
                    }
                }
            }

            return isValid && await base.ValidateEntityAsync(entity, path, isValid, accessor);
        }
    }
}