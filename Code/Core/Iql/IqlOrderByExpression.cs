namespace Iql
{
    public class IqlOrderByExpression : IqlExpression
    {
        public IqlOrderByExpression(IqlExpression parent = null) : base(IqlExpressionKind.Expand, IqlType.Collection, parent)
        {
        }

        public IqlOrderByExpression() : this(null)
        {

        }

        public IqlExpression OrderExpression { get; set; }
    }
}