namespace Iql
{
    public class IqlStringToUpperCaseExpression : IqlExpression
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