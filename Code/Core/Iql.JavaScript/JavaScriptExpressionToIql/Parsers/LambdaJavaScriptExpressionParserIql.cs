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
            var iqlLambda = new IqlLambdaExpression();
            if (!string.IsNullOrWhiteSpace(expression.ParameterName))
            {
                iqlLambda.Parameters.Add(new IqlRootReferenceExpression(expression.ParameterName));
            }

            iqlLambda.Body = context.Parse(expression.Expression).Value;
            return new IqlParseResult(iqlLambda);
            ////if (expression.Parent is CallJavaScriptExpressionNode)
            ////{
            ////    var call = expression.Parent as CallJavaScriptExpressionNode;
            ////    if (call.Callee is MemberJavaScriptExpressionNode)
            ////    {
            ////        var member = call.Callee as MemberJavaScriptExpressionNode;
            ////        if (member.Property is PropertyIdentifierJavaScriptExpressionNode)
            ////        {
            ////            var property = member.Property as PropertyIdentifierJavaScriptExpressionNode;
            ////            if (property.Name == "filter")
            ////            {

            ////            }
            ////        }
            ////    }
            ////}
            //return new IqlParseResult(
            //    new IqlPropertyExpression());
        }
    }
}