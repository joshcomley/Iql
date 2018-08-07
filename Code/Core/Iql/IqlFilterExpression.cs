namespace Iql
{
    public class IqlFilterExpression : IqlParentValueExpression
    {
        public string RootVariableName { get; set; }
        public IqlFilterExpression(
            string rootVariableName,
            IqlReferenceExpression parent,
            IqlExpression expression) : base(parent, expression, IqlExpressionKind.Count, IqlType.Integer)
        {
            RootVariableName = rootVariableName;
        }

        public IqlFilterExpression()
            : base(null, null, IqlExpressionKind.Count, IqlType.Integer)
        {

        }

        public override IqlExpression Clone()
        {
            // #CloneStart

			var expression = new IqlFilterExpression(null, null, null);
			expression.RootVariableName = RootVariableName;
			expression.Value = Value?.Clone();
			expression.Key = Key;
			expression.Kind = Kind;
			expression.ReturnType = ReturnType;
			expression.Parent = Parent?.Clone();
			return expression;

            // #CloneEnd
        }
    }
}
