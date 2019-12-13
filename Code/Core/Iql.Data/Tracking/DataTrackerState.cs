using System;
using System.Collections.Generic;
using System.Linq;
using Iql.Data.Crud.Operations;
using Iql.Data.Extensions;
using Iql.Data.Lists;
using Iql.Data.Tracking.State;
using Iql.Entities;
using Iql.Entities.Events;
using Iql.Events;
using Iql.Extensions;

namespace Iql.Data.Tracking
{
    public class DataTrackerState
    {
        public DataTracker DataTracker { get; }
        public bool TrackNewEntityProperties { get; set; }

        public DataTrackerState(DataTracker dataTracker, bool trackNewEntityProperties)
        {
            DataTracker = dataTracker;
            TrackNewEntityProperties = trackNewEntityProperties;
        }
        private bool _entitiesChangedDelayedInitialized;
        private Dictionary<IEntityStateBase, Tuple<EntityStatus, EntityStatus>> _entitiesChangedDelayed;
        private Dictionary<IEntityStateBase, Tuple<EntityStatus, EntityStatus>> _entitiesChanged { get { if (!_entitiesChangedDelayedInitialized) { _entitiesChangedDelayedInitialized = true; _entitiesChangedDelayed = new Dictionary<IEntityStateBase, Tuple<EntityStatus, EntityStatus>>(); } return _entitiesChangedDelayed; } set { _entitiesChangedDelayedInitialized = true; _entitiesChangedDelayed = value; } }
        private bool _propertiesChangedDelayedInitialized;
        private Dictionary<IPropertyState, Tuple<object, object>> _propertiesChangedDelayed;

        private Dictionary<IPropertyState, Tuple<object, object>> _propertiesChanged { get { if (!_propertiesChangedDelayedInitialized) { _propertiesChangedDelayedInitialized = true; _propertiesChangedDelayed = new Dictionary<IPropertyState, Tuple<object, object>>(); } return _propertiesChangedDelayed; } set { _propertiesChangedDelayedInitialized = true; _propertiesChangedDelayed = value; } }
        private bool _hasChanges;
        private int _propertiesChangedCount = 0;
        private EventEmitter<DataTrackerState> _changed;
        public EventEmitter<DataTrackerState> Changed => _changed = _changed ?? new EventEmitter<DataTrackerState>();

        public int PropertiesChangedCount => _propertiesChangedCount;
        public int EntitiesChangedCount => _entitiesChanged.Count;
        private EventEmitter<ValueChangedEvent<bool, DataTrackerState>> _propertiesChangedChanged;

        public EventEmitter<ValueChangedEvent<bool, DataTrackerState>> PropertiesChangedChanged => _propertiesChangedChanged = _propertiesChangedChanged ?? new EventEmitter<ValueChangedEvent<bool, DataTrackerState>>();
        private EventEmitter<ValueChangedEvent<bool, DataTrackerState>> _entitiesChangedChanged;

        public EventEmitter<ValueChangedEvent<bool, DataTrackerState>> EntitiesChangedChanged => _entitiesChangedChanged = _entitiesChangedChanged ?? new EventEmitter<ValueChangedEvent<bool, DataTrackerState>>();
        private EventEmitter<ValueChangedEvent<bool, DataTrackerState>> _hasChangesChanged;

        public EventEmitter<ValueChangedEvent<bool, DataTrackerState>> HasChangesChanged => _hasChangesChanged = _hasChangesChanged ?? new EventEmitter<ValueChangedEvent<bool, DataTrackerState>>();

        public bool HasChanges
        {
            get => _hasChanges;
            set
            {
                var old = _hasChanges;
                _hasChanges = value;
                if (old != value)
                {
                    _hasChangesChanged.EmitIfExists(() => new ValueChangedEvent<bool, DataTrackerState>(old, value, this));
                }
            }
        }

        public void Clear()
        {
            _propertiesChanged.Clear();
            _entitiesChanged.Clear();
            EmitChanged();
        }

        public Dictionary<IEntityStateBase, EntitySnapshot> GetEntitiesChanged(bool forceOppositeStatus = false)
        {
            var entityStates = _entitiesChanged.Keys.ToArray();
            var dic = new Dictionary<IEntityStateBase, EntitySnapshot>();
            for (var i = 0; i < entityStates.Length; i++)
            {
                var state = entityStates[i];
                dic.Add(state, new EntitySnapshot
                {
                    PreviousValue = forceOppositeStatus ? state.Status.Opposite() : state.Status,
                    CurrentValue = state.Status,
                    State = state
                });
            }

            return dic;
        }

