namespace Iql
{
    public abstract class IqlBinaryExpression : IqlExpression
    {
        protected IqlBinaryExpression(
            IqlExpressionKind kind,
            IqlExpression left,
            IqlExpression right) : base(kind)
        {
            Left = left;
            Right = right;
        }

        protected IqlBinaryExpression(IqlExpressionKind kind) : this(kind, null, null)
        {
        }

        public IqlExpression Left { get; set; }
        public IqlExpression Right { get; set; }

        public override bool ContainsRootEntity()
        {
            return Left?.ContainsRootEntity() == true || Right?.ContainsRootEntity() == true;
        }
    }
}