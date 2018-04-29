namespace Iql
{
    public class IqlStringIndexOfExpression : IqlParentValueExpression
    {
        public IqlStringIndexOfExpression(IqlReferenceExpression parent, IqlReferenceExpression value)
            : base(parent, value, IqlExpressionKind.StringIndexOf, IqlType.Integer)
        {
        }

        public IqlStringIndexOfExpression() : this(null, null)
        {
        }
    }
}