using System.Collections.Generic;
using Iql.Data.Tracking.State;

namespace Iql.Data.Tracking
{
    public class GlobalTracking
    {
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
            return !IsEntityTracked(entity) || GetEntityState(entity).IsNew;
        }
    }
}