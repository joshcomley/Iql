namespace Iql
{
    public class IqlAnyExpression : IqlAnyAllExpression
    {
        public IqlAnyExpression(
            string rootVariableName,
            IqlReferenceExpression parent,
            IqlExpression expression) : base(rootVariableName, IqlExpressionKind.Any, parent, expression)
        {
        }

        public IqlAnyExpression() : this(null, null, null)
        {
        }

		public override IqlExpression Clone()
		{
			// #CloneStart

			var expression = new IqlAnyExpression(null, null, null);
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
