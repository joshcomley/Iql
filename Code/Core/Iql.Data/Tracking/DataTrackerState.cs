using System;
using System.Collections.Generic;
using System.Linq;
using Iql.Data.Crud.Operations;
using Iql.Data.Tracking.State;
using Iql.Entities.Events;
using Iql.Events;

namespace Iql.Data.Tracking
{
    public class DataTrackerState
    {
        public bool PauseEvents { get; set; }
        private readonly Dictionary<IEntityStateBase, bool> _entitiesChanged =
            new Dictionary<IEntityStateBase, bool>();

        private readonly Dictionary<IPropertyState, bool> _propertiesChanged = new Dictionary<IPropertyState, bool>();
        private bool _hasChanges;
        public EventEmitter<DataTrackerState> Changed { get; } = new EventEmitter<DataTrackerState>();

        public int PropertiesChangedCount => _propertiesChanged.Count;

        public EventEmitter<ValueChangedEvent<bool>> PropertiesChangedChanged { get; } =
            new EventEmitter<ValueChangedEvent<bool>>();

        public int EntitiesPendingDeleteCount => _entitiesChanged.Count;

        public EventEmitter<ValueChangedEvent<bool>> EntitiesPendingDeleteChanged { get; } =
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

        public void UpdateStatusChanged<T>(T item, bool value)
            where T : IEntityStateBase
        {
            UpdateInternal<IEntityStateBase>(DataTrackerStateKind.EntityStatus, item, value);
        }

        public void UpdatePropertyChanged<T>(T item, bool value)
            where T : IPropertyState
        {
            UpdateInternal<IPropertyState>(DataTrackerStateKind.Property, item, value);
        }

        public void Update<TLookup>(DataTrackerStateKind changeKind, TLookup item, bool value)
        {
            switch (changeKind)
            {
                case DataTrackerStateKind.EntityStatus:
                    UpdateStatusChanged((IEntityStateBase) item, value);
                    break;
                case DataTrackerStateKind.Property:
                    UpdatePropertyChanged((IPropertyState) item, value);
                    break;
            }
        }

        protected void UpdateInternal<TLookup>(DataTrackerStateKind changeKind, TLookup item, bool value)
        {
            var resolved = ResolveLookup<TLookup>(changeKind);
            var lookup = resolved.Item1;
            if (value)
            {
                if (!lookup.ContainsKey(item))
                {
                    lookup.Add(item, true);
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
                         EntitiesPendingDeleteCount > 0;
            //if (!PauseEvents)
            {
                Changed.Emit(() => this);
            }
        }

        private Tuple<IDictionary<TLookup, bool>, IEventEmitterBase> ResolveLookup<TLookup>(
            DataTrackerStateKind changeKind)
        {
            IDictionary<TLookup, bool> lookup = null;
            IEventEmitterBase eventEmitter = null;
            switch (changeKind)
            {
                case DataTrackerStateKind.EntityStatus:
                    lookup = (IDictionary<TLookup, bool>) _entitiesChanged;
                    eventEmitter = EntitiesPendingDeleteChanged;
                    break;
                case DataTrackerStateKind.Property:
                    lookup = (IDictionary<TLookup, bool>) _propertiesChanged;
                    eventEmitter = PropertiesChangedChanged;
                    break;
            }

            return new Tuple<IDictionary<TLookup, bool>, IEventEmitterBase>(lookup, eventEmitter);
        }

        public bool Has<TLookup>(DataTrackerStateKind changeKind, TLookup item)
        {
            switch (changeKind)
            {
                case DataTrackerStateKind.EntityStatus:
                    return ResolveLookup<IEntityStateBase>(changeKind).Item1.ContainsKey((IEntityStateBase) item);
                case DataTrackerStateKind.Property:
                    return ResolveLookup<IPropertyState>(changeKind).Item1.ContainsKey((IPropertyState) item);
            }

            return false;
        }
    }
}