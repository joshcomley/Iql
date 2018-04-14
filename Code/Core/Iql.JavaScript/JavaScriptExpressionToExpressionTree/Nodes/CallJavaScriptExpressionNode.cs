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
            Callee.Parent = this;
            for (var i = 0; i < args.Count; i++)
            {
                var arg = args[i];
                arg.Parent = this;
            }
        }

        public List<JavaScriptExpressionNode> Args { get; set; }
        public JavaScriptExpressionNode Callee { get; set; }
    }
}