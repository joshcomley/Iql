namespace Iql
{
    public class IqlSubtractExpression : IqlBinaryExpression
    {
        public IqlSubtractExpression(
            IqlExpression left,
            IqlExpression right) : base(IqlExpressionKind.Subtract, left, right)
        {
        }

        public IqlSubtractExpression() : this(null, null)
        {
        }

		public override IqlExpression Clone()
		{
			// #CloneStart

			var expression = new IqlSubtractExpression(null, null);
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
