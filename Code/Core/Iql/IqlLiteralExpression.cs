namespace Iql
{
    public class IqlLiteralExpression : IqlLiteralExpressionBase<object>
    {
        public IqlLiteralExpression(
            object value, IqlType type = IqlType.Unknown) : base(IqlExpressionType.Literal,
            type)
        {
            Value = value;
            if (value != null && value.GetType().Name.Contains("Func"))
            {
                int a = 0;
            }
        }

        public IqlLiteralExpression() : this(null)
        {
        }
    }
}