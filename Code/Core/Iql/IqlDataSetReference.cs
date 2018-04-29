namespace Iql
{
    public class IqlDataSetReference : IqlExpression
    {
        public IqlDataSetReference(IqlExpression parent = null)
        : base(IqlExpressionKind.DataSetReference, IqlType.Collection, parent)
        {

        }

        public IqlDataSetReference() : this(null)
        {

        }
    }
}