namespace Iql
{
    public class IqlOrExpression : IqlBinaryExpression
    {
        public IqlOrExpression(
            IqlExpression left,
            IqlExpression right) : base(IqlExpressionKind.Or, left, right)
        {
        }

        public IqlOrExpression() : this(null, null)
        {
        }

		public override IqlExpression Clone()
		{
			// #CloneStart

			var expression = new IqlOrExpression(null, null);
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
