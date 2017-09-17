namespace Iql.JavaScript.JavaScriptExpressionToExpressionTree
{
    public class ConditionalJavaScriptExpressionNode : JavaScriptExpressionNode
    {
        public ConditionalJavaScriptExpressionNode(
            object test,
            object consequent,
            object alternate)
            : base(ExpressionType.Conditional)
        {
            Test = test;
            Consequent = consequent;
            Alternate = alternate;
        }

        public object Test { get; set; }
        public object Consequent { get; set; }
        public object Alternate { get; set; }
    }
}