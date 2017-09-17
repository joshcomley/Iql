namespace Iql
{
    public class IqlLiteralExpression : IqlReferenceExpression
    {
        //public IqlLiteralExpression(
        //    object value, Type type) : base(IqlExpressionType.Literal,
        //    type.ToIqlType())
        //{
        //    var xyzabc = type;
        //    xyzabc.ToIqlType();
        //    Value = value;
        //}

        public IqlLiteralExpression(
            object value, IqlType type) : base(IqlExpressionType.Literal,
            type)
        {
            Value = value;
        }

        public IqlLiteralExpression() : this(null, IqlType.Unknown)
        {
        }

        public object Value { get; set; }
    }
}