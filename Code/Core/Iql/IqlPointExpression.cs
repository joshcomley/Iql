namespace Iql
{
    public abstract class IqlPointExpression : IqlExpression
    {
        public long X { get; }
        public long Y { get; }

        protected IqlPointExpression(long x, long y, IqlExpressionKind kind, IqlType type) : base(kind, type)
        {
            X = x;
            Y = y;
        }
    }
}