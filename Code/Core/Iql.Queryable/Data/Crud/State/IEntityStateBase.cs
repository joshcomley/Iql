using System;
using System.Collections.Generic;
using Iql.Queryable.Data.Crud.Operations;
using Iql.Queryable.Data.EntityConfiguration;
using Iql.Queryable.Data.EntityConfiguration.Relationships;
using Iql.Queryable.Events;
using Iql.Queryable.Operations;

namespace Iql.Queryable.Data.Crud.State
{
    public interface IEntityStateBase
    {
        CompositeKey Key { get; }
        Guid? PersistenceKey { get; }
        List<CascadeDeletion> CascadeDeletedBy { get; }
        List<PropertyChange> ChangedProperties { get; }
        object Entity { get; }
        IEntityConfiguration EntityConfiguration { get; }
        Type EntityType { get; }

        bool IsNew { get; set; }
        bool MarkedForAnyDeletion { get; }
        bool MarkedForCascadeDeletion { get; }
        bool MarkedForDeletion { get; set; }
        EventEmitter<MarkedForDeletionChangeEvent> MarkedForDeletionChanged { get; }

        PropertyChange GetPropertyState(string name);
        void MarkForCascadeDeletion(object from, IRelationship relationship);
        void SetPropertyState(string name, object oldValue, object newValue);
        void UnmarkForDeletion();
    }
}