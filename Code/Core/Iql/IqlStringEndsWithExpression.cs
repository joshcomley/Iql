namespace Iql
{
    public class IqlStringEndsWithExpression : IqlParentValueExpression
    {
        public IqlStringEndsWithExpression(IqlReferenceExpression parent, IqlReferenceExpression value)
            : base(parent, value, IqlExpressionKind.StringEndsWith, IqlType.Boolean)
        {
        }

        public IqlStringEndsWithExpression() : this(null, null)
        {
        }

		public override IqlExpression Clone()
		{
			// #CloneStart

			var expression = new IqlStringEndsWithExpression(null, null);
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
