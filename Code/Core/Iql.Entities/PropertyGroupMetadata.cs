namespace Iql.Entities
{
    public class PropertyGroupMetadata
    {
        public PropertySearchKind? Kind { get; }
        public IPropertyContainer Property { get; }

        public PropertyGroupMetadata(IPropertyContainer property, PropertySearchKind? kind = null)
        {
            Property = property;
            Kind = kind;
        }
    }
}