namespace Iql
{
    public class IqlModuloEqualsExpression : IqlBinaryExpression
    {
        public IqlModuloEqualsExpression(
            IqlExpression left,
            IqlExpression right) : base(IqlExpressionKind.ModuloEquals, left, right)
        {
        }

        public IqlModuloEqualsExpression() : this(null, null)
        {
        }
    }
}