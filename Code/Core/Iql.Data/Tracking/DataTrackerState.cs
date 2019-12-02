using System;
using System.Collections.Generic;
using System.Linq;
using Iql.Data.Crud.Operations;
using Iql.Data.Extensions;
using Iql.Data.Tracking.State;
using Iql.Entities;
using Iql.Entities.Events;
using Iql.Events;

namespace Iql.Data.Tracking
{
    public class DataTrackerState
    {
        public bool TrackNewEntityProperties { get; set; }

        public DataTrackerState(bool trackNewEntityProperties)
        {
            TrackNewEntityProperties = trackNewEntityProperties;
        }
        private bool _entitiesChangedDelayedInitialized;
        private Dictionary<IEntityStateBase, Tuple<EntityStatus, EntityStatus>> _entitiesChangedDelayed;
        private Dictionary<IEntityStateBase, Tuple<EntityStatus, EntityStatus>> _entitiesChanged { get { if(!_entitiesChangedDelayedInitialized) { _entitiesChangedDelayedInitialized = true; _entitiesChangedDelayed =             new Dictionary<IEntityStateBase, Tuple<EntityStatus, EntityStatus>>(); } return _entitiesChangedDelayed; } set { _entitiesChangedDelayedInitialized = true; _entitiesChangedDelayed = value; } }
        private bool _propertiesChangedDelayedInitialized;
        private Dictionary<IPropertyState, Tuple<object, object>> _propertiesChangedDelayed;

        private Dictionary<IPropertyState, Tuple<object, object>> _propertiesChanged { get { if(!_propertiesChangedDelayedInitialized) { _propertiesChangedDelayedInitialized = true; _propertiesChangedDelayed = new Dictionary<IPropertyState, Tuple<object, object>>(); } return _propertiesChangedDelayed; } set { _propertiesChangedDelayedInitialized = true; _propertiesChangedDelayed = value; } }
        private bool _hasChanges;
        private int _propertiesChangedCount = 0;
        private EventEmitter<DataTrackerState> _changed;
        public EventEmitter<DataTrackerState> Changed => _changed = _changed ?? new EventEmitter<DataTrackerState>();

        public int PropertiesChangedCount => _propertiesChangedCount;
        public int EntitiesChangedCount => _entitiesChanged.Count;
        private EventEmitter<ValueChangedEvent<bool>> _propertiesChangedChanged;

        public EventEmitter<ValueChangedEvent<bool>> PropertiesChangedChanged => _propertiesChangedChanged = _propertiesChangedChanged ?? new EventEmitter<ValueChangedEvent<bool>>();
        private EventEmitter<ValueChangedEvent<bool>> _entitiesChangedChanged;

        public EventEmitter<ValueChangedEvent<bool>> EntitiesChangedChanged => _entitiesChangedChanged = _entitiesChangedChanged ?? new EventEmitter<ValueChangedEvent<bool>>();
        private EventEmitter<ValueChangedEvent<bool>> _hasChangesChanged;

        public EventEmitter<ValueChangedEvent<bool>> HasChangesChanged => _hasChangesChanged = _hasChangesChanged ?? new EventEmitter<ValueChangedEvent<bool>>();

        public bool HasChanges
        {
            get => _hasChanges;
            set
            {
                var old = _hasChanges;
                _hasChanges = value;
                if (old != value)
                {
                    HasChangesChanged.Emit(() => new ValueChangedEvent<bool>(old, value));
                }
            }
        }

        public void Clear()
        {
            _propertiesChanged.Clear();
            _entitiesChanged.Clear();
            EmitChanged();
        }

        public Dictionary<IEntityStateBase, EntitySnapshot> GetEntitiesChanged()
        {
            var entityStates = _entitiesChanged.Keys.ToArray();
            var dic = new Dictionary<IEntityStateBase, EntitySnapshot>();
            for (var i = 0; i < entityStates.Length; i++)
            {
                var state = entityStates[i];
                dic.Add(state, new EntitySnapshot
                {
                    PreviousValue = state.Status.Opposite(),
                    CurrentValue = state.Status,
                    State = state
                });
            }

            return dic;
        }

        public IPropertyState[] GetPropertiesChanged(bool allowRelationshipCollections = false)
        {
            return _propertiesChanged.Keys.Where(_ =>
            {
                //if (!allowRelationshipCollections && _.Property.Relationship != null && _.Property.TypeDefinition.IsCollection)
                //{
                //    return false;
                //}
                if (!TrackNewEntityProperties && _.EntityState.IsNew)
                {
                    return false;
                }
                if (_entitiesChanged.ContainsKey(_.EntityState) &&
                    _entitiesChanged[_.EntityState].Item2 == EntityStatus.NewAndDeleted)
                {
                    return false;
                }

                return true;
            }).ToArray();
        }

