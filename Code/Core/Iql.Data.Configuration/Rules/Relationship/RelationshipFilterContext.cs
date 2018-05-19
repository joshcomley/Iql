namespace Iql.Data.Configuration.Rules.Relationship
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