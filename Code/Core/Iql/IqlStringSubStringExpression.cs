namespace Iql
{
    public class IqlStringSubStringExpression : IqlParentValueExpression
    {
        public IqlStringSubStringExpression(IqlReferenceExpression parent, IqlReferenceExpression value,
            IqlReferenceExpression take) :
            base(parent, value, IqlExpressionType.StringSubString, IqlType.String)
        {
            Take = take;
        }

        public IqlStringSubStringExpression() : this(null, null, null)
        {
        }

        public IqlReferenceExpression Take { get; set; }
    }
}