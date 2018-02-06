using System;
// ReSharper disable once RedundantUsingDirective
using System.Linq;
using System.Reflection;

namespace Iql.Extensions
{
    public static class MethodInfoExtensions
    {
        public static object InvokeGeneric(
            this MethodInfo method,
            object context,
            object[] parameters,
            params Type[] types)
        {
#if TypeScript
            var list = (parameters ?? new object[] { }).ToList();
            list.AddRange(types);
            parameters = list.ToArray();
#endif
            return method.MakeGenericMethod(types)
                .Invoke(
                    context,
                    parameters
                );
        }
    }
}