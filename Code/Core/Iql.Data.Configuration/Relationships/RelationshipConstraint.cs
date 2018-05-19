namespace Iql.Queryable.Data.EntityConfiguration.Relationships
{
    public class RelationshipConstraint : IRelationshipConstraint
    {
        public IProperty SourceKeyProperty { get; set; }
        public IProperty TargetKeyProperty { get; set; }
        public RelationshipConstraint(IProperty sourceKeyProperty, IProperty targetKeyProperty)
        {
            SourceKeyProperty = sourceKeyProperty;
            TargetKeyProperty = targetKeyProperty;
        }
    }
}