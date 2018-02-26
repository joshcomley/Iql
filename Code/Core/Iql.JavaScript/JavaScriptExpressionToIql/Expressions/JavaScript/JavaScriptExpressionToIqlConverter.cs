using System;
using System.Linq;
using Iql.JavaScript.JavaScriptExpressionToExpressionTree;
using Iql.Queryable.Expressions;
using Iql.Queryable.Expressions.QueryExpressions;

namespace Iql.JavaScript.JavaScriptExpressionToIql.Expressions.JavaScript
{
    public class JavaScriptExpressionToIqlConverter : IExpressionToIqlConverter
    {
        public JavaScriptExpressionToIqlConverter(Func<string, object> evaluate = null)
        {
            Evaluate = evaluate;
        }

        public virtual Func<string, object> Evaluate { get; set; }

        public virtual ExpressionResult<IqlExpression> ConvertExpressionToIql<TEntity>
        (
            QueryExpression filter
        )
            where TEntity : class
        {
            var ctx = this;
            //if()
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

            var code = expression.GetExpression().ToString();
            var body = JavaScriptCodeExtractor.ExtractBody(
                code);

            var instance =
                new JavaScriptExpressionNodeParseContext<TEntity, JavaScriptExpressionNode>(
                    this,
#if TypeScript
                    expression.EvaluateContext ?? filter.EvaluateContext, 
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