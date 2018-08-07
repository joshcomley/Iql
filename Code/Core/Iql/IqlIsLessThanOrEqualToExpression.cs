namespace Iql
{
    public class IqlIsLessThanOrEqualToExpression : IqlBinaryExpression
    {
        public IqlIsLessThanOrEqualToExpression(
            IqlExpression left,
            IqlExpression right) : base(IqlExpressionKind.IsLessThanOrEqualTo, left, right)
        {
        }

        public IqlIsLessThanOrEqualToExpression() : this(null, null)
        {
        }

		public override IqlExpression Clone()
		{
			// #CloneStart

			var expression = new IqlIsLessThanOrEqualToExpression(null, null);
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
