namespace Iql
{
    public class IqlStringTrimExpression : IqlReferenceExpression
    {
        public IqlStringTrimExpression(IqlReferenceExpression parent) : base(IqlExpressionType.StringTrim,
            IqlType.String, parent)
        {
        }

        public IqlStringTrimExpression() : this(null)
        {
        }
    }
}