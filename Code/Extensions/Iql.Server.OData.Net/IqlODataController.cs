using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using Brandless.AspNetCore.OData.Extensions.Binding;
using Brandless.AspNetCore.OData.Extensions.Configuration;
using Brandless.AspNetCore.OData.Extensions.Controllers;
using Brandless.Data.EntityFramework.Crud;
using Brandless.Data.Models;
using Brandless.Data.Mptt;
using Iql.Data;
using Iql.Entities;
using Iql.Entities.NestedSets;
using Iql.Entities.PropertyGroups.Files;
using Iql.Extensions;
using Iql.Server.Extensions;
using Iql.Server.Media;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using PropertyKind = Iql.Entities.PropertyKind;

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

        public override async Task OnAfterPostAsync(T postedEntity)
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

        public override async Task OnAfterPatchAsync(KeyValuePair<string, object>[] id, T currentEntity, Delta<T> patch)
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

        protected override async Task OnBeforePostAndPatchAsync(T currentEntity, Delta<T> patch)
        {
            foreach (var property in EntityConfiguration.Properties)
            {
                if (property.InferredWithIql != null)
                {
                    var evaluatedValue = await ExpressionEvaluator.EvaluateIqlCustomAsync(
                        property.InferredWithIql,
                        Builder,
                        currentEntity,
                        async (entity, type, path, flattenedExpression) => await ProcessPropertyPathAsync(currentEntity, patch, path));
                    property.SetValue(currentEntity, evaluatedValue);
                    //var propertyExpressions = property.InferredWithIql.TopLevelPropertyExpressions();
                    //entityKey = entityKey ?? CrudManager.EntityKey(currentEntity);
                    //var path = IqlPropertyPath.FromPropertyExpression(property.EntityConfiguration, 
                    //    property.InferredWithIql as IqlPropertyExpression);
                    //var value = await ProcessPropertyPathAsync(currentEntity, patch, path);
                    ////postedValues[property.Name] = new JObject(value);
                    //if (property.Kind.HasFlag(PropertyKind.Primitive))
                    //{
                    //    if (value != null &&
                    //        property.TypeDefinition.Type == typeof(string) &&
                    //        !(value is string))
                    //    {
                    //        value = value.ToString();
                    //    }

                    //    patch?.TrySetPropertyValue(property.Name, value);
                    //    property.SetValue(currentEntity, value);
                    //}
                    //if (!Equals(null, value) && property.Kind.HasFlag(PropertyKind.Relationship))
                    //{
                    //    var key = property.Relationship.OtherEnd.GetCompositeKey(value, true);
                    //    foreach (var constraint in key.Keys)
                    //    {
                    //        //postedValues[constraint.Name] = new JValue(constraint.Value);
                    //        currentEntity.SetPropertyValueByName(constraint.Name, constraint.Value);
                    //        patch?.TrySetPropertyValue(constraint.Name, constraint.Value);
                    //    }
                    //}
                }
            }

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

        private async Task<object> ProcessPropertyPathAsync(T currentEntity, Delta<T> patch, IqlPropertyPath path)
        {
            var patchEntity = patch == null ? currentEntity : patch.GetInstance();
            var propertyNames = patch == null ? new string[] { } : patch.GetChangedPropertyNames().ToArray();
            object current = null;
            foreach (var pathPart in path.PropertyPath)
            {
                var isRelationship = pathPart.Property.Kind.HasFlag(PropertyKind.Relationship);
                var hasPostedRelationshipKey = false;
                var hasPostedValue = false;
                if (propertyNames.Contains(pathPart.PropertyName))
                {
                    hasPostedValue = true;
                    if (current == null)
                    {
                        current = patchEntity;
                    }
                    current = current.GetPropertyValueByName(pathPart.PropertyName);
                }
                else if (isRelationship)
                {
                    hasPostedRelationshipKey = true;
                    foreach (var constraint in pathPart.Property.Relationship.ThisEnd.Constraints)
                    {
                        if (!propertyNames.Contains(constraint.PropertyName))
                        {
                            hasPostedRelationshipKey = false;
                            break;
                        }
                    }

                    if (hasPostedRelationshipKey)
                    {
                        var dic = new List<KeyValuePair<string, object>>();
                        var compositeKey = pathPart.Property.Relationship.ThisEnd.GetCompositeKey(current ?? patchEntity, true);
                        foreach (var constraint in compositeKey.Keys)
                        {
                            dic.Add(new KeyValuePair<string, object>(
                                constraint.Name,
                                constraint.Value));
                        }
                        current = CrudManager.FindEntity(dic, pathPart.Property.Relationship.OtherEnd.Type);
                    }
                    else
                    {
                        var key = pathPart.Property.EntityConfiguration.Key;
                        var keyPairs = new List<KeyValuePair<string, object>>();
                        foreach (var keyPart in key.Properties)
                        {
                            keyPairs.Add(new KeyValuePair<string, object>(
                                keyPart.Name,
                                keyPart.GetValue(current ?? currentEntity)));
                        }
                        var dbCurrent = CrudManager.FindEntity(keyPairs, pathPart.Property.EntityConfiguration.Type);

                        var dic = new List<KeyValuePair<string, object>>();
                        var compositeKey = pathPart.Property.Relationship.ThisEnd.GetCompositeKey(dbCurrent, true);
                        foreach (var constraint in compositeKey.Keys)
                        {
                            dic.Add(new KeyValuePair<string, object>(
                                constraint.Name,
                                constraint.Value));
                        }
                        current = CrudManager.FindEntity(dic, pathPart.Property.Relationship.OtherEnd.Type);
                    }
                }
                else
                {
                    if (current == null)
                    {
                        current = currentEntity;
                    }
                    current = current.GetPropertyValueByName(pathPart.PropertyName);
                }
                propertyNames = new string[] { };
            }
            return current;
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
                    var requiresNewPreviews = false;
                    if (revisionKeysForMediaProperty != null && HasChangedPropertyValue(currentEntity, patch, revisionKeysForMediaProperty.PropertyName))
                    {
                        requiresNewPreviews = true;
                    }
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

        public async Task<string> GetMediaUrl(KeyValuePair<string, object>[] keys, IEntityProperty<T> propertyMetadata, MediaAccessKind mediaAccessKind,
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