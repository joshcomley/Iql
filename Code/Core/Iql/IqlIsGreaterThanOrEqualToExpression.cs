namespace Iql
{
    public class IqlIsGreaterThanOrEqualToExpression : IqlBinaryExpression
    {
        public IqlIsGreaterThanOrEqualToExpression(
            IqlExpression left,
            IqlExpression right) : base(IqlExpressionKind.IsGreaterThanOrEqualTo, left, right)
        {
        }

        public IqlIsGreaterThanOrEqualToExpression() : this(null, null)
        {
        }

		public override IqlExpression Clone()
		{
			// #CloneStart

			var expression = new IqlIsGreaterThanOrEqualToExpression(null, null);
			expression.Left = Left?.Clone();
			expression.Right = Right?.Clone();
			expression.Kind = Kind;
			expression.ReturnType = ReturnType;
			expression.Parent = Parent?.Clone();
			return expression;

			// #CloneEnd
		}
    }
}
