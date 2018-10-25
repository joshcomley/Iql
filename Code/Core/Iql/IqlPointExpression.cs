namespace Iql
{
    public abstract class IqlPointExpression : IqlExpression
    {
        public long X { get; }
        public long Y { get; }

        protected IqlPointExpression(long x, long y, IqlExpressionKind kind) : base(kind, IqlType.Class)
        {
            X = x;
            Y = y;
        }
    }
}