        public void UpdateStatusChanged<T>(T item, bool value, EntityStatus oldValue, EntityStatus newValue)
            where T : IEntityStateBase
        {
            if (_entitiesChanged.ContainsKey(item))
            {
                var existingValue = _entitiesChanged[item].Item1;
                if (existingValue == newValue.Opposite() ||
                    existingValue.AreOpposing(newValue))
                {
                    _entitiesChanged.Remove(item);
                    EmitChanged();
                }
            }
            else
            {
                UpdateInternal<IEntityStateBase, EntityStatus>(DataTrackerStateKind.EntityStatus, item, value, oldValue, newValue);
            }
        }

        public void RemovePropertyChange<T>(T item)
            where T : IPropertyState
        {
            ResolveLookup<IPropertyState, object>(DataTrackerStateKind.Property).Item1.Remove(item);
            EmitChanged();
        }

        public void UpdatePropertyChanged<T>(T item, bool value, object oldValue, object newValue)
            where T : IPropertyState
        {
            UpdateInternal<IPropertyState, object>(DataTrackerStateKind.Property, item, value, oldValue, newValue);
        }

        public void Update<TLookup>(DataTrackerStateKind changeKind, TLookup item, bool value, object oldValue, object newValue)
        {
            switch (changeKind)
            {
                case DataTrackerStateKind.EntityStatus:
                    UpdateStatusChanged((IEntityStateBase)item, value, (EntityStatus)oldValue, (EntityStatus)newValue);
                    break;
                case DataTrackerStateKind.Property:
                    UpdatePropertyChanged((IPropertyState)item, value, oldValue, newValue);
                    break;
            }
        }

        protected void UpdateInternal<TLookup, TValue>(DataTrackerStateKind changeKind, TLookup item, bool value, TValue oldValue, TValue newValue)
        {
            var resolved = ResolveLookup<TLookup, TValue>(changeKind);
            var lookup = resolved.Item1;
            if (value)
            {
                if (!lookup.ContainsKey(item))
                {
                    lookup.Add(item, new Tuple<TValue, TValue>(oldValue, newValue));
                    //if (!PauseEvents)
                    {
                        resolved.Item2.Emit(() => new ValueChangedEvent<bool>(false, true));
                    }
                    EmitChanged();
                }
            }
            else if (lookup.ContainsKey(item))
            {
                lookup.Remove(item);
                //if (!PauseEvents)
                {
                    resolved.Item2.Emit(() => new ValueChangedEvent<bool>(true, false));
                }
                EmitChanged();
            }
        }

        private void EmitChanged()
        {
            _propertiesChangedCount = GetPropertiesChanged().Length;
            HasChanges = PropertiesChangedCount > 0 ||
                         EntitiesChangedCount > 0;
            //if (!PauseEvents)
            {
                Changed.Emit(() => this);
            }
        }

        private Tuple<IDictionary<TLookup, Tuple<TValue, TValue>>, IEventEmitterBase> ResolveLookup<TLookup, TValue>(
            DataTrackerStateKind changeKind)
        {
            IDictionary<TLookup, Tuple<TValue, TValue>> lookup = null;
            IEventEmitterBase eventEmitter = null;
            switch (changeKind)
            {
                case DataTrackerStateKind.EntityStatus:
                    lookup = (IDictionary<TLookup, Tuple<TValue, TValue>>)_entitiesChanged;
                    eventEmitter = EntitiesChangedChanged;
                    break;
                case DataTrackerStateKind.Property:
                    lookup = (IDictionary<TLookup, Tuple<TValue, TValue>>)_propertiesChanged;
                    eventEmitter = PropertiesChangedChanged;
                    break;
            }

            return new Tuple<IDictionary<TLookup, Tuple<TValue, TValue>>, IEventEmitterBase>(lookup, eventEmitter);
        }

        public bool Has<TLookup>(DataTrackerStateKind changeKind, TLookup item)
        {
            switch (changeKind)
            {
                case DataTrackerStateKind.EntityStatus:
                    return ResolveLookup<IEntityStateBase, EntityStatus>(changeKind).Item1.ContainsKey((IEntityStateBase)item);
                case DataTrackerStateKind.Property:
                    return ResolveLookup<IPropertyState, object>(changeKind).Item1.ContainsKey((IPropertyState)item);
            }

            return false;
        }

