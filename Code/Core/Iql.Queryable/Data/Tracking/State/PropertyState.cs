using System.Diagnostics;
using Iql.Queryable.Data.Crud.Operations;
using Iql.Queryable.Data.EntityConfiguration;

namespace Iql.Queryable.Data.Tracking.State
{
    [DebuggerDisplay("{Property.Name}")]
    public class PropertyState : IPropertyState
    {
        private bool _hasChanged;
        private object _oldValue;
        private bool _originalValueSet = false;
        private object _newValue;

        public PropertyState(
            IProperty property, 
            IEntityStateBase entityState)
        {
            Property = property;
            EntityState = entityState;
        }

        public bool HasChanged => _hasChanged;

        public object OldValue
        {
            get
            {
                if (!_originalValueSet)
                {
                    _originalValueSet = true;
                    if (EntityState != null)
                    {
                        _oldValue = Property.PropertyGetter(EntityState.Entity);
                    }
                }
                return _oldValue;
            }
            set
            {
                _oldValue = value;
                _hasChanged = !Equals(OldValue, NewValue);
            }
        }

        public object NewValue
        {
            get => _newValue;
            set
            {
                _newValue = value;
                _hasChanged = !Equals(OldValue, NewValue);
                (EntityState as IEntityStateInternal)?.UpdateChanged(this);
            }
        }

        public IEntityStateBase EntityState { get; }
        public IProperty Property { get; }
        public void Reset()
        {
            OldValue = Property.PropertyGetter(EntityState.Entity);
            NewValue = OldValue;
        }
    }
}