namespace Iql
{
    public class IqlSubtractEqualsExpression : IqlBinaryExpression
    {
        public IqlSubtractEqualsExpression(
            IqlExpression left,
            IqlExpression right) : base(IqlExpressionKind.SubtractEquals, left, right)
        {
        }

        public IqlSubtractEqualsExpression() : this(null, null)
        {
        }

		public override IqlExpression Clone()
		{
			// #CloneStart

			var expression = new IqlSubtractEqualsExpression(null, null);
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
