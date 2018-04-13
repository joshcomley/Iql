namespace Iql
{
    public class IqlNowExpression : IqlExpression
    {
        public IqlNowExpression() : base(IqlExpressionType.Now, IqlType.Date)
        {
        }
    }
}