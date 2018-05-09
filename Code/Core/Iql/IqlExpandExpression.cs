namespace Iql
{
    public class IqlExpandExpression : IqlExpression
    {
#if !TypeScript
        public IqlExpandExpression() : this(null)
        {
            
        }
#endif
        public IqlExpandExpression(IqlExpression parent = null) : base(IqlExpressionKind.Expand, IqlType.Collection, parent)
        {
        }

        public IqlPropertyExpression NavigationProperty { get; set; }
        public IqlCollectitonQueryExpression Query { get; set; }
        public bool Count { get; set; }

		public override IqlExpression Clone()
		{
			// #CloneStart
			return null;
			// #CloneEnd
		}
    }
}
