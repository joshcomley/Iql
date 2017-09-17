namespace Iql
{
    public class IqlNotExpression : IqlExpression
    {
        public IqlNotExpression(
            IqlExpression expression) : base(IqlExpressionType.Not)
        {
            Expression = expression;
        }

        public IqlNotExpression() : this(null)
        {
        }

        public IqlExpression Expression { get; set; }

        public override bool ContainsRootEntity()
        {
            return Expression.ContainsRootEntity();
        }
    }
}