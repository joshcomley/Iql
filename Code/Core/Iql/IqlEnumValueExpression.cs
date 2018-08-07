namespace Iql
{
    public class IqlEnumValueExpression : IqlLiteralExpressionBase<long>
    {
        public string Name { get; set; }

        public IqlEnumValueExpression(long value, string name) : base(value, IqlType.EnumValue, IqlExpressionKind.EnumValue)
        {
            Kind = IqlExpressionKind.EnumValue;
            Name = name;
            Value = value;
        }

        public IqlEnumValueExpression() : this(0, null)
        {
        }

		public override IqlExpression Clone()
		{
			// #CloneStart

			var expression = new IqlEnumValueExpression(0, null);
			expression.Name = Name;
			expression.Value = Value;
			expression.InferredReturnType = InferredReturnType;
			expression.Key = Key;
			expression.Kind = Kind;
			expression.ReturnType = ReturnType;
			expression.Parent = Parent?.Clone();
			return expression;

			// #CloneEnd
		}
    }
}
