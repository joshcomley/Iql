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

#if !TypeScript
        public IqlEnumValueExpression() : this(0, null)
        {
        }
#endif

		public override IqlExpression Clone()
		{
			// #CloneStart
			return null;
			// #CloneEnd
		}
    }
}
