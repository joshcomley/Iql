namespace Iql
{
    public class IqlStringSubStringExpression : IqlParentValueExpression
    {
        public IqlStringSubStringExpression(IqlReferenceExpression parent, IqlReferenceExpression value,
            IqlReferenceExpression take) :
            base(parent, value, IqlExpressionKind.StringSubString, IqlType.String)
        {
            Take = take;
        }

        public IqlStringSubStringExpression() : this(null, null, null)
        {
        }

        public IqlReferenceExpression Take { get; set; }

		public override IqlExpression Clone()
		{
			// #CloneStart

			var expression = new IqlStringSubStringExpression(null, null, null);
			expression.Take = (IqlReferenceExpression)Take?.Clone();
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
