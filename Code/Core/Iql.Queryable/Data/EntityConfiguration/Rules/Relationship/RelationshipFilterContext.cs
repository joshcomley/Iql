namespace Iql.Queryable.Data.EntityConfiguration.Rules.Relationship
{
    public class RelationshipFilterContext<TOwner> : IRelationshipFilterContext
    {
        public TOwner Owner { get; set; }
        object IRelationshipFilterContext.Owner
        {
            get => Owner;
            set => Owner = (TOwner)value;
        }
    }
}