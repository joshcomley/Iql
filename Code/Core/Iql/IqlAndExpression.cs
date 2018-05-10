namespace Iql
{
    public class IqlAndExpression : IqlBinaryExpression
    {
        public IqlAndExpression(
            IqlExpression left,
            IqlExpression right) :
            base(IqlExpressionKind.And, left, right)
        {
        }

        public IqlAndExpression() : this(null, null)
        {
        }

		public override IqlExpression Clone()
		{
			// #CloneStart

			var expression = new IqlAndExpression(null, null);
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
