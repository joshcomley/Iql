using System;
using Iql.Data.Events;
using Iql.Entities.Events;
using Iql.Events;

namespace Iql.Data
{
    public class EntityBase : IEntity
    {
        protected Boolean _propertyChangingSet;
        protected EventEmitter<IPropertyChangeEvent> _propertyChanging;
        public EventEmitter<IPropertyChangeEvent> PropertyChanging
        {
            get => _propertyChanging;
            set
            {
                _propertyChanging = value;
                this._propertyChangingSet = value != null;
            }
        }
        protected Boolean _propertyChangedSet;
        protected EventEmitter<IPropertyChangeEvent> _propertyChanged;
        public EventEmitter<IPropertyChangeEvent> PropertyChanged
        {
            get => _propertyChanged;
            set
            {
                _propertyChanged = value;
                this._propertyChangedSet = value != null;
            }
        }
        protected Boolean _existsChangedSet;
        protected EventEmitter<ExistsChangeEvent> _existsChanged;
        public EventEmitter<ExistsChangeEvent> ExistsChanged
        {
            get => _existsChanged;
            set
            {
                _existsChanged = value;
                this._existsChangedSet = value != null;
            }
        }

        protected void SetPropertyValue<T, TProperty>(string propertyName, TProperty oldValue, TProperty value, Action setPrivate)
        {
            var changedSet = false;
            var changed = false;
            if (this._propertyChangingSet)
            {
                changed = !Equals(value, oldValue);
                changedSet = true;
                if (changed)
                {
                    this.PropertyChanging.Emit(() => new PropertyChangeEvent<T>(propertyName, this, oldValue, value));
                }
            }

            setPrivate();

            if (this._propertyChangedSet)
            {
                if (!(changedSet))
                {
                    changed = !Equals(value, oldValue);
                }
                if (changed)
                {
                    this.PropertyChanged.Emit(() => new PropertyChangeEvent<T>(propertyName, this, oldValue, value));
                }
            }
        }
    }
}