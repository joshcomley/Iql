using System.Linq;
using System.Linq.Expressions;

namespace Iql.DotNet.DotNetExpressionToIql.Parsers
{
    public class ParameterDotNetExpressionParser<T> : DotNetExpressionParserBase<T, ParameterExpression>
    {
        public override bool CanHandleNode(Expression node)
        {
            return node.NodeType == ExpressionType.Parameter;
        }

        public override IqlExpression PerformParse(ParameterExpression node, DotNetExpressionParserContext context)
        {
            if (node.Name == context.RootVariableNames.Last())
            {
                return new IqlRootReferenceExpression(node.Name, null, node.Type);
            }
            var expression = new IqlVariableExpression(node.Name, null, node.Type);
            context.VariableValues.Add(expression, node);
            return expression;
        }
    }
}