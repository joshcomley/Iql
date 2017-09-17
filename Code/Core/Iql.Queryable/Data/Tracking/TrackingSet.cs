using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Iql.Queryable.Data.Crud.Operations;
using Iql.Queryable.Data.EntityConfiguration;

namespace Iql.Queryable.Data.Tracking
{
    public class TrackingSet<T> : ITrackingSet where T : class
    {
        public TrackingSet(IDataContext dataContext)
        {
            DataContext = dataContext;
            Set = new List<T>();
            Clone = new List<T>();
        }

        public List<T> Set { get; set; }
        public List<T> Clone { get; set; }
        private IDataContext DataContext { get; }

        List<IEntityCrudOperationBase> ITrackingSet.GetChanges()
        {
            return GetChanges().Cast<IEntityCrudOperationBase>().ToList();
        }

        public void Reset()
        {
            Clone = Set.Clone();
        }

        public void Track(object entity)
        {
        }

        public void Merge(IList data)
        {
        }

        public void Track(T entity)
        {
            Set.Add(entity);
            Clone.Add(entity.Clone());
        }

        public void Merge(List<T> data)
        {
            var newIndex = 0;
            for (var i = 0; i < data.Count; i++)
                //data.ForEach(element =>
            {
                var element = data[i];
                // TODO: Update this to look up the entity by tracking GUID first
                var index = Entity.FindIndexOfEntityInSetByKey(
                    DataContext,
                    element,
                    Set);
                if (index != -1)
                {
                    var currentEntity = Set[index];
                    var cloneIndex = Entity.FindIndexOfEntityInSetByKey(
                        DataContext,
                        element,
                        Clone);
                    var new1 = element;
                    ResolveCollisions(currentEntity, Clone[cloneIndex], new1);
                    data[newIndex] = currentEntity;
                    Clone[cloneIndex] = currentEntity.Clone();
                }
                Track(element);
                newIndex++;
            }
        }

        private void ResolveCollisions<TEntity>(TEntity currentEntity, TEntity entityAtLastFetch, TEntity newEntity)
        {
        }

        public List<UpdateEntityOperation<T>> GetChanges()
        {
            var updates = new List<UpdateEntityOperation<T>>();
            Set.ForEach(entity =>
            {
                //let ctor = entity["__ctor"]();
                var entityDefinition = DataContext.EntityConfigurationContext.GetEntity<T>();
                var properties = DataContext.EntityConfigurationContext
                    .GetEntity<T>().Properties;
                var index = Entity.FindIndexOfEntityInSetByKey(
                    DataContext,
                    entity,
                    Clone
                );
                if (index != -1)
                {
                    var clone = Clone[index];
                    for (var i = 0; i < properties.Count; i++)
                    {
                        var property = properties[i];
                        if (!Equals(entity.GetPropertyValue(property.Name), clone.GetPropertyValue(property.Name)))
                        {
                            updates.Add(new UpdateEntityOperation<T>(entity, DataContext));
                            break;
                        }
                    }
                }
            });
            return updates;
        }
    }
}