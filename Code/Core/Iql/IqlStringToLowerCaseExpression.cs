namespace Iql
{
    public class IqlStringToLowerCaseExpression : IqlExpression
    {
        public IqlStringToLowerCaseExpression(IqlReferenceExpression parent)
            : base(IqlExpressionType.StringToLowerCase, IqlType.String, parent)
        {
        }

        public IqlStringToLowerCaseExpression() : this(null)
        {
        }
    }
}