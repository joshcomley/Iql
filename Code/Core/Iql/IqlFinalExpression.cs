namespace Iql
{
    public class IqlFinalExpression<TValue> : IqlFinalExpressionBase
    {
        public IqlFinalExpression(
            TValue value) : base(IqlExpressionKind.Final, null)
        {
            Value = value;
        }

        public IqlFinalExpression() : this(default(TValue))
        {
        }

        public TValue Value { get; set; }

		public override IqlExpression Clone()
		{
			// #CloneStart

			var expression = new IqlFinalExpression<TValue>(default(TValue));
			expression.Value = Value;
			expression.Kind = Kind;
			expression.ReturnType = ReturnType;
			expression.Parent = Parent?.Clone();
			return expression;

			// #CloneEnd
		}
    }
}
