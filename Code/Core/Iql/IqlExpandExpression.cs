namespace Iql
{
    public class IqlExpandExpression : IqlExpression
    {
#if !TypeScript
        public IqlExpandExpression() : this(null)
        {
            
        }
#endif
        public IqlExpandExpression(IqlExpression parent = null) : base(IqlExpressionType.Expand, IqlType.Collection, parent)
        {
        }

        public IqlPropertyExpression NavigationProperty { get; set; }
    }
}