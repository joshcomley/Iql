namespace Iql
{
    public class IqlDataSetReference : IqlExpression
    {
        public string Name { get; set; }
        public IqlDataSetReference(IqlExpression parent = null)
        : base(IqlExpressionKind.DataSetReference, IqlType.Collection, parent)
        {

        }

        public IqlDataSetReference() : this(null)
        {

        }
    }
}