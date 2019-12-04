using System;
using System.Collections.Generic;
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

        public void Empty()
        {
            _values?.Clear();
            _entities?.Clear();
        }
    }

    public class PropertySnapshot
    {
        public IPropertyState State { get; set; }
        public object PreviousValue { get; set; }
        public object CurrentValue { get; set; }
    }

    public class EntitySnapshot
    {
        public IEntityStateBase State { get; set; }
        public EntityStatus PreviousValue { get; set; }
        public EntityStatus CurrentValue { get; set; }
    }
}