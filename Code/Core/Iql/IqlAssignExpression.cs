namespace Iql
{
    public class IqlAssignExpression : IqlBinaryExpression
    {
        public IqlAssignExpression(
            IqlExpression left,
            IqlExpression right) : base(IqlExpressionKind.Assign, left, right)
        {
        }

        public IqlAssignExpression() : this(null, null)
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
