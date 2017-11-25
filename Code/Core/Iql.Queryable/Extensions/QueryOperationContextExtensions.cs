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
        public static WhereOperation ResolveWithKeyWhereOperation(this IQueryOperationContextBase context)
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
            var compositeKey = new CompositeKey();
            compositeKey.Keys
                .AddRange(
                    keyDefinition.Properties.Select(kp => new KeyValue(
                        kp.PropertyName, 
                        entity.GetPropertyValue(kp.PropertyName),
                        configuration.FindProperty(kp.PropertyName).Type))
                );
            var withKeyOperation = new WithKeyOperation(compositeKey);
            return withKeyOperation;
        }

        public static WhereOperation ResolveIdentityWhereOperation(this IDataContext dataContext, Type itemType,
            CompositeKey key)
        {
            var configuration = dataContext.EntityConfigurationContext.GetEntityByType(itemType);
            var keyDefinition = configuration.Key;
            var root = new IqlRootReferenceExpression("entity", null);
            var checks = new List<IqlExpression>();
            keyDefinition.Properties.ForEach(property =>
            {
                var keyValue = key.Keys.Single(k => k.Name == property.PropertyName);
                var propertyExpression = new IqlPropertyExpression(
                    property.PropertyName,
                    itemType.Name,
                    keyValue.Value.GetType().ToIqlType());
                propertyExpression.Parent = root;
                var check = new IqlIsEqualToExpression(
                    propertyExpression,
                    new IqlLiteralExpression(keyValue.Value.ToString(), keyValue.Value.GetType().ToIqlType())
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