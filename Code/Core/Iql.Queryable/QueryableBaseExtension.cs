using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Iql.Extensions;
using Iql.Parsing.Reduction;
using Iql.Queryable.Data;
using Iql.Queryable.Expressions.QueryExpressions;
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
            IDataContext dataContext,
            IQueryOperationContextBase parentContext,
            IQueryResultBase parentResult
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
            var data = NewQueryData(adapter, parentResult);            
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
                ApplyOperation(
                    adapter,
                    operation,
                    dataContext,
                    data,
                    applicator,
                    parentContext);
            });
            return data;
        }

        public IqlExpression ToIql(
            IExpressionQueryOperation operation, IDataContext dataContext)
        {
            var adapter = new IqlQueryableAdapter();
            var applicator = adapter
                .ResolveApplicator(operation);
            var newQueryData = NewQueryData(adapter, null);
            ApplyOperation(adapter, operation, dataContext, newQueryData, applicator, null);
            return new IqlReducer(
#if TypeScript
                    operation.EvaluateContext ?? Queryable.EvaluateContext ?? dataContext.EvaluateContext
#endif
                // TODO: Add reducer registry

                //queryOperation.getExpression().evaluateContext || this.evaluateContext
                )
                .ReduceStaticContent(operation.Expression);
        }

        private IQueryResultBase NewQueryData(IQueryableAdapterBase adapter, IQueryResultBase parentResult)
        {
            var newQueryData =
                (IQueryResultBase) adapter
                    .GetType()
                    .GetMethod(nameof(adapter.NewQueryData))
                    .InvokeGeneric(adapter, new object[]
                        {
                            Queryable
                        },
                        Queryable.ItemType);
            newQueryData.ParentResult = parentResult;
            return newQueryData;
        }

        private void ApplyOperation(
            IQueryableAdapterBase adapter, 
            IQueryOperation operation, 
            IDataContext dataContext,
            IQueryResultBase queryResult, 
            IQueryOperationApplicatorBase applicator,
            IQueryOperationContextBase parentContext)
        {
#if TypeScript
            operation.EvaluateContext = operation.EvaluateContext ?? dataContext.EvaluateContext;
#endif
            var contextArgs = new List<object>();
            contextArgs.Add(dataContext);
            contextArgs.Add(operation);
            contextArgs.Add(queryResult);
            contextArgs.Add(Queryable);
            contextArgs.Add(adapter);
            contextArgs.Add(parentContext);
            if (Platform.Name == "JavaScript")
            {
                contextArgs.Add(null);
                contextArgs.Add(null);
                contextArgs.Add(null);
                contextArgs.Add(null);
            }
            var context =  (IQueryOperationContextBase)
                Activator.CreateInstance(
                typeof(QueryOperationContext<,,,>)
                    .MakeGenericType(
                        Queryable.ItemType, 
                        operation.GetType(), 
                        queryResult.GetType(),
                        adapter.GetType()
                        ),
                contextArgs.ToArray()
            );
            queryResult.Context = context;
            var name = nameof(IQueryOperationApplicator<IExpressionQueryOperation, IQueryResultBase, IQueryableAdapterBase>.Apply);
            applicator.GetType()
                .GetRuntimeMethods()
                .First(m => m.Name == name)
                .InvokeGeneric(applicator, new object[]{context}, Queryable.ItemType);
        }

        private Type ResolveExpand(
            IDataContext dataContext,
            IExpandOperation operation,
            IqlPropertyExpression expression,
            Type typeConstructor,
            int depth = 0
        )
        {
            var expandExpression = operation.GetExpression() as IExpandQueryExpression;
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
            var sourceEntityConfiguration = dataContext.EntityConfigurationContext.GetEntityByType(
                typeConstructor
            );
            var relationshipEntityType = sourceEntityConfiguration.FindProperty(propertyToExpand.PropertyName).ElementType;
            var relatedEntityConfiguration = dataContext.EntityConfigurationContext.GetEntityByType(
                relationshipEntityType
            );

            var relationships = sourceEntityConfiguration == relatedEntityConfiguration
                ? sourceEntityConfiguration.Relationships
                : sourceEntityConfiguration.Relationships.Concat(
                    relatedEntityConfiguration.Relationships).ToList();

            for (var i = 0; i < relationships.Count; i++)
            {
                var relationship = relationships[i];
                var relationshipSource = relationship.Source;
                var relationshipTarget = relationship.Target;
                var targetExpand = false;
                var selfTargetingRelationship = sourceEntityConfiguration == relatedEntityConfiguration;
                if ((selfTargetingRelationship && relationshipSource.Property.Name != propertyToExpand.PropertyName) ||
                    (!selfTargetingRelationship && relationshipTarget.Configuration == sourceEntityConfiguration))
                {
                    targetExpand = true;
                    var temp = relationshipSource;
                    relationshipSource = relationshipTarget;
                    relationshipTarget = temp;
                }
                if (relationshipSource.Property.Name != propertyToExpand.PropertyName)
                {
                    continue;
                }
                var detail = new ExpandDetail(
                    dataContext.AsDbSetByType(relationshipSource.Type),
                    dataContext.AsDbSetByType(relationshipTarget.Type),
                    relationship,
                    targetExpand);
                // We have our relationship
                operation.ExpandDetails.Add(detail);
                if (depth == 0)
                {
                    detail.TargetQueryable = operation.ApplyQuery(detail.TargetQueryable);
                }
                //if (targetExpand)
                //{
                //    detail.SourceQueryable = expandExpression.GetQueryable()(detail.SourceQueryable);
                //}
                //else
                //{
                //    detail.TargetQueryable = expandExpression.GetQueryable()(detail.TargetQueryable);
                //}
                if (expandExpression != null)
                {
                    detail.TargetQueryable = expandExpression.GetQueryable()(detail.TargetQueryable);
                }
                return relationshipTarget.Type;
            }
            return null;
        }
    }
}