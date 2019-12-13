using System.Collections.Generic;

namespace Iql.Data.Tracking.State
{
    public static class EntityStates
    {
        private static readonly Dictionary<object, IEntityStateBase> States = new Dictionary<object, IEntityStateBase>();

        public static void Register(IEntityStateBase state)
        {
            States.Add(state.Entity, state);
        }

        public static IEntityStateBase Find(object entity)
        {
            if (States.ContainsKey(entity))
            {
                return States[entity];
            }

            return null;
        }
        public static IEntityState<T> FindTyped<T>(object entity)
            where T : class
        {
            return (IEntityState<T>)Find(entity);
        }
    }
}