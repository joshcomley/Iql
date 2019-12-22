using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Iql.Data.Evaluation;
using Iql.Entities;
using Iql.Entities.PropertyGroups.Files;
using Iql.Entities.Relationships;

namespace Iql.Server.Media
{
    public abstract class MediaManager : IMediaManager
    {
        static MediaManager()
        {
            DeleteAssociatedMediaInternalAsyncMethod = typeof(MediaManager).GetMethod(nameof(DeleteAssociatedMediaInternalAsync),
                BindingFlags.NonPublic | BindingFlags.Instance);
        }

        protected static MethodInfo DeleteAssociatedMediaInternalAsyncMethod { get; set; }

        public async Task<List<Func<Task>>> GetDeleteAssociatedMediaTasksAsync<T>(T entity,
            IEntityConfigurationBuilder configuration, IIqlDataEvaluator evaluator) where T : class
        {
            var tasks = new List<Func<Task>>();
            await DeleteAssociatedMediaInternalAsync<T>(
                entity,
                configuration,
                evaluator,
                tasks);
            return tasks;
        }

        private async Task DeleteAssociatedMediaInternalAsync<T>(
            T entity, 
            IEntityConfigurationBuilder configuration, 
            IIqlDataEvaluator evaluator,
            List<Func<Task>> tasks)
            where T : class
            {
            foreach (var file in GetMediaProperties<T>(configuration))
            {
                tasks.Add(await GetDeleteTaskAsync(entity, file));
            }

            var expands = new List<string>();
            var relationships = new List<ITargetRelationshipSourceDetail>();
            var entityConfiguration = configuration.EntityType<T>();
            foreach (var relationship in entityConfiguration.AllRelationships())
            {
                var collectionRelationship = relationship.ThisEnd as ITargetRelationshipSourceDetail;
                if (collectionRelationship != null && relationship.ThisIsTarget && collectionRelationship.SupportsCascadeDelete)
                {
                    expands.Add(relationship.ThisEnd.Property.Name);
                    relationships.Add(collectionRelationship);
                }
            }

            if (expands.Count > 0)
            {
                var entityWithExpands = await evaluator.GetEntityByKeyAsync(entityConfiguration, entityConfiguration.GetCompositeKey(entity), expands.ToArray(), false);
                foreach (var relationship in relationships)
                {
                    if (relationship.Property.GetValue(entityWithExpands) is IEnumerable entities)
                    {
                        foreach (var childEntity in entities)
                        {
                            var task = (Task)DeleteAssociatedMediaInternalAsyncMethod
                                .MakeGenericMethod(relationship.OtherSide.EntityConfiguration.Type)
                                .Invoke(this, new object[]
                                {
                                    childEntity,
                                    configuration,
                                    evaluator,
                                    tasks
                                });
                            await task;
                        }
                    }
                }
            }
        }

        public IEnumerable<File<T>> GetMediaProperties<T>(IEntityConfigurationBuilder configuration) where T : class
        {
            foreach (var file in configuration.EntityType<T>().Files)
            {
                yield return (File<T>)file;
            }
        }

        public abstract Task<string> SetMediaUriAsync<T>(T entity, IFileUrl<T> entityProperty, MediaAccessKind accessKind, TimeSpan? lifetime = null) where T : class;

        public async Task DeleteAsync<T>(T entity, IFileUrl<T> entityProperty) where T : class
        {
            var task = await GetDeleteTaskAsync(entity, entityProperty);
            await task();
        }

        public abstract Task CloneAsync<T>(T fromEntity, T toEntity, IFileUrl<T> file) where T : class;
        public abstract Task CloneUrlAsync(string fromUrl, string toUrl);

        public abstract Task<Func<Task>> GetDeleteTaskAsync<T>(T entity, IFileUrl<T> entityProperty) where T : class;
    }
}