using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Iql.Parsing.Reduction;
using Iql.Queryable.Data;
using Iql.Queryable.Operations;
using Iql.Queryable.Operations.Applicators;
using TypeSharp.Extensions;

namespace Iql.Queryable
{
    public class QueryableBaseExtension
    {
        public QueryableBaseExtension(IQueryableBase queryable)
        {
            Queryable = queryable;
        }

        public IQueryableBase Queryable { get; set; }

        public IQueryResultBase ToQueryWithAdapterBase
        (
            IQueryableAdapterBase adapter,
            IDataContext dataContext
        )
        {
            // if (!adapter) {
            //     if (!QueryableBase.DefaulTQueryAdapterResolver) {
            //         QueryableBase.DefaulTQueryAdapterResolver = () => new JavaScriptQueryableAdapter();
            //     }
            //     adapter = QueryableBase.DefaulTQueryAdapterResolver();
            // }
            //var adapter = new adapterCtor();
            adapter.Context = dataContext;
            adapter.Begin(dataContext);
            var data = NewQueryData(adapter);
            Queryable.Operations.ForEach(operation =>
            {
                var applicator = adapter.ResolveApplicator(operation);
                if (operation is OrderByOperation ||
                    operation is WhereOperation ||
                    operation is IExpandOperation)
                {
                    ToIql(operation as IExpressionQueryOperation, dataContext);
                }
                if (operation is IExpandOperation)
                {
                    var expand = operation as IExpandOperation;
                    expand.ExpandDetails = new List<ExpandDetail>();
                    ResolveExpand(
                        dataContext,
                        expand,
                        expand.Expression as IqlPropertyExpression,
                        Queryable.ItemType);
                }
                if (applicator == null)
                {
                    throw new Exception("Operation not supported: " + operation.ToString());
                }
                ApplyOperation(operation,
                    dataContext,
                    data,
                    applicator);
            });
            return data;
        }

        public IqlExpression ToIql(
            IExpressionQueryOperation operation, IDataContext dataContext)
        {
            var adapter = new IqlQueryableAdapter();
            var applicator = adapter
                .ResolveApplicator(operation);
            var newQueryData = NewQueryData(adapter);
            ApplyOperation(operation, dataContext, newQueryData, applicator);
            return new IqlReducer(
                    operation.EvaluateContext ?? Queryable.EvaluateContext
                    // TODO: Add reducer registry

                    //queryOperation.getExpression().evaluateContext || this.evaluateContext
                )
                .ReduceStaticContent(operation.Expression);
        }

        private IQueryResultBase NewQueryData(IQueryableAdapterBase adapter)
        {
            var newQueryData =
                (IQueryResultBase) adapter.GetType().GetMethod(nameof(adapter.NewQueryData)).MakeGenericMethod(
                        Queryable.ItemType)
                    .Invoke(adapter, new object[]
                    {
                        Queryable
#if TypeScript
                        ,Queryable.ItemType
#endif
                    });
            return newQueryData;
        }

        private void ApplyOperation(IQueryOperation operation, IDataContext dataContext,
            IQueryResultBase newQueryData, IQueryOperationApplicatorBase applicator)
        {
            var contextArgs = new List<object>();
            contextArgs.Add(dataContext);
            contextArgs.Add(operation);
            contextArgs.Add(newQueryData);
            contextArgs.Add(Queryable);
            if (Platform.Name == "JavaScript")
            {
                contextArgs.Add(null);
                contextArgs.Add(null);
                contextArgs.Add(null);
            }
            var context = Activator.CreateInstance(
                typeof(QueryOperationContext<,,>)
                    .MakeGenericType(Queryable.ItemType, operation.GetType(), newQueryData.GetType()),
                contextArgs.ToArray()
            );
            var name = nameof(IQueryOperationApplicator<IExpressionQueryOperation, IQueryResultBase>.Apply);
            var method = applicator.GetType()
                .GetRuntimeMethods()
                .Single(m => m.Name == name)
                .MakeGenericMethod(Queryable.ItemType);
            var args = new List<object>();
            args.Add(context);
            if (Platform.Name == "JavaScript")
            {
                args.Add(Queryable.ItemType);
            }
            method.Invoke(applicator, args.ToArray());
        }

        private Type ResolveExpand(
            IDataContext dataContext,
            IExpandOperation operation,
            IqlPropertyExpression expression,
            Type typeConstructor,
            int depth = 0
        )
        {
            if (expression.Parent.Type != IqlExpressionType.RootReference)
            {
                typeConstructor = ResolveExpand(
                    dataContext,
                    operation,
                    expression.Parent as IqlPropertyExpression,
                    typeConstructor,
                    depth + 1);
            }
            var propertyToExpand = expression;
            var entityConfiguration = dataContext.EntityConfigurationContext.GetEntityByName(
                typeConstructor.Name
            );
            for (var i = 0; i < entityConfiguration.Relationships.Count; i++)
            {
                var relationship = entityConfiguration.Relationships[i];
                var sourceExpand = relationship.SourceProperty.PropertyName == propertyToExpand.PropertyName;
                var targetExpand = relationship.TargetProperty.PropertyName == propertyToExpand.PropertyName;
                if (sourceExpand || targetExpand)
                {
                    var detail = new ExpandDetail(
                        dataContext.AsDbSetByType(relationship.SourceType),
                        dataContext.AsDbSetByType(relationship.TargetType),
                        relationship,
                        targetExpand);
                    // We have our relationship
                    operation.ExpandDetails.Add(detail);
                    if (depth == 0)
                    {
                        detail.TargetQueryable = operation.ApplyQuery(detail.TargetQueryable);
                    }
                    return relationship.TargetType;
                }
            }
            return null;
        }
    }
}