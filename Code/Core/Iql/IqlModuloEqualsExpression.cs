namespace Iql
{
    public class IqlModuloEqualsExpression : IqlBinaryExpression
    {
        public IqlModuloEqualsExpression(
            IqlExpression left,
            IqlExpression right) : base(IqlExpressionKind.ModuloEquals, left, right)
        {
        }

        public IqlModuloEqualsExpression() : this(null, null)
        {
        }

		public override IqlExpression Clone()
		{
			// #CloneStart

			var expression = new IqlModuloEqualsExpression(null, null);
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
