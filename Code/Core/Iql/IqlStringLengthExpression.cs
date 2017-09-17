namespace Iql
{
    public class IqlStringLengthExpression : IqlExpression
    {
        public IqlStringLengthExpression(IqlExpression parent) : base(IqlExpressionType.StringLength, IqlType.Integer,
            parent)
        {
        }

        public IqlStringLengthExpression() : this(null)
        {
        }
    }
}