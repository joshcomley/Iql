namespace Iql.JavaScript.JavaScriptExpressionToExpressionTree
{
    public class ArrayJavaScriptExpressionNode : JavaScriptExpressionNode
    {
        public ArrayJavaScriptExpressionNode(object elements)
            : base(ExpressionType.Array)
        {
            Elements = elements;
        }

        public object Elements { get; set; }
    }
}