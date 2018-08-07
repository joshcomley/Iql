namespace Iql
{
    public class IqlAddEqualsExpression : IqlBinaryExpression
    {
        public IqlAddEqualsExpression(
            IqlExpression left,
            IqlExpression right) : base(IqlExpressionKind.AddEquals, left, right)
        {
        }

        public IqlAddEqualsExpression() : this(null, null)
        {
        }

		public override IqlExpression Clone()
		{
			// #CloneStart

			var expression = new IqlAddEqualsExpression(null, null);
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
