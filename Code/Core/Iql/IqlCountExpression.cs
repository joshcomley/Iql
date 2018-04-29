namespace Iql
{
    public class IqlCountExpression : IqlParentValueExpression
    {
        public string RootVariableName { get; set; }
        public IqlCountExpression(
            string rootVariableName,
            IqlReferenceExpression parent,
            IqlExpression expression) : base(parent, expression, IqlExpressionKind.Count, IqlType.Integer)
        {
            RootVariableName = rootVariableName;
        }

        public IqlCountExpression()
        : base(null, null, IqlExpressionKind.Count, IqlType.Integer)
        {

        }
    }
}