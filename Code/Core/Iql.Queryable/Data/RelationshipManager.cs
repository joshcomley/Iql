using System;
using Iql.Queryable.Data.EntityConfiguration.Relationships;
using Iql.Queryable.Data.Tracking;
using Iql.Queryable.Operations;

namespace Iql.Queryable.Data
{
    public class RelationshipManager<TSource, TTarget> : RelationshipManagerBase, IRelationshipManager
    {
        public IRelationship Relationship { get; }
        public IDataContext DataContext { get; }

        public RelationshipManager(IRelationship relationship, IDataContext dataContext)
        {
            Relationship = relationship;
            DataContext = dataContext;
            var tracking = dataContext.DataStore.GetTracking();
            SourceTrackingSet = tracking.TrackingSet(relationship.Source.Type);
            TargetTrackingSet = tracking.TrackingSet(relationship.Target.Type);
        }

        public ITrackingSet TargetTrackingSet { get; set; }

        public ITrackingSet SourceTrackingSet { get; set; }

        /// <summary>
        /// Customer.Address = [new]
        /// </summary>
        /// <typeparam name="TTarget"></typeparam>
        public void ProcessOneToOneInverseReferenceChange(TSource entity)
        {

        }

        /// <summary>
        /// Address.Customer = [new]
        /// </summary>
        /// <typeparam name="TTarget"></typeparam>
        public void ProcessOneToOneReferenceChange(TTarget entity)
        {
            var referenceValue = entity.GetPropertyValue(
                Relationship.Target.Property.PropertyName);
            TargetTrackingSet.SilentlyChangeEntity(entity, () =>
            {
                foreach (var constraint in Relationship.Constraints)
                {
                    entity.SetPropertyValue(constraint.TargetKeyProperty.PropertyName,
                        referenceValue.GetPropertyValue(constraint.SourceKeyProperty.PropertyName));
                }
            });
            SourceTrackingSet.SilentlyChangeEntity(referenceValue, () =>
            {
                referenceValue.SetPropertyValue(
                    Relationship.Source.Property.PropertyName,
                    entity);
            });
        }

        /// <summary>
        /// Address.CustomerId = [new]
        /// </summary>
        /// <typeparam name="TTarget"></typeparam>
        public void ProcessOneToOneKeyChange(TTarget entity)
        {
            var keyValueInverse =
                Relationship.Target.GetCompositeKey(entity, true);
            var trackedEntity = SourceTrackingSet.FindTrackedEntityByKey(keyValueInverse);
            if (trackedEntity != null)
            {
                TargetTrackingSet.SilentlyChangeEntity(entity, () =>
                {
                    entity.SetPropertyValue(Relationship.Target.Property.PropertyName, trackedEntity.Entity);
                });
                SourceTrackingSet.SilentlyChangeEntity(trackedEntity.Entity, () =>
                {
                    trackedEntity.Entity.SetPropertyValue(
                        Relationship.Source.Property.PropertyName, entity);
                });
            }
        }

        /// <summary>
        /// Order.Customer = [new]
        /// </summary>
        /// <typeparam name="TTarget"></typeparam>
        public void ProcessOneToManyReferenceChange(TTarget entity)
        {

        }

        /// <summary>
        /// Order.CustomerId = [new]
        /// </summary>
        /// <typeparam name="TTarget"></typeparam>
        public void ProcessOneToManyKeyChange(TTarget entity)
        {

        }

        /// <summary>
        /// Customer.Orders.Remove([order])
        /// </summary>
        /// <typeparam name="TTarget"></typeparam>
        public void ProcessOneToManyCollectionRemove(TTarget entity, TSource toRemove, CompositeKey toRemoveKey)
        {

        }

        /// <summary>
        /// Customer.Orders.Add([order])
        /// </summary>
        /// <typeparam name="TTarget"></typeparam>
        public void ProcessOneToManyCollectionAdd(TTarget entity, TSource toAdd, CompositeKey toAddKey)
        {

        }

        //public static void PersistRelationship(IRelationshipDetail relationshipSide, IRelationshipDetail otherSide, object entity, object relatedEntity)
        //{
        //    // Set the entity's relationship entity
        //    if (relationshipSide.IsCollection)
        //    {
        //        // Add it to the entity's related collection
        //        var relatedValue = entity.GetPropertyValue(relationshipSide.Property.PropertyName);
        //        var collection = relatedValue as IList;
        //        if (collection != null && !collection.Contains(relatedEntity))
        //        {
        //            collection.Add(relatedEntity);
        //        }
        //    }
        //    else
        //    {
        //        entity.SetPropertyValue(relationshipSide.Property.PropertyName, relatedEntity);
        //        var compositeKey = otherSide.GetCompositeKey(relatedEntity, true);
        //        foreach (var key in compositeKey.Keys)
        //        {
        //            entity.SetPropertyValue(key.Name, key.Value);
        //        }
        //    }
        //}
        void IRelationshipManager.ProcessOneToManyCollectionAdd(object entity, object toAdd, CompositeKey toAddKey)
        {
            ProcessOneToManyCollectionAdd((TTarget) entity, (TSource) toAdd, toAddKey);
        }

        void IRelationshipManager.ProcessOneToManyCollectionRemove(object entity, object toRemove, CompositeKey toRemoveKey)
        {
            ProcessOneToManyCollectionRemove((TTarget) entity, (TSource) toRemove, toRemoveKey);
        }

        void IRelationshipManager.ProcessOneToManyKeyChange(object entity)
        {
            ProcessOneToManyKeyChange((TTarget) entity);
        }

        void IRelationshipManager.ProcessOneToManyReferenceChange(object entity)
        {
            ProcessOneToManyReferenceChange((TTarget) entity);
        }

        void IRelationshipManager.ProcessOneToOneInverseReferenceChange(object entity)
        {
            ProcessOneToOneInverseReferenceChange((TSource) entity);
        }

        void IRelationshipManager.ProcessOneToOneKeyChange(object entity)
        {
            ProcessOneToOneKeyChange((TTarget) entity);
        }

        void IRelationshipManager.ProcessOneToOneReferenceChange(object entity)
        {
            ProcessOneToOneReferenceChange((TTarget) entity);
        }
    }
}