namespace Iql.Entities
{
    public interface IPropertyContainer : IConfiguration, IEntityConfigurationItem
    {
        IPropertyGroup[] GetGroupProperties();
        IqlPropertyGroupKind GroupKind { get; }
    }
}