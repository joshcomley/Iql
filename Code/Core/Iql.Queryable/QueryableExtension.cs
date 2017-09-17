//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Reflection;
//using Iql.Parsing.Reduction;
//using Iql.Queryable.Data.Crud.Operations;
//using Iql.Queryable.Data.DataContext;
//using Iql.Queryable.Expressions;
//using Iql.Queryable.Queryable.Operations;
//using Iql.Queryable.Queryable.Operations.Applicators;
//using TypeScript.Converter;

//namespace Iql.Queryable.Queryable
//{
//    public class QueryableExtension<T>
//        where T : class
//    {
//        public QueryableExtension(IQueryable<T> queryable)
//        {
//            this.Querable = queryable;
//        }

//        public IQueryable<T> Querable { get; set; }

//        public IQueryResultBase ToQueryWithAdapterBase
//        (
//            IQueryableAdapterBase adapter,
//            IDataContext dataContext
//        )
//        {
//            // if (!adapter) {
//            //     if (!QueryableBase.DefaulTQueryAdapterResolver) {
//            //         QueryableBase.DefaulTQueryAdapterResolver = () => new JavaScriptQueryableAdapter();
//            //     }
//            //     adapter = QueryableBase.DefaulTQueryAdapterResolver();
//            // }
//            //var adapter = new adapterCtor();
//            adapter.Context = dataContext;
//            adapter.Begin(dataContext);
//            var data = adapter.NewQueryData(Querable);
//            Querable.Operations.ForEach(operation =>
//            {
//                var applicator = adapter.ResolveApplicator<IQueryOperation>(operation);
//                if (operation is OrderByOperation ||
//                    operation is WhereOperation ||
//                    operation is IExpandOperation)
//                {
//                    ToIql(operation as IExpressionQueryOperation, dataContext);
//                }
//                if (operation is IExpandOperation)
//                {
//                    var expand = operation as IExpandOperation;
//                    expand.ExpandDetails = new List<ExpandDetail>();
//                    ResolveExpand(
//                        dataContext,
//                        expand,
//                        expand.Expression as IqlPropertyExpression,
//                        typeof(T));
//                }
//                if (applicator == null)
//                {
//                    throw new Exception("Operation not supported: " + operation.ToString());
//                }
//                ApplyOperation(operation as IExpressionQueryOperation,
//                    dataContext,
//                    data,
//                    applicator);
//            });
//            return data;
//        }

//        public IqlExpression ToIql(IExpressionQueryOperation operation, IDataContext dataContext)
//        {
//            var adapter = new IqlQueryableAdapter();
//            var applicator = adapter
//                .ResolveApplicator(operation);
//            var newQueryData = adapter.NewQueryData(Querable);
//            ApplyOperation(operation, dataContext, newQueryData, applicator);
//            return new IqlReducer(
//                // TODO: Add reducer registry

//                //queryOperation.getExpression().evaluateContext || this.evaluateContext
//                )
//                .ReduceStaticContent(operation.Expression);
//        }
//        private void ApplyOperation(IExpressionQueryOperation operation, IDataContext dataContext,
//            IQueryResultBase newQueryData, IQueryOperationApplicatorBase applicator)
//        {
//            var contextArgs = new List<object>();
//            if (Platform.Name == "JavaScript")
//            {
//                contextArgs.Add(null);
//                contextArgs.Add(null);
//                contextArgs.Add(null);
//            }
//            contextArgs.Add(dataContext);
//            contextArgs.Add(operation);
//            contextArgs.Add(newQueryData);
//            contextArgs.Add(this);
//            var context = Activator.CreateInstance(
//                typeof(QueryOperationContext<,,>)
//                    .MakeGenericType(typeof(T), operation.GetType(), newQueryData.GetType()),
//                contextArgs.ToArray()
//            );
//            var name = nameof(IQueryOperationApplicator<IExpressionQueryOperation, IQueryResultBase>.Apply);
//            var method = applicator.GetType()
//                .GetRuntimeMethods()
//                .Single(m => m.Name == name)
//                .MakeGenericMethod(typeof(T));
//            var args = new List<object>();
//            if (Platform.Name == "JavaScript")
//            {
//                args.Add(null);
//            }
//            args.Add(context);
//            method.Invoke(applicator, args.ToArray());
//        }

//        private Type ResolveExpand(
//            IDataContext dataContext,
//            IExpandOperation operation,
//            IqlPropertyExpression expression,
//            Type typeConstructor,
//            int depth = 0
//        )
//        {
//            if (expression.Parent.Type != IqlExpressionType.RootReference)
//            {
//                typeConstructor = ResolveExpand(
//                    dataContext,
//                    operation,
//                    expression.Parent as IqlPropertyExpression,
//                    typeConstructor,
//                    depth + 1);
//            }
//            var propertyToExpand = expression;
//            var entityConfiguration = dataContext.EntityConfigurationContext.GetEntityByName(
//                typeConstructor.Name
//            );
//            for (var i = 0; i < entityConfiguration.Relationships.Count; i++)
//            {
//                var relationship = entityConfiguration.Relationships[i];
//                if (relationship.SourceProperty.PropertyName == propertyToExpand.PropertyName ||
//                    relationship.TargetProperty.PropertyName == propertyToExpand.PropertyName)
//                {
//                    var detail = new ExpandDetail(
//                        dataContext.AsDbSetByType(relationship.SourceType),
//                        dataContext.AsDbSetByType(relationship.TargetType),
//                        relationship);
//                    // We have our relationship
//                    operation.ExpandDetails.Add(detail);
//                    if (depth == 0)
//                    {
//                        detail.TargetQueryable = operation.ApplyQuery(detail.TargetQueryable);
//                    }
//                    return relationship.TargetType;
//                }
//            }
//            return null;
//        }

//    }
//}

