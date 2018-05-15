using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Iql.Extensions;
using Iql.JavaScript.IqlToJavaScriptExpression;
using Iql.JavaScript.IqlToJavaScriptExpression.Parsers;
using Iql.JavaScript.JavaScriptExpressionToExpressionTree;
using Iql.JavaScript.JavaScriptExpressionToExpressionTree.Nodes;
using Iql.Parsing.Reduction;
using Iql.Queryable.Data.Context;
using Iql.Queryable.Data.EntityConfiguration;
using Iql.Queryable.Expressions;
using Iql.Queryable.Expressions.Conversion;
using Iql.Queryable.Expressions.QueryExpressions;
#if TypeScript
using Iql.Parsing;
#endif

namespace Iql.JavaScript.JavaScriptExpressionToIql
{
    public class JavaScriptExpressionConverter : ExpressionConverterBase
    {
        public override ExpressionResult<IqlExpression> ConvertQueryExpressionToIql<TEntity>
        (
            QueryExpression filter
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        )
        {
            ExpressionQueryExpressionBase expression;
            if (filter.CanFlatten())
            {
                expression = filter.Flatten<TEntity>();
            }
            else
            {
                expression = filter as ExpressionQueryExpressionBase;
            }
            //var whereExpression = filter.CanFlatten();

            var lambdaExpression = expression.GetExpression();
            return ConvertLambdaExpressionToIql<TEntity>(lambdaExpression
#if TypeScript
                , expression.EvaluateContext ?? filter.EvaluateContext ?? evaluateContext
#endif
                  );
        }

        public override ExpressionResult<IqlExpression> ConvertLambdaExpressionToIql<TEntity>(LambdaExpression lambdaExpression
#if TypeScript
                , EvaluateContext evaluateContext = null
#endif
            )
        {
            return ConvertJavaScriptStringToIql<TEntity>(lambdaExpression.ToString()
#if TypeScript
            , evaluateContext
#endif
            );
        }

        public string ConvertJavaScriptStringToJavaScriptString<TEntity>(string code
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        ) where TEntity : class
        {
            var iql = ConvertJavaScriptStringToIql<TEntity>(code);
            var javascript = ConvertIqlToExpressionStringByType(iql.Expression, typeof(TEntity)
#if TypeScript
            , evaluateContext
#endif
                );
            return javascript;
        }


        public ExpressionResult<IqlExpression> ConvertJavaScriptStringToIql<TEntity>(string code
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        ) where TEntity : class
        {
            var ctx = this;
            var body = JavaScriptCodeExtractor.ExtractBody(
                code);

            var instance =
                new JavaScriptExpressionNodeParseContext<TEntity>(
                    this,
#if TypeScript
                    evaluateContext,
#endif
                    null,
                    body.ParameterNames.FirstOrDefault() ?? "");
            var expressionResult = new ExpressionResult<IqlExpression>();
            var jsp = new JavaScriptExpressionStringToExpressionTreeParser(body.CleanedCode);
            var expressionTree = jsp.Parse();
            Func<TEntity, IqlExpression> expression2 = entityx =>
            {
                instance.RootEntity = entityx;
                var result = ctx.ParseJavaScriptExpressionTree(expressionTree, instance);
                return result.ResolveFinalResult() as IqlExpression;
            };

            expressionResult.Expression = expression2(null);
            
            // Now try to correct any property types
            var entityConfigurationBuilder = ResolvEntityConfigurationBuilder(typeof(TEntity));
            if (entityConfigurationBuilder != null)
            {
                var entityConfig = entityConfigurationBuilder.EntityType<TEntity>();
                var reducer = new IqlReducer();
                var flattened = reducer.Traverse(expressionResult.Expression);
                for (var i = 0; i < flattened.Length; i++)
                {
                    var expression = flattened[i];
                    if (expression is IqlPropertyExpression)
                    {
                        var propertyExpression = expression as IqlPropertyExpression;
                        if (propertyExpression.IsOrHasRootEntity())
                        {
                            var path = IqlPropertyPath.FromPropertyExpression(entityConfig, propertyExpression);
                            if (path != null && path.Property != null && path.Property.TypeDefinition != null)
                            {
                                propertyExpression.ReturnType = path.Property.TypeDefinition.Type.ToIqlType();
                            }
                        }
                    }
                }
            }

            var iqlRedudcer = new IqlReducer(
#if TypeScript
                    evaluateContext
#endif
                );
            expressionResult.Expression = iqlRedudcer.ReduceStaticContent(expressionResult.Expression);
            var lambdaExpression = new IqlLambdaExpression(IqlType.Unknown);
            lambdaExpression.Body = expressionResult.Expression;
            for (var i = 0; i < body.ParameterNames.Length; i++)
            {
                var parameter = body.ParameterNames[i];
                lambdaExpression.Parameters = lambdaExpression.Parameters ?? new List<IqlRootReferenceExpression>();
                lambdaExpression.Parameters.Add(new IqlRootReferenceExpression(parameter));
            }

            expressionResult.Expression = lambdaExpression;
            return expressionResult;
        }

