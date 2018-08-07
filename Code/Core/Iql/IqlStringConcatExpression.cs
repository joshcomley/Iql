namespace Iql
{
    public class IqlStringConcatExpression : IqlParentValueExpression
    {
        public IqlStringConcatExpression(IqlReferenceExpression parent, IqlReferenceExpression value)
            : base(parent, value, IqlExpressionKind.StringConcat, IqlType.String)
        {
        }

        public IqlStringConcatExpression() : this(null, null)
        {
        }

		public override IqlExpression Clone()
		{
			// #CloneStart

			var expression = new IqlStringConcatExpression(null, null);
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
