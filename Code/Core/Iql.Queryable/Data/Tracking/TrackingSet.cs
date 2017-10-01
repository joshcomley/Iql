using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Iql.Queryable.Data.Crud.Operations;
using Iql.Queryable.Data.DataStores;
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

        private readonly Dictionary<T, T> _trackedEntityClones = new Dictionary<T, T>();

        public void Untrack(T entity)
        {
            var existingClone = FindClone(entity);
            if (existingClone != null)
            {
                Clone.Remove(existingClone);
                Set.Remove(entity);
                _trackedEntityClones.Remove(entity);
            }
        }
        public void Track(T entity)
        {
            Untrack(entity);
            Set.Add(entity);
            var clone = entity.Clone();
            Clone.Add(clone);
            _trackedEntityClones.Add(entity, clone);
        }

        public T FindClone(T entity)
        {
            if (_trackedEntityClones.ContainsKey(entity))
            {
                return _trackedEntityClones[entity];
            }
            return null;
        }
        public void Merge(List<T> data)
        {
            for (var i = 0; i < data.Count; i++)
            {
                var element = data[i];
                if (element == null)
                {
                    continue;
                }
                // TODO: Update this to look up the entity by tracking GUID first
                var index = Entity.FindIndexOfEntityInSetByKey(
                    DataContext,
                    element,
                    Set);
                if (index != -1)
                {
                    var currentEntity = Set[index];
                    Untrack(element);
                    ObjectMerger.Merge(DataContext, currentEntity, element);
                    Track(currentEntity);
                }
                else
                {
                    Track(element);
                }
            }
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
                var clone = FindClone(entity);
                if (clone != null)
                {
                    var changedProperties = new List<IKeyProperty>();
                    for (var i = 0; i < properties.Count; i++)
                    {
                        var property = properties[i];
                        var entityValue = entity.GetPropertyValue(property.Name);
                        var cloneValue = clone.GetPropertyValue(property.Name);
                        var entityHasChanged = false;
                        if (new[] { entityValue, cloneValue }.Count(e => e == null) == 1)
                        {
                            entityHasChanged = true;
                        }
                        else if (entityValue is IEnumerable && !(entityValue is string))
                        {
                            var entityValueEnumerable = (entityValue as IEnumerable).Cast<object>().ToArray();
                            var cloneValueEnumerable = (cloneValue as IEnumerable).Cast<object>().ToArray();
                            for (var valueIndex = 0; valueIndex < entityValueEnumerable.Length; valueIndex++)
                            {
                                var entityValueAtIndex = entityValueEnumerable[valueIndex];
                                var cloneValueAtIndex = cloneValueEnumerable[valueIndex];
                                if (Equals(entityValueAtIndex, cloneValueAtIndex))
                                {
                                    continue;
                                }
                                entityHasChanged = true;
                                break;
                            }
                        }
                        else if (!Equals(entityValue, cloneValue))
                        {
                            entityHasChanged = true;
                        }
                        if (!entityHasChanged)
                        {
                            continue;
                        }
                        changedProperties.Add(property);
                    }
                    if (changedProperties.Any())
                    {
                        updates.Add(new UpdateEntityOperation<T>(entity, DataContext, changedProperties.ToArray()));
                    }
                }
            });
            return updates;
        }
    }
}