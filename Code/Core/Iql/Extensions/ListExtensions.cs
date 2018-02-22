using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Iql.Extensions
{
    public static class ListExtensions
    {
        static ListExtensions()
        {
            ToListTypedMethod = typeof(ListExtensions).GetMethod(nameof(ToListTyped),
                BindingFlags.Static | BindingFlags.Public);
            ToArrayTypedMethod = typeof(ListExtensions).GetMethod(nameof(ToArrayTyped),
                BindingFlags.Static | BindingFlags.Public);
        }

        public static MethodInfo ToListTypedMethod { get; set; }
        public static MethodInfo ToArrayTypedMethod { get; set; }

        public static IList EnumerableToList(this IEnumerable<object> enumerable, Type type)
        {
            return (IList)ToListTypedMethod.InvokeGeneric(null, new object[] { enumerable }, type);
        }

        public static IList ToList(this IList enumerable, Type type)
        {
            return (IList)ToListTypedMethod.InvokeGeneric(null, new object[] { enumerable }, type);
        }

        public static Array ToArray(this IList enumerable, Type type)
        {
            return (Array)ToArrayTypedMethod.InvokeGeneric(null, new object[] { enumerable }, type);
        }

        public static List<TEntity> ToListTyped<TEntity>(this IEnumerable<object> enumerable)
        {
            var list = new List<TEntity>();
            foreach (var item in enumerable)
            {
                list.Add((TEntity)item);
            }
            return list;
        }

        public static TEntity[] ToArrayTyped<TEntity>(this IEnumerable<object> enumerable)
        {
            var arr = new TEntity[enumerable.Count()];
            var i = 0;
            foreach (var item in enumerable)
            {
                arr[i] = (TEntity)item;
            }
            return arr;
        }

    }
}