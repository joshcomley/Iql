namespace Iql
{
    public class IqlStringStartsWithExpression : IqlParentValueExpression
    {
        public IqlStringStartsWithExpression(IqlReferenceExpression parent, IqlReferenceExpression value)
            : base(parent, value, IqlExpressionKind.StringStartsWith, IqlType.Boolean)
        {
        }

        public IqlStringStartsWithExpression() : this(null, null)
        {
        }

		public override IqlExpression Clone()
		{
			// #CloneStart

			var expression = new IqlStringStartsWithExpression(null, null);
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
