using Iql.Entities.Relationships;

namespace Iql.Server.Serialization
{
    public class RelationshipDetail : RelationshipDetailBase
    {
        public RelationshipDetail() : base(null, RelationshipSide.Source)
        {
        }
    }
}