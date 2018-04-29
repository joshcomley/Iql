namespace Iql
{
    public class IqlDataSetReference : IqlExpression
    {
        public IqlDataSetReference(IqlExpression parent = null)
        : base(IqlExpressionType.DataSetReference, IqlType.Collection, parent)
        {

        }

        public IqlDataSetReference() : this(null)
        {

        }
    }
}