namespace Iql.Server.Serialization.Serialization
{
    public enum IqlPropertyGroupKind
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