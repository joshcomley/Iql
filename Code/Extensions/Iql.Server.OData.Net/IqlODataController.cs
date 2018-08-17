using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using Brandless.Data.EntityFramework.Crud;
using Brandless.Data.Models;
using Brandless.Data.Mptt;
using Iql.Entities;
using Iql.Entities.NestedSets;
using Iql.Entities.PropertyGroups.Files;
using Iql.Extensions;
using Iql.Server.Extensions;
using Iql.Server.Media;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Builder;
using Microsoft.AspNetCore.OData.EntityFramework.Controllers;
using Microsoft.AspNetCore.OData.Routing.Conventions;
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
        protected IEntityConfigurationBuilder _builder;
        protected IMediaManager _mediaManager;
        protected CrudManager _crudManager;
        protected ODataMediaManager<TService> _oDataMediaManager;
        protected EntityConfiguration<T> _entityConfiguration;

        public EntityConfiguration<T> EntityConfiguration =>
            _entityConfiguration = _entityConfiguration ?? Builder.EntityType<T>();
        public IEntityConfigurationBuilder Builder => _builder = _builder ?? HttpContext.RequestServices.GetService<IEntityConfigurationProvider>().Get<TService>();

        public IMediaManager MediaManager => _mediaManager = _mediaManager ?? HttpContext.RequestServices.GetService<IMediaManager>();

        public CrudManager CrudManager => _crudManager = _crudManager ?? new CrudManager(Crud.Unsecured.Context);
        public ODataMediaManager<TService> ODataMediaManager => _oDataMediaManager = _oDataMediaManager ?? new ODataMediaManager<TService>(
                                                                                         HttpContext.RequestServices.GetService<IEntityConfigurationProvider>(),
                                                                                         MediaManager,
                                                                                         Crud.Unsecured.Context);

        public override async Task OnAfterPostAsync(T currentEntity, T patchEntity, JObject postedValues, JObject originalValues)
        {
            if (EntityConfiguration.NestedSets != null && EntityConfiguration.NestedSets.Any())
            {
                foreach (var nestedSet in EntityConfiguration.NestedSets)
                {
                    await ApplyNewNestedSetItem(nestedSet, currentEntity, patchEntity, postedValues);
                }
            }
            await base.OnAfterPostAsync(currentEntity, patchEntity, postedValues, originalValues);
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

        protected virtual async Task<bool?> DeleteNestedSetEntriesAsync(T entity)
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

        public override async Task OnAfterPatchAsync(KeyValuePair<string, object>[] id, T currentEntity, T patchEntity, JObject postedValues, JObject originalValues)
        {
            if (EntityConfiguration.NestedSets != null && EntityConfiguration.NestedSets.Any())
            {
                foreach (var nestedSet in EntityConfiguration.NestedSets)
                {
                    await ApplyExistingNestedSetItem(nestedSet, currentEntity, patchEntity, postedValues, originalValues);
                }
            }
            await base.OnAfterPatchAsync(id, currentEntity, patchEntity, postedValues, originalValues);
        }

        protected virtual async Task<bool> ApplyDeleteNestedSetItem(INestedSet nestedSet, T entity)
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
            return await (Task<bool>)genericMethod
                .Invoke(this, new object[] { nestedSet, entity, nestedSet.KeyProperty.GetValue(entity) });
        }

        protected virtual async Task<bool> ApplyDeleteNestedSetItemTyped<TKey, TNullableKey, TTreeKey>(
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
            return await mptt.DeleteAsync((TKey)id);
        }


        protected virtual async Task ApplyNewNestedSetItem(INestedSet nestedSet, T currentEntity, T patchEntity, JObject postedValues)
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
            await (Task)genericMethod
                .Invoke(this, new object[] { nestedSet, currentEntity, patchEntity, postedValues, nestedSet.KeyProperty.GetValue(patchEntity) });
        }

        protected virtual async Task ApplyExistingNestedSetItem(INestedSet nestedSet, T currentEntity, T patchEntity, JObject postedValues, JObject previousValues)
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
            await (Task)genericMethod
                .Invoke(this, new object[] { nestedSet, currentEntity, patchEntity, postedValues, previousValues, nestedSet.KeyProperty.GetValue(patchEntity) });
        }

        protected virtual async Task ApplyExistingNestedSetItemTyped<TKey, TNullableKey, TTreeKey>(
            INestedSet nestedSet,
            T currentEntity,
            T patchEntity,
            JObject postedValues,
            JObject previousValues,
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
            var id = nestedSet.IdProperty.GetValue(patchEntity);
            var parentId = nestedSet.ParentIdProperty.GetValue(patchEntity);
            var leftOf = nestedSet.LeftOfProperty.GetValue(patchEntity);
            var rightOf = nestedSet.RightOfProperty.GetValue(patchEntity);
            if (!Equals(leftOf, null))
            {
                await mptt.MoveToAsync((TKey)id, (TNullableKey)leftOf, NodeMoveKind.Left);
            }
            else if (!Equals(rightOf, null))
            {
                await mptt.MoveToAsync((TKey)id, (TNullableKey)rightOf, NodeMoveKind.Right);
            }
            else if (!Equals(previousValues.GetValue(nestedSet.ParentIdProperty.PropertyName),
                parentId) && postedValues.ContainsKey(nestedSet.ParentIdProperty.PropertyName))
            {
                await mptt.MoveToAsync((TKey)id, (TNullableKey)parentId, NodeMoveKind.Beneath);
            }
        }

        protected virtual async Task ApplyNewNestedSetItemTyped<TKey, TNullableKey, TTreeKey>(
            INestedSet nestedSet,
            T currentEntity,
            T patchEntity,
            JObject postedValues,
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
            var parentId = nestedSet.ParentIdProperty.GetValue(patchEntity);
            var leftOf = nestedSet.LeftOfProperty.GetValue(patchEntity);
            var rightOf = nestedSet.RightOfProperty.GetValue(patchEntity);
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

        public override async Task OnBeforePostAndPatchAsync(T currentEntity, T patchEntity, JObject postedValues, JObject originalValues)
        {
            KeyValuePair<string, object>[] entityKey = null;
            foreach (var property in EntityConfiguration.Properties)
            {
                if (property.InferredWith != null)
                {
                    entityKey = entityKey ?? CrudManager.EntityKey(patchEntity);
                    var path = IqlPropertyPath.FromLambdaExpression(property.InferredWith, property.EntityConfiguration);
                    var value = await ProcessPropertyPathAsync(currentEntity, patchEntity, path, postedValues);
                    //postedValues[property.Name] = new JObject(value);
                    if (property.Kind.HasFlag(PropertyKind.Primitive))
                    {
                        if (value != null &&
                            property.TypeDefinition.Type == typeof(string) &&
                            !(value is string))
                        {
                            value = value.ToString();
                        }
                        property.SetValue(patchEntity, value);
                        property.SetValue(currentEntity, value);
                    }
                    if (!Equals(null, value) && property.Kind.HasFlag(PropertyKind.Relationship))
                    {
                        var key = property.Relationship.OtherEnd.GetCompositeKey(value, true);
                        foreach (var constraint in key.Keys)
                        {
                            //postedValues[constraint.Name] = new JValue(constraint.Value);
                            currentEntity.SetPropertyValueByName(constraint.Name, constraint.Value);
                            patchEntity.SetPropertyValueByName(constraint.Name, constraint.Value);
                        }
                    }
                }
            }

            if (EntityConfiguration.Files != null)
            {
                foreach (var file in EntityConfiguration.Files)
                {
                    if (file.VersionProperty != null && postedValues.ContainsKey(file.VersionProperty.Name))
                    {
                        UpdateFileUrlWithVersion(currentEntity, patchEntity, (IFileUrl<T>)file);
                    }
                }
            }
            await base.OnBeforePostAndPatchAsync(currentEntity, patchEntity, postedValues, originalValues);
        }

        protected static void UpdateFileUrlWithVersion(T currentEntity, T patchEntity, IFileUrl<T> file)
        {
            var url = (string)file.UrlProperty.GetValue(currentEntity);
            if (!string.IsNullOrWhiteSpace(url))
            {
                var uriBuilder = new UriBuilder(url);
                var query = HttpUtility.ParseQueryString(uriBuilder.Query);
                query[$"iql_{file.UrlProperty.PropertyName}_{file.RootFile.VersionProperty.PropertyName}"] =
                    (string)file.RootFile.VersionProperty.GetValue(patchEntity);
                uriBuilder.Query = query.ToString();
                url = uriBuilder.ToString();
                file.UrlProperty.SetValue(patchEntity, url);
                file.UrlProperty.SetValue(currentEntity, url);
            }
        }

        protected virtual async Task<object> ProcessPropertyPathAsync(T currentEntity, T patchEntity, IqlPropertyPath path, JObject postedValues)
        {
            JToken currentPostedValues = postedValues;
            object current = null;
            foreach (var pathPart in path.PropertyPath)
            {
                var isRelationship = pathPart.Property.Kind.HasFlag(PropertyKind.Relationship);
                var hasPostedRelationshipKey = false;
                var hasPostedValue = false;
                if (currentPostedValues.HasProperty(pathPart.PropertyName))
                {
                    hasPostedValue = true;
                    currentPostedValues = currentPostedValues.GetPropertyValue(pathPart.PropertyName);
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
                        if (!currentPostedValues.HasProperty(constraint.PropertyName))
                        {
                            hasPostedRelationshipKey = false;
                            break;
                        }
                    }

                    currentPostedValues = null;
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
            }
            return current;
        }

        public override async Task OnAfterPostAndPatchAsync(T currentEntity, T patchEntity, JObject postedValues, JObject originalValues)
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
                    entityKey = entityKey ?? CrudManager.EntityKey(patchEntity);
                    var populatedEntity = await PreloadMediaKeyDependenciesAsync(entityKey, file);
                    var revisionKeysForMediaProperty =
                        file.VersionProperty;
                    var requiresNewPreviews = false;
                    if (revisionKeysForMediaProperty != null && postedValues.ContainsKey(revisionKeysForMediaProperty.Name))
                    {
                        if (originalValues.TryGetValue(revisionKeysForMediaProperty.Name, out var originalVersion))
                        {
                            if (!Equals(originalVersion, revisionKeysForMediaProperty.GetValue(patchEntity)))
                            {
                                requiresNewPreviews = true;
                            }
                        }
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
                            UpdateFileUrlWithVersion(currentEntity, patchEntity, filePreview);
                        }
                    }
                }
            }
            await base.OnAfterPostAndPatchAsync(currentEntity, patchEntity, postedValues, originalValues);
        }

        protected abstract Task EnqueueThumbnailRequest(string sourceUrl, string targetUrl);

        [ODataGenericFunction(ForTypeTypeParameterName = nameof(T), BindingName = "keys")]
        public virtual async Task<string> GetMediaUploadUrl(
            [ModelBinder(typeof(KeyValueBinder))] KeyValuePair<string, object>[] keys,
            [FromRoute]string property
            )
        {
            return await GetMediaUrl(keys, Builder.EntityType<T>().FindProperty(property), MediaAccessKind.Admin, TimeSpan.FromSeconds(10));
        }

        public async Task<string> GetMediaUrl(KeyValuePair<string, object>[] keys, IEntityProperty<T> propertyMetadata, MediaAccessKind mediaAccessKind,
            TimeSpan? lifetime = null)
        {
            var file = (File<T>)propertyMetadata.File;
            var populatedEntity = await PreloadMediaKeyDependenciesAsync(keys, file);
            var oldValue = propertyMetadata.GetValue(populatedEntity) as string;
            var newUrl = await MediaManager.GetMediaUriAsync(populatedEntity, file, mediaAccessKind, lifetime);
            var newValue = propertyMetadata.GetValue(populatedEntity) as string;
            if (oldValue != newValue)
            {
                await ODataMediaManager.Context.SaveChangesAsync();
            }
            return newUrl;
        }

        public virtual async Task DeleteAssociatedMediaAsync(KeyValuePair<string, object>[] key, T entity)
        {
            await MediaManager.DeleteAssociatedMediaAsync(entity, Builder);
        }

        protected virtual async Task<T> PreloadMediaKeyDependenciesAsync(KeyValuePair<string, object>[] key,
            IFileUrl<T> file)
        {
            return await ODataMediaManager.PreloadMediaKeyDependenciesAsync<T>(key, file);
        }

        public virtual async Task<T> PreloadPropertyPathAsync(KeyValuePair<string, object>[] key,
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
            return await finalQuery.SingleOrDefaultAsync();
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