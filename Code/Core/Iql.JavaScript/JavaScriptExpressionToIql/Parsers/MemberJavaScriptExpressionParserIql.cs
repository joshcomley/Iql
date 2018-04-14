using Iql.JavaScript.JavaScriptExpressionToExpressionTree;
using Iql.JavaScript.JavaScriptExpressionToExpressionTree.Nodes;

namespace Iql.JavaScript.JavaScriptExpressionToIql.Parsers
{
    public class MemberJavaScriptExpressionParserIql<T> : IqlQueryJavaScriptExpressionParser<T,
        MemberJavaScriptExpressionNode>
        where T : class
    {
        public override IqlParseResult Parse(
            JavaScriptExpressionNodeParseContext<T> context,
            MemberJavaScriptExpressionNode expression)
        {
            var owner = context.Parse(expression.Owner).Value;
            var property = context.ParseWith(expression.Property, owner).Value;
            property.Parent = owner;
            return new IqlParseResult(property);
        }
    }
}