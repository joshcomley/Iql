namespace Iql
{
    public class IqlStringTrimExpression : IqlReferenceExpression
    {
        public IqlStringTrimExpression(IqlReferenceExpression parent) : base(IqlExpressionKind.StringTrim,
            IqlType.String, parent)
        {
        }

        public IqlStringTrimExpression() : this(null)
        {
        }

		public override IqlExpression Clone()
		{
			// #CloneStart

			var expression = new IqlStringTrimExpression(null);
			expression.Kind = Kind;
			expression.ReturnType = ReturnType;
			expression.Parent = Parent?.Clone();
			return expression;

			// #CloneEnd
		}
    }
}
