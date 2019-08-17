using System;
using System.Reflection;
using Iql.Data.Context;
using Iql.Data.Tracking.State;
using Iql.Extensions;

namespace Iql.Data.Events
{
    public class IqlEntityEventFactory
    {
        static IqlEntityEventFactory()
        {
            NewEventMethod =
                typeof(IqlEntityEventFactory).GetMethod(nameof(NewEvent), BindingFlags.Static | BindingFlags.NonPublic);
        }

        static MethodInfo NewEventMethod { get; }

        public static IEntityEventBase New(object entityState, Type type, IDataContext dataContext)
        {
            return (IEntityEventBase) NewEventMethod.InvokeGeneric(null, new[] { entityState, dataContext }, type);
        }

        private static IEntityEventBase NewEvent<TEntity>(IEntityState<TEntity> entityState, IDataContext dataContext)
            where TEntity : class
        {
            return new IqlEntityEvent<TEntity>(entityState, dataContext);
        }
    }
}