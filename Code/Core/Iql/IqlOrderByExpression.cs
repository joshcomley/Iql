namespace Iql
{
    public class IqlOrderByExpression : IqlExpression
    {
        public IqlOrderByExpression(IqlExpression orderExpression, IqlExpression parent = null) : base(IqlExpressionKind.OrderBy, IqlType.Collection, parent)
        {
            OrderExpression = orderExpression;
        }

        public IqlOrderByExpression() : this(null)
        {

        }

        public IqlExpression OrderExpression { get; set; }
        public bool Descending { get; set; }

		public override IqlExpression Clone()
		{
			// #CloneStart

			var expression = new IqlOrderByExpression(null);
			expression.OrderExpression = OrderExpression?.Clone();
			expression.Descending = Descending;
			expression.Key = Key;
			expression.Kind = Kind;
			expression.ReturnType = ReturnType;
			expression.Parent = Parent?.Clone();
			return expression;

			// #CloneEnd
		}
    }
}
