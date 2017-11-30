using System;
using System.Collections;
using System.Collections.Generic;
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
                var targetKey = sourceEntity.GetPropertyValue(sourceTargetKeyProperty);
                for (var j = 0; j < target.Count(); j++)
                {
                    var targetEntity = target.ItemAt(j);
                    if (Equals(targetEntity.GetPropertyValue(targetKeyProperty), targetKey))
                    {
                        sourceEntity.SetPropertyValue(sourceProperty, targetEntity);
                        targetEntity.SetPropertyValue(targetProperty, sourceEntity);
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
                var targetKey = sourceEntity.GetPropertyValue(sourceTargetKeyProperty);
                for (var j = 0; j < target.Count(); j++)
                {
                    var targetEntity = target.ItemAt(j);
                    if (!targetsRefreshed.ContainsKey(targetEntity))
                    {
                        targetsRefreshed.Add(targetEntity, true);
                        targetEntity.SetPropertyValue(targetProperty,
                            Activator.CreateInstance(typeof(List<>).MakeGenericType(sourceType)));
                    }
                    //if (Equals(targetEntity.GetPropertyValue(targetProperty), null))
                    //{
                    //}
                    if (Equals(targetEntity.GetPropertyValue(targetKeyProperty), targetKey))
                    {
                        sourceEntity.SetPropertyValue(sourceProperty, targetEntity);
                        targetEntity.GetPropertyValueAs<IList>(targetProperty).Add(sourceEntity);
                    }
                }
            }
            foreach (var targetEntity in targetsRefreshed.Keys)
            {
                targetEntity.SetPropertyValue($"{targetProperty}Count",
                    targetEntity.GetPropertyValueAs<IList>(targetProperty).Count);
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
                if (Equals(sourceEntity.GetPropertyValue(sourceProperty), null))
                {
                    sourceEntity.SetPropertyValue(sourceProperty,
                        Activator.CreateInstance(typeof(List<>).MakeGenericType(targetType)));
                }
                for (var j = 0; j < target.Count(); j++)
                {
                    var targetEntity = target.ItemAt(j);
                    if (Equals(targetEntity.GetPropertyValue(targetProperty), null))
                    {
                        targetEntity.SetPropertyValue(targetProperty,
                            Activator.CreateInstance(typeof(List<>).MakeGenericType(sourceType)));
                    }
                    for (var k = 0; k < pivot.Count(); k++)
                    {
                        var pivotEntity = pivot.ItemAt(k);
                        if (Equals(pivotEntity.GetPropertyValue(pivotSourceKeyProperty),
                                sourceEntity.GetPropertyValue(sourceKeyProperty)) &&
                            Equals(pivotEntity.GetPropertyValue(pivotTargetKeyPropery),
                                targetEntity.GetPropertyValue(targetKeyProperty)))
                        {
                            targetEntity.GetPropertyValueAs<IList>(targetProperty).Add(sourceEntity);
                            sourceEntity.GetPropertyValueAs<IList>(sourceProperty).Add(targetEntity);
                        }
                    }
                }
            }
        }
    }
}