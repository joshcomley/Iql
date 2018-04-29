namespace Iql
{
    public class IqlStringToUpperCaseExpression : IqlReferenceExpression
    {
        public IqlStringToUpperCaseExpression(IqlReferenceExpression parent) : base(IqlExpressionKind.StringToUpperCase,
            IqlType.String, parent)
        {
        }

        public IqlStringToUpperCaseExpression() : this(null)
        {
        }
    }
}