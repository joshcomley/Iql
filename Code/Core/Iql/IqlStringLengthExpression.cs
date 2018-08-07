namespace Iql
{
    public class IqlStringLengthExpression : IqlReferenceExpression
    {
        public IqlStringLengthExpression(IqlReferenceExpression parent) : base(IqlExpressionKind.StringLength, IqlType.Integer,
            parent)
        {
        }

        public IqlStringLengthExpression() : this(null)
        {
        }

		public override IqlExpression Clone()
		{
			// #CloneStart

			var expression = new IqlStringLengthExpression(null);
			expression.Key = Key;
			expression.Kind = Kind;
			expression.ReturnType = ReturnType;
			expression.Parent = Parent?.Clone();
			return expression;

			// #CloneEnd
		}
    }
}
