using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Iql.Queryable.Data.EntityConfiguration.Relationships;

namespace Iql.Queryable.Native
{
    public static class ListExtensions
    {
        static ListExtensions()
        {
            ExpandOneToOneTypedMethod = FindMethod(nameof(ExpandOneToOneTyped));
            ExpandOneToManyTypedMethod = FindMethod(nameof(ExpandOneToManyTyped));
        }

        private static MethodInfo FindMethod(string methodName)
        {
            return typeof(ListExtensions)
                .GetMethod(methodName,
                    BindingFlags.Public | BindingFlags.Static);
        }

        public static MethodInfo ExpandOneToManyTypedMethod { get; set; }

        public static MethodInfo ExpandOneToOneTypedMethod { get; set; }

        public static IList ExpandOneToOne(
            this IList source,
            IList target,
            IRelationship relationship)
        {
            return InvokeExpander(source, target, relationship, ExpandOneToOneTypedMethod);
        }

        private static IList InvokeExpander(IList source, IList target, IRelationship relationship, MethodInfo method)
        {
            return (IList)
                method.MakeGenericMethod(
                        relationship.Source.Type,
                        relationship.Target.Type)
                    .Invoke(null, new object[]
                    {
                        source,
                        target,
                        relationship
#if TypeScript
                        , relationship.Source.Type
                        , relationship.Target.Type
#endif
                    });
        }

        public static List<TSource> ExpandOneToOneTyped<TSource, TTarget>(
            this List<TSource> source,
            List<TTarget> target,
            IRelationship relationship)
            where TSource : class where TTarget : class
        {
            return new RelationshipExpander<TSource, TTarget>()
                .ExpandOneToOne(source, target, relationship);
        }

        public static IList ExpandOneToMany(
            this IList source,
            IList target,
            IRelationship relationship)
        {
            return InvokeExpander(source, target, relationship, ExpandOneToManyTypedMethod);
        }

        public static List<TSource> ExpandOneToManyTyped<TSource, TTarget>(
            this List<TSource> source,
            List<TTarget> target,
            IRelationship relationship)
            where TSource : class where TTarget : class
        {
            return new RelationshipExpander<TSource, TTarget>()
                .ExpandOneToMany(source, target, relationship);
        }
    }
}