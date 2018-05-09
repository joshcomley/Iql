namespace Iql
{
    public class IqlSubtractEqualsExpression : IqlBinaryExpression
    {
        public IqlSubtractEqualsExpression(
            IqlExpression left,
            IqlExpression right) : base(IqlExpressionKind.SubtractEquals, left, right)
        {
        }

        public IqlSubtractEqualsExpression() : this(null, null)
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
