namespace Iql
{
    public class IqlFinalExpression<TValue> : IqlFinalExpressionBase
    {
        public IqlFinalExpression(
            TValue value) : base(IqlExpressionType.Final, null)
        {
            Value = value;
        }

        public IqlFinalExpression() : this(default(TValue))
        {
        }

        public TValue Value { get; set; }
    }
}