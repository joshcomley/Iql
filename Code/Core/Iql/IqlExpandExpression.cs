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

			var expression = new IqlExpandExpression();
			expression.NavigationProperty = (IqlPropertyExpression)NavigationProperty?.Clone();
			expression.Query = (IqlCollectitonQueryExpression)Query?.Clone();
			expression.Count = Count;
			expression.Kind = Kind;
			expression.ReturnType = ReturnType;
			expression.Parent = Parent?.Clone();
			return expression;

			// #CloneEnd
		}
    }
}
