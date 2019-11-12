using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Iql.Conversion;
using Iql.Data.Context;
using Iql.Data.Crud.Operations;
using Iql.Data.Events;
using Iql.Data.Lists;
using Iql.Entities;
using Iql.Entities.Events;
using Iql.Entities.Extensions;
using Iql.Entities.PropertyChangers;
using Iql.Events;

namespace Iql.Data.Tracking.State
{
    [DebuggerDisplay("{Property.Name}")]
    public class PropertyState : IPropertyState
    {
        private bool _isLocked = false;
        public ILockable Parent => EntityState;
        public bool IsLocked => _isLocked || Parent == null || Parent.IsLocked;
        public void Lock()
        {
            _isLocked = true;
        }
        public void Unlock()
        {
            _isLocked = false;
        }

        public DataTracker DataTracker => EntityState == null ? null : EntityState.DataTracker;
        private bool _hasChangedSinceSnapshot;
        private bool _hasChanged;
        private object _remoteValueClone;
        private bool _remoteValueSet;
        private object _localValue;

        public PropertyState(
            IProperty property,
            IEntityStateBase entityState)
        {
            Guid = Guid.NewGuid();
            Property = property;
            EntityState = entityState;
            // Ensure it is loaded
            EnsureRemoteValue();
            //if (!(PropertyChanger is PrimitivePropertyChanger))
            //{
            //}
        }

        public IOperationEvents<IEntityPropertyEvent, IEntityPropertyEvent> StatefulSaveEvents { get; } = new OperationEvents<IEntityPropertyEvent, IEntityPropertyEvent>();
        IOperationEventsBase IStateful.StatefulSaveEvents => StatefulSaveEvents;
        public IOperationEvents<IEntityPropertyEvent, IEntityPropertyEvent> SaveEvents { get; } = new OperationEvents<IEntityPropertyEvent, IEntityPropertyEvent>();
        IOperationEventsBase IStateful.SaveEvents => SaveEvents;
        public IOperationEvents<AbandonChangeEvent, AbandonChangeEvent> AbandonEvents { get; } = new OperationEvents<AbandonChangeEvent, AbandonChangeEvent>();
        IOperationEventsBase IStateful.AbandonEvents => AbandonEvents;

        private PropertyChanger _propertyChanger;
        private object _remoteValue;

        private PropertyChanger PropertyChanger
        {
            get { return _propertyChanger = _propertyChanger ?? Property.TypeDefinition.ResolveChanger(); }
        }

        public string HasChangedText { get; private set; }
        public bool HasChangedSinceSnapshot => _hasChangedSinceSnapshot;

        public bool CanUndo
        {
            get => _canUndo;
            set
            {
                var old = _canUndo;
                _canUndo = value;
                if (value != old)
                {
                    CanUndoChanged.Emit(() => new ValueChangedEvent<bool>(old, value));
                }
            }
        }

        private bool _nestedHasChanged = false;
        public bool HasChanged
        {
            get
            {
                var forceUpdate = false;
                if (!_localValueSet && EntityState != null)
                {
                    LocalValue = Property.GetValue(EntityState.Entity);
                    forceUpdate = true;
                }
                if (PropertyChanger.CanSilentlyChange)
                {
                    forceUpdate = true;
                }
                if (forceUpdate && !_nestedHasChanged)
                {
                    _nestedHasChanged = true;
                    UpdateHasChanged();
                }
                _nestedHasChanged = false;
                return _hasChanged;
            }
        }

        private bool CalculateHasChanged(object remoteValue, ChangeCalculationKind kind)
        {
            if (Property.Kind.HasFlag(PropertyKind.Count))
            {
                HasChangedText = "Count field";
                return false;
            }

            if (Property.Kind.HasFlag(PropertyKind.Relationship))
            {
                HasChangedText = "Relationship field";
                if (Property.Relationship.ThisIsSource)
                {
                    return RelationshipSourceHasChanged(remoteValue);
                }
                else if (Property.TypeDefinition.Kind == IqlType.Collection)
                {
                    return RelationshipTargetHasChanged(remoteValue, kind);
                }
            }

            HasChangedText = $"PropertyChanger: {PropertyChanger.GetType().Name}";
            return !PropertyChanger.AreEquivalent(remoteValue, EntityState == null || _localValueSet ? LocalValue : Property.GetValue(EntityState.Entity));
        }

