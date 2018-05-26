using System.Diagnostics;
using Iql.Data.Crud.Operations;
using Iql.Entities;

namespace Iql.Data.Tracking.State
{
    [DebuggerDisplay("{Property.Name}")]
    public class PropertyState : IPropertyState
    {
        private bool _hasChanged;
        private object _oldValue;
        private bool _originalValueSet;
        private object _newValue;

        public PropertyState(
            IProperty property,
            IEntityStateBase entityState)
        {
            Property = property;
            EntityState = entityState;
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

                return _hasChanged;
            }
        }

        public object OldValue
        {
            get
            {
                if (!_originalValueSet)
                {
                    _originalValueSet = true;
                    if (EntityState != null)
                    {
                        _oldValue = Property.GetValue(EntityState.Entity);
                    }
                }
                return _oldValue;
            }
            set
            {
                _originalValueSet = true;
                _oldValue = value;
                _hasChanged = !Equals(OldValue, NewValue);
            }
        }

        private bool RelationshipHasChanged()
        {
            if (OldValue != null && NewValue != null && OldValue != NewValue)
            {
                return true;
            }

            var constraints = Property.Relationship.ThisEnd.Constraints();
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
                _hasChanged = !Equals(OldValue, NewValue);
            }
        }

        public IEntityStateBase EntityState { get; }
        public IProperty Property { get; }
        public void Reset()
        {
            OldValue = Property.GetValue(EntityState.Entity);
            NewValue = OldValue;
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
            NewValue = OldValue;
        }
    }
}