using System;

namespace Iql.Entities.Rules.Relationship
{
    public class RelationshipFilterContext<TOwner> : IRelationshipFilterContext
    {
        public Type EntityType => typeof(TOwner) ?? Owner?.GetType();
        public TOwner Owner { get; set; }
        object IRelationshipFilterContext.Owner
        {
            get => Owner;
            set => Owner = (TOwner)value;
        }
    }
}