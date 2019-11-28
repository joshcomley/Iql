using System;
using System.Collections.Generic;
using System.Linq;
using Iql.Data.Crud.Operations;
using Iql.Data.Tracking.State;
using Iql.Entities;
using Iql.Entities.Events;
using Iql.Events;

namespace Iql.Data.Tracking
{
    public class DataTrackerState
    {
        public bool PauseEvents { get; set; }
        private readonly Dictionary<IEntityStateBase, Tuple<EntityStatus, EntityStatus>> _entitiesChanged =
            new Dictionary<IEntityStateBase, Tuple<EntityStatus, EntityStatus>>();

        private readonly Dictionary<IPropertyState, Tuple<object, object>> _propertiesChanged = new Dictionary<IPropertyState, Tuple<object, object>>();
        private bool _hasChanges;
        public EventEmitter<DataTrackerState> Changed { get; } = new EventEmitter<DataTrackerState>();

        public int PropertiesChangedCount => _propertiesChanged.Count;

        public EventEmitter<ValueChangedEvent<bool>> PropertiesChangedChanged { get; } =
            new EventEmitter<ValueChangedEvent<bool>>();

        public int EntitiesChangedCount => _entitiesChanged.Count;

        public EventEmitter<ValueChangedEvent<bool>> EntitiesChangedChanged { get; } =
            new EventEmitter<ValueChangedEvent<bool>>();

        public EventEmitter<ValueChangedEvent<bool>> HasChangesChanged { get; }
            = new EventEmitter<ValueChangedEvent<bool>>();

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
                    PreviousValue = state.SnapshotStatus,
                    CurrentValue = state.Status,
                    State = state
                });
            }

            return dic;
        }

        public IPropertyState[] GetPropertiesChanged()
        {
            return _propertiesChanged.Keys.ToArray();
        }

        public void UpdateStatusChanged<T>(T item, bool value, EntityStatus oldValue, EntityStatus newValue)
            where T : IEntityStateBase
        {
            UpdateInternal<IEntityStateBase, EntityStatus>(DataTrackerStateKind.EntityStatus, item, value, oldValue, newValue);
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

        public bool UndoChanges(object[] allowedEntities = null, IProperty[] allowedProperties = null)
        {
            bool IsEntityAllowed(IEntityStateBase entityState)
            {
                if (allowedEntities == null)
                {
                    return true;
                }

                return allowedEntities.Contains(entityState.Entity);
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

                return allowedProperties.Contains(propertyState.Property);
            }
            var entityStates = _entitiesChanged.Keys.Select(_=>new
            {
                State = _,
                Value = _entitiesChanged[_]
            }).ToArray();
            var propertyStates = _propertiesChanged.Keys.Select(_=>new
            {
                State = _,
                Value = _propertiesChanged[_]
            }).ToArray();

            // Restore tracking first, if need be
            for (var i = 0; i < entityStates.Length; i++)
            {
                var entityState = entityStates[i];
                if (IsEntityAllowed(entityState.State) &&
                    (entityState.Value.Item1 == EntityStatus.Existing ||
                     entityState.Value.Item1 == EntityStatus.ExistingAndPendingDelete ||
                     entityState.Value.Item1 == EntityStatus.New))
                {
                    entityState.State.Status = DataTracker.GetOppositeStatus(entityState.Value.Item1);
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
                    entityState.State.Status = DataTracker.GetOppositeStatus(entityState.Value.Item1);
                    _entitiesChanged.Remove(entityState.State);
                }
            }

            EmitChanged();
            return true;
        }

        public void MergeWith(TrackerSnapshot snapshot)
        {
            foreach (var snapshotEntity in snapshot.Entities)
            {
                if (_entitiesChanged.ContainsKey(snapshotEntity.Key))
                {
                    _entitiesChanged.Remove(snapshotEntity.Key);
                }

                _entitiesChanged.Add(
                    snapshotEntity.Key,
                    new Tuple<EntityStatus, EntityStatus>(
                        snapshotEntity.Value.PreviousValue,
                        snapshotEntity.Value.CurrentValue
                    )
                );
            }
            foreach (var snapshotProperty in snapshot.Values)
            {
                if (_propertiesChanged.ContainsKey(snapshotProperty.Key))
                {
                    _propertiesChanged.Remove(snapshotProperty.Key);
                }

                _propertiesChanged.Add(
                    snapshotProperty.Key,
                    new Tuple<object, object>(
                        snapshotProperty.Value.PreviousValue,
                        snapshotProperty.Value.CurrentValue
                    )
                );
            }
            EmitChanged();
        }
    }
}