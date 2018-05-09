namespace Iql
{
    public class IqlStringLengthExpression : IqlReferenceExpression
    {
        public IqlStringLengthExpression(IqlReferenceExpression parent) : base(IqlExpressionKind.StringLength, IqlType.Integer,
            parent)
        {
        }

        public IqlStringLengthExpression() : this(null)
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
