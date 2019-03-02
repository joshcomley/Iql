namespace Iql.Entities.Rules.Relationship
{
    public interface IRelationshipFilterContext : IEntityType
    {
        object Owner { get; set; }
    }
}