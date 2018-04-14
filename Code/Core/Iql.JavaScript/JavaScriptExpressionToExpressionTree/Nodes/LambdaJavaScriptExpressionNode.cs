namespace Iql.JavaScript.JavaScriptExpressionToExpressionTree.Nodes
{
    public class LambdaJavaScriptExpressionNode : JavaScriptExpressionNode
    {
        public string ParameterName { get; }
        public JavaScriptExpressionNode Expression { get; }

        public LambdaJavaScriptExpressionNode(string parameterName, JavaScriptExpressionNode expression)
            : base(ExpressionType.Lambda)
        {
            ParameterName = parameterName;
            Expression = expression;
            Expression.Parent = this;
        }
    }
}