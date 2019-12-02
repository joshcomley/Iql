using System;
using Iql.Conversion;
using Iql.Conversion.State;
using Iql.Data.Tracking;
using Iql.Data.Tracking.State;
using Iql.Entities;
using Iql.Entities.Events;
using Iql.Entities.Lists;
using Iql.Entities.PropertyChangers;
using Iql.Events;

namespace Iql.Data.Crud.Operations
{
    public interface IPropertyState : IJsonSerializable, IStateful, IDisposable, ILockable
    {
        bool HasSnapshotValue { get; }
        // Non-new items with changes
        ObservableList<IEntityStateBase> ItemsChanged { get; }
        // Non-new items removed
        ObservableList<IEntityStateBase> ItemsRemoved { get; }
        // Any new items
        ObservableList<IEntityStateBase> ItemsAdded { get; }
        ObservableList<IEntityStateBase> ItemsChangedSinceSnapshot { get; }
        ObservableList<IEntityStateBase> ItemsRemovedSinceSnapshot { get; }
        ObservableList<IEntityStateBase> ItemsAddedSinceSnapshot { get; }
        IPropertyState RelationshipPropertyState { get; }
        bool IsRelationshipCollection { get; }
        void UpdateHasChanged(bool? ignoreRelationshipOtherSide = null);
        DataTracker DataTracker { get; }
        Guid Guid { get; }
        //IEntityPropertyEvent
        EventEmitter<ValueChangedEvent<bool>> HasSnapshotValueChanged { get; }
        EventEmitter<ValueChangedEvent<bool>> HasNestedChangesChanged { get; }
        EventEmitter<ValueChangedEvent<bool>> HasNestedChangesSinceSnapshotChanged { get; }
        EventEmitter<ValueChangedEvent<bool>> CanUndoChanged { get; }
        EventEmitter<ValueChangedEvent<bool>> HasChangesChanged { get; }
        EventEmitter<ValueChangedEvent<bool>> HasAnyChangesChanged { get; }
        EventEmitter<ValueChangedEvent<bool>> HasAnyChangesSinceSnapshotChanged { get; }
        EventEmitter<ValueChangedEvent<bool>> HasChangesSinceSnapshotChanged { get; }
        EventEmitter<ValueChangedEvent<object>> RemoteValueChanged { get; }
        EventEmitter<ValueChangedEvent<object>> LocalValueChanged { get; }
        IPropertyState[] SiblingStates { get; }
        IPropertyState[] GroupStates { get; }
        IEntityStateBase EntityState { get; }
        bool CanUndo { get; }
        bool HasChanges { get; }
        bool HasAnyChanges { get; }
        bool HasAnyChangesSinceSnapshot { get; }
        bool HasChangesSinceSnapshot { get; }
        bool HasNestedChanges { get; }
        bool HasNestedChangesSinceSnapshot { get; }
        bool LocalValueSet { get; }
        object LocalValue { get; set; }
        object RemoteValue { get; set; }
        object SnapshotValue { get; }
        void AddSnapshot();
        void ClearSnapshotValue();
        void SetSnapshotValue(object value);
        string Data { get; set; }
        IProperty Property { get; }
        PropertyChanger PropertyChanger { get; }
        void HardReset();
        void SoftReset();
        IPropertyState Copy();
        void Restore(SerializedPropertyState state);
        void UndoChanges(bool? undoNestedChanges = null);
    }
}