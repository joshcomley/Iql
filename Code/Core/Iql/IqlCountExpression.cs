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

		public override IqlExpression Clone()
		{
			// #CloneStart

			var expression = new IqlCountExpression(null, null, null);
			expression.RootVariableName = RootVariableName;
			expression.Value = Value?.Clone();
			expression.Kind = Kind;
			expression.ReturnType = ReturnType;
			expression.Parent = Parent?.Clone();
			return expression;

			// #CloneEnd
		}
    }
}
