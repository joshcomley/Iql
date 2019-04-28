using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Iql.Data.Context;
using Iql.Data.Evaluation;
using Iql.Entities;
using Iql.Entities.Extensions;
using Iql.Extensions;
using Iql.Serialization;

namespace Iql.Data
{
    public static class EntityCopier
    {
        public static async Task<T> CopyAsAsync<T>(
            this T obj,
            IDataContext dataContext,
            Type entityType,
            Dictionary<object, object> mergeMap = null)
            where T : class
        {
            var result = await obj.CopyAsync(dataContext, entityType, mergeMap);
            return (T)result;
        }

        public static Task<object> CopyAsync(
            this object obj,
            IDataContext dataContext,
            Type entityType, 
            Dictionary<object, object> mergeMap = null)
        {
            return obj.CopyAsyncInternal(
                dataContext,
                entityType, 
                new InferredValueEvaluationSession(), 
                new Dictionary<object, object>(),
                mergeMap);
        }

        private static async Task<object> CopyListAsync<T>(
            this List<T> list,
            IDataContext dataContext,
            Type entityType,
            InferredValueEvaluationSession inferredValueEvaluationSession,
            Dictionary<object, object> copiedObjects,
            Dictionary<object, object> mergeMap)
        {
            var newList = new List<T>();
            copiedObjects.Add(list, newList);
            // ReSharper disable once ForCanBeConvertedToForeach
            for (var i = 0; i < list.Count; i++)
            {
                var copydItem = await list[i].CopyAsyncInternal(dataContext, entityType, inferredValueEvaluationSession, copiedObjects, mergeMap);
                newList.Add((T)copydItem);
            }
            return newList;
        }

        static EntityCopier()
        {
            CopyListAsyncMethod = typeof(EntityCopier).GetMethod(
                nameof(CopyListAsync),
                BindingFlags.NonPublic | BindingFlags.Static);
        }

        private static MethodInfo CopyListAsyncMethod { get; set; }

        private static async Task<object> CopyAsyncInternal(
            this object obj,
            IDataContext dataContext,
            Type entityType,
            InferredValueEvaluationSession inferredValueEvaluationSession,
            Dictionary<object, object> copiedObjects,
            Dictionary<object, object> mergeMap)
        {
            IEntityConfigurationBuilder entityConfigurationBuilder = dataContext.EntityConfigurationContext;
            if (obj == null)
            {
                return null;
            }
            if (copiedObjects.ContainsKey(obj))
            {
                return copiedObjects[obj];
            }
            if (obj.IsArray())
            {
#if !TypeScript
                var listType = (obj as IList).GetListType();
                var listElementType = listType.GetGenericArguments().First();
#else
                var listType = typeof(IList);
                var listElementType = typeof(object);
#endif
                var newListTask = (Task<object>)CopyListAsyncMethod
                        .InvokeGeneric(null,
                        new[]
                        {
                            obj,
                            entityConfigurationBuilder,
                            entityType,
                            inferredValueEvaluationSession,
                            copiedObjects,
                            mergeMap
                        },
                        listElementType);
                return await newListTask;
            }

            object copy;
            if (mergeMap != null && mergeMap.ContainsKey(obj))
            {
                copy = mergeMap[obj];
            }
            else
            {
                copy = Activator.CreateInstance(entityType);
                if (mergeMap != null)
                {
                    mergeMap.Add(obj, copy);
                }
            }
            copiedObjects.Add(obj, copy);
            var entityConfiguration = entityConfigurationBuilder.GetEntityByType(entityType);
            for (var i = 0; i < entityConfiguration.Properties.Count; i++)
            {
                var property = entityConfiguration.Properties[i];
                if (property.Kind.HasFlag(PropertyKind.Key) || property == entityConfiguration.PersistenceKeyProperty)
                {
                    continue;
                }
                if (await inferredValueEvaluationSession.IsReadOnlyWithDataContextAsync(property, copy, dataContext))
                {
                    continue;
                }
                if (property.Kind.HasFlag(PropertyKind.Primitive) ||
                    property.Kind.HasFlag(PropertyKind.RelationshipKey))
                {
                    var propertyValue = obj.GetPropertyValue(property);
                    if (propertyValue.ClaimsToBeIql())
                    {
                        propertyValue = (propertyValue as IqlExpression).EnsureIsIql();
                    }
                    copy.SetPropertyValue(property, propertyValue);
                }
            }

            return copy;
        }
    }
}