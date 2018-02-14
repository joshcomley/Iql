﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace Iql.Extensions
{
    public static class ListExtensions
    {
        static ListExtensions()
        {
            ToListTypedMethod = typeof(ListExtensions).GetMethod(nameof(ToListTyped),
                BindingFlags.Static | BindingFlags.Public);
        }

        public static MethodInfo ToListTypedMethod { get; set; }

        public static IList ToList(this IEnumerable<object> enumerable, Type type)
        {
            return (IList)ToListTypedMethod.InvokeGeneric(null, new object[] {enumerable}, type);
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

    }
}