using System;
using System.Linq;

namespace Iql.Extensions
{
    public static class TypeExtensions
    {
        public static object ActivateGeneric(
            this Type type,
            object[] parameters,
            params Type[] types)
        {
            return Activator.CreateInstance(type.MakeGenericType(types), parameters
#if TypeScript
                .Concat(types)
#endif
            );
        }
    }
}