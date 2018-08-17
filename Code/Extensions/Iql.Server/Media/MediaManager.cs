using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Iql.Entities;
using Iql.Entities.PropertyGroups.Files;

namespace Iql.Server.Media
{
    public abstract class MediaManager : IMediaManager
    {
        public async Task DeleteAssociatedMediaAsync<T>(T entity, IEntityConfigurationBuilder configuration) where T : class
        {
            foreach (var file in GetMediaProperties<T>(configuration))
            {
                await DeleteAsync(entity, file);
            }
        }

        public IEnumerable<File<T>> GetMediaProperties<T>(IEntityConfigurationBuilder configuration) where T : class
        {
            foreach (var file in configuration.EntityType<T>().Files)
            {
                    yield return (File<T>)file;
            }
        }

        public abstract Task<string> GetMediaUriAsync<T>(T entity, IFileUrl<T> entityProperty, MediaAccessKind accessKind, TimeSpan? lifetime = null) where T : class;
        public abstract Task DeleteAsync<T>(T entity, IFileUrl<T> entityProperty) where T : class;
    }
}