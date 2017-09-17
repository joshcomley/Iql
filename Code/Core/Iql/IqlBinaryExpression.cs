namespace Iql
{
    public abstract class IqlBinaryExpression : IqlExpression
    {
        protected IqlBinaryExpression(
            IqlExpressionType type,
            IqlExpression left,
            IqlExpression right) : base(type)
        {
            Left = left;
            Right = right;
        }

        protected IqlBinaryExpression(IqlExpressionType type) : this(type, null, null)
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