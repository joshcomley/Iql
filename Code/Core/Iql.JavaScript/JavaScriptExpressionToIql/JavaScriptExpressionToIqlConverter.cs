using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Iql.Extensions;
using Iql.JavaScript.JavaScriptExpressionToExpressionTree;
using Iql.JavaScript.JavaScriptExpressionToExpressionTree.Nodes;
using Iql.Parsing;
using Iql.Queryable.Expressions;
using Iql.Queryable.Expressions.Conversion;
using Iql.Queryable.Expressions.QueryExpressions;

namespace Iql.JavaScript.JavaScriptExpressionToIql
{
    public class JavaScriptExpressionToIqlConverter : IExpressionToIqlConverter
    {
        public JavaScriptExpressionToIqlConverter(Func<string, object> evaluate = null)
        {
            Evaluate = evaluate;
        }

        public virtual Func<string, object> Evaluate { get; set; }

        public virtual ExpressionResult<IqlExpression> ConvertQueryExpressionToIql<TEntity>
        (
            QueryExpression filter
        )
            where TEntity : class
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
                , expression.EvaluateContext ?? filter.EvaluateContext
#endif
                  );
        }

        public ExpressionResult<IqlExpression> ConvertLambdaExpressionToIql<TEntity>(LambdaExpression lambdaExpression
#if TypeScript
                , EvaluateContext evaluateContext
#endif
            ) where TEntity : class
        {
            var ctx = this;
            var code = lambdaExpression.ToString();
            var body = JavaScriptCodeExtractor.ExtractBody(
                code);

            var instance =
                new JavaScriptExpressionNodeParseContext<TEntity, JavaScriptExpressionNode>(
                    this,
#if TypeScript
                    evaluateContext,
#endif
                    null,
                    body.ParameterNames.FirstOrDefault() ?? "");
            var expressionResult = new ExpressionResult<IqlExpression>();
            var jsp = new JavaScriptExpressionStringToExpressionTreeParser(body.CleanedCode);
            var expressionTree = jsp.Parse();
            Func<TEntity, IqlExpression> expression2 = entity =>
            {
                instance.RootEntity = entity;
                var result = ctx.ParseJavaScriptExpressionTree(expressionTree, instance);
                return result.ResolveFinalResult() as IqlExpression;
            };
            expressionResult.Expression = expression2(null);
            return expressionResult;
        }

        static JavaScriptExpressionToIqlConverter()
        {
            ConvertLambdaExpressionToIqlByTypeMethod = typeof(ExpressionConverterBase)
                .GetMethod(nameof(ConvertLambdaExpressionToIqlByType));
        }

        public static MethodInfo ConvertLambdaExpressionToIqlByTypeMethod { get; set; }

        public ExpressionResult<IqlExpression> ConvertLambdaExpressionToIqlByType(LambdaExpression filter, Type entityType
#if TypeScript
            , EvaluateContext evaluateContext
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
            , EvaluateContext evaluateContext
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
            var result = parser.Parse(instance);
            instance.Expression = oldExpression;
            return result;
        }
    }
}