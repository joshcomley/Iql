using System;
using System.Collections.Generic;
using Iql.Extensions;
using Iql.Queryable.Expressions;
using Iql.Queryable.Expressions.QueryExpressions;
using Iql.Queryable.Operations;
using Iql.Queryable.Operations.Applicators;

namespace Iql.Queryable.Extensions
{
    public static class QueryOperationContextExtensions
    {
        public static WhereOperation ResolveWithKkeyWhereOperation(this IQueryOperationContextBase context)
        {
            var key = (context.Operation as WithKeyOperation).Key;
            var configuration = context.DataContext.EntityConfigurationContext.GetEntityByType(context.Queryable.ItemType);
            var keyDefinition = configuration.Key;
            var root = new IqlRootReferenceExpression("entity", null);
            var checks = new List<IqlExpression>();
            keyDefinition.Properties.ForEach(property =>
            {
                var propertyExpression = new IqlPropertyExpression(
                    property.PropertyName,
                    context.Queryable.ItemType.Name,
                    key.GetType().ToIqlType());
                propertyExpression.Parent = root;
                var check = new IqlIsEqualToExpression(
                    propertyExpression,
                    new IqlLiteralExpression(key.ToString(), key.GetType().ToIqlType())
                );
                checks.Add(check);
            });
            var rootOperation = checks[0];
            for (var i = 1; i < checks.Count; i++)
            {
                rootOperation = new IqlAndExpression(rootOperation, checks[i]);
            }
            var operation = new WhereOperation();
            var queryExpressinType = typeof(WhereQueryExpression<>).MakeGenericType(context.Queryable.ItemType);
            var queryExpression = (ExpressionQueryExpressionBase)Activator.CreateInstance(queryExpressinType, new object[]
            {
                null,
                context.DataContext.EvaluateContext
#if TypeScript
                , context.Queryable.ItemType
#endif
            });
            operation.QueryExpression = queryExpression;
            operation.Expression = rootOperation;
            return operation;
        }
    }
}