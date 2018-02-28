using Iql.JavaScript.JavaScriptExpressionToExpressionTree;
using Iql.JavaScript.JavaScriptExpressionToExpressionTree.Nodes;

namespace Iql.JavaScript.JavaScriptExpressionToIql.Parsers
{
    public class MemberJavaScriptExpressionParserIql<T> : IqlQueryJavaScriptExpressionParser<T,
        MemberJavaScriptExpressionNode>
        where T : class
    {
        public override IqlParseResult Parse(
            JavaScriptExpressionNodeParseContext<T, MemberJavaScriptExpressionNode>
                context)
        {
            var owner = context.Parse(context.Expression.Owner).Value;
            var property = context.ParseWith(context.Expression.Property, owner).Value;
            property.Parent = owner;
            return new IqlParseResult(property);
        }
    }
}