        public bool UndoChanges(object[] allowedEntities = null, object[] allowedProperties = null)
        {
            bool IsEntityAllowed(IEntityStateBase entityState)
            {
                if (allowedEntities == null)
                {
                    return true;
                }

                return allowedEntities.Contains(entityState.Entity) || allowedEntities.Contains(entityState);
            }
            bool IsPropertyAllowed(IPropertyState propertyState)
            {
                if (propertyState.Property.Relationship != null &&
                    propertyState.Property.TypeDefinition.IsCollection)
                {
                    return false;
                }
                if (allowedProperties == null)
                {
                    return true;
                }

                return allowedProperties.Contains(propertyState.Property) || allowedProperties.Contains(propertyState);
            }
            var entityStates = _entitiesChanged.Keys.Select(_ => new
            {
                State = _,
                Value = _entitiesChanged[_]
            }).ToArray();
            var propertyStates = _propertiesChanged.Keys.Select(_ => new
            {
                State = _,
                Value = _propertiesChanged[_]
            }).ToArray();

            // Restore tracking first, if need be
            for (var i = 0; i < entityStates.Length; i++)
            {
                var entityState = entityStates[i];
                if (IsEntityAllowed(entityState.State) &&
                    entityState.Value.Item1.WillUntrack())
                {
                    entityState.State.Status = entityState.Value.Item1.Opposite();
                    _entitiesChanged.Remove(entityState.State);
                }
            }

            for (var i = 0; i < propertyStates.Length; i++)
            {
                var propertyState = propertyStates[i];
                if (IsEntityAllowed(propertyState.State.EntityState) &&
                    IsPropertyAllowed(propertyState.State))
                {
                    propertyState.State.Property.SetValue(propertyState.State.EntityState.Entity,
                        propertyState.Value.Item1);
                    _propertiesChanged.Remove(propertyState.State);
                }
            }

            for (var i = 0; i < entityStates.Length; i++)
            {
                var entityState = entityStates[i];
                if (IsEntityAllowed(entityState.State))
                {
                    entityState.State.Status = entityState.Value.Item1.Opposite();
                    _entitiesChanged.Remove(entityState.State);
                }
            }

            //if (allowedProperties != null)
            //{
            //    foreach (var property in allowedProperties)
            //    {
            //        IProperty p = null;
            //        if (property is PropertyBase pb)
            //        {
            //            p = (IProperty) pb;
            //        }
            //        else
            //        {
            //            p = ((IPropertyState) property).Property;
            //        }
            //    }
            //}

            EmitChanged();
            return true;
        }

        public void MergeWith(TrackerSnapshot snapshot)
        {
            MergeWithInternal(snapshot, true);
        }

        private void MergeWithInternal(TrackerSnapshot snapshot, bool add)
        {
            foreach (var snapshotEntity in snapshot.Entities)
            {
                if (_entitiesChanged.ContainsKey(snapshotEntity.Key))
                {
                    _entitiesChanged.Remove(snapshotEntity.Key);
                }

                if (add)
                {
                    snapshotEntity.Key.SetSnapshotValue(snapshotEntity.Value.PreviousValue);
                    _entitiesChanged.Add(
                        snapshotEntity.Key,
                        new Tuple<EntityStatus, EntityStatus>(
                            snapshotEntity.Value.PreviousValue,
                            snapshotEntity.Value.CurrentValue
                        )
                    );
                }
            }
            foreach (var snapshotProperty in snapshot.Values)
            {
                if (_propertiesChanged.ContainsKey(snapshotProperty.Key))
                {
                    _propertiesChanged.Remove(snapshotProperty.Key);
                }

                if (add)
                {
                    snapshotProperty.Key.SetSnapshotValue(snapshotProperty.Value.PreviousValue);
                    //_propertiesChanged.Add(
                    //    snapshotProperty.Key,
                    //    new Tuple<object, object>(
                    //        snapshotProperty.Value.PreviousValue,
                    //        snapshotProperty.Value.CurrentValue
                    //    )
                    //);
                }
            }
            EmitChanged();
        }

        public void RemoveMatching(TrackerSnapshot snapshot)
        {
            MergeWithInternal(snapshot, false);
        }

        public EntityStatus? GetEntityStatus<T>(EntityState<T> entityState) where T : class
        {
            if (_entitiesChanged.ContainsKey(entityState))
            {
                return _entitiesChanged[entityState].Item2;
            }

            return null;
        }
    }
}