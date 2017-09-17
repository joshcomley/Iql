namespace Iql
{
    public class IqlPropertyExpression : IqlReferenceExpression
    {
        public IqlPropertyExpression(
            string propertyName,
            string owningEntityTypeName,
            IqlType propertyType) : base(
            IqlExpressionType.Property,
            propertyType)
        {
            PropertyName = propertyName;
            OwningEntityTypeName = owningEntityTypeName;
        }

        public IqlPropertyExpression() : this(null, null, IqlType.Unknown)
        {
        }


        public string PropertyName { get; set; }
        public string OwningEntityTypeName { get; }
    }
}