        public Dictionary<IPropertyState, PropertySnapshot> GetPropertiesChanged(bool allowRelationshipCollections = false)
        {
            var lookup = new Dictionary<IPropertyState, PropertySnapshot>();
            foreach (var propertyChanged in _propertiesChanged)
            {
                var propertyState = propertyChanged.Key;
                if (!TrackNewEntityProperties && propertyState.EntityState.IsNew)
                {
                    continue;
                }
                if (_entitiesChanged.ContainsKey(propertyState.EntityState) &&
                    _entitiesChanged[propertyState.EntityState].Item2 == EntityStatus.NewAndDeleted)
                {
                    continue;
                }
                lookup.Add(propertyState, new PropertySnapshot()
                {
                    PreviousValue = propertyChanged.Value.Item1,
                    CurrentValue = propertyChanged.Value.Item2,
                    State = propertyState
                });
            }

            return lookup;
        }

        public IPropertyState[] GetPropertiesChangedArray(bool allowRelationshipCollections = false)
        {
            var propertiesChanged = GetPropertiesChanged();
            return propertiesChanged.Keys.ToArray();
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
                        resolved.Item2.Emit(() => new ValueChangedEvent<bool, DataTrackerState>(false, true, this));
                    }
                    EmitChanged();
                }
            }
            else if (lookup.ContainsKey(item))
            {
                lookup.Remove(item);
                //if (!PauseEvents)
                {
                    resolved.Item2.Emit(() => new ValueChangedEvent<bool, DataTrackerState>(true, false, this));
                }
                EmitChanged();
            }
        }

        private void EmitChanged()
        {
            _propertiesChangedCount = GetPropertiesChangedArray().Length;
            HasChanges = PropertiesChangedCount > 0 ||
                         EntitiesChangedCount > 0;
            //if (!PauseEvents)
            {
                _changed.EmitIfExists(() => this);
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

        public void MergeWithSnapshot(TrackerSnapshot snapshot)
        {
            MergeWithInternal(snapshot, true);
        }

        public void MergeWithState(DataTrackerState state)
        {
            var propertiesChanged = state.GetPropertiesChanged();
            var entitiesChanged = state.GetEntitiesChanged(false);
            MergeWith(true, entitiesChanged, propertiesChanged, true);
        }

        private void MergeWithInternal(TrackerSnapshot snapshot, bool add)
        {
            MergeWith(add, snapshot.Entities, snapshot.Values);
        }

        private void MergeWith(
            bool add,
            Dictionary<IEntityStateBase, EntitySnapshot> entitiesChanged,
            Dictionary<IPropertyState, PropertySnapshot> propertiesChanged,
            bool cloneRelationshipCollectionState = false)
        {
            foreach (var snapshotEntity in entitiesChanged)
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

            foreach (var snapshotProperty in propertiesChanged)
            {
                if (snapshotProperty.Key.IsRelationshipCollection)
                {
                    continue;
                }
                if (_propertiesChanged.ContainsKey(snapshotProperty.Key))
                {
                    _propertiesChanged.Remove(snapshotProperty.Key);
                }

                if (add)
                {
                    snapshotProperty.Key.SetSnapshotValue(snapshotProperty.Value.PreviousValue);
                    //if (_propertiesChanged.ContainsKey(snapshotProperty.Key))
                    //{
                    //    _propertiesChanged.Remove(snapshotProperty.Key);
                    //}
                    //_propertiesChanged.Add(
                    //    snapshotProperty.Key,
                    //    new Tuple<object, object>(
                    //        snapshotProperty.Value.PreviousValue,
                    //        snapshotProperty.Value.CurrentValue
                    //    )
                    //);
                }
            }
            foreach (var snapshotProperty in propertiesChanged)
            {
                if (!snapshotProperty.Key.IsRelationshipCollection)
                {
                    continue;
                }
                if (_propertiesChanged.ContainsKey(snapshotProperty.Key))
                {
                    _propertiesChanged.Remove(snapshotProperty.Key);
                }

                if (add)
                {
                    if (!cloneRelationshipCollectionState)
                    {
                        snapshotProperty.Key.SetSnapshotValue(snapshotProperty.Value.PreviousValue);
                    }
                    else
                    {
                        var all = new List<object>();
                        var list = (IRelatedList)snapshotProperty.Key.LocalValue;
                        if (list != null)
                        {
                            var added = snapshotProperty.Key.ItemsAdded.Select(_ => _.Entity).ToArray();
                            foreach (var item in list)
                            {
                                if (!added.Contains(item))
                                {
                                    all.Add(item);
                                }
                            }
                        }
                        all.AddRange(snapshotProperty.Key.ItemsRemoved.Select(_ => _.Entity));
                        snapshotProperty.Key.SetSnapshotValue(all);
                    }
                    //if (_propertiesChanged.ContainsKey(snapshotProperty.Key))
                    //{
                    //    _propertiesChanged.Remove(snapshotProperty.Key);
                    //}
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
