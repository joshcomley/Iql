namespace Iql.Queryable.Data.EntityConfiguration.Relationships
{
    public interface IRelationshipConstraint
    {
        IqlPropertyExpression SourceKeyProperty { get; set; }
        IqlPropertyExpression TargetKeyProperty { get; set; }
    }
}