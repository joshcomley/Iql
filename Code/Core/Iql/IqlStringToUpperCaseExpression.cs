namespace Iql
{
    public class IqlStringToUpperCaseExpression : IqlReferenceExpression
    {
        public IqlStringToUpperCaseExpression(IqlReferenceExpression parent) : base(IqlExpressionType.StringToUpperCase,
            IqlType.String, parent)
        {
        }

        public IqlStringToUpperCaseExpression() : this(null)
        {
        }
    }
}