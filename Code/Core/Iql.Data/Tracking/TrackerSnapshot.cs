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
        private bool _valuesInitialized;
        private Dictionary<IPropertyState, PropertySnapshot> _values;
        public Dictionary<IPropertyState, PropertySnapshot> Values { get { if(!_valuesInitialized) { _valuesInitialized = true; _values = new Dictionary<IPropertyState, PropertySnapshot>(); } return _values; } set { _valuesInitialized = true; _values = value; } }
        private bool _entitiesInitialized;
        private Dictionary<IEntityStateBase, EntitySnapshot> _entities;
        public Dictionary<IEntityStateBase, EntitySnapshot> Entities { get { if(!_entitiesInitialized) { _entitiesInitialized = true; _entities = new Dictionary<IEntityStateBase, EntitySnapshot>(); } return _entities; } set { _entitiesInitialized = true; _entities = value; } }
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
            }
        }

        public EntityStatus CurrentValue
        {
            get => _currentValue;
            set
            {
                _currentValueSet = true;
                _currentValue = value;
            }
        }
    }
}