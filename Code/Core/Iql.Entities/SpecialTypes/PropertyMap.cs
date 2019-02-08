namespace Iql.Entities.SpecialTypes
{
    public class PropertyMap
    {
        public IEntityConfiguration EntityConfiguration { get; }
        public string InternalPropertyName { get; set; }
        public IProperty CustomProperty { get; set; }

        public PropertyMap(IEntityConfiguration entityConfiguration, string internalPropertyName, IProperty customProperty)
        {
            EntityConfiguration = entityConfiguration;
            InternalPropertyName = internalPropertyName;
            CustomProperty = customProperty;
        }
    }
}