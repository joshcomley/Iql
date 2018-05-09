namespace Iql
{
    public class IqlDivideEqualsExpression : IqlBinaryExpression
    {
        public IqlDivideEqualsExpression(
            IqlExpression left,
            IqlExpression right) : base(IqlExpressionKind.DivideEquals, left, right)
        {
        }

        public IqlDivideEqualsExpression() : this(null, null)
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
