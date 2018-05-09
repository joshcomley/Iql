namespace Iql
{
    public class IqlIsLessThanOrEqualToExpression : IqlBinaryExpression
    {
        public IqlIsLessThanOrEqualToExpression(
            IqlExpression left,
            IqlExpression right) : base(IqlExpressionKind.IsLessThanOrEqualTo, left, right)
        {
        }

        public IqlIsLessThanOrEqualToExpression() : this(null, null)
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
