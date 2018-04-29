namespace Iql
{
    public class IqlStringToLowerCaseExpression : IqlReferenceExpression
    {
        public IqlStringToLowerCaseExpression(IqlReferenceExpression parent)
            : base(IqlExpressionKind.StringToLowerCase, IqlType.String, parent)
        {
        }

        public IqlStringToLowerCaseExpression() : this(null)
        {
        }
    }
}