using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Brandless.Data.EntityFramework.Crud;
using Iql.Entities;
using Iql.Entities.PropertyGroups.Files;
using Iql.Server.Media;
using Microsoft.EntityFrameworkCore;

namespace Iql.Server.OData.Net
{
    public class IqlMediaManager<TService>
    {
        public IEntityConfigurationBuilder Builder { get; }
        public IMediaManager MediaManager { get; }

        public IqlMediaManager(IEntityConfigurationProvider builderProvider,
            IMediaManager mediaManager,
            DbContext context)
        {
            Builder = builderProvider.Get<TService>();
            MediaManager = mediaManager;
            Context = context;
            CrudManager = new CrudManager(context);
        }

        public DbContext Context { get; set; }

        public CrudManager CrudManager { get; set; }

        /// <summary>
        /// TODO: Use file or preview property
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <param name="file"></param>
        /// <param name="mediaAccessKind"></param>
        /// <param name="lifetime"></param>
        /// <returns></returns>
        public async Task<string> GetMediaUrl<TEntity>(TEntity entity, IFileUrl<TEntity> file,
            MediaAccessKind mediaAccessKind,
            TimeSpan? lifetime = null)
            where TEntity : class
        {
            var populatedEntity = await PreloadMediaKeyDependenciesAsync<TEntity>(CrudManager.EntityKey(entity), file);
            return await MediaManager.SetMediaUriAsync(populatedEntity, (File<TEntity>)file, mediaAccessKind, lifetime);
        }

        public virtual async Task<TEntity> PreloadMediaKeyDependenciesAsync<TEntity>(KeyValuePair<string, object>[] key,
            IFileUrl<TEntity> file)
            where TEntity : class
        {
            var query = CrudManager.FindQuery<TEntity>(key);
            foreach (var group in file.MediaKey.Groups)
            {
                foreach (var part in group.Parts)
                {
                    if (part.IsPropertyPath)
                    {
                        var iqlPropertyPath = part.GetRelationshipPath();
                        if (iqlPropertyPath != null && !string.IsNullOrWhiteSpace(iqlPropertyPath.PathToHere))
                        {
                            query = query.Include(iqlPropertyPath.GetPathToHere("."));
                        }
                    }
                }
            }

            var where = CrudManager.KeyEqualsExpression<TEntity>(key);
            var finalQuery = query.Where(where);
            return await finalQuery.SingleOrDefaultAsync();
        }

        public virtual async Task<MediaUrl> GetAndSetMediaUrlAsync<TModel>(
            KeyValuePair<string, object>[] key,
            string property)
            where TModel : class
        {
            var url = await GetAndSetMediaUrlAsync(key,
                Builder.EntityType<TModel>().FindProperty(property),
                MediaAccessKind.Admin,
                TimeSpan.FromSeconds(10));
            return url;
        }

        public async Task<MediaUrl> GetAndSetMediaUrlAsync<TModel>(KeyValuePair<string, object>[] key,
            IEntityProperty<TModel> propertyMetadata,
            MediaAccessKind mediaAccessKind,
            TimeSpan? lifetime = null)
            where TModel : class
        {
            var file = (File<TModel>)propertyMetadata.File;
            var populatedEntity = await PreloadMediaKeyDependenciesAsync(key, file);
            if (populatedEntity == null)
            {
                return new MediaUrl
                {
                    NotFound = true
                };
            }
            var oldValue = propertyMetadata.GetValue(populatedEntity) as string;
            lifetime ??= TimeSpan.FromDays(1);
            var newUrl = await MediaManager.SetMediaUriAsync(populatedEntity, file, mediaAccessKind, lifetime);
            var newValue = propertyMetadata.GetValue(populatedEntity) as string;
            var uri = new Uri(newValue);
            var clippedUri = $"{uri.Scheme}://{uri.Host}{(uri.Port == 80 ? "" : $":{uri.Port}")}{uri.LocalPath}";
            if (oldValue != clippedUri)
            {
                propertyMetadata.SetValue(populatedEntity, clippedUri);
                await Context.SaveChangesAsync();
            }

            return new MediaUrl
            {
                ReadUrl = clippedUri,
                UploadUrl = newUrl
            };
        }
    }
}