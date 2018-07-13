namespace Iql.Entities
{
    public interface IConfiguration : IMetadata
    {
        MetadataHint FindHint(string name);
        bool HasHint(string name);
        IConfiguration SetHint(string name, string value = null);
        IConfiguration RemoveHint(string name);
    }
}