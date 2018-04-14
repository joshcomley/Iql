namespace Iql.JavaScript.JavaScriptExpressionToExpressionTree.Nodes
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
            Owner.Parent = this;
            Property.Parent = Owner;
        }

        public bool Computed { get; set; }
        public JavaScriptExpressionNode Owner { get; set; }
        public JavaScriptExpressionNode Property { get; set; }
    }
}