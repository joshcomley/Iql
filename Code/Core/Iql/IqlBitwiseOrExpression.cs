namespace Iql
{
    public class IqlBitwiseOrExpression : IqlBinaryExpression
    {
        public IqlBitwiseOrExpression(
            IqlExpression left,
            IqlExpression right) : base(IqlExpressionKind.BitwiseOr, left, right)
        {
        }

        public IqlBitwiseOrExpression() : this(null, null)
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
