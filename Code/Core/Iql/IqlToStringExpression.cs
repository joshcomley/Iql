namespace Iql
{
    public class IqlToStringExpression : IqlReferenceExpression
    {
        public IqlToStringExpression(IqlReferenceExpression parent)
            : base(IqlExpressionKind.ToString, IqlType.String, parent)
        {
        }

        public IqlToStringExpression() : this(null)
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
