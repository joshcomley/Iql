using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using Iql.Data.Crud.Operations;
using Iql.Data.Tracking.State;

namespace Iql.Data.Tracking
{
    public class TrackerSnapshot
    {
        public DateTimeOffset Date { get; set; } = DateTimeOffset.Now;
        public Guid Id { get; set; }
        private Dictionary<IPropertyState, PropertySnapshot> _values = null;
        public Dictionary<IPropertyState, PropertySnapshot> Values { get => _values = _values ?? new Dictionary<IPropertyState, PropertySnapshot>(); set => _values = value; }
        private Dictionary<IEntityStateBase, EntitySnapshot> _entities = null;
        public Dictionary<IEntityStateBase, EntitySnapshot> Entities { get => _entities = _entities ?? new Dictionary<IEntityStateBase, EntitySnapshot>(); set => _entities = value; }
    }

    public class PropertySnapshot
    {
        public IPropertyState State { get; set; }
        public object PreviousValue { get; set; }
        public object CurrentValue { get; set; }
    }
    public class EntitySnapshot
    {
        private EntityStatus _previousValue;
        private EntityStatus _currentValue;
        public IEntityStateBase State { get; set; }
        private bool _previousValueSet = false;
        private bool _currentValueSet = false;
        public EntityStatus PreviousValue
        {
            get => _previousValue;
            set
            {
                _previousValueSet = true;
                _previousValue = value;
                Check();
            }
        }

        public EntityStatus CurrentValue
        {
            get => _currentValue;
            set
            {
                _currentValueSet = true;
                _currentValue = value;
                Check();
            }
        }

        private void Check()
        {
            if (_previousValueSet && _currentValueSet && PreviousValue == CurrentValue)
            {
                int a = 0;
            }
        }
    }
}