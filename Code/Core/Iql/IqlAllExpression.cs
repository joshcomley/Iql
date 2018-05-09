namespace Iql
{
    public class IqlAllExpression : IqlAnyAllExpression
    {
        public IqlAllExpression(
            string rootVariableName,
            IqlReferenceExpression parent,
            IqlExpression expression) : base(rootVariableName, IqlExpressionKind.All, parent, expression)
        {
        }

        public IqlAllExpression() : this(null, null, null)
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
