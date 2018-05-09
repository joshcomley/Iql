namespace Iql
{
    public class IqlLiteralExpression : IqlLiteralExpressionBase<object>
    {
        public IqlLiteralExpression(
            object value, IqlType type = IqlType.Unknown) : base(IqlExpressionKind.Literal,
            type)
        {
            Value = value;
        }

        public IqlLiteralExpression() : this(null)
        {
        }

		public override IqlExpression Clone()
		{
			// #CloneStart
			return null;
			// #CloneEnd
		}
    }
}
