namespace Iql.Queryable.Data.EntityConfiguration
{
    public interface IEntityMetadata : IMetadata
    {
        string SetFriendlyName { get; set; }
        string SetName { get; set; }
        string ResolveSetFriendlyName();
        string ResolveSetName();
    }
}