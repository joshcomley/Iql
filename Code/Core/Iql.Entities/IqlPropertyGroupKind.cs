namespace Iql.Entities
{
    public enum IqlPropertyGroupKind
    {
        Unknown = 0,
        EntityConfiguration = 1,
        Primitive = 2,
        RelationshipTarget = 3,
        RelationshipSource = 4,
        DateRange = 5,
        File = 6,
        GeographicPoint = 7,
        NestedSet = 8,
        PropertyPath = 9,
        PropertyCollection = 10,
        SpecialType = 11
    }
}