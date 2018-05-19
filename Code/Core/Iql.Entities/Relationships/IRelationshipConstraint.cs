namespace Iql.Entities.Relationships
{
    public interface IRelationshipConstraint
    {
        IProperty SourceKeyProperty { get; set; }
        IProperty TargetKeyProperty { get; set; }
    }
}