using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Iql.Extensions;

namespace Iql.DotNet.DotNetExpressionToIql.Parsers
{
    public class LambdaDotNetExpressionParser<T> : DotNetExpressionParserBase<T, LambdaExpression>
    {
        public override bool CanHandleNode(Expression node)
        {
            return node.NodeType == ExpressionType.Lambda;
        }

        public override IqlExpression PerformParse(LambdaExpression node, DotNetExpressionParserContext context)
        {
            return (IqlExpression) typeof(LambdaDotNetExpressionParser<T>)
                .GetMethods(BindingFlags.Instance | BindingFlags.NonPublic)
                .Single(m => m.Name == nameof(VisitLambda) && m.ContainsGenericParameters)
                .InvokeGeneric(this, new object[] {node, context}, node.Type);
        }

        protected IqlExpression VisitLambda<TLambda>(Expression<TLambda> lambda, DotNetExpressionParserContext context)
        {
            return context.ToIqlExpression(lambda.Body);
        }
    }
}