using System.Collections.Generic;
using Iql.Queryable.Data.Tracking.State;

namespace Iql.Queryable.Data.Tracking
{
    public abstract class TrackingSetBase
    {
        internal abstract IEntityStateBase TrackEntityInternal(object entity, object mergeWith = null, bool isNew = true, bool onlyMergeWithExisting = false);
        //internal abstract void UpdateRelationships(List<IEntityStateBase> states);
        protected static Dictionary<object, ITrackingSet> TrackedEntities { get; }
            = new Dictionary<object, ITrackingSet>();

        public static bool IsEntityTracked(object entity)
        {
            return TrackedEntities.ContainsKey(entity);
        }

        public static IEntityStateBase GetEntityState(object entity)
        {
            if (!IsEntityTracked(entity))
            {
                return null;
            }

            return TrackedEntities[entity].GetEntityState(entity);
        }

        public static bool IsEntityNew(object entity)
        {
            return !IsEntityTracked(entity) || GetEntityState(entity).IsNew;
        }
    }
}