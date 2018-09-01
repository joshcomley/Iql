namespace Iql.JavaScript.JavaScriptExpressionToExpressionTree.Nodes
{
    public class ConditionalJavaScriptExpressionNode : JavaScriptExpressionNode
    {
        public ConditionalJavaScriptExpressionNode(
            JavaScriptExpressionNode test,
            JavaScriptExpressionNode consequent,
            JavaScriptExpressionNode alternate)
            : base(ExpressionType.Conditional)
        {
            Test = test;
            Consequent = consequent;
            Alternate = alternate;
        }

        public JavaScriptExpressionNode Test { get; set; }
        public JavaScriptExpressionNode Consequent { get; set; }
        public JavaScriptExpressionNode Alternate { get; set; }
    }
}