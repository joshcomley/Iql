using System;
using System.Collections.Generic;
using System.Linq;
using Iql.Data.Configuration;
using Iql.Data.Configuration.Extensions;
using Iql.Data.Operations;
using Iql.Extensions;
using Iql.Parsing.Expressions;
using Iql.Parsing.Expressions.QueryExpressions;
using Iql.Queryable.Operations;

namespace Iql.Data.Context
{
    public static class QueryOperationContextExtensions
    {
        //public static WhereOperation ResolveWithKeyWhereOperation(this IQueryOperationContextBase context)
        //{
        //    var key = (context.Operation as WithKeyOperation).Key;
        //    var dataContext = context.DataContext;
        //    var queryable = context.Queryable;
        //    return ResolveIdentityWhereOperation(dataContext, queryable.ItemType, key);
        //}

        public static WithKeyOperation ResolveWithKeyOperationFromEntity<TEntity>(this IDataContext dataContext,
            TEntity entity
#if TypeScript
            , Type entityType
#endif
            ) where TEntity : class
        {
#if !TypeScript
            var entityType = entity.GetType();
#endif
            var configuration = dataContext.EntityConfigurationContext.GetEntityByType(entityType);
            var keyDefinition = configuration.Key;
            var compositeKey = new CompositeKey(keyDefinition.Properties.Length);
            for (var i = 0; i < keyDefinition.Properties.Length; i++)
            {
                var kp = keyDefinition.Properties[i];
                compositeKey.Keys[i] = new KeyValue(
                    kp.Name,
                    entity.GetPropertyValue(kp),
                    kp.TypeDefinition);
            }
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
            foreach (var property in keyDefinition.Properties)
            {
                var keyValue = key.Keys.Single(k => k.Name == property.Name);
                var propertyExpression = new IqlPropertyExpression(
                    property.Name,
                    root,
                    keyValue.Value.GetType().ToIqlType());
                var check = new IqlIsEqualToExpression(
                    propertyExpression,
                    new IqlLiteralExpression(keyValue.Value.ToString(), keyValue.Value.GetType().ToIqlType())
                );
                checks.Add(check);
            }
            var rootOperation = checks[0];
            for (var i = 1; i < checks.Count; i++)
            {
                rootOperation = new IqlAndExpression(rootOperation, checks[i]);
            }
            var operation = new WhereOperation();
            var queryExpressinType = typeof(WhereQueryExpression);
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