using System.Diagnostics;
using Iql.Conversion;
using Iql.Data.Crud.Operations;
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
        private bool _hasChanged;
        private object _remoteValueClone;
        private bool _originalValueSet;
        private object _localValue;

        public PropertyState(
            IProperty property,
            IEntityStateBase entityState)
        {
            Property = property;
            EntityState = entityState;
            if (!(PropertyChanger is PrimitivePropertyChanger))
            {
                // Ensure it is loaded
                EnsureOldValue();
            }
        }

        private PropertyChanger _propertyChanger;
        private object _remoteValue;

        private PropertyChanger PropertyChanger
        {
            get { return _propertyChanger = _propertyChanger ?? Property.TypeDefinition.ResovleChanger(); }
        }

        public string HasChangedText { get; private set; }
        public bool HasChanged
        {
            get
            {
                //if (Property.IsReadOnly)
                //{
                //    HasChangedText = "Read only";
                //    return false;
                //}

                if (Property.Kind.HasFlag(PropertyKind.Count))
                {
                    HasChangedText = "Count field";
                    return false;
                }

                if (Property.Kind.HasFlag(PropertyKind.Relationship))
                {
                    HasChangedText = "Relationship field";
                    return RelationshipHasChanged();
                }

                if (PropertyChanger is PrimitivePropertyChanger)
                {
                    HasChangedText = "_hasChanged";
                    return _hasChanged;
                }

                HasChangedText = $"PropertyChanger: {PropertyChanger.GetType().Name}";
                return !PropertyChanger.AreEquivalent(RemoteValue, Property.GetValue(EntityState.Entity));
            }
        }

        private void EnsureOldValue()
        {
            if (EntityState != null &&
                _originalValueSet &&
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
                        _originalValueSet = true;
                    }
                }
            }
            if (!_originalValueSet)
            {
                _originalValueSet = true;
                if (EntityState != null)
                {
                    SetRemoteValue(Property.GetValue(EntityState.Entity));
                }
            }
        }

        private bool RelationshipHasChanged()
        {
            if (RemoteValue != null && LocalValue != null && RemoteValue != LocalValue)
            {
                return true;
            }

            var constraints = Property.Relationship.ThisEnd.Constraints;
            for (var i = 0; i < constraints.Length; i++)
            {
                var constraint = constraints[i];
                if (EntityState.GetPropertyState(constraint.Name).HasChanged)
                {
                    return true;
                }
            }

            return false;
        }

        public object RemoteValue
        {
            get
            {
                EnsureOldValue();
                return _remoteValueClone;
            }
            set
            {
                _originalValueSet = true;
                SetRemoteValue(value);
                UpdateHasChanged();
            }
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

        private void UpdateHasChanged()
        {
            var oldValue = _hasChanged;
            _hasChanged = _localValueSet && !PropertyChanger.AreEquivalent(RemoteValue, LocalValue);
            if (oldValue != _hasChanged)
            {
                HasChangedChanged.Emit(() => new ValueChangedEvent<bool>(oldValue, _hasChanged));
            }
        }

        public EventEmitter<ValueChangedEvent<bool>> HasChangedChanged { get; } = new EventEmitter<ValueChangedEvent<bool>>();
        public EventEmitter<ValueChangedEvent<object>> RemoteValueChanged { get; } = new EventEmitter<ValueChangedEvent<object>>();
        public EventEmitter<ValueChangedEvent<object>> LocalValueChanged { get; } = new EventEmitter<ValueChangedEvent<object>>();
        public IEntityStateBase EntityState { get; }
        public IProperty Property { get; }

        public void HardReset()
        {
            var newValue = Property.GetValue(EntityState.Entity);
            _originalValueSet = false;
            _localValueSet = false;
            EnsureOldValue();
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
                LocalValue = LocalValue
            };
        }

        public void AbandonChange()
        {
            if (HasChanged)
            {
                EntityState.Entity.SetPropertyValue(Property, _remoteValueClone);
                HardReset();
                //PropertyChanger.ApplyTo(_oldObjectClone, _oldObject);
            }
            //_hasChanged = false;
        }

        public void Restore(Conversion.State.SerializedPropertyState state)
        {
            LocalValue = Property.TypeDefinition.EnsureValueType(state.LocalValue);
            RemoteValue = Property.TypeDefinition.EnsureValueType(state.RemoteValue);
            Property.SetValue(EntityState.Entity, LocalValue);
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
                Property = Property.PropertyName
            };
        }
    }
}