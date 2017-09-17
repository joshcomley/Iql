namespace Iql
{
    public class IqlModuloExpression : IqlBinaryExpression
    {
        public IqlModuloExpression(
            IqlExpression left,
            IqlExpression right) : base(IqlExpressionType.Modulo, left, right)
        {
        }

        public IqlModuloExpression() : this(null, null)
        {
        }
    }
}