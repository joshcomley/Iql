using System.Diagnostics;
using Iql.Data.Crud.Operations;
using Iql.Data.Extensions;
using Iql.Entities;
using Iql.Entities.Extensions;
using Iql.Entities.PropertyChangers;

namespace Iql.Data.Tracking.State
{
    [DebuggerDisplay("{Property.Name}")]
    public class PropertyState : IPropertyState
    {
        private bool _hasChanged;
        private object _oldObjectClone;
        private bool _originalValueSet;
        private object _newValue;

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
        private object _oldObject;

        private PropertyChanger PropertyChanger
        {
            get { return _propertyChanger = _propertyChanger ?? Property.TypeDefinition.ResovleChanger(); }
        }

        public bool HasChanged
        {
            get
            {
                if (Property.Kind.HasFlag(PropertyKind.Count))
                {
                    return false;
                }

                if (Property.Kind.HasFlag(PropertyKind.Relationship))
                {
                    return RelationshipHasChanged();
                }

                if (PropertyChanger is PrimitivePropertyChanger)
                {
                    return _hasChanged;
                }

                return !PropertyChanger.AreEquivalent(RemoteValue, Property.GetValue(EntityState.Entity));
            }
        }

        private void EnsureOldValue()
        {
            if (!_originalValueSet)
            {
                _originalValueSet = true;
                if (EntityState != null)
                {
                    _oldObject = Property.GetValue(EntityState.Entity);
                    _oldObjectClone = PropertyChanger.CloneValue(_oldObject);
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
                return _oldObjectClone;
            }
            set
            {
                _originalValueSet = true;
                _oldObjectClone = PropertyChanger.CloneValue(value);
                _oldObject = value;
                UpdateHasChanged();
            }
        }

        public object LocalValue
        {
            get => _newValue;
            set
            {
                _newValue = value;
                UpdateHasChanged();
            }
        }

        private void UpdateHasChanged()
        {
            _hasChanged = !PropertyChanger.AreEquivalent(RemoteValue, LocalValue);
        }

        public IEntityStateBase EntityState { get; }
        public IProperty Property { get; }
        public void Reset()
        {
            var newValue = Property.GetValue(EntityState.Entity);
            _originalValueSet = false;
            EnsureOldValue();
            LocalValue = newValue;
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
                EntityState.Entity.SetPropertyValue(Property, _oldObjectClone);
                Reset();
                //PropertyChanger.ApplyTo(_oldObjectClone, _oldObject);
            }
            //_hasChanged = false;
        }
    }
}