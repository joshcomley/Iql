using Iql.Extensions;

namespace Iql
{
    public class IqlLiteralExpression : IqlReferenceExpression
    {
        private object _value;
        //public IqlLiteralExpression(
        //    object value, Type type) : base(IqlExpressionType.Literal,
        //    type.ToIqlType())
        //{
        //    var xyzabc = type;
        //    xyzabc.ToIqlType();
        //    Value = value;
        //}

        public IqlLiteralExpression(
            object value, IqlType type = IqlType.Unknown) : base(IqlExpressionType.Literal,
            type)
        {
            Value = value;
        }

        public IqlLiteralExpression() : this(null, IqlType.Unknown)
        {
        }

        public object Value
        {
            get => _value;
            set
            {
                _value = value;
                UpdateInferredType();
            }
        }

        public IqlType InferredReturnType { get; set; }

        private void UpdateInferredType()
        {
            InferredReturnType = Value?.GetType().ToIqlType() ?? ReturnType;
        }
    }
}