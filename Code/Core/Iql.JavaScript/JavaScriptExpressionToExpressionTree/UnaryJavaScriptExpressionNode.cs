namespace Iql.JavaScript.JavaScriptExpressionToExpressionTree
{
    public class UnaryJavaScriptExpressionNode : JavaScriptExpressionNode
    {
        public JavaScriptExpressionNode Argument;
        public bool Prefix;

        public UnaryJavaScriptExpressionNode(
            OperatorType _operator,
            JavaScriptExpressionNode argument,
            bool prefix)
            : base(ExpressionType.Unary)
        {
            Operator = OperatorMap.OperatorTypes.ResolveValue(_operator);
            OperatorName = OperatorMap.OperatorTypes.ResolveName(Operator);
            Argument = argument;
            Prefix = prefix;
        }

        public string OperatorName { get; set; }

        public OperatorType Operator { get; set; }
    }
}