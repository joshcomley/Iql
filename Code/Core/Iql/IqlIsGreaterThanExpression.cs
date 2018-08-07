namespace Iql
{
    public class IqlIsGreaterThanExpression : IqlBinaryExpression
    {
        public IqlIsGreaterThanExpression(
            IqlExpression left,
            IqlExpression right) : base(IqlExpressionKind.IsGreaterThan, left, right)
        {
        }

        public IqlIsGreaterThanExpression() : this(null, null)
        {
        }

		public override IqlExpression Clone()
		{
			// #CloneStart

			var expression = new IqlIsGreaterThanExpression(null, null);
			expression.Left = Left?.Clone();
			expression.Right = Right?.Clone();
			expression.Key = Key;
			expression.Kind = Kind;
			expression.ReturnType = ReturnType;
			expression.Parent = Parent?.Clone();
			return expression;

			// #CloneEnd
		}
    }
}
