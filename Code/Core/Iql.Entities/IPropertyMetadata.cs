namespace Iql.Data.Configuration
{
    public interface IPropertyMetadata : IMetadata
    {
        string Placeholder { get; set; }
        PropertyKind Kind { get; set; }
        PropertySearchKind SearchKind { get; set; }
        bool ReadOnly { get; set; }
        bool Hidden { get; set; }
        bool Sortable { get; set; }
        bool Searchable { get; set; }
        bool? Nullable { get; set; }
    }
}