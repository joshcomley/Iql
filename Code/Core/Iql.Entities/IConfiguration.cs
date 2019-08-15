namespace Iql.Entities
{
    public interface IConfiguration : IMetadata
    {
        MetadataHint FindHint(string name, bool? onlySelf = null);
        bool HasHint(string name, bool? onlySelf = null);
        IConfiguration SetHint(string name, string value = null);
        IConfiguration RemoveHint(string name, bool? onlySelf = null);
    }
}