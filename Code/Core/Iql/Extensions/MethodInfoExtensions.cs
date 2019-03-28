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
#else
            //for (var i = 0; i < types.Length; i++)
            //{
            //    var type = types[i];
            //    if (type.ContainsGenericParameters)
            //    {
            //        types[i] = type.GetGenericTypeDefinition();
            //    }
            //}
            //method = method.GetGenericMethodDefinition();
#endif
            method = method.MakeGenericMethod(types);
            return method
                .Invoke(
                    context,
                    parameters
                );
        }
    }
}