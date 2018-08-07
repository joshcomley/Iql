namespace Iql
{
    public class IqlHasExpression : IqlBinaryExpression
    {
        public IqlHasExpression(
            IqlExpression left,
            IqlExpression right) : base(IqlExpressionKind.Has, left, right)
        {
        }

        public IqlHasExpression() : this(null, null)
        {
        }

		public override IqlExpression Clone()
		{
			// #CloneStart

			var expression = new IqlHasExpression(null, null);
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
