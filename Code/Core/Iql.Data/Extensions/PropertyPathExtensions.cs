using System.Collections.Generic;
using System.Threading.Tasks;
using Iql.Data.Context;
using Iql.Data.Evaluation;
using Iql.Entities;
using Iql.Entities.Extensions;
using Iql.Extensions;

namespace Iql.Data.Extensions
{
    public static class PropertyPathExtensions
    {
        public static async Task<IqlPropertyPathEvaluationResult> EvaluateAsync(
            this IqlPropertyPath propertyPath, 
            object entity,
            IDataContext dataContext,
            bool populate)
        {
            return await EvaluateCustomAsync(propertyPath, entity, dataContext, populate);
        }

        public static async Task<IqlPropertyPathEvaluationResult> EvaluateCustomAsync(
            this IqlPropertyPath propertyPath, 
            object entity, 
            IIqlDataEvaluator dataEvaluator,
            bool populate)
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
                if (result == null)
                {
                    break;
                }
                result = part.PropertyName == null ? result : result.GetPropertyValueByName(part.PropertyName);
                if (part.Property != null && part.Property.Kind.HasFlag(PropertyKind.Relationship))
                {
                    var key = part.Property.EntityProperty().Relationship.ThisEnd.GetCompositeKey(parent, true);
                    result = await dataEvaluator.GetEntityByKeyAsync(
                        part.Property.EntityProperty().Relationship.OtherEnd.EntityConfiguration,
                        key,
                        new string[]{});
                    if (populate && part.Property.GetValue(parent) != result)
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
                        result,
                        propertyPath.PropertyPath.Length,
                        i));
                }
            }

            evaluationResult.Success = success;
            evaluationResult.Results = results.ToArray();
            return evaluationResult;
        }

        public static async Task<T> EvaluateAsAsync<T>(this IqlPropertyPath propertyPath, object entity, IDataContext dataContext, bool populate)
        {
            var result = await propertyPath.EvaluateAsync(entity, dataContext, populate);
            return (T)result.Value;
        }
    }
}