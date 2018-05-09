namespace Iql
{
    public class IqlAddExpression : IqlBinaryExpression
    {
        public IqlAddExpression(
            IqlExpression left,
            IqlExpression right) : base(IqlExpressionKind.Add, left, right)
        {
        }

        public IqlAddExpression() : this(null, null)
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
