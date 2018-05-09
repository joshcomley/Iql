namespace Iql
{
    public class IqlIsLessThanExpression : IqlBinaryExpression
    {
        public IqlIsLessThanExpression(
            IqlExpression left,
            IqlExpression right) : base(IqlExpressionKind.IsLessThan, left, right)
        {
        }

        public IqlIsLessThanExpression() : this(null, null)
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
