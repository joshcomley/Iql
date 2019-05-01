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
    public class ODataMediaManager<TService>
    {
        public IEntityConfigurationBuilder Builder { get; }
        public IMediaManager MediaManager { get; }

        public ODataMediaManager(IEntityConfigurationProvider builderProvider,
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
        public async Task<string> GetMediaUrl<TEntity>(TEntity entity, IFileUrl<TEntity> file, MediaAccessKind mediaAccessKind,
            TimeSpan? lifetime = null)
            where TEntity : class
        {
            var populatedEntity = await PreloadMediaKeyDependenciesAsync<TEntity>(CrudManager.EntityKey(entity), file);
            return await MediaManager.SetMediaUriAsync(populatedEntity, (File<TEntity>) file, mediaAccessKind, lifetime);
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
    }
}