namespace Iql
{
    public class IqlToStringExpression : IqlReferenceExpression
    {
        public IqlToStringExpression(IqlReferenceExpression parent)
            : base(IqlExpressionType.ToString, IqlType.String, parent)
        {
        }

        public IqlToStringExpression() : this(null)
        {
        }
    }
}