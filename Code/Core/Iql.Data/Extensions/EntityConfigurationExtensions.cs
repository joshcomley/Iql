using System;
using Iql.Data.Context;
using Iql.Data.Operations;
using Iql.Entities;
using Iql.Queryable.Expressions;
using Iql.Queryable.Operations;

namespace Iql.Data.Extensions
{
    public static class EntityConfigurationExtensions
    {
        public static IExpressionQueryOperation BuildExpandOperation(
            this IDataContext dataContext,
            Type entityType,
            string propertyPath)
        {
            var entityConfiguration = dataContext
                .EntityConfigurationContext
                .GetEntityByType(entityType);
            var path = IqlPropertyPath.FromString(dataContext.EntityConfigurationContext, propertyPath, entityConfiguration.TypeMetadata).Top;

            var expandOperation = new ExpandOperation();
            var returnOperation = expandOperation;
            var property = entityConfiguration.FindProperty(path.PropertyName);
            if (property == null)
            {
                return null;
            }
            var expandedDbSet = dataContext.GetDbSetByEntityType(
                property.Relationship.OtherEnd.Type);
            if (path.Child != null && path.Child.Property.Kind.HasFlag(IqlPropertyKind.Relationship))
            {
                expandedDbSet = expandedDbSet.ExpandRelationship(path.Child.PathFromHere);
            }
            expandOperation.QueryExpression = new ExpandQueryExpression(null
                , q => expandedDbSet
                // .ClearOperations()
                );
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