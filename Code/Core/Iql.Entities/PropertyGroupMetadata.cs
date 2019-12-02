namespace Iql.Entities
{
    public class PropertyGroupMetadata
    {
        public IqlPropertySearchKind? Kind { get; }
        public IPropertyContainer Property { get; }

        public PropertyGroupMetadata(IPropertyContainer property, IqlPropertySearchKind? kind = null)
        {
            Property = property;
            Kind = kind;
        }
    }
}