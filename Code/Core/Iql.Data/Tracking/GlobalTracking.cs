using System.Collections.Generic;
using Iql.Data.Tracking.State;

namespace Iql.Data.Tracking
{
    public class GlobalTracking
    {
                private static Dictionary<object, ITrackingSet> _trackedEntities;
        //internal abstract void UpdateRelationships(List<IEntityStateBase> states);
        protected static Dictionary<object, ITrackingSet> TrackedEntities => _trackedEntities = _trackedEntities ?? new Dictionary<object, ITrackingSet>();

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
            return TrackedEntities[entity].FindMatchingEntityState(entity);
        }

        public static void RegisterAsTracked(object entity, ITrackingSet set)
        {
            TrackedEntities.Add(entity, set);
        }

        public static ITrackingSet GetTrackingSet(object entity)
        {
            if (TrackedEntities.ContainsKey(entity))
            {
                return TrackedEntities[entity];
            }

            return null;
        }

        public static bool IsEntityNew(object entity)
        {
            return IsEntityTracked(entity) && GetEntityState(entity)?.IsNew == true;
        }
    }
}