namespace Iql
{
    public class IqlAddEqualsExpression : IqlBinaryExpression
    {
        public IqlAddEqualsExpression(
            IqlExpression left,
            IqlExpression right) : base(IqlExpressionKind.AddEquals, left, right)
        {
        }

        public IqlAddEqualsExpression() : this(null, null)
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
