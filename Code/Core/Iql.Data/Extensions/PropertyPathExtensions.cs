using System.Collections.Generic;
using System.Threading.Tasks;
using Iql.Data.Context;
using Iql.Data.Evaluation;
using Iql.Entities;
using Iql.Extensions;

namespace Iql.Data.Extensions
{
    public static class PropertyPathExtensions
    {
        public static async Task<IqlPropertyPathEvaluationResult> EvaluateAsync(
            this IqlPropertyPath propertyPath, 
            object entity,
            IDataContext dataContext)
        {
            return await EvaluateCustomAsync(propertyPath, entity, new DefaultEvaluator(dataContext));
        }

        public static async Task<IqlPropertyPathEvaluationResult> EvaluateCustomAsync(
            this IqlPropertyPath propertyPath, 
            object entity, 
            IIqlCustomEvaluator customEvaluator)
        {
            var evaluationResult = new IqlPropertyPathEvaluationResult(
                false,
                entity,
                propertyPath,
                new IqlPropertyPathEvaluated[] { });

            if (entity == null)
            {
                evaluationResult.Success = propertyPath.Parent == null;
                if (evaluationResult.Success)
                {
                    evaluationResult.Results = new IqlPropertyPathEvaluated[]
                    {
                        new IqlPropertyPathEvaluated(
                            evaluationResult,
                            propertyPath,
                            null,
                            null,
                            propertyPath.PropertyPath.Length,
                            0)
                    };
                }
                return evaluationResult;
            }

            var success = true;
            var results = new List<IqlPropertyPathEvaluated>();
            var result = entity;
            for (var i = 0; i < propertyPath.PropertyPath.Length; i++)
            {
                var part = propertyPath.PropertyPath[i];
                var parent = result;
                result = part.PropertyName == null ? result : result.GetPropertyValueByName(part.PropertyName);
                if (result == null)
                {
                    if (part.Property.Kind.HasFlag(PropertyKind.Relationship))
                    {
                        //if (dataContext.DataStore.Tracking.IsTracked(parent))
                        //{
                        //    await customEvaluator.FetchRelationshipPropertyAsync(
                        //        part.Property,
                        //        part.Property.BuildRelationshipCompositeKey(parent));
                        //    result = parent.GetPropertyValueByName(part.PropertyName);
                        //}
                        //else
                        //{
                        //}
                        var key = part.Property.Relationship.ThisEnd.GetCompositeKey(parent, true);
                        result = await customEvaluator.GetEntityByKeyAsync(part.Property.Relationship.OtherEnd.EntityConfiguration,
                            key);
                        if (part.Property.GetValue(parent) != result)
                        {
                            part.Property.SetValue(parent, result);
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