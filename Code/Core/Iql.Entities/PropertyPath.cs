namespace Iql.Entities
{
    public class PropertyPath : SimplePropertyGroupBase<IPropertyPath>, IPropertyPath
    {
        public override IProperty PrimaryProperty
        {
            get { return Property.PrimaryProperty; }
        }

        protected ISimpleProperty _property;
        public override IqlPropertyKind Kind { get; set; } = IqlPropertyKind.SimpleCollection;
        public override IqlPropertyGroupKind GroupKind { get; } = IqlPropertyGroupKind.PropertyPath;
        public string Path { get; set; }

        public ISimpleProperty Property => _property;

        public PropertyPath(IEntityConfiguration configuration, string path, string key = null)
            : base(configuration, key)
        {
            Path = path;
            if (configuration != null)
            {
                _property = configuration.FindNestedProperty(path);
            }
        }

        public override PropertyGroupMetadata[] GetPropertyGroupMetadata()
        {
            return new PropertyGroupMetadata[]{ new PropertyGroupMetadata(Property, null) };
        }

        public override IPropertyGroup[] GetGroupProperties()
        {
            return new[] { Property };
        }

        public IqlPropertyPath BuildPropertyPath()
        {
            return IqlPropertyPath.FromString(EntityConfiguration.Builder, Path, EntityConfiguration.TypeMetadata);
        }
    }
}