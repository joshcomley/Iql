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
using Iql.Data.Extensions;
using Iql.Entities;
using Iql.Entities.NestedSets;
using Iql.Entities.PropertyGroups.Files;
using Iql.Entities.Services;
using Iql.Extensions;
using Iql.Server.Media;
using Iql.Server.OData.Net.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Iql.Server.OData.Net
{
    public abstract class IqlODataController<
        TService,
        TDbContextSecured,
        TDbContextUnsecured,
        TUser,
        T> : ODataCrudController<TDbContextSecured,
        TDbContextUnsecured,
        TUser,
        T>
        where TDbContextSecured : DbContext
        where TDbContextUnsecured : DbContext
        where TUser : class
        where T : class
    {
        private IEntityConfigurationBuilder _builder;
        private IMediaManager _mediaManager;
        private CrudManager _crudManager;
        private ODataMediaManager<TService> _oDataMediaManager;
        private EntityConfiguration<T> _entityConfiguration;

        public EntityConfiguration<T> EntityConfiguration =>
            _entityConfiguration = _entityConfiguration ?? Builder.EntityType<T>();
        public IEntityConfigurationBuilder Builder => _builder = _builder ?? HttpContext.RequestServices.GetService<IEntityConfigurationProvider>().Get<TService>();

        public IMediaManager MediaManager => _mediaManager = _mediaManager ?? HttpContext.RequestServices.GetService<IMediaManager>();

        public CrudManager CrudManager => _crudManager = _crudManager ?? new CrudManager(Crud.Unsecured.Context);
        public ODataMediaManager<TService> ODataMediaManager => _oDataMediaManager = _oDataMediaManager ?? new ODataMediaManager<TService>(
                                                                                         HttpContext.RequestServices.GetService<IEntityConfigurationProvider>(),
                                                                                         MediaManager,
                                                                                         Crud.Unsecured.Context);

        [ODataGenericAction(ForTypeTypeParameterName = nameof(T), BindingName = "keys")]
        public virtual async Task<IActionResult> IncrementVersion([ModelBinder(typeof(KeyValueBinder))] KeyValuePair<string, object>[] keys, [FromBody]IncrementVersionModel model)
        {
            if (!string.IsNullOrWhiteSpace(model.PropertyName))
            {
                var entity = Crud.Unsecured.Find(keys);
                if (entity != null)
                {
                    var propertyFound = entity.GetType().GetProperties().SingleOrDefault(_ => _.Name == model.PropertyName);
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

        protected override async Task OnAfterPostAsync(T postedEntity)
        {
            if (EntityConfiguration.NestedSets != null && EntityConfiguration.NestedSets.Any())
            {
                foreach (var nestedSet in EntityConfiguration.NestedSets)
                {
                    await ApplyNewNestedSetItem(nestedSet, postedEntity);
                }
            }
            await base.OnAfterPostAsync(postedEntity);
        }

        protected override async Task OnAfterDeleteAsync(KeyValuePair<string, object>[] key, T entity, DeleteActionResult result)
        {
            if (result.Success)
            {
                await DeleteAssociatedMediaAsync(key, entity);
            }
            await base.OnAfterDeleteAsync(key, entity, result);
        }

        protected override async Task<DeleteActionResult> DeleteEntityAsync(KeyValuePair<string, object>[] key, T entity)
        {
            var nestedSetDelete = await DeleteNestedSetEntriesAsync(entity);
            if (nestedSetDelete.HasValue)
            {
                return new DeleteActionResult(entity, nestedSetDelete.Value, nestedSetDelete.Value ? DeleteEntityResult.Success : DeleteEntityResult.Conflict);
            }
            return await base.DeleteEntityAsync(key, entity);
        }

        private async Task<bool?> DeleteNestedSetEntriesAsync(T entity)
        {
            bool? success = null;
            if (EntityConfiguration.NestedSets != null && EntityConfiguration.NestedSets.Any())
            {
                success = true;
                foreach (var nestedSet in EntityConfiguration.NestedSets)
                {
                    if (!await ApplyDeleteNestedSetItem(nestedSet, entity))
                    {
                        success = false;
                    }
                }
            }
            return success;
        }

        protected override async Task OnAfterPatchAsync(KeyValuePair<string, object>[] id, T currentEntity, Delta<T> patch)
        {
            if (EntityConfiguration.NestedSets != null && EntityConfiguration.NestedSets.Any())
            {
                foreach (var nestedSet in EntityConfiguration.NestedSets)
                {
                    await ApplyExistingNestedSetItem(nestedSet, currentEntity, patch);
                }
            }
            await base.OnAfterPatchAsync(id, currentEntity, patch);
        }

        private Task<bool> ApplyDeleteNestedSetItem(INestedSet nestedSet, T entity)
        {
            var method = typeof(IqlODataController<
                    TService,
                    TDbContextSecured,
                    TDbContextUnsecured,
                    TUser,
                    T>).GetMethod(nameof(ApplyDeleteNestedSetItemTyped), BindingFlags.Instance | BindingFlags.NonPublic)
                ;
            var genericMethod = method.MakeGenericMethod(
                nestedSet.IdProperty.TypeDefinition.Type,
                nestedSet.ParentIdProperty.TypeDefinition.Type,
                nestedSet.KeyProperty.TypeDefinition.Type);
            return (Task<bool>)genericMethod
                .Invoke(this, new object[] { nestedSet, entity, nestedSet.KeyProperty.GetValue(entity) });
        }

        private Task<bool> ApplyDeleteNestedSetItemTyped<TKey, TNullableKey, TTreeKey>(
            INestedSet nestedSet,
            T entity,
            TTreeKey key)
        {
            var mptt = new MpttManager<TKey, TNullableKey, TTreeKey>(
                () => Data.Context,
                key,
                nestedSet.LeftProperty.PropertyName,
                nestedSet.RightProperty.PropertyName,
                nestedSet.LeftOfProperty.PropertyName,
                nestedSet.RightOfProperty.PropertyName,
                nestedSet.IdProperty.PropertyName,
                nestedSet.ParentIdProperty.PropertyName,
                nestedSet.LevelProperty.PropertyName,
                nestedSet.KeyProperty.PropertyName,
                Data.Context.Model.FindEntityType(typeof(T)).Relational().TableName
            );
            var id = nestedSet.IdProperty.GetValue(entity);
            return mptt.DeleteAsync((TKey)id);
        }

        private Task ApplyNewNestedSetItem(INestedSet nestedSet, T postedEntity)
        {
            var method = typeof(IqlODataController<
                    TService,
                    TDbContextSecured,
                    TDbContextUnsecured,
                    TUser,
                    T>).GetMethod(nameof(ApplyNewNestedSetItemTyped), BindingFlags.Instance | BindingFlags.NonPublic)
                ;
            var genericMethod = method.MakeGenericMethod(
                nestedSet.IdProperty.TypeDefinition.Type,
                nestedSet.ParentIdProperty.TypeDefinition.Type,
                nestedSet.KeyProperty.TypeDefinition.Type);
            return (Task)genericMethod
                .Invoke(this, new object[] { nestedSet, postedEntity, nestedSet.KeyProperty.GetValue(postedEntity) });
        }

        private Task ApplyExistingNestedSetItem(INestedSet nestedSet, T currentEntity, Delta<T> patch)
        {
            var method = typeof(IqlODataController<
                    TService,
                    TDbContextSecured,
                    TDbContextUnsecured,
                    TUser,
                    T>).GetMethod(nameof(ApplyExistingNestedSetItemTyped), BindingFlags.Instance | BindingFlags.NonPublic)
                ;
            var genericMethod = method.MakeGenericMethod(
                nestedSet.IdProperty.TypeDefinition.Type,
                nestedSet.ParentIdProperty.TypeDefinition.Type,
                nestedSet.KeyProperty.TypeDefinition.Type);
            return (Task)genericMethod
                .Invoke(this, new object[] { nestedSet, currentEntity, patch, nestedSet.KeyProperty.GetValue(currentEntity) });
        }

        private async Task ApplyExistingNestedSetItemTyped<TKey, TNullableKey, TTreeKey>(
            INestedSet nestedSet,
            T currentEntity,
            Delta<T> patch,
            TTreeKey key)
        {
            var mptt = new MpttManager<TKey, TNullableKey, TTreeKey>(
                () => Data.Context,
                key,
                nestedSet.LeftProperty.PropertyName,
                nestedSet.RightProperty.PropertyName,
                nestedSet.LeftOfProperty.PropertyName,
                nestedSet.RightOfProperty.PropertyName,
                nestedSet.IdProperty.PropertyName,
                nestedSet.ParentIdProperty.PropertyName,
                nestedSet.LevelProperty.PropertyName,
                nestedSet.KeyProperty.PropertyName,
                Data.Context.Model.FindEntityType(typeof(T)).Relational().TableName
            );
            var id = nestedSet.IdProperty.GetValue(currentEntity);
            var parentId = nestedSet.ParentIdProperty.GetValue(currentEntity);
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
            else if (patch.TryGetPropertyValue(nestedSet.ParentIdProperty.PropertyName, out var value))
            {
                if (!Equals(value, parentId))
                {
                    await mptt.MoveToAsync((TKey)id, (TNullableKey)parentId, NodeMoveKind.Beneath);
                }
            }
        }

        private async Task ApplyNewNestedSetItemTyped<TKey, TNullableKey, TTreeKey>(
            INestedSet nestedSet,
            T postedEntity,
            TTreeKey key)
        {
            var mptt = new MpttManager<TKey, TNullableKey, TTreeKey>(
                () => Data.Context,
                key,
                nestedSet.LeftProperty.PropertyName,
                nestedSet.RightProperty.PropertyName,
                nestedSet.LeftOfProperty.PropertyName,
                nestedSet.RightOfProperty.PropertyName,
                nestedSet.IdProperty.PropertyName,
                nestedSet.ParentIdProperty.PropertyName,
                nestedSet.LevelProperty.PropertyName,
                nestedSet.KeyProperty.PropertyName,
                Data.Context.Model.FindEntityType(typeof(T)).Relational().TableName
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

        public virtual IServiceProviderProvider ResolveServiceProviderProvider()
        {
            return new IqlHttpServiceProviderProvider<TUser>(
                new IqlHttpServiceProviderContext(
                    HttpContext,
                    ResolverUserIdByName));
        }

        public virtual Task<object> ResolverUserIdByName(string name)
        {
            return Task.FromResult<object>(null);
        }

        protected override async Task OnPatchAsync(Delta<T> patch, T dbObject)
        {
            var serverEvaluator = new IqlServerEvaluator(CrudManager, () => Crud.NewUnsecuredDb());
            var clone = (T)dbObject.Clone(Builder, EntityConfiguration.Type);
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

        private static bool IsGuid(string expression)
        {
            if (expression != null)
            {
                Regex guidRegEx = new Regex(@"^(\{{0,1}([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}\}{0,1})$");

                return guidRegEx.IsMatch(expression);
            }
            return false;
        }

        protected override async Task OnBeforePostAsync(T currentEntity)
        {
            var serverEvaluator = new IqlServerEvaluator(CrudManager, () => Crud.NewUnsecuredDb(), currentEntity);
            if (EntityConfiguration.PersistenceKeyProperty != null)
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
                    var param = Expression.Parameter(typeof(T));
                    var pred = Expression.Lambda(
                        Expression.Equal(
                            Expression.Property(param, EntityConfiguration.PersistenceKeyProperty.PropertyName),
                            Expression.Constant(value)
                        ),
                        param
                    );
                    var compiled = (Func<T, bool>)pred.Compile();
                    var result = CrudManager.DbSet<T>().Where(compiled).SingleOrDefault();
                    if (result == null)
                    {
                        break;
                    }
                }

                EntityConfiguration.PersistenceKeyProperty.SetValue(currentEntity, value);
            }
            await new InferredValueEvaluationSession().TrySetInferredValuesCustomAsync(
                EntityConfiguration,
                null,
                currentEntity,
                true,
                serverEvaluator,
                ResolveServiceProviderProvider());
            ClearNestedEntities(currentEntity);
        }

        protected override async Task OnBeforePostAndPatchAsync(T currentEntity, Delta<T> patch)
        {
            if (EntityConfiguration.Files != null)
            {
                foreach (var file in EntityConfiguration.Files)
                {
                    if (file.VersionProperty != null &&
                        (
                            (patch == null && !Equals(null,
                                 currentEntity.GetPropertyValueByName(file.VersionProperty.PropertyName)))
                            ||
                            (patch != null && patch.GetChangedPropertyNames().Contains(file.VersionProperty.Name))
                        )
                    )
                    {
                        UpdateFileUrlWithVersion(currentEntity, patch, (IFileUrl<T>)file);
                    }
                }
            }
            await base.OnBeforePostAndPatchAsync(currentEntity, patch);
        }

        protected virtual void ClearNestedEntities(T currentEntity)
        {
            // Clear any nested entities
            foreach (var property in EntityConfiguration.Properties)
            {
                property.Relationship?.ThisEnd.Property.SetValue(currentEntity, null);
            }
        }

        private static void UpdateFileUrlWithVersion(T currentEntity, Delta<T> patch, IFileUrl<T> file)
        {
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
                    if (patch.TryGetPropertyValue(file.RootFile.VersionProperty.PropertyName, out var v))
                    {
                        version = (string)v;
                    }
                }
                var query = HttpUtility.ParseQueryString(uriBuilder.Query);
                query[$"iql_{file.UrlProperty.PropertyName}_{file.RootFile.VersionProperty.PropertyName}"] = version;
                uriBuilder.Query = query.ToString();
                url = uriBuilder.ToString();
                patch.TrySetPropertyValue(file.UrlProperty.PropertyName, url);
                file.UrlProperty.SetValue(currentEntity, url);
            }
        }

        private bool HasChangedPropertyValue(T entity, Delta<T> patch, string propertyName)
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

        private bool HasPostedPropertyValue(T entity, Delta<T> patch, string propertyName)
        {
            return !Equals(null, PostedPropertyValue(entity, patch, propertyName));
        }

        private object PostedPropertyValue(T entity, Delta<T> patch, string propertyName)
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
        protected override async Task OnAfterPostAndPatchAsync(T currentEntity, Delta<T> patch)
        {
            // Download the file, generate thumbnail, update preview URL, delete file
            // Enqueue a worker item
            KeyValuePair<string, object>[] entityKey = null;
            var saveChanges = false;
            foreach (var file_ in EntityConfiguration.Files)
            {
                var file = (File<T>)file_;
                if (file.MediaKey != null)
                {
                    entityKey = entityKey ?? CrudManager.EntityKey(currentEntity);
                    var populatedEntity = await PreloadMediaKeyDependenciesAsync(entityKey, file);
                    var revisionKeysForMediaProperty =
                        file.VersionProperty;
                    var requiresNewPreviews = 
                        revisionKeysForMediaProperty != null &&
                        HasChangedPropertyValue(currentEntity, patch, revisionKeysForMediaProperty.PropertyName);
                    // TODO: Only refresh previews if file version has changed
                    if (requiresNewPreviews)
                    {
                        foreach (var filePreview_ in file.Previews)
                        {
                            var filePreview = (IFileUrl<T>)filePreview_;
                            var previewProperty = filePreview.UrlProperty;
                            populatedEntity = await PreloadMediaKeyDependenciesAsync(entityKey, file);
                            populatedEntity = await PreloadMediaKeyDependenciesAsync(entityKey, filePreview);
                            var sourceUrl = await MediaManager.GetMediaUriAsync(populatedEntity, file, MediaAccessKind.ReadOnly);
                            // Should be preview
                            var targetUrl = await MediaManager.GetMediaUriAsync(populatedEntity, filePreview, MediaAccessKind.Admin);
                            // Should be preview
                            var targetUrlReadOnly = await MediaManager.GetMediaUriAsync(populatedEntity, filePreview, MediaAccessKind.ReadOnly);
                            previewProperty.SetValue(populatedEntity, targetUrlReadOnly);
                            await ODataMediaManager.Context.SaveChangesAsync();
                            // Should be preview
                            await MediaManager.DeleteAsync(populatedEntity, filePreview);
                            await EnqueueThumbnailRequest(sourceUrl, targetUrl);
                            UpdateFileUrlWithVersion(currentEntity, patch, filePreview);
                        }
                    }
                }
            }
            await base.OnAfterPostAndPatchAsync(currentEntity, patch);
        }

        protected abstract Task EnqueueThumbnailRequest(string sourceUrl, string targetUrl);

        [ODataGenericFunction(ForTypeTypeParameterName = nameof(T), BindingName = "keys")]
        public virtual Task<string> GetMediaUploadUrl(
            [ModelBinder(typeof(KeyValueBinder))] KeyValuePair<string, object>[] keys,
            [FromRoute]string property
            )
        {
            return GetMediaUrl(keys, Builder.EntityType<T>().FindProperty(property), MediaAccessKind.Admin, TimeSpan.FromSeconds(10));
        }

        public virtual async Task<string> GetMediaUrl(KeyValuePair<string, object>[] keys, IEntityProperty<T> propertyMetadata, MediaAccessKind mediaAccessKind,
            TimeSpan? lifetime = null)
        {
            var file = (File<T>)propertyMetadata.File;
            var populatedEntity = await PreloadMediaKeyDependenciesAsync(keys, file);
            var oldValue = propertyMetadata.GetValue(populatedEntity) as string;
            lifetime = TimeSpan.FromDays(1);
            var newUrl = await MediaManager.GetMediaUriAsync(populatedEntity, file, mediaAccessKind, lifetime);
            var newValue = propertyMetadata.GetValue(populatedEntity) as string;
            if (oldValue != newValue)
            {
                await ODataMediaManager.Context.SaveChangesAsync();
            }
            return newUrl;
        }

        public virtual Task DeleteAssociatedMediaAsync(KeyValuePair<string, object>[] key, T entity)
        {
            return MediaManager.DeleteAssociatedMediaAsync(entity, Builder);
        }

        protected virtual Task<T> PreloadMediaKeyDependenciesAsync(KeyValuePair<string, object>[] key,
            IFileUrl<T> file)
        {
            return ODataMediaManager.PreloadMediaKeyDependenciesAsync<T>(key, file);
        }

        public virtual Task<T> PreloadPropertyPathAsync(KeyValuePair<string, object>[] key,
            IqlPropertyPath path)
        {
            var query = CrudManager.FindQuery<T>(key);
            var relationshipPath = path.GetRelationshipPathToHere(".");
            if (!string.IsNullOrWhiteSpace(relationshipPath))
            {
                query = query.Include(relationshipPath);
            }
            var where = CrudManager.KeyEqualsExpression<T>(key);
            var finalQuery = query.Where(where);
            return finalQuery.SingleOrDefaultAsync();
        }

        public override async Task<bool> ValidateEntityAsync<TEntity>(TEntity entity, string path, bool isValid, string accessor)
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