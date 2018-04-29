namespace Iql
{
    public class IqlParenthesisExpression : IqlExpression
    {
        public IqlParenthesisExpression(
            IqlExpression expression) : base(IqlExpressionKind.Parenthesis)
        {
            Expression = expression;
        }

        public IqlParenthesisExpression() : this(null)
        {
        }

        public IqlExpression Expression { get; set; }

        public override bool ContainsRootEntity()
        {
            return Expression.ContainsRootEntity();
        }
    }
}