using System;
using System.Collections.Generic;
using Iql.Queryable.Data.Crud.Operations;
using Iql.Queryable.Data.EntityConfiguration;
using Iql.Queryable.Data.EntityConfiguration.Relationships;
using Iql.Queryable.Events;

namespace Iql.Queryable.Data.Tracking.State
{
    public interface IEntityStateBase
    {
        void Reset();
        CompositeKey Key { get; set; }
        CompositeKey ResolveKey();
        Guid? PersistenceKey { get; }
        List<CascadeDeletion> CascadeDeletedBy { get; }
        List<IPropertyState> ChangedProperties { get; }
        object Entity { get; }
        IEntityConfiguration EntityConfiguration { get; }
        Type EntityType { get; }

        bool IsNew { get; set; }
        bool MarkedForAnyDeletion { get; }
        bool MarkedForCascadeDeletion { get; }
        bool MarkedForDeletion { get; set; }
        EventEmitter<MarkedForDeletionChangeEvent> MarkedForDeletionChanged { get; }

        IPropertyState GetPropertyState(string name);
        void MarkForCascadeDeletion(object from, IRelationship relationship);
        void SetPropertyState(string name, object oldValue, object newValue);
        void UnmarkForDeletion();
    }
}