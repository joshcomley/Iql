namespace Iql
{
    public class IqlIsGreaterThanOrEqualToExpression : IqlBinaryExpression
    {
        public IqlIsGreaterThanOrEqualToExpression(
            IqlExpression left,
            IqlExpression right) : base(IqlExpressionKind.IsGreaterThanOrEqualTo, left, right)
        {
        }

        public IqlIsGreaterThanOrEqualToExpression() : this(null, null)
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
