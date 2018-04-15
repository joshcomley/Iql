namespace Iql
{
    public class IqlCountExpression : IqlParentValueExpression
    {
        public string RootVariableName { get; set; }
        public IqlCountExpression(
            string rootVariableName,
            IqlReferenceExpression parent,
            IqlExpression expression) : base(parent, expression, IqlExpressionType.Count, IqlType.Integer)
        {
            RootVariableName = rootVariableName;
        }

        public IqlCountExpression()
        : base(null, null, IqlExpressionType.Count, IqlType.Integer)
        {

        }
    }
}