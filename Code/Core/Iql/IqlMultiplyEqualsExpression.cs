namespace Iql
{
    public class IqlMultiplyEqualsExpression : IqlBinaryExpression
    {
        public IqlMultiplyEqualsExpression(
            IqlExpression left,
            IqlExpression right) : base(IqlExpressionKind.MultiplyEquals, left, right)
        {
        }

        public IqlMultiplyEqualsExpression() : this(null, null)
        {
        }

		public override IqlExpression Clone()
		{
			// #CloneStart

			var expression = new IqlMultiplyEqualsExpression(null, null);
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
