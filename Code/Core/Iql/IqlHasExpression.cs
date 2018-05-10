namespace Iql
{
    public class IqlHasExpression : IqlBinaryExpression
    {
        public IqlHasExpression(
            IqlExpression left,
            IqlExpression right) : base(IqlExpressionKind.Has, left, right)
        {
        }

        public IqlHasExpression() : this(null, null)
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