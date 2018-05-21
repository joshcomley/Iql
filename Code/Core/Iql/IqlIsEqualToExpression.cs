namespace Iql
{
    public class IqlIsEqualToExpression : IqlBinaryExpression
    {
        public IqlIsEqualToExpression(
            IqlExpression left = null,
            IqlExpression right = null) : base(IqlExpressionKind.IsEqualTo, left, right)
        {
        }

        public IqlIsEqualToExpression() : this(null, null)
        {
        }

		public override IqlExpression Clone()
		{
			// #CloneStart

			var expression = new IqlIsEqualToExpression(null, null);
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
