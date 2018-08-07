namespace Iql
{
    public class IqlIsLessThanExpression : IqlBinaryExpression
    {
        public IqlIsLessThanExpression(
            IqlExpression left,
            IqlExpression right) : base(IqlExpressionKind.IsLessThan, left, right)
        {
        }

        public IqlIsLessThanExpression() : this(null, null)
        {
        }

		public override IqlExpression Clone()
		{
			// #CloneStart

			var expression = new IqlIsLessThanExpression(null, null);
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
