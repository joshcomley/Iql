using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Iql.Extensions;

namespace Iql.DotNet.Parsers
{
    public class LambdaExpressionParser<T> : ExpressionParserBase<T, LambdaExpression>
    {
        public override bool CanHandleNode(Expression node)
        {
            return node.NodeType == ExpressionType.Lambda;
        }

        public override IqlExpression PerformParse(LambdaExpression node, ExpressionParserContext context)
        {
            return (IqlExpression) typeof(LambdaExpressionParser<T>)
                .GetMethods(BindingFlags.Instance | BindingFlags.NonPublic)
                .Single(m => m.Name == nameof(VisitLambda) && m.ContainsGenericParameters)
                .InvokeGeneric(this, new object[] {node, context}, node.Type);
        }

        protected IqlExpression VisitLambda<TLambda>(Expression<TLambda> lambda, ExpressionParserContext context)
        {
            return context.ToIqlExpression(lambda.Body);
        }
    }
}