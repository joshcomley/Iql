using System.Collections.Generic;

namespace Iql.JavaScript.JavaScriptExpressionToExpressionTree.Nodes
{
    public class CompoundJavaScriptExpressionNode : JavaScriptExpressionNode
    {
        public CompoundJavaScriptExpressionNode(List<JavaScriptExpressionNode> body) : base(ExpressionType.Compound)
        {
            Body = body;
            for (var i = 0; i < body.Count; i++)
            {
                var item = body[i];
                item.Parent = this;
            }
        }

        public List<JavaScriptExpressionNode> Body { get; set; }
    }
}