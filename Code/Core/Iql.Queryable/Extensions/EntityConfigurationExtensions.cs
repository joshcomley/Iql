using System;
using System.Linq.Expressions;
using Iql.Queryable.Data.Context;
using Iql.Queryable.Data.DataStores.InMemory.QueryApplicator;
using Iql.Queryable.Data.Lists;
using Iql.Queryable.Expressions;
using Iql.Queryable.Expressions.QueryExpressions;
using Iql.Queryable.Operations;

namespace Iql.Queryable.Extensions
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
            var path = IqlPropertyPath.FromString(propertyPath, entityConfiguration).Top;

            var expandOperation = new ExpandOperation();
            var returnOperation = expandOperation;
            IDbQueryable expandedDbSet = dataContext.GetDbSetByEntityType(
                entityConfiguration.FindProperty(path.PropertyName).Relationship.OtherEnd.Type);
            if (path.Child != null)
            {
                expandedDbSet = expandedDbSet.ExpandRelationship(path.Child.PathFromHere);
            }
            expandOperation.QueryExpression = new ExpandQueryExpression(null, q => expandedDbSet);
            path.Expression.Parent = new IqlRootReferenceExpression();
            expandOperation.Expression = path.Expression;
            return returnOperation;
        }

        public static IExpressionQueryOperation BuildExpandOperationFromLambdaExpression<T, TProperty>(
            this IDataContext dataContext,
            Expression<Func<T, TProperty>> expression) where T : class
        {
            return dataContext.BuildExpandOperationFromIqlExpression(
                typeof(T),
                (IqlPropertyExpression) IqlQueryableAdapter.ExpressionConverter().ConvertLambdaExpressionToIql<T>(expression).Expression);
        }

        public static IExpressionQueryOperation BuildExpandOperationFromIqlExpression(
            this IDataContext dataContext,
            Type entityType,
            IqlPropertyExpression expression)
        {
            var expandOperation = new ExpandOperation();
            var property = dataContext
                .EntityConfigurationContext
                .GetEntityByType(entityType)
                .FindPropertyByIqlExpression(expression);
            expandOperation.QueryExpression = new ExpandQueryExpression(null, q => dataContext.GetDbSetByEntityType(property.Relationship.OtherEnd.Type));
            expandOperation.Expression = expression;
            return expandOperation;
        }
    }
}