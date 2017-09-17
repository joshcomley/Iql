namespace Iql.JavaScript.JavaScriptExpressionToExpressionTree
{
    public class BinaryJavaScriptExpressionNode : JavaScriptExpressionNode
    {
        // Note that `a && b` and `a || b` are *logical* expressions, not binary expressions
        public BinaryJavaScriptExpressionNode(OperatorType _operator, JavaScriptExpressionNode left,
            JavaScriptExpressionNode right)
            : base(_operator == OperatorType.Or || _operator == OperatorType.And
                ? ExpressionType.Logical
                : ExpressionType.Binary)
        {
            Operator = _operator; //OperatorMap.OperatorTypes.ResolveValue(_operator);
            Left = left;
            Right = right;
        }

        public JavaScriptExpressionNode Left { get; set; }
        public JavaScriptExpressionNode Right { get; set; }
        public OperatorType Operator { get; set; }
    }
}