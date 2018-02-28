namespace Iql.JavaScript.JavaScriptExpressionToExpressionTree.Nodes
{
    public class LiteralJavaScriptExpressionNode : JavaScriptExpressionNode
    {
        public LiteralJavaScriptExpressionNode(object value, object raw)
            : base(ExpressionType.Literal)
        {
            Value = value;
            Raw = raw;
        }

        public object Value { get; set; }
        public object Raw { get; set; }
    }
}