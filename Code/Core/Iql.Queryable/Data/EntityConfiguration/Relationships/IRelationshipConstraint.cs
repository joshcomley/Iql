namespace Iql.Queryable.Data.EntityConfiguration.Relationships
{
    public interface IRelationshipConstraint
    {
        IProperty SourceKeyProperty { get; set; }
        IProperty TargetKeyProperty { get; set; }
    }
}