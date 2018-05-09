namespace Iql
{
    public class IqlStringTrimExpression : IqlReferenceExpression
    {
        public IqlStringTrimExpression(IqlReferenceExpression parent) : base(IqlExpressionKind.StringTrim,
            IqlType.String, parent)
        {
        }

        public IqlStringTrimExpression() : this(null)
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
