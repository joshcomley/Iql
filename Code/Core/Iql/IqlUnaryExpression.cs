namespace Iql
{
    public abstract class IqlUnaryExpression : IqlExpression
    {
        protected IqlUnaryExpression(
            object value, IqlExpressionType type) : base(type)
        {
            Value = value;
        }

        public object Value { get; set; }
    }
}