namespace Iql
{
    public class IqlUnarySubtractExpression : IqlUnaryExpression
    {
        public IqlUnarySubtractExpression(object value) : base(value, IqlExpressionKind.UnarySubtract)
        {
        }

        public IqlUnarySubtractExpression() : this(null)
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
