using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ExpressionModifier;
using Iql.Parsing;
using Iql.Parsing.Reduction;
using Iql.Queryable.Data.DataStores.InMemory;
using Iql.Queryable.Types;

namespace Iql.DotNet.IqlToDotNetExpression
{
    /// <summary>
    /// updates the parameter in the expression
    /// </summary>
    class ParameterUpdateVisitor : ExpressionVisitor
    {
        private ParameterExpression _oldParameter;
        private ParameterExpression _newParameter;

        public ParameterUpdateVisitor(ParameterExpression oldParameter, ParameterExpression newParameter)
        {
            _oldParameter = oldParameter;
            _newParameter = newParameter;
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            if (object.ReferenceEquals(node, _oldParameter))
                return _newParameter;

            return base.VisitParameter(node);
        }
    }
    public class DotNetIqlParserInstance : ActionParserInstance<DotNetIqlData, DotNetIqlExpressionAdapter, Expression, DotNetOutput, DotNetExpressionConverter>
    {
        public DotNetIqlParserInstance(DotNetIqlExpressionAdapter adapter, Type rootEntityType, DotNetExpressionConverter expressionConverter) : base(adapter, rootEntityType, expressionConverter, new TypeResolver())
        {
            ContextParameter = System.Linq.Expressions.Expression.Parameter(
                typeof(InMemoryContext<>).MakeGenericType(rootEntityType), "context");
        }

        public Dictionary<IqlLambdaExpression, Dictionary<string, ParameterExpression>> ParameterExpressions { get; } = new Dictionary<IqlLambdaExpression, Dictionary<string, ParameterExpression>>();

        public ParameterExpression GetParameterExpression(IqlLambdaExpression lambda,
            IqlRootReferenceExpression parameter)
        {
            if (!ParameterExpressions.ContainsKey(lambda))
            {
                ParameterExpressions.Add(lambda, new Dictionary<string, ParameterExpression>());
            }

            var lookup = ParameterExpressions[lambda];
            if (!lookup.ContainsKey(parameter.VariableName))
            {
                lookup.Add(parameter.VariableName, System.Linq.Expressions.Expression.Parameter(ResolveParameterType(parameter), parameter.VariableName));
            }

            return lookup[parameter.VariableName];
        }

        public ParameterExpression ContextParameter { get; set; }

        public Expression Chain<TEntity>(
            Expression body,
            Expression<Func<InMemoryContext<TEntity>, InMemoryContext<TEntity>>> expression) where TEntity : class
        {
            return WithContext(body, expression);
        }

        public Expression WithContext<TEntity, T>(
            Expression body,
            Expression<Func<InMemoryContext<TEntity>, T>> expression) where TEntity : class
        {
            //var contextParameter = ContextParameter;
            //if (body != null)
            //{
            //    contextParameter = System.Linq.Expressions.Expression.Parameter(
            //        contextParameter.Type,
            //        contextParameter.Name);
            //}
            var updated = (LambdaExpression)new ParameterUpdateVisitor(expression.Parameters[0], ContextParameter).Visit(expression);
            if (body != null)
            {
                var methodCall = updated.Body as MethodCallExpression;
                //var updatedMethodCall = methodCall.Update(body, methodCall.Arguments);
                var updatedMethodCall = System.Linq.Expressions.Expression.Call(body, methodCall.Method, methodCall.Arguments);
                return updatedMethodCall;
            }
            return updated.Body;
            //return expression.Substitute<ParameterExpression>(expression.Parameters[0].Name, ContextParameter).Body;
        }

        //private ParameterExpression NewRootParameter(Type rootEntityType)
        //{
        //    return System.Linq.Expressions.Expression.Parameter(rootEntityType, $"entity{(RootEntities.Count == 0 ? "" : RootEntities.Count.ToString())}");
        //}

        //public List<ParameterExpression> RootEntities { get; } = new List<ParameterExpression>();
        //public ParameterExpression RootEntity => RootEntities.Last();
        public bool ConvertToLambda { get; set; } = true;

//        public DotNetOutput ParseLambda(IqlExpression expression, Type rootEntityType
//#if TypeScript
//            , EvaluateContext evaluateContext
//#endif
//        )
//        {
//            RootEntities.Add(NewRootParameter(rootEntityType));
//            var result = Parse(expression
//#if TypeScript
//                , evaluateContext
//#endif
//            );
//            RootEntities.RemoveAt(RootEntities.Count - 1);
//            return result;
//        }

        public override DotNetOutput ParseExpression(IqlExpression expression
#if TypeScript
                , EvaluateContext evaluateContext = null
#endif
        )
        {
            return new DotNetOutput(ParseAsExpression(expression
#if TypeScript
                , evaluateContext
#endif
            ),
                ResolveParentParameters());
        }

        private IEnumerable<ParameterExpression> ResolveParentParameters()
        {
            var parameters = new List<ParameterExpression>();
            var lambda = GetNearestAncestor<IqlLambdaExpression>();
            if (lambda.Parameters != null)
            {
                foreach (var parameter in lambda.Parameters)
                {
                    parameters.Add(GetParameterExpression(lambda, parameter));
                }
            }

            return parameters;
        }

        private Expression ParseAsExpression(IqlExpression expression
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        )
        {
            while (true)
            {
                if (expression == null)
                {
                    return null;
                }
                var finalExpression = expression as IqlFinalExpression<Expression>;
                if (finalExpression != null)
                {
                    return finalExpression.Value;
                }
                var aggregateExpression = expression as IqlAggregateExpression;
                if (aggregateExpression != null)
                {
                    throw new NotSupportedException($"{nameof(IqlAggregateExpression)} is not supported in DotNet");
//                    var aggregate = aggregateExpression;
//                    var str1 = "";
//                    aggregate.Expressions.ForEach(element =>
//                    {
//                        str1 += Parse(element
//#if TypeScript
//                        , evaluateContext
//#endif
//                        );
//                    });
//                    return str1;
                }
                var oldExpression = Expression;
                Expression = expression;
                var parser = Adapter.Registry.Resolve(Expression.GetType());
                if (parser == null)
                {
                    throw new Exception("No parser found for " + expression.GetType().Name);
                }
                var result = parser.ToQueryString(expression, this);
                // Reduce result
                var reducer = new IqlReducer(
#if TypeScript
                            evaluateContext
#endif
                );
                result = reducer.ReduceStaticContent(result);

                if (result != null)
                {
                    if (result is IqlFinalExpressionBase)
                    {
                        return (result as IqlFinalExpression<Expression>).Value;
                    }
                    expression = result;
                    continue;
                }
                Expression = oldExpression;
                return null;
            }
        }
    }
}