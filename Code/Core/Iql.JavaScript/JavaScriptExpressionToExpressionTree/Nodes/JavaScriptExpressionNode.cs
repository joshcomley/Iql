namespace Iql.JavaScript.JavaScriptExpressionToExpressionTree.Nodes
{
    public class JavaScriptExpressionNode
    {
        public JavaScriptExpressionNode Parent { get; set; }
        public JavaScriptExpressionNode(ExpressionType type)
        {
            Type = type;
        }

        public ExpressionType Type { get; }

        public T As<T>()
            where T : JavaScriptExpressionNode
        {
            return this as T;
        }
    }
}