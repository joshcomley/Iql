namespace Iql.JavaScript.JavaScriptExpressionToExpressionTree
{
    public class PropertyIdentifierJavaScriptExpressionNode : JavaScriptExpressionNode
    {
        public PropertyIdentifierJavaScriptExpressionNode(string name)
            : base(ExpressionType.PropertyIdentifier)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}