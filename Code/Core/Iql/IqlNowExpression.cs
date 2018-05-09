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
			return null;
			// #CloneEnd
		}
    }
}
