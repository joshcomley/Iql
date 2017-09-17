using System;
using System.Collections;
using System.Collections.Generic;
using Iql.Queryable.Data;

namespace Iql.JavaScript.QueryToJavaScript
{
    public static class ListExpandExtensions
    {
        public static void ExpandOneToOne(
            this IList source,
            IList target,
            string sourceProperty,
            string targetProperty,
            string sourceTargetKeyProperty,
            string targetKeyProperty
        )
        {
            for (var i = 0; i < source.Count; i++)
            {
                var sourceEntity = source[i];
                var targetKey = sourceEntity.GetPropertyValue(sourceTargetKeyProperty);
                for (var j = 0; j < target.Count; j++)
                {
                    var targetEntity = target[j];
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
            this IList source,
            Type sourceType,
            IList target,
            string sourceProperty,
            string targetProperty,
            string sourceTargetKeyProperty,
            string targetKeyProperty)
        {
            for (var i = 0; i < source.Count; i++)
            {
                var sourceEntity = source[i];
                var targetKey = sourceEntity.GetPropertyValue(sourceTargetKeyProperty);
                for (var j = 0; j < target.Count; j++)
                {
                    var targetEntity = target[j];
                    if (Equals(targetEntity.GetPropertyValue(targetProperty), null))
                    {
                        targetEntity.SetPropertyValue(targetProperty,
                            Activator.CreateInstance(typeof(List<>).MakeGenericType(sourceType)));
                    }
                    if (Equals(targetEntity.GetPropertyValue(targetKeyProperty), targetKey))
                    {
                        sourceEntity.SetPropertyValue(sourceProperty, targetEntity);
                        targetEntity.GetPropertyValueAs<IList>(targetProperty).Add(sourceEntity);
                    }
                }
            }
        }

        public static void ExpandManyToMany(
            this IList source,
            Type sourceType,
            Type targetType,
            IList target,
            IList pivot,
            string pivotSourceKeyProperty,
            string pivotTargetKeyPropery,
            string sourceProperty,
            string targetProperty,
            string sourceKeyProperty,
            string targetKeyProperty)
        {
            for (var i = 0; i < source.Count; i++)
            {
                var sourceEntity = source[i];
                if (Equals(sourceEntity.GetPropertyValue(sourceProperty), null))
                {
                    sourceEntity.SetPropertyValue(sourceProperty,
                        Activator.CreateInstance(typeof(List<>).MakeGenericType(targetType)));
                }
                for (var j = 0; j < target.Count; j++)
                {
                    var targetEntity = target[j];
                    if (Equals(targetEntity.GetPropertyValue(targetProperty), null))
                    {
                        targetEntity.SetPropertyValue(targetProperty,
                            Activator.CreateInstance(typeof(List<>).MakeGenericType(sourceType)));
                    }
                    for (var k = 0; k < pivot.Count; k++)
                    {
                        var pivotEntity = pivot[k];
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