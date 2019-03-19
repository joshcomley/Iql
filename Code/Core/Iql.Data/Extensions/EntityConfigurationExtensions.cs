using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Iql.Data.Context;
using Iql.Data.Evaluation;
using Iql.Data.Operations;
using Iql.Entities;
using Iql.Entities.InferredValues;
using Iql.Entities.Services;
using Iql.Queryable.Expressions;
using Iql.Queryable.Operations;

namespace Iql.Data.Extensions
{
    public static class EntityConfigurationExtensions
    {
        public static async Task<InferredValuesResult> TrySetInferredValuesAsync(
            this IEntityConfiguration config,
            object oldEntity,
            object entity,
            bool isInitialize,
            IIqlCustomEvaluator customEvaluator,
            IServiceProviderProvider serviceProviderProvider = null)
        {
            var result = await TryGetInferredValuesAsync(config, oldEntity, entity, isInitialize, customEvaluator, serviceProviderProvider);
            result.ApplyChanges();
            return result;
        }

        public static async Task<InferredValuesResult> TryGetInferredValuesAsync(
            this IEntityConfiguration config, 
            object oldEntity, 
            object entity,
            bool isInitialize,
            IIqlCustomEvaluator customEvaluator, 
            IServiceProviderProvider serviceProviderProvider)
        {
            serviceProviderProvider = serviceProviderProvider ?? config.Builder;
            var changes = new List<InferredValueChanges>();
            for (var i = 0; i < config.Properties.Count; i++)
            {
                var propety = config.Properties[i];
                var inferredValueChanges = await propety.TryGetInferredValueCustomAsync(
                    oldEntity,
                    entity,
                    isInitialize,
                    customEvaluator,
                    serviceProviderProvider);
                changes.Add(inferredValueChanges);
            }

            return new InferredValuesResult(oldEntity, entity, changes.ToArray());
        }

        public static IExpressionQueryOperation BuildExpandOperation(
            this IDataContext dataContext,
            Type entityType,
            string propertyPath)
        {
            var entityConfiguration = dataContext
                .EntityConfigurationContext
                .GetEntityByType(entityType);
            var path = IqlPropertyPath.FromString(propertyPath, entityConfiguration).Top;

            var expandOperation = new ExpandOperation();
            var returnOperation = expandOperation;
            var expandedDbSet = dataContext.GetDbSetByEntityType(
                entityConfiguration.FindProperty(path.PropertyName).Relationship.OtherEnd.Type);
            if (path.Child != null && path.Child.Property.Kind.HasFlag(PropertyKind.Relationship))
            {
                expandedDbSet = expandedDbSet.ExpandRelationship(path.Child.PathFromHere);
            }
            expandOperation.QueryExpression = new ExpandQueryExpression(null, q => expandedDbSet);
            path.Expression.Parent = new IqlRootReferenceExpression();
            expandOperation.Expression = path.Expression;
            return returnOperation;
        }

//        public static IExpressionQueryOperation BuildExpandOperationFromLambdaExpression<T, TProperty>(
//            this IDataContext dataContext,
//            Expression<Func<T, TProperty>> expression
//#if TypeScript
//         , EvaluateContext evaluateContext
//#endif

//            ) where T : class
//        {
//            return dataContext.BuildExpandOperationFromIqlExpression(
//                typeof(T),
//                (IqlPropertyExpression) IqlQueryableAdapter.ExpressionConverter()
//                    .ConvertLambdaExpressionToIqlByType(expression, typeof(T)
//#if TypeScript
//            , evaluateContext
//#endif
//                    ).Expression);
//        }

//        public static IExpressionQueryOperation BuildExpandOperationFromIqlExpression(
//            this IDataContext dataContext,
//            Type entityType,
//            IqlPropertyExpression expression)
//        {
//            var expandOperation = new ExpandOperation();
//            var property = dataContext
//                .EntityConfigurationContext
//                .GetEntityByType(entityType)
//                .FindPropertyByIqlExpression(expression);
//            expandOperation.QueryExpression = new ExpandQueryExpression(null, q => dataContext.GetDbSetByEntityType(property.Relationship.OtherEnd.Type));
//            expandOperation.Expression = expression;
//            return expandOperation;
//        }
    }
}