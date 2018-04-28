namespace Iql
{
    public class IqlOrderByExpression : IqlExpression
    {
#if !TypeScript
        public IqlOrderByExpression() : this(null)
        {

        }
#endif
        public IqlOrderByExpression(IqlExpression parent = null) : base(IqlExpressionType.Expand, IqlType.Collection, parent)
        {
        }

        public IqlExpression OrderExpression { get; set; }
    }
}