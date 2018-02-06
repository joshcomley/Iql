using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Iql.Extensions;

namespace Iql.Queryable.Native
{
    public class RelationshipObserver
    {
        static RelationshipObserver()
        {
            ObserveAllTypedMethod = typeof(RelationshipObserver)
                .GetMethod(nameof(ObserveAllTyped));
        }

        public static MethodInfo ObserveAllTypedMethod { get; set; }

        public void ObserveAll(IList list, Type entityType)
        {
            ObserveAllTypedMethod.InvokeGeneric(
                this,
                new object[] {list},
                entityType);
        }

        public void ObserveAllTyped<T>(List<T> list)
        {

        }
    }
}