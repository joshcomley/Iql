namespace Iql
{
    public class IqlNotExpression : IqlExpression
    {
        public IqlNotExpression(
            IqlExpression expression) : base(IqlExpressionKind.Not)
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

		public override IqlExpression Clone()
		{
			// #CloneStart

			var expression = new IqlNotExpression(null);
			expression.Expression = Expression?.Clone();
			expression.Kind = Kind;
			expression.ReturnType = ReturnType;
			expression.Parent = Parent?.Clone();
			return expression;

			// #CloneEnd
		}
    }
}
