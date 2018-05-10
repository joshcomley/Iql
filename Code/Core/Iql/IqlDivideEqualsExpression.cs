namespace Iql
{
    public class IqlDivideEqualsExpression : IqlBinaryExpression
    {
        public IqlDivideEqualsExpression(
            IqlExpression left,
            IqlExpression right) : base(IqlExpressionKind.DivideEquals, left, right)
        {
        }

        public IqlDivideEqualsExpression() : this(null, null)
        {
        }

		public override IqlExpression Clone()
		{
			// #CloneStart

			var expression = new IqlDivideEqualsExpression(null, null);
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
