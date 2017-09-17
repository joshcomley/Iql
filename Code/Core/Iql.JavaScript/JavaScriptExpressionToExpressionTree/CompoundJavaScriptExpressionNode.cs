using System.Collections.Generic;

namespace Iql.JavaScript.JavaScriptExpressionToExpressionTree
{
    public class CompoundJavaScriptExpressionNode : JavaScriptExpressionNode
    {
        public CompoundJavaScriptExpressionNode(List<JavaScriptExpressionNode> body) : base(ExpressionType.Compound)
        {
            Body = body;
        }

        public List<JavaScriptExpressionNode> Body { get; set; }
    }
}