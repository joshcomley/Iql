namespace Iql
{
    public abstract class IqlUnaryExpression : IqlExpression
    {
        protected IqlUnaryExpression(
            object value, IqlExpressionKind kind) : base(kind)
        {
            Value = value;
        }

        public object Value { get; set; }
    }
}