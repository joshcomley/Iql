using Iql.Entities;
using Iql.Entities.Relationships;

namespace Iql.Server.Serialization.Deserialization.EntityConfiguration
{
    public class RelationshipDetail : RelationshipDetailBase
    {
        public RelationshipDetail() : base(null, RelationshipSide.Source)
        {
        }

        public override IqlPropertyGroupKind GroupKind { get; } = IqlPropertyGroupKind.RelationshipSource;
        public override PropertyGroupMetadata[] GetPropertyGroupMetadata()
        {
            throw new System.NotImplementedException();
        }
    }
}