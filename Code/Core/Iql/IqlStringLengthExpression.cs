namespace Iql
{
    public class IqlStringLengthExpression : IqlReferenceExpression
    {
        public IqlStringLengthExpression(IqlReferenceExpression parent) : base(IqlExpressionKind.StringLength, IqlType.Integer,
            parent)
        {
        }

        public IqlStringLengthExpression() : this(null)
        {
        }
    }
}