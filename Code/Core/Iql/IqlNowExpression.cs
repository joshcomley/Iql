namespace Iql
{
    public class IqlNowExpression : IqlExpression
    {
        public IqlNowExpression() : base(IqlExpressionKind.Now, IqlType.Date)
        {
        }

		public override IqlExpression Clone()
		{
			// #CloneStart

			var expression = new IqlNowExpression();
			expression.Key = Key;
			expression.Kind = Kind;
			expression.ReturnType = ReturnType;
			expression.Parent = Parent?.Clone();
			return expression;

			// #CloneEnd
		}
    }
}
