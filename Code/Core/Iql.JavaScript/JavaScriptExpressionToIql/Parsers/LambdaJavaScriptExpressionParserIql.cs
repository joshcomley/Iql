using System;
using Iql.JavaScript.JavaScriptExpressionToExpressionTree.Nodes;

namespace Iql.JavaScript.JavaScriptExpressionToIql.Parsers
{
    public class LambdaJavaScriptExpressionParserIql<T> : IqlQueryJavaScriptExpressionParser<T,
        LambdaJavaScriptExpressionNode>
        where T : class
    {
        public override IqlParseResult Parse(
            JavaScriptExpressionNodeParseContext<T> context,
            LambdaJavaScriptExpressionNode expression)
        {
            //if (expression.Parent is CallJavaScriptExpressionNode)
            //{
            //    var call = expression.Parent as CallJavaScriptExpressionNode;
            //    if (call.Callee is MemberJavaScriptExpressionNode)
            //    {
            //        var member = call.Callee as MemberJavaScriptExpressionNode;
            //        if (member.Property is PropertyIdentifierJavaScriptExpressionNode)
            //        {
            //            var property = member.Property as PropertyIdentifierJavaScriptExpressionNode;
            //            if (property.Name == "filter")
            //            {

            //            }
            //        }
            //    }
            //}
            return new IqlParseResult(
                new IqlPropertyExpression());
        }
    }
}