namespace Iql.Entities
{
    public interface IPropertyContainer : IMetadata, IEntityConfigurationItem
    {
        IPropertyGroup[] GetGroupProperties();
    }
}