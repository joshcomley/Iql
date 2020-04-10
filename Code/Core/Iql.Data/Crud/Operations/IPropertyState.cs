using System;
using System.Threading.Tasks;
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
        object DebugKey { get; set; }
        void PauseEvents();
        void ResumeEvents();
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
        EventEmitter<IPropertyState> OnReset { get; }
        EventEmitter<ValueChangedEvent<object, IPropertyState>> SnapshotValueChanged { get; }
        EventEmitter<ValueChangedEvent<bool, IPropertyState>> HasSnapshotValueChanged { get; }
        EventEmitter<ValueChangedEvent<bool, IPropertyState>> HasNestedChangesChanged { get; }
        EventEmitter<ValueChangedEvent<bool, IPropertyState>> HasNestedChangesSinceSnapshotChanged { get; }
        EventEmitter<ValueChangedEvent<bool, IPropertyState>> CanUndoChanged { get; }
        EventEmitter<ValueChangedEvent<bool, IPropertyState>> HasChangesChanged { get; }
        EventEmitter<ValueChangedEvent<bool, IPropertyState>> HasAnyChangesChanged { get; }
        EventEmitter<ValueChangedEvent<bool, IPropertyState>> HasAnyChangesSinceSnapshotChanged { get; }
        EventEmitter<ValueChangedEvent<bool, IPropertyState>> HasChangesSinceSnapshotChanged { get; }
        EventEmitter<ValueChangedEvent<object, IPropertyState>> RemoteValueChanged { get; }
        EventEmitter<ValueChangedEvent<object, IPropertyState>> LocalValueChanged { get; }
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