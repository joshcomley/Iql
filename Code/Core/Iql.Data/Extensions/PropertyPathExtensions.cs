using System.Collections.Generic;
using System.Threading.Tasks;
using Iql.Data.Context;
using Iql.Entities;
using Iql.Extensions;

namespace Iql.Data.Extensions
{
    public static class PropertyPathExtensions
    {
        public static async Task<IqlPropertyPathEvaluationResult> EvaluateAsync(this IqlPropertyPath propertyPath, object entity, IDataContext dataContext)
        {
            var evaluationResult = new IqlPropertyPathEvaluationResult(
                false,
                entity,
                propertyPath,
                new IqlPropertyPathEvaluated[] { });

            if (entity == null)
            {
                return evaluationResult;
            }

            var success = true;
            var results = new List<IqlPropertyPathEvaluated>();
            var result = entity;
            for (var i = 0; i < propertyPath.PropertyPath.Length; i++)
            {
                var part = propertyPath.PropertyPath[i];
                var parent = result;
                result = result.GetPropertyValueByName(part.PropertyName);
                if (result == null)
                {
                    if (part.Property.Kind.HasFlag(PropertyKind.Relationship))
                    {
                        if (dataContext.DataStore.Tracking.IsTracked(parent))
                        {
                            await dataContext.LoadRelationshipPropertyAsync(parent, part.Property);
                            result = parent.GetPropertyValueByName(part.PropertyName);
                        }
                        else
                        {
                            var key = part.Property.Relationship.ThisEnd.GetCompositeKey(parent, true);
                            result = await dataContext.GetDbSetByEntityType(part.Property.Relationship.OtherEnd.Type)
                                .GetWithKeyAsync(key);
                        }

                        results.Add(new IqlPropertyPathEvaluated(
                            evaluationResult,
                            part,
                            parent,
                            result,
                            propertyPath.PropertyPath.Length,
                            i));

                        if (result == null)
                        {
                            success = i == propertyPath.PropertyPath.Length - 1;
                            break;
                        }
                    }
                    else
                    {
                        results.Add(new IqlPropertyPathEvaluated(
                            evaluationResult,
                            part,
                            parent,
                            null,
                            propertyPath.PropertyPath.Length,
                            i));
                        break;
                    }
                }
                else
                {
                    results.Add(new IqlPropertyPathEvaluated(
                        evaluationResult,
                        part,
                        parent,
                        result,
                        propertyPath.PropertyPath.Length,
                        i));
                }
            }

            evaluationResult.Success = success;
            evaluationResult.Results = results.ToArray();
            return evaluationResult;
        }

        public static async Task<T> EvaluateAsAsync<T>(this IqlPropertyPath propertyPath, object entity, IDataContext dataContext)
        {
            var result = await propertyPath.EvaluateAsync(entity, dataContext);
            return (T)result.Value;
        }
    }
}