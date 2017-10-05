namespace Iql.Queryable.Data.EntityConfiguration.Relationships
{
    public class RelationshipConstraint : IRelationshipConstraint
    {
        public IqlPropertyExpression SourceKeyProperty { get; set; }
        public IqlPropertyExpression TargetKeyProperty { get; set; }
        public RelationshipConstraint(IqlPropertyExpression sourceKeyProperty, IqlPropertyExpression targetKeyProperty)
        {
            SourceKeyProperty = sourceKeyProperty;
            TargetKeyProperty = targetKeyProperty;
        }
    }
}