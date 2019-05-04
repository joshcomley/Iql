using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Iql.Entities.Relationships
{
    [DebuggerDisplay("{Property.Name} - Relationship Collection")]
    public class CollectionRelationshipDetail<T, TProperty>
        : RelationshipDetailTypedBase<T, IEnumerable<TProperty>, CollectionRelationshipDetail<T, TProperty>>,
            ITargetRelationshipSourceDetail
        where T : class
    {
        public bool SupportsCascadeDelete { get; set; }
        protected override bool CanWriteDefaultValue { get; } = false;
        public override PropertyEditKind EditKind { get; set; } = PropertyEditKind.Hidden;
        public override PropertyReadKind ReadKind { get; set; } = PropertyReadKind.Hidden;

        public override ReadOnlyEditDisplayKind ReadOnlyEditDisplayKind { get; set; } = ReadOnlyEditDisplayKind.Hide;

        public CollectionRelationshipDetail(
            IRelationship relationship,
            RelationshipSide relationshipSide,
            IEntityConfigurationBuilder configuration,
            LambdaExpression expression,
            Type elementType) : base(relationship, relationshipSide, configuration, expression, elementType)
        {
            
        }

        public override IqlPropertyGroupKind GroupKind { get; } = IqlPropertyGroupKind.RelationshipTarget;
        public override PropertyGroupMetadata[] GetPropertyGroupMetadata()
        {
            return new PropertyGroupMetadata[]{};
        }
    }
}