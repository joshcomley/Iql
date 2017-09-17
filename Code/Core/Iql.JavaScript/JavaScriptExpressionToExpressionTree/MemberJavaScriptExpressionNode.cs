namespace Iql.JavaScript.JavaScriptExpressionToExpressionTree
{
    public class MemberJavaScriptExpressionNode : JavaScriptExpressionNode
    {
        public MemberJavaScriptExpressionNode(
            bool computed,
            JavaScriptExpressionNode owner,
            JavaScriptExpressionNode property)
            : base(ExpressionType.Member)
        {
            Computed = computed;
            Owner = owner;
            Property = property;
        }

        public bool Computed { get; set; }
        public JavaScriptExpressionNode Owner { get; set; }
        public JavaScriptExpressionNode Property { get; set; }
    }
}