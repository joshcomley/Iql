using System;
using System.Collections.Generic;
using System.Linq;
using Iql.Extensions;
using Iql.Queryable.Data;
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
            var dataContext = context.DataContext;
            var queryable = context.Queryable;
            return ResolveIdentityWhereOperation(dataContext, queryable.ItemType, key);
        }

        public static WithKeyOperation ResolveWithKeyOperationFromEntity<TEntity>(this IDataContext dataContext,
            TEntity entity) where TEntity : class
        {
            var configuration = dataContext.EntityConfigurationContext.GetEntityByType(typeof(TEntity));
            var keyDefinition = configuration.Key;
            var key = entity.GetPropertyValue(keyDefinition.Properties.First().PropertyName);
            var withKeyOperation = new WithKeyOperation(key);
            return withKeyOperation;
        }

        public static WhereOperation ResolveIdentityWhereOperation(this IDataContext dataContext, Type itemType,
            object key)
        {
            var configuration = dataContext.EntityConfigurationContext.GetEntityByType(itemType);
            var keyDefinition = configuration.Key;
            var root = new IqlRootReferenceExpression("entity", null);
            var checks = new List<IqlExpression>();
            keyDefinition.Properties.ForEach(property =>
            {
                var propertyExpression = new IqlPropertyExpression(
                    property.PropertyName,
                    itemType.Name,
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
            var queryExpressinType = typeof(WhereQueryExpression<>).MakeGenericType(itemType);
            var queryExpression = (ExpressionQueryExpressionBase)Activator.CreateInstance(queryExpressinType, new object[]
            {
                null
#if TypeScript
                , dataContext.EvaluateContext
#endif
            });
            operation.QueryExpression = queryExpression;
            operation.Expression = rootOperation;
            return operation;
        }
    }
}