        private void EnsureRemoteValue()
        {
            if (EntityState != null &&
                _remoteValueSet &&
                _remoteValue == null &&
                _localValue != null &&
                Property.Relationship != null &&
                !Property.Relationship.ThisIsTarget &&
                Property == Property.Relationship.ThisEnd.Property)
            {
                var canMatchToKey = true;
                for (var i = 0; i < Property.Relationship.ThisEnd.Constraints.Length; i++)
                {
                    var key = Property.Relationship.ThisEnd.Constraints[i];
                    if (EntityState.GetPropertyState(key.PropertyName).HasChanged)
                    {
                        canMatchToKey = false;
                    }
                }

                if (canMatchToKey)
                {
                    var objectKey = Property.Relationship.OtherEnd.GetCompositeKey(_localValue, true);
                    var relationshipKey = Property.Relationship.ThisEnd.GetCompositeKey(EntityState.Entity);
                    if (objectKey.Matches(relationshipKey))
                    {
                        SetRemoteValue(_localValue);
                        _remoteValueSet = true;
                    }
                }
            }
            if (!_remoteValueSet)
            {
                _remoteValueSet = true;
                if (EntityState != null)
                {
                    SetRemoteValue(Property.GetValue(EntityState.Entity));
                }
            }
        }

        private bool RelationshipTargetHasChanged(object remoteValue, ChangeCalculationKind kind)
        {
            var remoteList = (IList)remoteValue;
            var remoteCount = 0;
            if (remoteList != null)
            {
                remoteCount = remoteList.Count;
            }

            var localCount = 0;
            var localList = (IList)LocalValue;
            if (localList != null)
            {
                localCount = localList.Count;
            }
            if (remoteCount == 0 && localCount == 0)
            {
                return false;
            }
            if (remoteCount > 0 && localCount < remoteCount)
            {
                return true;
            }
            if (remoteCount == 0 && localList != null)
            {
                for (var i = 0; i < localList.Count; i++)
                {
                    var item = localList[i];
                    if (HasRelationshipItemChanged(item, kind))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private List<IProperty> _otherSideProperties = null;
        private bool HasRelationshipItemChanged(object item, ChangeCalculationKind kind)
        {
            var state = DataTracker.GetEntityState(item);
            if (state == null)
            {
                return false;
            }

            if (_otherSideProperties == null)
            {
                _otherSideProperties = Property.Relationship.OtherEnd.Constraints.ToList();
                _otherSideProperties.Add(Property.Relationship.OtherEnd.Property);
            }

            for (var i = 0; i < _otherSideProperties.Count; i++)
            {
                var prop = _otherSideProperties[i];
                var propState = state.GetPropertyState(prop.PropertyName);
                if (propState != null)
                {
                    if (kind == ChangeCalculationKind.Remote && propState.HasChanged)
                    {
                        return true;
                    }

                    if (kind == ChangeCalculationKind.Snapshot && propState.HasChangedSinceSnapshot)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private bool RelationshipSourceHasChanged(object remoteValue)
        {
            if (remoteValue != null && LocalValue != null && remoteValue != LocalValue)
            {
                return true;
            }
            if (remoteValue == null && LocalValue != null)
            {
                return true;
            }
            if (remoteValue != null && LocalValue == null)
            {
                return true;
            }

            if (EntityState != null)
            {
                var constraints = Property.Relationship.ThisEnd.Constraints;
                for (var i = 0; i < constraints.Length; i++)
                {
                    var constraint = constraints[i];
                    var propertyState = EntityState.GetPropertyState(constraint.Name);
                    if (propertyState != null && propertyState.HasChanged)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public object RemoteValue
        {
            get
            {
                EnsureRemoteValue();
                return _remoteValueClone;
            }
            set
            {
                _remoteValueSet = true;
                SetRemoteValue(value);
                UpdateHasChanged();
            }
        }

        public object SnapshotValue => _snapshotValueSet ? _snapshotValue : RemoteValue;

        public void AddSnapshot()
        {
            if (_snapshotValueSet)
            {
                if (HasChangedSinceSnapshot)
                {
                    AddSnapshotInternal();
                }
            }
            else if (HasChanged)
            {
                AddSnapshotInternal();
            }
        }

        public void SetSnapshotValue(object value)
        {
            _snapshotValue = PropertyChanger.CloneValue(value);
            _snapshotValueSet = true;
            UpdateHasChanged();
        }

        private void AddSnapshotInternal()
        {
            SetSnapshotValue(LocalValue);
        }

        public void ClearSnapshotValue()
        {
            _snapshotValueSet = false;
            _snapshotValue = null;
            UpdateHasChanged();
        }

        private void SetRemoteValue(object value)
        {
            var oldValue = _remoteValue;
            _remoteValueClone = PropertyChanger.CloneValue(value);
            _remoteValue = value;
            UpdateHasChanged();
            if (_remoteValue != oldValue)
            {
                DataTracker.NotifyRemoteValueChanged(this);
                RemoteValueChanged.Emit(() => new ValueChangedEvent<object>(oldValue, value));
            }
        }

        private bool _localValueSet = false;
        private object _snapshotValue;
        private bool _snapshotValueSet;
        private bool _canUndo;
        private IPropertyState[] _groupStates;
        private IPropertyState[] _siblingStates;
        public bool LocalValueSet => _localValueSet;
        public object LocalValue
        {
            get => _localValueSet ? _localValue : RemoteValue;
            set
            {
                _localValueSet = true;
                var oldValue = _localValue;
                _localValue = value;
                UpdateHasChanged();
                if (oldValue != value)
                {
                    LocalValueChanged.Emit(() => new ValueChangedEvent<object>(oldValue, value));
                }
            }
        }

        private bool? _otherEndIsCollection = null;
        private bool OtherEndIsCollection
        {
            get
            {
                if (_otherEndIsCollection == null)
                {
                    _otherEndIsCollection = EntityState != null && Property.Kind.HasFlag(PropertyKind.Relationship) &&
                                            Property.Relationship.ThisIsSource &&
                                            Property.Relationship.OtherEnd.Property.TypeDefinition.Kind ==
                                            IqlType.Collection;
                }
                return _otherEndIsCollection.Value;
            }
        }

        private IPropertyState _relationshipPropertyState = null;
        private IPropertyState RelationshipPropertyState
        {
            get
            {
                if (_relationshipPropertyState == null)
                {
                    _relationshipPropertyState = EntityState.GetPropertyState(Property.Relationship.ThisEnd.Property.PropertyName);
                }

                return _relationshipPropertyState;
            }
        }

        private bool _isUpdatingHasChanged = false;
        public void UpdateHasChanged(bool? ignoreRelationshipOtherSide = null)
        {
            if (_isUpdatingHasChanged)
            {
                return;
            }

            if (IsLocked)
            {
                return;
            }

            _isUpdatingHasChanged = true;
            UpdateHasChangedSinceStart();
            UpdateHasChangedSinceSnapshot();
            if (ignoreRelationshipOtherSide != true && OtherEndIsCollection && RelationshipPropertyState != null)
            {
                var oldRelatedList = _relationshipEntityRemote == RelationshipPropertyState.RemoteValue
                    ? _relationshipPropertyStateRemote
                    : ResolveRelatedListStateFromSource(RelationshipPropertyState.RemoteValue);
                var newRelatedList = _relationshipEntityLocal == RelationshipPropertyState.LocalValue
                    ? _relationshipPropertyStateLocal
                    : ResolveRelatedListStateFromSource(RelationshipPropertyState.LocalValue);
                var snapshotRelatedList = _relationshipEntitySnapshot == RelationshipPropertyState.SnapshotValue
                    ? _relationshipPropertyStateSnapshot
                    : ResolveRelatedListStateFromSource(RelationshipPropertyState.LocalValue);
                _relationshipPropertyStateLocal = newRelatedList;
                _relationshipEntityLocal = RelationshipPropertyState.LocalValue;
                _relationshipPropertyStateRemote = newRelatedList;
                _relationshipEntityRemote = RelationshipPropertyState.RemoteValue;
                _relationshipPropertyStateSnapshot = snapshotRelatedList;
                _relationshipEntitySnapshot = RelationshipPropertyState.SnapshotValue;
                if (oldRelatedList != null)
                {
                    oldRelatedList.UpdateHasChanged();
                }
                if (newRelatedList != oldRelatedList && newRelatedList != null)
                {
                    newRelatedList.UpdateHasChanged();
                }
                if (snapshotRelatedList != oldRelatedList && snapshotRelatedList != newRelatedList && snapshotRelatedList != null)
                {
                    snapshotRelatedList.UpdateHasChanged();
                }
            }
            _isUpdatingHasChanged = false;
        }

        private IPropertyState _relationshipPropertyStateLocal = null;
        private object _relationshipEntityLocal = null;
        private IPropertyState _relationshipPropertyStateRemote = null;
        private object _relationshipEntityRemote = null;
        private IPropertyState _relationshipPropertyStateSnapshot = null;
        private object _relationshipEntitySnapshot = null;
        private IPropertyState ResolveRelatedListStateFromSource(object relationshipPropertyValue)
        {
            //var state = DataTracker.FindTrackedEntity(compositeKey);
            if (relationshipPropertyValue == null)
            {
                return null;
            }

            var state = DataTracker.GetEntityState(relationshipPropertyValue);
            if (state != null)
            {
                return state.GetPropertyState(Property.Relationship.OtherEnd.Property.PropertyName);
            }

            return null;
        }

        private void UpdateHasChangedSinceStart()
        {
            var oldValue = _hasChanged;
            _hasChanged = CalculateHasChanged(RemoteValue, ChangeCalculationKind.Remote);
            if (oldValue != _hasChanged)
            {
                HasChangedChanged.Emit(() => new ValueChangedEvent<bool>(oldValue, _hasChanged));
                if (EntityState != null)
                {
                    EntityState.CheckHasChanged();
                }
            }
        }

        private void UpdateHasChangedSinceSnapshot()
        {
            var oldValue = _hasChangedSinceSnapshot;
            _hasChangedSinceSnapshot = CalculateHasChanged(SnapshotValue, ChangeCalculationKind.Snapshot);
            if (oldValue != _hasChangedSinceSnapshot)
            {
                if (EntityState != null)
                {
                    EntityState.DataTracker.NotifyChangedSinceSnapshotChanged(this, _hasChangedSinceSnapshot);
                }
                HasChangedSinceSnapshotChanged.Emit(() => new ValueChangedEvent<bool>(oldValue, _hasChangedSinceSnapshot));
                if (EntityState != null)
                {
                    EntityState.CheckHasChanged();
                }
            }
            CanUndo = (_snapshotValueSet && HasChangedSinceSnapshot) || HasChanged;
        }

        public EventEmitter<ValueChangedEvent<bool>> CanUndoChanged { get; } = new EventEmitter<ValueChangedEvent<bool>>();
        public EventEmitter<ValueChangedEvent<bool>> HasChangedChanged { get; } = new EventEmitter<ValueChangedEvent<bool>>();
        public EventEmitter<ValueChangedEvent<bool>> HasChangedSinceSnapshotChanged { get; } = new EventEmitter<ValueChangedEvent<bool>>();
        public EventEmitter<ValueChangedEvent<object>> RemoteValueChanged { get; } = new EventEmitter<ValueChangedEvent<object>>();
        public EventEmitter<ValueChangedEvent<object>> LocalValueChanged { get; } = new EventEmitter<ValueChangedEvent<object>>();

        public IPropertyState[] SiblingStates
        {
            get
            {
                if (_siblingStates == null)
                {
                    if (GroupStates == null)
                    {
                        return null;
                    }

                    _siblingStates = GroupStates.Where(_ => _ != this).ToArray();
                }

                return _siblingStates;
            }
        }

        public IPropertyState[] GroupStates
        {
            get
            {
                if (_groupStates == null)
                {
                    if (EntityState == null)
                    {
                        return null;
                    }

                    if (Property.Kind.HasFlag(PropertyKind.Relationship) && Property.Relationship.ThisIsTarget)
                    {
                        _groupStates = new IPropertyState[] { this };
                    }
                    else
                    {
                        if (Property.PropertyGroup == null)
                        {
                            _groupStates = new IPropertyState[] { this };
                        }
                        else
                        {
                            var properties = Property.PropertyGroup.GetGroupProperties();
                            _groupStates = properties.Where(_ => _.GroupKind == IqlPropertyGroupKind.Primitive)
                                .Select(_ => EntityState.GetPropertyState(((IProperty)_).PropertyName))
                                .Where(_ => _ != null)
                                .ToArray();
                        }
                    }
                }
                return _groupStates;
            }
        }

        public IEntityStateBase EntityState { get; }
        public IProperty Property { get; }
        public string Data { get; set; }

        public Guid Guid { get; set; }

        public void HardReset()
        {
            var newValue = Property.GetValue(EntityState.Entity);
            _remoteValueSet = false;
            _localValueSet = false;
            EnsureRemoteValue();
            LocalValue = newValue;
            UpdateHasChanged();
        }

        public void SoftReset()
        {
            if (!HasChanged)
            {
                HardReset();
            }
        }

        public IPropertyState Copy()
        {
            return new PropertyState(Property, null)
            {
                RemoteValue = RemoteValue,
                LocalValue = LocalValue,
                Data = Data
            };
        }

        public void AbandonChanges()
        {
            if (HasChanged)
            {
                var ev = new AbandonChangeEvent(EntityState, this);
                AbandonEvents.EmitStartedAsync(() => ev);
                StateEvents.AbandoningPropertyChange.Emit(() => ev);
                // If collection: restore the original entities to the current list
                if (Property.TypeDefinition.Kind == IqlType.Collection)
                {
                    RestoreList((IList)_remoteValueClone, (IList)LocalValue);
                }
                else
                {
                    EntityState.Entity.SetPropertyValue(Property, _remoteValueClone);
                }
                HardReset();
                AbandonEvents.EmitSuccessAsync(() => ev);
                AbandonEvents.EmitCompletedAsync(() => ev);
                StateEvents.AbandonedPropertyChange.Emit(() => ev);
                //PropertyChanger.ApplyTo(_oldObjectClone, _oldObject);
            }
            ClearStatefulEvents();
            //_hasChanged = false;
        }

        private void RestoreList(IList source, IList destination)
        {
            if (source == null && destination != null)
            {
                destination.Clear();
            }

            if (source != null && destination == null)
            {
                throw new NotSupportedException("Cannot merge non-empty list with null list.");
            }

            var toRemove = new List<object>();
            for (var i = 0; i < source.Count; i++)
            {
                var item = source[i];
                if (!destination.Contains(item))
                {
                    toRemove.Add(item);
                }
            }
            var toAdd = new List<object>();
            for (var i = 0; i < destination.Count; i++)
            {
                var item = destination[i];
                if (!source.Contains(item))
                {
                    toAdd.Add(item);
                }
            }

            for (var i = 0; i < toRemove.Count; i++)
            {
                var item = toRemove[i];
                destination.Remove(item);
            }

            for (var i = 0; i < toAdd.Count; i++)
            {
                var item = toAdd[i];
                destination.Add(item);
            }
        }

        public void ClearStatefulEvents()
        {
            Data = null;
            StatefulSaveEvents.UnsubscribeAll();
        }

        public void Restore(Conversion.State.SerializedPropertyState state)
        {
            LocalValue = Property.TypeDefinition.EnsureValueType(state.LocalValue);
            RemoteValue = Property.TypeDefinition.EnsureValueType(state.RemoteValue);
            Property.SetValue(EntityState.Entity, LocalValue);
            Data = state.Data;
            if (!string.IsNullOrWhiteSpace(state.Guid))
            {
                Guid = new Guid(state.Guid);
            }
        }

        public void UndoChange()
        {
            if (EntityState != null)
            {
                EntityState.DataTracker.UndoChanges(new[] { EntityState.Entity }, new[] { Property });
            }
            else
            {
                LocalValue = RemoteValue;
            }
        }

        public string SerializeToJson()
        {
            return this.ToJson();
        }

        public object PrepareForJson()
        {
            if (Property.Kind.HasFlag(PropertyKind.Count) ||
                Property.Kind.HasFlag(PropertyKind.Relationship))
            {
                return null;
            }
            return new
            {
                RemoteValue,
                LocalValue = Property.GetValue(EntityState.Entity),
                Property = Property.PropertyName,
                Data = Data,
                Guid = Guid
            };
        }

        public void Dispose()
        {
            HasChangedChanged?.Dispose();
            RemoteValueChanged?.Dispose();
            LocalValueChanged?.Dispose();
        }
    }
}