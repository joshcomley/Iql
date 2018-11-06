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

                return !PropertyChanger.AreEquivalent(OldValue, Property.GetValue(EntityState.Entity));
            }
        }

        public object OldValue
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
                _hasChanged = !PropertyChanger.AreEquivalent(OldValue, NewValue);
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
            if (OldValue != null && NewValue != null && OldValue != NewValue)
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

        public object NewValue
        {
            get => _newValue;
            set
            {
                _newValue = value;
                _hasChanged = !PropertyChanger.AreEquivalent(OldValue, NewValue);
            }
        }

        public IEntityStateBase EntityState { get; }
        public IProperty Property { get; }
        public void Reset()
        {
            var newValue = Property.GetValue(EntityState.Entity);
            _originalValueSet = false;
            EnsureOldValue();
            NewValue = newValue;
        }

        public IPropertyState Copy()
        {
            return new PropertyState(Property, null)
            {
                OldValue = OldValue,
                NewValue = NewValue
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