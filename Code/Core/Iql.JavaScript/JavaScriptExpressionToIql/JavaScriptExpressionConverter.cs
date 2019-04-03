using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;
using Iql.Conversion;
using Iql.Data.IqlToIql;
using Iql.Data.Types;
using Iql.Entities;
using Iql.Extensions;
using Iql.JavaScript.IqlToJavaScriptExpression;
using Iql.JavaScript.IqlToJavaScriptExpression.Parsers;
using Iql.JavaScript.JavaScriptExpressionToExpressionTree;
using Iql.JavaScript.JavaScriptExpressionToExpressionTree.Nodes;
using Iql.Parsing.Expressions;
using Iql.Parsing.Reduction;
using Iql.Parsing.Types;

#if TypeScript
using Iql.Parsing;
#endif

namespace Iql.JavaScript.JavaScriptExpressionToIql
{
    public class JavaScriptExpressionConverter : ExpressionConverterBase
    {
        protected override ExpressionResult<IqlExpression> ConvertLambdaExpressionToIqlInternal<TEntity>(LambdaExpression lambda
            , ITypeResolver typeResolver
#if TypeScript
                , EvaluateContext evaluateContext = null
#endif
            )
        {
            return ConvertJavaScriptStringToIql<TEntity>(lambda.ToString()
                , typeResolver
#if TypeScript
            , evaluateContext
#endif
            );
        }

        public string ConvertJavaScriptStringToJavaScriptString<TEntity>(string code
            , ITypeResolver typeResolver
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        ) where TEntity : class
        {
            var iql = ConvertJavaScriptStringToIql<TEntity>(code, typeResolver);
            var javascript = ConvertIqlToExpressionStringByType(iql.Expression, typeResolver, typeof(TEntity)
#if TypeScript
            , evaluateContext
#endif
                );
            return javascript;
        }


        public ExpressionResult<IqlExpression> ConvertJavaScriptStringToIql<TEntity>(string code
            , ITypeResolver typeResolver
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
                    typeResolver,
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
            var entityConfig = typeResolver.FindType<TEntity>();
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
                        var path = IqlPropertyPath.FromPropertyExpression(
                            typeResolver,
                            entityConfig, 
                            propertyExpression);
                        if (path != null && path.Property != null)
                        {
                            propertyExpression.ReturnType = path.Property.ToIqlType();
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
            ConvertLambdaToIqlMethod = typeof(ExpressionConverterBase)
                .GetMethod(nameof(ConvertLambdaToIql));
        }

        public static MethodInfo ConvertLambdaToIqlMethod { get; set; }

//        public ExpressionResult<IqlExpression> ConvertLambdaExpressionToIqlByType(LambdaExpression filter, Type entityType
//#if TypeScript
//            , EvaluateContext evaluateContext = null
//#endif
//        )
//        {
//            return (ExpressionResult<IqlExpression>)ConvertLambdaToIqlMethod
//                .InvokeGeneric(this, new object[]
//                {
//                    filter
//#if TypeScript
//                    , evaluateContext
//#endif
//                }, entityType);
//        }

//        public ExpressionResult<IqlExpression> ConvertLambdaToIql<TEntity>(Expression<Func<TEntity, object>> filter
//#if TypeScript
//            , EvaluateContext evaluateContext = null
//#endif
//        ) where TEntity : class
//        {
//            return ConvertLambdaExpressionToIql<TEntity>(filter
//#if TypeScript
//                , evaluateContext
//#endif
//            );
//        }

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
            , ITypeResolver typeResolver
#if TypeScript
, EvaluateContext evaluateContext = null
#endif
        ) where TEntity : class
        {
            var iql = IqlConverter.Instance
                .ConvertLambdaToIql(lambdaExpression
                    , typeResolver
#if TypeScript
            , null
#endif
                );
            var js = ConvertIqlToTypeScriptExpressionString(iql.Expression, typeResolver, typeof(TEntity));
            return js;
        }

        public override LambdaExpression ConvertIqlToExpression<TEntity>(IqlExpression expression
            , ITypeResolver typeResolver
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        )
        {
            return ConvertIql(expression, typeResolver, typeof(TEntity)
#if TypeScript
            , evaluateContext
#endif
            );
        }

        public LambdaExpression ConvertIql(IqlExpression expression
            , ITypeResolver typeResolver
            , Type type = null
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        )
        {
            var exp = ConvertIqlToJavaScript(expression, typeResolver, type
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

        public override LambdaExpression ConvertIqlToLambdaExpression(IqlExpression expression
            , ITypeResolver typeResolver
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        )
        {
            var result = ConvertIql(expression, typeResolver, null
#if TypeScript
            , evaluateContext
#endif
            );
            if (!(result is LambdaExpression))
            {
                return (Expression<Func<object>>)(() => result);
            }
            return result;
        }

        public override string ConvertIqlToExpressionStringByType(IqlExpression expression
            , ITypeResolver typeResolver
            , Type rootEntityType
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        )
        {
            var javascript = ConvertIqlToJavaScript(expression, typeResolver, rootEntityType
#if TypeScript
            , evaluateContext
#endif
                );
            return javascript.AsFunction();
        }

        public string ConvertIqlToTypeScriptExpressionString(IqlExpression expression
            , ITypeResolver typeResolver
            , Type rootEntityType = null
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        )
        {
            var javascript = ConvertIqlToJavaScript(expression, typeResolver, rootEntityType
#if TypeScript
            , evaluateContext
#endif
            );
            return javascript.AsFunction(true);
        }

        private JavaScriptExpression ConvertIqlToJavaScript(IqlExpression expression
            , ITypeResolver typeResolver
            , Type rootEntityType = null
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
        )
        {
            var adapter = new JavaScriptIqlExpressionAdapter();
            var parser = new JavaScriptIqlParserContext(adapter, rootEntityType, this, typeResolver);
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