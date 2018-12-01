namespace Iql.Entities.Relationships
{
    public class RelationshipMapping : IMapping<IRelationshipDetail>
    {
        public IRelationshipDetail Container { get; set; }
        public IqlExpression Expression { get; set; }
    }
}