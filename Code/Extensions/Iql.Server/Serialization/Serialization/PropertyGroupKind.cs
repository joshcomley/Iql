namespace Iql.Server.Serialization
{
    public enum PropertyGroupKind
    {
        Property = 1,
        PropertyCollection,
        Geographic,
        NestedSet,
        Relationship,
        DateRange,
        File
    }
}