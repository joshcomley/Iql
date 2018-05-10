namespace Iql
{
    public class IqlStringToUpperCaseExpression : IqlReferenceExpression
    {
        public IqlStringToUpperCaseExpression(IqlReferenceExpression parent) : base(IqlExpressionKind.StringToUpperCase,
            IqlType.String, parent)
        {
        }

        public IqlStringToUpperCaseExpression() : this(null)
        {
        }

		public override IqlExpression Clone()
		{
			// #CloneStart

			var expression = new IqlStringToUpperCaseExpression(null);
			expression.Kind = Kind;
			expression.ReturnType = ReturnType;
			expression.Parent = Parent?.Clone();
			return expression;

			// #CloneEnd
		}
    }
}
