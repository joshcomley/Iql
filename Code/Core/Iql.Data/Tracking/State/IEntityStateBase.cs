using System;
using System.Collections.Generic;
using Iql.Conversion;
using Iql.Conversion.State;
using Iql.Data.Crud.Operations;
using Iql.Data.Events;
using Iql.Entities;
using Iql.Entities.Events;
using Iql.Entities.Relationships;
using Iql.Events;

namespace Iql.Data.Tracking.State
{
    public interface IEntityStateBase : IJsonSerializable, IStateful, IDisposable
    {
        bool AttachedToTracker { get; set; }
        EventEmitter<ValueChangedEvent<bool>> AttachedToTrackerChanged { get; }
        bool PendingInsert { get; }
        EventEmitter<ValueChangedEvent<bool>> PendingInsertChanged { get; }
        //IAsyncEventSubscriber<IEntityEvent> SavingAsync { get; }
        //IAsyncEventSubscriber<IEntityEvent> SavedAsync { get; }
        bool IsAttachedToGraph { get; set; }
        EventEmitter<ValueChangedEvent<bool>> IsAttachedToGraphChanged { get; }
        Guid Id { get; set; }
        void Restore(SerializedEntityState state);
        bool Floating { get; set; }
        DataTracker DataTracker { get; }
        object Entity { get; }
        object EntityBeforeChanges();
        IPropertyState[] PropertyStates { get; }
        CompositeKey LocalKey { get; set; }
        CompositeKey RemoteKey { get; }
        bool MarkedForDeletion { get; set; }
        bool MarkedForCascadeDeletion { get; set; }
        Guid? PersistenceKey { get; set; }
        bool IsNew { get; set; }
        EventEmitter<ValueChangedEvent<bool>> IsNewChanged { get; }
        Type EntityType { get; }
        bool MarkedForAnyDeletion { get; }
        List<CascadeDeletion> CascadeDeletedBy { get; }
        bool HasValidKey();
        void HardReset();
        void SoftReset(bool markAsNotNew);
        CompositeKey KeyBeforeChanges();
        IPropertyState[] GetChangedProperties(IProperty[] properties = null);
        IEntityConfiguration EntityConfiguration { get; }
        EventEmitter<MarkedForDeletionChangeEvent> MarkedForDeletionChanged { get; }
        string StateKey { get; set; }
        IPropertyState GetPropertyState(string name);
        IPropertyState FindPropertyState(string name);
        void MarkForCascadeDeletion(object from, IRelationship relationship);
        void UnmarkForDeletion();
        void AbandonPropertyChanges(IProperty[] properties);
    }
}