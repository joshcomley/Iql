namespace Iql
{
    public class IqlStringToLowerCaseExpression : IqlReferenceExpression
    {
        public IqlStringToLowerCaseExpression(IqlReferenceExpression parent)
            : base(IqlExpressionKind.StringToLowerCase, IqlType.String, parent)
        {
        }

        public IqlStringToLowerCaseExpression() : this(null)
        {
        }

		public override IqlExpression Clone()
		{
			// #CloneStart

			var expression = new IqlStringToLowerCaseExpression(null);
			expression.Key = Key;
			expression.Kind = Kind;
			expression.ReturnType = ReturnType;
			expression.Parent = Parent?.Clone();
			return expression;

			// #CloneEnd
		}
    }
}
