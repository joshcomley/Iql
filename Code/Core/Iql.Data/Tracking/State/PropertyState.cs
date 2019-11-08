using System;
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
            get { return _propertyChanger = _propertyChanger ?? Property.TypeDefinition.ResovleChanger(); }
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

        private bool CalculateHasChanged(object remoteValue)
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
                    return RelationshipHasChanged(remoteValue);
                }

                return false;
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

        private bool RelationshipHasChanged(object remoteValue)
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
            _snapshotValue = value;
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

        internal void UpdateHasChanged()
        {
            UpdateHasChangedSinceStart();
            UpdateHasChangedSinceSnapshot();
        }

        private void UpdateHasChangedSinceStart()
        {
            var oldValue = _hasChanged;
            _hasChanged = CalculateHasChanged(RemoteValue);
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
            _hasChangedSinceSnapshot = CalculateHasChanged(SnapshotValue);
            if (oldValue != _hasChangedSinceSnapshot)
            {
                if (EntityState != null)
                {
                    EntityState.DataTracker.NotifyChangedSinceSnapshot(this, _hasChangedSinceSnapshot);
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
                EntityState.Entity.SetPropertyValue(Property, _remoteValueClone);
                HardReset();
                AbandonEvents.EmitSuccessAsync(() => ev);
                AbandonEvents.EmitCompletedAsync(() => ev);
                StateEvents.AbandonedPropertyChange.Emit(() => ev);
                //PropertyChanger.ApplyTo(_oldObjectClone, _oldObject);
            }
            ClearStatefulEvents();
            //_hasChanged = false;
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
            if (this.EntityState != null)
            {
                this.EntityState.DataTracker.UndoChanges(new[] { EntityState.Entity }, new[] { Property });
            }
            else
            {
                this.LocalValue = RemoteValue;
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