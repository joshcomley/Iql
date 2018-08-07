namespace Iql
{
    public class IqlMultiplyExpression : IqlBinaryExpression
    {
        public IqlMultiplyExpression(
            IqlExpression left,
            IqlExpression right) : base(IqlExpressionKind.Multiply, left, right)
        {
        }

        public IqlMultiplyExpression() : this(null, null)
        {
        }

		public override IqlExpression Clone()
		{
			// #CloneStart

			var expression = new IqlMultiplyExpression(null, null);
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
