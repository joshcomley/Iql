namespace Iql.Entities
{
    public interface IPropertyContainer : IConfiguration
    {
        IProperty PrimaryProperty { get; }
        IPropertyGroup[] GetGroupProperties();
        bool IsTypeGroup { get; }
        IqlPropertyGroupKind GroupKind { get; }
        PropertyGroupMetadata[] GetPropertyGroupMetadata();
    }
}