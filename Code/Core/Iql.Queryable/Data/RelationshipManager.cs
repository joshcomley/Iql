using System;
using Iql.Queryable.Data.EntityConfiguration.Relationships;
using Iql.Queryable.Operations;

namespace Iql.Queryable.Data
{
    public class RelationshipManager<TSource, TTarget> : IRelationshipManager
    {
        public IRelationship Relationship { get; }
        public IDataContext DataContext { get; }

        public RelationshipManager(IRelationship relationship, IDataContext dataContext)
        {
            Relationship = relationship;
            DataContext = dataContext;
        }

        public static void TrackRelationships(object entity, Type entityType, IDataContext dataContext)
        {
            foreach (var relationship in dataContext.EntityConfigurationContext.GetEntityByType(entityType).AllRelationships())
            {
                
            }
            // Here we need to fake the 
            //foreach (var relationship in DataContext.EntityConfigurationContext.GetEntityByType(entityType).AllRelationships())
            //{
            //    // We are:
            //    // One-To-Many, on the one side
            //    // - We inspect each child in the collection property
            //    // - If the child's parent key or value match the parent, ensure both key and value are set
            //    // - If the child's parent key and value are empty, set to that of the parent
            //    // - If the child's parent key and value to not match the parent, throw an InconsistentRelationshipException
            //    //
            //    // One-To-Many, on the many side
            //    // - If the entity has no relationship key, do nothing
            //    // - If the entity has a relationship key, then look up the related entity and insert this entity into the 
            //    //   related entity's relationship collection property
            //    // - If the entity has a relationship key and a relationship value, then ensure the relationship key and value's key match,
            //    //   otherwise throw an InconsistentRelationshipException.
            //    // - If the entity has a relationship key and a relationship value, and they are consistent, ensure this entity
            //    //   is in the child entity's relationship collection property, and remove it from any collections for this 
            //    //   relationship that it currently belongs to
            //    //
            //    // One-To-One, on either side
            //    // - If the entity has a relationship key, then look up the related entity and assign this entity into the 
            //    //   related entity's relationship property
            //    if (!relationship.PartnerIsSource)
            //    {
            //        continue;
            //    }
            //    var key = relationship.ThisEnd.GetCompositeKey(entity);
            //    //var inverseKey = relationship.ThisEnd.GetCompositeKey(entity);
            //    if (key.HasDefaultValue())
            //    {
            //        continue;
            //    }
            //    var set = DataContext.DataStore.GetTracking().TrackingSet(relationship.OtherEnd.Type);
            //    if (relationship.Relationship.Type == RelationshipType.OneToMany)
            //    {
            //        if (relationship.ThisEnd.IsCollection)
            //        {
            //            var collection = entity.GetPropertyValue(relationship.ThisEnd.Property.PropertyName) as IList;
            //            if (collection != null)
            //            {
            //                foreach (var child in collection)
            //                {
            //                    foreach (var keyPart in key.Keys)
            //                    {
            //                        child.SetPropertyValue(keyPart.Name, keyPart.Value);
            //                    }
            //                    child.SetPropertyValue(relationship.OtherEnd.Property.PropertyName, entity);
            //                }
            //            }
            //        }
            //        else
            //        {
            //            foreach (var relatedEntity in set.TrackedEntites())
            //            {
            //                if (DataContext.EntityPropertiesMatch(relatedEntity, key))
            //                {
            //                    PersistRelationship(relationship.OtherEnd, relationship.ThisEnd, relatedEntity, entity);
            //                    PersistRelationship(relationship.ThisEnd, relationship.OtherEnd, entity, relatedEntity);
            //                }
            //            }
            //        }
            //    }
            //    else
            //    {
            //        var ourKey = new CompositeKey();
            //        foreach (var constraint in relationship.Relationship.Constraints)
            //        {
            //            ourKey.Keys.Add(new KeyValue(constraint.SourceKeyProperty.PropertyName,
            //                entity.GetPropertyValue(constraint.TargetKeyProperty.PropertyName),
            //                relationship.Relationship.Source.Configuration.FindProperty(constraint.SourceKeyProperty.PropertyName).Type));
            //        }
            //        foreach (var relatedEntity in set.TrackedEntites())
            //        {
            //            //var constraints = relationship.Relationship.InverseRelationship.Constraints;
            //            //var invertedKey = new CompositeKey();
            //            //foreach (var constraint in constraints)
            //            //{
            //            //    invertedKey.Keys.Add(new KeyValue(constraint.TargetKeyProperty.PropertyName, 
            //            //        relatedEntity.GetPropertyValue(constraint.SourceKeyProperty.PropertyName),
            //            //        relationship.Relationship.InverseRelationship.Source.Configuration.FindProperty(constraint.SourceKeyProperty.PropertyName).Type));
            //            //}
            //            if (DataContext.EntityPropertiesMatch(relatedEntity, ourKey))
            //            {
            //                PersistRelationship(relationship.OtherEnd, relationship.ThisEnd, relatedEntity, entity);
            //                PersistRelationship(relationship.ThisEnd, relationship.OtherEnd, entity, relatedEntity);
            //            }
            //        }
            //    }
            //}
        }

        /// <summary>
        /// Customer.Address = [new]
        /// </summary>
        /// <typeparam name="TTarget"></typeparam>
        public void ProcessOneToOneInverseReferenceChange(TTarget entity)
        {

        }

        /// <summary>
        /// Address.Customer = [new]
        /// </summary>
        /// <typeparam name="TTarget"></typeparam>
        public void ProcessOneToOneReferenceChange(TSource entity)
        {

        }

        /// <summary>
        /// Address.CustomerId = [new]
        /// </summary>
        /// <typeparam name="TTarget"></typeparam>
        public void ProcessOneToOneKeyChange(TSource entity)
        {

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
        public void ProcessOneToManyCollectionRemove(TSource entity, TTarget toRemove, CompositeKey toRemoveKey)
        {

        }

        /// <summary>
        /// Customer.Orders.Add([order])
        /// </summary>
        /// <typeparam name="TTarget"></typeparam>
        public void ProcessOneToManyCollectionAdd(TSource entity, TTarget toAdd, CompositeKey toAddKey)
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
            ProcessOneToManyCollectionAdd((TSource) entity, (TTarget) toAdd, toAddKey);
        }

        void IRelationshipManager.ProcessOneToManyCollectionRemove(object entity, object toRemove, CompositeKey toRemoveKey)
        {
            ProcessOneToManyCollectionRemove((TSource) entity, (TTarget) toRemove, toRemoveKey);
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
            ProcessOneToOneInverseReferenceChange((TTarget) entity);
        }

        void IRelationshipManager.ProcessOneToOneKeyChange(object entity)
        {
            ProcessOneToOneKeyChange((TSource) entity);
        }

        void IRelationshipManager.ProcessOneToOneReferenceChange(object entity)
        {
            ProcessOneToOneReferenceChange((TSource) entity);
        }
    }
}