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
        private readonly Dictionary<IEntityStateBase, bool> _entitiesPendingDelete =
            new Dictionary<IEntityStateBase, bool>();

        private readonly Dictionary<IEntityStateBase, bool> _entitiesPendingInsert =
            new Dictionary<IEntityStateBase, bool>();

        private readonly Dictionary<IPropertyState, bool> _propertiesChanged = new Dictionary<IPropertyState, bool>();
        private bool _hasChanges;
        public EventEmitter<DataTrackerState> Changed { get; } = new EventEmitter<DataTrackerState>();

        public int PropertiesChangedCount => _propertiesChanged.Count;

        public EventEmitter<ValueChangedEvent<bool>> PropertiesChangedChanged { get; } =
            new EventEmitter<ValueChangedEvent<bool>>();

        public int EntitiesPendingInsertCount => _entitiesPendingInsert.Count;

        public EventEmitter<ValueChangedEvent<bool>> EntitiesPendingInsertChanged { get; } =
            new EventEmitter<ValueChangedEvent<bool>>();

        public int EntitiesPendingDeleteCount => _entitiesPendingDelete.Count;

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
            _entitiesPendingInsert.Clear();
            _entitiesPendingDelete.Clear();
            EmitChanged();
        }

        public IEntityStateBase[] GetEntitiesPendingInsert()
        {
            return _entitiesPendingInsert.Keys.ToArray();
        }

        public IEntityStateBase[] GetEntitiesPendingDelete()
        {
            return _entitiesPendingDelete.Keys.ToArray();
        }

        public IPropertyState[] GetPropertiesChanged()
        {
            return _propertiesChanged.Keys.ToArray();
        }

        public void UpdateInserted<T>(T item, bool value)
            where T : IEntityStateBase
        {
            UpdateInternal<IEntityStateBase>(DataTrackerStateKind.EntityInserted, item, value);
        }

        public void UpdateDeleted<T>(T item, bool value)
            where T : IEntityStateBase
        {
            UpdateInternal<IEntityStateBase>(DataTrackerStateKind.EntityDeleted, item, value);
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
                case DataTrackerStateKind.EntityInserted:
                    UpdateInserted((IEntityStateBase) item, value);
                    break;
                case DataTrackerStateKind.EntityDeleted:
                    UpdateDeleted((IEntityStateBase) item, value);
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
                         EntitiesPendingInsertCount > 0 ||
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
                case DataTrackerStateKind.EntityDeleted:
                    lookup = (IDictionary<TLookup, bool>) _entitiesPendingDelete;
                    eventEmitter = EntitiesPendingDeleteChanged;
                    break;
                case DataTrackerStateKind.EntityInserted:
                    lookup = (IDictionary<TLookup, bool>) _entitiesPendingInsert;
                    eventEmitter = EntitiesPendingInsertChanged;
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
                case DataTrackerStateKind.EntityDeleted:
                case DataTrackerStateKind.EntityInserted:
                    return ResolveLookup<IEntityStateBase>(changeKind).Item1.ContainsKey((IEntityStateBase) item);
                case DataTrackerStateKind.Property:
                    return ResolveLookup<IPropertyState>(changeKind).Item1.ContainsKey((IPropertyState) item);
            }

            return false;
        }
    }
}