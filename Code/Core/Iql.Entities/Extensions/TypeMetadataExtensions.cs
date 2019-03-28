namespace Iql.Entities.Extensions
{
    public static class TypeMetadataExtensions
    {
        public static IProperty EntityProperty(this ITypeProperty property)
        {
            return property.UnderlyingObject as IProperty;
        }
        public static IEntityConfiguration EntityConfiguration(this IIqlTypeMetadata type)
        {
            return type.UnderlyingObject as IEntityConfiguration;
        }
    }
}