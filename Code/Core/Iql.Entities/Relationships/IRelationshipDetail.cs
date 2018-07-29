using System;
using System.Linq.Expressions;

namespace Iql.Entities.Relationships
{
    public interface IRelationshipDetail : IRelationshipDetailMetadata, IConfigurable<IRelationshipDetail>
    {
        IRelationshipDetail OtherSide { get; }
        IRelationship Relationship { get; }
        CompositeKey GetCompositeKey(object entityOrCompositeKey, bool inverse = false);
        void MarkDirty(object entity);
        IProperty[] Constraints { get; }
    }
}