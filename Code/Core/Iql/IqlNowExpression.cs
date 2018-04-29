namespace Iql
{
    public class IqlNowExpression : IqlExpression
    {
        public IqlNowExpression() : base(IqlExpressionKind.Now, IqlType.Date)
        {
        }
    }
}