using System;
using System.Collections;
using System.Collections.Generic;
using Iql.Queryable;
using Iql.Queryable.Data;

namespace Iql.JavaScript.QueryToJavaScript
{
    public static class ListExpandExtensions
    {
        public static int Count(this IEnumerable source)
        {
            if (source is IList)
            {
                return (source as IList).Count;
            }
            int i = 0;
            foreach (var item in source)
            {
                i++;
            }
            return i;
        }

        public static object ItemAt(this IEnumerable source, int index)
        {
            if (source is IList)
            {
                return (source as IList)[index];
            }
            long i = 0;
            foreach (var item in source)
            {
                if (i == index)
                {
                    return item;
                }
            }
            throw new IndexOutOfRangeException();
        }

        public static void ExpandOneToOne(
            this IEnumerable source,
            IEnumerable target,
            string sourceProperty,
            string targetProperty,
            string sourceTargetKeyProperty,
            string targetKeyProperty
        )
        {
            for (var i = 0; i < source.Count(); i++)
            {
                var sourceEntity = source.ItemAt(i);
                var targetKey = sourceEntity.GetPropertyValueByName(sourceTargetKeyProperty);
                for (var j = 0; j < target.Count(); j++)
                {
                    var targetEntity = target.ItemAt(j);
                    if (Equals(targetEntity.GetPropertyValueByName(targetKeyProperty), targetKey))
                    {
                        sourceEntity.SetPropertyValueByName(sourceProperty, targetEntity);
                        targetEntity.SetPropertyValueByName(targetProperty, sourceEntity);
                        break;
                    }
                }
            }
        }

        public static void ExpandOneToMany(
            this IEnumerable source,
            Type sourceType,
            IEnumerable target,
            string sourceProperty,
            string targetProperty,
            string sourceTargetKeyProperty,
            string targetKeyProperty)
        {
            var targetsRefreshed = new Dictionary<object, bool>();
            for (var i = 0; i < source.Count(); i++)
            {
                var sourceEntity = source.ItemAt(i);
                var targetKey = sourceEntity.GetPropertyValueByName(sourceTargetKeyProperty);
                for (var j = 0; j < target.Count(); j++)
                {
                    var targetEntity = target.ItemAt(j);
                    if (!targetsRefreshed.ContainsKey(targetEntity))
                    {
                        targetsRefreshed.Add(targetEntity, true);
                        var targetType = targetEntity.GetType();
                        targetEntity.SetPropertyValueByName(targetProperty,
                            Activator.CreateInstance(typeof(RelatedList<,>).MakeGenericType(targetType, sourceType), new object[]
                            {
                                targetEntity,
                                targetProperty,
                                null
#if TypeScript
                                ,
                                targetType,
                                sourceType
#endif
                            }));
                    }
                    //if (Equals(targetEntity.GetPropertyValue(targetProperty), null))
                    //{
                    //}
                    if (Equals(targetEntity.GetPropertyValueByName(targetKeyProperty), targetKey))
                    {
                        sourceEntity.SetPropertyValueByName(sourceProperty, targetEntity);
                        targetEntity.GetPropertyValueByNameAs<IList>(targetProperty).Add(sourceEntity);
                    }
                }
            }
            foreach (var targetEntity in targetsRefreshed.Keys)
            {
                targetEntity.SetPropertyValueByName($"{targetProperty}Count",
                    targetEntity.GetPropertyValueByNameAs<IList>(targetProperty).Count);
            }
        }

        public static void ExpandManyToMany(
            this IEnumerable source,
            Type sourceType,
            Type targetType,
            IEnumerable target,
            IEnumerable pivot,
            string pivotSourceKeyProperty,
            string pivotTargetKeyPropery,
            string sourceProperty,
            string targetProperty,
            string sourceKeyProperty,
            string targetKeyProperty)
        {
            for (var i = 0; i < source.Count(); i++)
            {
                var sourceEntity = source.ItemAt(i);
                if (Equals(sourceEntity.GetPropertyValueByName(sourceProperty), null))
                {
                    sourceEntity.SetPropertyValueByName(sourceProperty,
                        Activator.CreateInstance(typeof(List<>).MakeGenericType(targetType)));
                }
                for (var j = 0; j < target.Count(); j++)
                {
                    var targetEntity = target.ItemAt(j);
                    if (Equals(targetEntity.GetPropertyValueByName(targetProperty), null))
                    {
                        targetEntity.SetPropertyValueByName(targetProperty,
                            Activator.CreateInstance(typeof(List<>).MakeGenericType(sourceType)));
                    }
                    for (var k = 0; k < pivot.Count(); k++)
                    {
                        var pivotEntity = pivot.ItemAt(k);
                        if (Equals(pivotEntity.GetPropertyValueByName(pivotSourceKeyProperty),
                                sourceEntity.GetPropertyValueByName(sourceKeyProperty)) &&
                            Equals(pivotEntity.GetPropertyValueByName(pivotTargetKeyPropery),
                                targetEntity.GetPropertyValueByName(targetKeyProperty)))
                        {
                            targetEntity.GetPropertyValueByNameAs<IList>(targetProperty).Add(sourceEntity);
                            sourceEntity.GetPropertyValueByNameAs<IList>(sourceProperty).Add(targetEntity);
                        }
                    }
                }
            }
        }
    }
}