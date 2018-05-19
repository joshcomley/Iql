using System;
using System.Collections.Generic;
using Iql.Data.Configuration;
using Iql.Data.Configuration.Events;
using Iql.Data.Configuration.Relationships;
using Iql.Data.Crud.Operations;
using Iql.Data.Events;

namespace Iql.Data.Tracking.State
{
    public interface IEntityStateBase
    {
        bool HasValidKey();
        bool IsInsertable();
        IPropertyState[] PropertyStates { get; }
        void Reset();
        CompositeKey CurrentKey { get; set; }

        CompositeKey KeyBeforeChanges();
        //CompositeKey RemoteKey { get; }
        Guid? PersistenceKey { get; }
        List<CascadeDeletion> CascadeDeletedBy { get; }

        IPropertyState[] GetChangedProperties();

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
        void UnmarkForDeletion();
        void AbandonChanges();
    }
}