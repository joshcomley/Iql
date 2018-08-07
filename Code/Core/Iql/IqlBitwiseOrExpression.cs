namespace Iql
{
    public class IqlBitwiseOrExpression : IqlBinaryExpression
    {
        public IqlBitwiseOrExpression(
            IqlExpression left,
            IqlExpression right) : base(IqlExpressionKind.BitwiseOr, left, right)
        {
        }

        public IqlBitwiseOrExpression() : this(null, null)
        {
        }

		public override IqlExpression Clone()
		{
			// #CloneStart

			var expression = new IqlBitwiseOrExpression(null, null);
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
