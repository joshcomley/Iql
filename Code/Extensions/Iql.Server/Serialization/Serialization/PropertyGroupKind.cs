namespace Iql.Server.Serialization
{
    public enum PropertyGroupKind
    {
        Property = 1,
        PropertyCollection,
        PropertyPath,
        Geographic,
        NestedSet,
        Relationship,
        DateRange,
        File
    }
}