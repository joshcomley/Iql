namespace Iql
{
    public class IqlAddExpression : IqlBinaryExpression
    {
        public IqlAddExpression(
            IqlExpression left,
            IqlExpression right) : base(IqlExpressionKind.Add, left, right)
        {
        }

        public IqlAddExpression() : this(null, null)
        {
        }

		public override IqlExpression Clone()
		{
			// #CloneStart

			var expression = new IqlAddExpression(null, null);
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
