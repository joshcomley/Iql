namespace Iql
{
    public class IqlMultiplyExpression : IqlBinaryExpression
    {
        public IqlMultiplyExpression(
            IqlExpression left,
            IqlExpression right) : base(IqlExpressionKind.Multiply, left, right)
        {
        }

        public IqlMultiplyExpression() : this(null, null)
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
