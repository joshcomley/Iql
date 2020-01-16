using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
    public interface IEntityStateBase : IJsonSerializable, IStateful, IDisposable, ILockable
    {
        ITrackingSet TrackingSet { get; }
        bool IsTracked { get; }
        bool IsIqlEntityState { get; }
        EventEmitter<ValueChangedEvent<object, IPropertyState>> PropertyLocalValueChanged { get; }
        EventEmitter<ValueChangedEvent<EntityStatus, IEntityStateBase>> StatusChanged { get; }
        EventEmitter<ValueChangedEvent<bool, IEntityStateBase>> StatusHasChangedChanged { get; }
        EventEmitter<ValueChangedEvent<bool, IEntityStateBase>> StatusHasChangedSinceSnapshotChanged { get; }
        bool StatusHasChanged { get; }
        bool StatusHasChangedSinceSnapshot { get; }
        EntityStatus Status { get; set; }
        EntityStatus SnapshotStatus { get; }
        void ClearSnapshotValue();
        void SetSnapshotValue(EntityStatus value);
        void UpdateHasChanges();
        void CheckHasChanged();
        bool HasChanges { get; }
        bool HasChangesSinceSnapshot { get; }
        bool HasNestedChanges { get; }
        bool HasNestedChangesSinceSnapshot { get; }
        bool HasAnyChanges { get; }
        bool HasAnyChangesSinceSnapshot { get; }
        EventEmitter<ValueChangedEvent<bool, IEntityStateBase>> HasChangesChanged { get; }
        EventEmitter<ValueChangedEvent<bool, IEntityStateBase>> HasChangesSinceSnapshotChanged { get; }
        EventEmitter<ValueChangedEvent<bool, IEntityStateBase>> HasNestedChangesChanged { get; }
        EventEmitter<ValueChangedEvent<bool, IEntityStateBase>> HasNestedChangesSinceSnapshotChanged { get; }
        EventEmitter<ValueChangedEvent<bool, IEntityStateBase>> HasAnyChangesChanged { get; }
        EventEmitter<ValueChangedEvent<bool, IEntityStateBase>> HasAnyChangesSinceSnapshotChanged { get; }
        EventEmitter<ValueChangedEvent<bool, IEntityStateBase>> AttachedToTrackerChanged { get; }
        EventEmitter<ValueChangedEvent<bool, IEntityStateBase>> PendingInsertChanged { get; }
        EventEmitter<ValueChangedEvent<bool, IEntityStateBase>> PendingDeleteChanged { get; }
        EventEmitter<ValueChangedEvent<bool, IEntityStateBase>> IsAttachedToGraphChanged { get; }
        EventEmitter<IEntityStateBase> Fetched { get; }
        EventEmitter<IEntityStateBase> Disposed { get; }
        AsyncEventEmitter<IEntityStateBase> FetchedAsync { get; }
        EventEmitter<ValueChangedEvent<bool, IEntityStateBase>> IsNewChanged { get; }
        EventEmitter<MarkedForDeletionChangeEvent> MarkedForDeletionChanged { get; }
        EventEmitter<MarkedForDeletionChangeEvent> MarkedForAnyDeletionChanged { get; }
        Task NotifyFetchedAsync();
        bool AttachedToTracker { get; set; }
        bool PendingInsert { get; }
        bool PendingDelete { get; }
        //IAsyncEventSubscriber<IEntityEvent> SavingAsync { get; }
        //IAsyncEventSubscriber<IEntityEvent> SavedAsync { get; }
        bool IsAttachedToGraph { get; set; }
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
        Type EntityType { get; }
        bool MarkedForAnyDeletion { get; }
        List<CascadeDeletion> CascadeDeletedBy { get; }
        bool HasValidKey();
        void HardReset();
        void SoftReset(bool markAsNotNew);
        CompositeKey KeyBeforeChanges();
        IPropertyState[] GetChangedProperties(IProperty[] properties = null);
        IEntityConfiguration EntityConfiguration { get; }
        string StateKey { get; set; }
        IPropertyState GetPropertyState(string name);
        IPropertyState FindPropertyState(string name);
        void MarkForCascadeDeletion(object from, IRelationship relationship);
        void UnmarkForDeletion();
        void AbandonPropertyChanges(IProperty[] properties);
        void AddSnapshot();
        /// <summary>
        /// For internal use only.
        /// </summary>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        /// <param name="propertyState"></param>
        void NotifyPropertyLocalValueChange(object oldValue, object newValue, IPropertyState propertyState);
    }
}