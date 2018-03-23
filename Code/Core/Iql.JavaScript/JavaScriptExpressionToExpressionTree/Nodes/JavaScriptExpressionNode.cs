namespace Iql.JavaScript.JavaScriptExpressionToExpressionTree.Nodes
{
    public class JavaScriptExpressionNode
    {
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