namespace Iql
{
    public class IqlStringEndsWithExpression : IqlParentValueExpression
    {
        public IqlStringEndsWithExpression(IqlReferenceExpression parent, IqlReferenceExpression value)
            : base(parent, value, IqlExpressionKind.StringEndsWith, IqlType.Boolean)
        {
        }

        public IqlStringEndsWithExpression() : this(null, null)
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
