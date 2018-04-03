using Iql.Extensions;

namespace Iql
{
    public abstract class IqlLiteralExpressionBase<TValue> : IqlReferenceExpression, IIqlLiteralExpression
    {
        private TValue _value;
        //public IqlLiteralExpression(
        //    object value, Type type) : base(IqlExpressionType.Literal,
        //    type.ToIqlType())
        //{
        //    var xyzabc = type;
        //    xyzabc.ToIqlType();
        //    Value = value;
        //}

        protected IqlLiteralExpressionBase(
            TValue value, IqlType type = IqlType.Unknown) : base(IqlExpressionType.Literal,
            type)
        {
            Value = value;
        }

        protected IqlLiteralExpressionBase() : this(default(TValue), IqlType.Unknown)
        {
        }

        public TValue Value
        {
            get => _value;
            set
            {
                _value = value;
                UpdateInferredType();
            }
        }

        object IIqlLiteralExpression.Value
        {
            get => Value;
            set => Value = (TValue) value;
        }

        public IqlType InferredReturnType { get; set; }

        private void UpdateInferredType()
        {
            InferredReturnType = Value?.GetType().ToIqlType() ?? ReturnType;
        }
    }
}