using System;
using System.Collections;
using System.Collections.Generic;
using Iql.Queryable.Data.DataStores;

namespace Iql.Queryable.Native
{
    public interface IRelationshipObserver
    {
        IDataStore DataStore { get; }
        bool TrackEntities { get; }

        void ObserveAll(Dictionary<Type, IList> dictionary);
        void ObserveListTyped<T>(List<T> list) where T : class;
        void ObserveList(IList list, Type entityType);
        void Observe(object entity, Type entityType);
        void Unobserve(object entity, Type entityType);
        bool IsAssignedToAnyRelationship(object entity, Type entityType);
        void DeleteRelationships(object entity, Type type);
    }
}