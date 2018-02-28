using System.Collections.Generic;

namespace Iql.JavaScript.JavaScriptExpressionToExpressionTree.Nodes
{
    public class CallJavaScriptExpressionNode : JavaScriptExpressionNode
    {
        public CallJavaScriptExpressionNode(List<JavaScriptExpressionNode> args, JavaScriptExpressionNode callee)
            : base(ExpressionType.Call)
        {
            Args = args;
            Callee = callee;
        }

        public List<JavaScriptExpressionNode> Args { get; set; }
        public JavaScriptExpressionNode Callee { get; set; }
    }
}