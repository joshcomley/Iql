using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Iql.Data.Evaluation;
using Iql.Entities;
using Iql.Entities.PropertyGroups.Files;

namespace Iql.Server.Media
{
    public interface IMediaManager
    {
        Task<List<Func<Task>>> GetDeleteAssociatedMediaTasksAsync<T>(T entity, IEntityConfigurationBuilder configuration, IIqlDataEvaluator evaluator) where T : class;
        IEnumerable<File<T>> GetMediaProperties<T>(IEntityConfigurationBuilder configuration) where T : class;
        Task<string> GetMediaUriAsync<T>(T entity, IFileUrl<T> file, MediaAccessKind accessKind, TimeSpan? lifetime = null) where T : class;
        Task DeleteAsync<T>(T entity, IFileUrl<T> file) where T : class;
        Task CloneAsync<T>(T fromEntity, T toEntity, IFileUrl<T> file) where T : class;
        Task CloneUrlAsync(string fromUrl, string toUrl);
    }
}