namespace Iql.Queryable.Data.EntityConfiguration
{
    public interface IPropertyMetadata : IMetadata
    {
        string Placeholder { get; set; }
        string ConvertedFromType { get; set; }
        PropertyKind Kind { get; set; }
        PropertySearchKind SearchKind { get; set; }
        bool Nullable { get; set; }
        bool ReadOnly { get; set; }
    }
}