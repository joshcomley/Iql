using System;
using System.Linq.Expressions;

namespace Iql.Entities.Relationships
{
    public interface IRelationshipDetail : IRelationshipDetailMetadata
    {
        LambdaExpression InferredWith { get; set; }
        bool AllowInlineEditing { get; set; }
        IRelationshipDetail OtherSide { get; }
        IRelationship Relationship { get; }
        IEntityConfiguration Configuration { get; set; }
        CompositeKey GetCompositeKey(object entityOrCompositeKey, bool inverse = false);
        void MarkDirty(object entity);
        IProperty[] Constraints();
    }
}