        static JavaScriptExpressionConverter()
        {
            ConvertLambdaExpressionToIqlByTypeMethod = typeof(ExpressionConverterBase)
                .GetMethod(nameof(ConvertLambdaExpressionToIqlByType));
        }

        public static MethodInfo ConvertLambdaExpressionToIqlByTypeMethod { get; set; }

        public ExpressionResult<IqlExpression> ConvertLambdaExpressionToIqlByType(LambdaExpression filter, Type entityType
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        )
        {
            return (ExpressionResult<IqlExpression>)ConvertLambdaExpressionToIqlByTypeMethod
                .InvokeGeneric(this, new object[]
                {
                    filter
#if TypeScript
                    , evaluateContext
#endif
                }, entityType);
        }

        public ExpressionResult<IqlExpression> ConvertLambdaToIql<TEntity>(Expression<Func<TEntity, object>> filter
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        ) where TEntity : class
        {
            return ConvertLambdaExpressionToIql<TEntity>(filter
#if TypeScript
                , evaluateContext
#endif
            );
        }

        public virtual IExpressionParseResultBase ParseJavaScriptExpressionTree(
            JavaScriptExpressionNode expression,
            IExpressionParserInstance instance)
        {
            var oldExpression = instance.Expression;
            instance.Expression = expression;
            var parser = instance.Adapter.ResolveParser(expression);
            if (parser == null)
            {
                throw new Exception("No parser found for " + expression.GetType().Name);
            }
            var result = parser.Parse(instance, expression);
            instance.Expression = oldExpression;
            return result;
        }

        public string ConvertLambdaToJavaScript<TEntity>(Expression<Func<TEntity, object>> lambdaExpression
#if TypeScript
, EvaluateContext evaluateContext = null
#endif
        ) where TEntity : class
        {
            var iql = IqlConverter.Instance
                .ConvertLambdaToIql(lambdaExpression
#if TypeScript
            , null
#endif
                );
            var js = ConvertIqlToTypeScriptExpressionString(iql.Expression);
            return js;
        }

        public override LambdaExpression ConvertIqlToExpression<TEntity>(IqlExpression expression
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        )
        {
            var exp = ConvertIqlToJavaScript(expression, typeof(TEntity)
#if TypeScript
                , evaluateContext            
#endif
                ).Expression;
            var id = Guid.NewGuid().ToString();
            id = id.Replace("-", "");
            id = $"fn_{id}";
            exp = $"var {id} = {exp}";
            exp += $";{id}";
            var result = (LambdaExpression)Evaluator.Eval(exp);
            return result;
        }

        public override string ConvertIqlToExpressionStringByType(IqlExpression expression, Type rootEntityType
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        )
        {
            var javascript = ConvertIqlToJavaScript(expression, rootEntityType
#if TypeScript
            , evaluateContext
#endif
                );
            return javascript.AsFunction();
        }

        public string ConvertIqlToTypeScriptExpressionString(IqlExpression expression,
            Type rootEntityType = null
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        )
        {
            var javascript = ConvertIqlToJavaScript(expression, rootEntityType
#if TypeScript
            , evaluateContext
#endif
            );
            return javascript.AsFunction(true);
        }

        private JavaScriptExpression ConvertIqlToJavaScript(IqlExpression expression,
            Type rootEntityType = null
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        )
        {
            var adapter = new JavaScriptIqlExpressionAdapter();
            var parser = new JavaScriptIqlParserInstance(adapter, rootEntityType, this);
            parser.IsFilter = true;
            var javascriptExpression = parser.Parse(
                expression
#if TypeScript
                , evaluateContext
#endif
            );
            var javascript = new JavaScriptExpression("entity", javascriptExpression.ToCodeString());
            return javascript;
        }
    }
}