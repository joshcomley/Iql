namespace Iql
{
    public class IqlIsGreaterThanExpression : IqlBinaryExpression
    {
        public IqlIsGreaterThanExpression(
            IqlExpression left,
            IqlExpression right) : base(IqlExpressionKind.IsGreaterThan, left, right)
        {
        }

        public IqlIsGreaterThanExpression() : this(null, null)
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
