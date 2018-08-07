namespace Iql
{
    public class IqlModuloExpression : IqlBinaryExpression
    {
        public IqlModuloExpression(
            IqlExpression left,
            IqlExpression right) : base(IqlExpressionKind.Modulo, left, right)
        {
        }

        public IqlModuloExpression() : this(null, null)
        {
        }

		public override IqlExpression Clone()
		{
			// #CloneStart

			var expression = new IqlModuloExpression(null, null);
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
