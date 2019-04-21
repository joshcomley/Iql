namespace Iql.Entities
{
    public interface IPropertyContainer : IConfiguration
    {
        IPropertyGroup[] GetGroupProperties();
        bool IsTypeGroup { get; }
        IqlPropertyGroupKind GroupKind { get; }
        PropertyGroupMetadata[] GetPropertyGroupMetadata();
    }
}