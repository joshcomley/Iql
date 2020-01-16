using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Iql.Conversion;
using Iql.Conversion.State;
using Iql.Data.Context;
using Iql.Data.Crud;
using Iql.Data.Crud.Operations;
using Iql.Data.Events;
using Iql.Data.Extensions;
using Iql.Entities;
using Iql.Entities.Events;
using Iql.Entities.Relationships;
using Iql.Events;

namespace Iql.Data.Tracking.State
{
    [DebuggerDisplay("{EntityType.Name} - {Status}")]
    public class EntityState<T> : IEntityState<T>
        where T : class
    {
        private IOperationEvents<AbandonChangeEvent, AbandonChangeEvent> _abandonEvents;
        private bool _attachedToTracker = true;
        private EventEmitter<ValueChangedEvent<bool, IEntityStateBase>> _attachedToTrackerChanged;
        private List<CascadeDeletion> _cascadeDeletedBy;
        private IqlEventSubscriberManager _eventSubscriptionManager;
        private EventEmitter<ValueChangedEvent<bool, IEntityStateBase>> _hasAnyChangedChanged;
        private EventEmitter<ValueChangedEvent<bool, IEntityStateBase>> _hasAnyChangedSinceSnapshotChanged;
        private bool _hasAnyChanges;
        private bool _hasAnyChangesSinceSnapshot;
        private bool _hasChanges;
        private EventEmitter<ValueChangedEvent<bool, IEntityStateBase>> _hasChangesChanged;
        private bool _hasChangesSinceSnapshot;
        private EventEmitter<ValueChangedEvent<bool, IEntityStateBase>> _hasChangesSinceSnapshotChanged;
        private EventEmitter<ValueChangedEvent<bool, IEntityStateBase>> _hasNestedChangedChanged;
        private EventEmitter<ValueChangedEvent<bool, IEntityStateBase>> _hasNestedChangedSinceSnapshotChanged;
        private bool _hasNestedChanges;
        private bool _hasNestedChangesSinceSnapshot;
        private bool _isAttachedToGraph;
        private EventEmitter<ValueChangedEvent<bool, IEntityStateBase>> _isAttachedToGraphChanged;
        private EventEmitter<IEntityStateBase> _fetched;
        private EventEmitter<IEntityStateBase> _disposed;
        private AsyncEventEmitter<IEntityStateBase> _fetchedAsync;

        private bool _isLocked;
        private bool _isNew;
        private EventEmitter<ValueChangedEvent<bool, IEntityStateBase>> _isNewChanged;
        private CompositeKey _localKey;
        private bool _markedForCascadeDeletion;
        private bool _markedForDeletion;
        private EventEmitter<MarkedForDeletionChangeEvent> _markedForDeletionChanged;
        private EventEmitter<MarkedForDeletionChangeEvent> _markedForAnyDeletionChanged;
        private EventEmitter<MarkedForDeletionChangeEvent> _markedForAdditionChanged;
        private Dictionary<Guid, IPropertyState[]> _operationsDelayed;
        private bool _operationsDelayedInitialized;
        private bool _pendingInsert;
        private EventEmitter<ValueChangedEvent<bool, IEntityStateBase>> _pendingInsertChanged;
        private EventEmitter<ValueChangedEvent<bool, IEntityStateBase>> _pendingDeleteChanged;
        private EventEmitter<ValueChangedEvent<object, IPropertyState>> _propertyLocalValueChanged;
        private Dictionary<string, IPropertyState> _propertyStateByNameDelayed;
        private bool _propertyStateByNameDelayedInitialized;
        private CompositeKey _remoteKey;
        private IOperationEvents<IQueuedCrudOperation, IEntityCrudResult> _saveEvents;
        private bool _settingStatus;
        private IOperationEvents<IQueuedCrudOperation, IEntityCrudResult> _statefulSaveEvents;
        private EntityStatus _status = EntityStatus.Unattached;
        private EventEmitter<ValueChangedEvent<EntityStatus, IEntityStateBase>> _statusChanged;
        private EventEmitter<ValueChangedEvent<bool, IEntityStateBase>> _statusHasChangedChanged;
        private EventEmitter<ValueChangedEvent<bool, IEntityStateBase>> _statusHasChangedSinceSnapshotChanged;
        private TrackingSet<T> _trackingSet;
        private bool _markedForAnyDeletion = false;
        private bool _pendingDelete = false;
        private bool _markedForAddition = false;

        public EntityState(
            TrackingSet<T> trackingSet,
            T entity,
            Type entityType,
            IEntityConfiguration entityConfiguration,
            bool isNew,
            bool isTracked,
            Guid id)
        {
            IsTracked = isTracked;
            Id = id;
            _trackingSet = trackingSet;
            //if (DataTracker != null)
            //{
            //    EventSubscriberManager.Subscribe(DataTracker.HasSnapshotChanged, _ => { CheckHasChanged(); });
            //}

            Entity = entity;
            EntityStates.Register(this);
            EntityType = entityType;
            EntityConfiguration = entityConfiguration;
            Properties = new List<IPropertyState>();
            for (var i = 0; i < EntityConfiguration.Properties.Count; i++)
            {
                var property = EntityConfiguration.Properties[i];
                var propertyState = new PropertyState(property, this);
                Properties.Add(propertyState);
            }

            UpdateHasChanges();
            LocalKey = entityConfiguration.GetCompositeKey(entity);
            if (isNew)
            {
                PendingInsert = true;
            }

            IsNew = isNew;
            SnapshotStatus = Status;
            MonitorSaveEvents();
            if(Status == EntityStatus.Unattached)
            {
                UpdateStatus();
            }
            CanNotifyStatusChange = true;
            NotifyStatusChange();
        }

        public DataTracker DataTracker => _trackingSet == null ? null : _trackingSet.DataTracker;
        public TrackingSet<T> TrackingSet => _trackingSet;

        private bool CanNotifyStatusChange { get; }

        private Dictionary<Guid, IPropertyState[]> _operations
        {
            get
            {
                if (!_operationsDelayedInitialized)
                {
                    _operationsDelayedInitialized = true;
                    _operationsDelayed = new Dictionary<Guid, IPropertyState[]>();
                }

                return _operationsDelayed;
            }
            set
            {
                _operationsDelayedInitialized = true;
                _operationsDelayed = value;
            }
        }

        private IqlEventSubscriberManager EventSubscriberManager => _eventSubscriptionManager =
            _eventSubscriptionManager ?? new IqlEventSubscriberManager();

        private List<IPropertyState> Properties { get; }

        private Dictionary<string, IPropertyState> _propertyStateByName
        {
            get
            {
                if (!_propertyStateByNameDelayedInitialized)
                {
                    _propertyStateByNameDelayedInitialized = true;
                    _propertyStateByNameDelayed = new Dictionary<string, IPropertyState>();
                }

                return _propertyStateByNameDelayed;
            }
            set
            {
                _propertyStateByNameDelayedInitialized = true;
                _propertyStateByNameDelayed = value;
            }
        }

        public async Task NotifyFetchedAsync()
        {
            _fetched.EmitIfExists(() => this);
            await _fetchedAsync.EmitIfExistsAsync(() => this);
        }

        // TODO: This somehow needs to be maintained
        public bool AttachedToTracker
        {
            get => _attachedToTracker;
            set
            {
                var old = _attachedToTracker;
                _attachedToTracker = value;
                if (old != value)
                {
                    DataTracker.NotifyAttachedToTrackerChanged(this, value);
                    _attachedToTrackerChanged.EmitIfExists(() =>
                        new ValueChangedEvent<bool, IEntityStateBase>(old, value, this));
                    UpdateStatus();
                }
            }
        }

        public ILockable Parent => DataTracker;
        public bool IsLocked => _isLocked || Parent == null || Parent.IsLocked;

        public void Lock()
        {
            _isLocked = true;
        }

        public void Unlock()
        {
            _isLocked = false;
        }

        public void CheckHasChanged()
        {
            var hasSnapshots = DataTracker != null && DataTracker.HasSnapshot;
            HasChanges = IsNew || Properties.Any(_ => _.HasChanges);
            HasChangesSinceSnapshot = !hasSnapshots && HasChanges || Properties.Any(_ => _.HasChangesSinceSnapshot);
            HasNestedChanges = Properties.Any(_ => _.HasNestedChanges);
            HasNestedChangesSinceSnapshot = Properties.Any(_ => _.HasNestedChangesSinceSnapshot);
            HasAnyChanges = HasChanges || HasNestedChanges;
            HasAnyChangesSinceSnapshot = HasChangesSinceSnapshot || HasNestedChangesSinceSnapshot;
        }

        public bool HasChanges
        {
            get => _hasChanges;
            set
            {
                var old = _hasChanges;
                _hasChanges = value;
                if (old != value)
                {
                    _hasChangesChanged.EmitIfExists(() =>
                        new ValueChangedEvent<bool, IEntityStateBase>(old, value, this));
                    if (DataTracker != null)
                    {
                        DataTracker.NotifyEntityHasChangedChanged(this);
                    }
                }
            }
        }

        public bool HasChangesSinceSnapshot
        {
            get => _hasChangesSinceSnapshot;
            set
            {
                var old = _hasChangesSinceSnapshot;
                _hasChangesSinceSnapshot = value;
                if (old != value)
                {
                    _hasChangesSinceSnapshotChanged.EmitIfExists(() =>
                        new ValueChangedEvent<bool, IEntityStateBase>(old, value, this));
                    if (DataTracker != null)
                    {
                        DataTracker.NotifyEntityHasChangedSinceSnapshotChanged(this);
                    }
                }
            }
        }

        public EventEmitter<ValueChangedEvent<bool, IEntityStateBase>> HasChangesChanged => _hasChangesChanged =
            _hasChangesChanged ?? new EventEmitter<ValueChangedEvent<bool, IEntityStateBase>>();

        public EventEmitter<ValueChangedEvent<bool, IEntityStateBase>> HasChangesSinceSnapshotChanged =>
            _hasChangesSinceSnapshotChanged =
                _hasChangesSinceSnapshotChanged ?? new EventEmitter<ValueChangedEvent<bool, IEntityStateBase>>();

        public bool HasNestedChanges
        {
            get => _hasNestedChanges;
            set
            {
                var old = _hasNestedChanges;
                _hasNestedChanges = value;
                if (old != value)
                {
                    _hasNestedChangedChanged.EmitIfExists(() =>
                        new ValueChangedEvent<bool, IEntityStateBase>(old, value, this));
                    //if (DataTracker != null)
                    //{
                    //    DataTracker.NotifyEntityHasNestedChangedChanged(this);
                    //}
                }
            }
        }

        public bool HasNestedChangesSinceSnapshot
        {
            get => _hasNestedChangesSinceSnapshot;
            set
            {
                var old = _hasNestedChangesSinceSnapshot;
                _hasNestedChangesSinceSnapshot = value;
                if (old != value)
                {
                    _hasNestedChangedSinceSnapshotChanged.EmitIfExists(() =>
                        new ValueChangedEvent<bool, IEntityStateBase>(old, value, this));
                    //if (DataTracker != null)
                    //{
                    //    DataTracker.NotifyEntityHasNestedChangedSinceSnapshotChanged(this);
                    //}
                }
            }
        }

        public EventEmitter<ValueChangedEvent<bool, IEntityStateBase>> HasNestedChangesChanged =>
            _hasNestedChangedChanged =
                _hasNestedChangedChanged ?? new EventEmitter<ValueChangedEvent<bool, IEntityStateBase>>();

        public EventEmitter<ValueChangedEvent<bool, IEntityStateBase>> HasNestedChangesSinceSnapshotChanged =>
            _hasNestedChangedSinceSnapshotChanged =
                _hasNestedChangedSinceSnapshotChanged ?? new EventEmitter<ValueChangedEvent<bool, IEntityStateBase>>();

        public bool HasAnyChanges
        {
            get => _hasAnyChanges;
            set
            {
                var old = _hasAnyChanges;
                _hasAnyChanges = value;
                if (old != value)
                {
                    _hasAnyChangedChanged.EmitIfExists(() =>
                        new ValueChangedEvent<bool, IEntityStateBase>(old, value, this));
                    //if (DataTracker != null)
                    //{
                    //    DataTracker.NotifyEntityHasAnyChangedChanged(this);
                    //}
                }
            }
        }

        public bool HasAnyChangesSinceSnapshot
        {
            get => _hasAnyChangesSinceSnapshot;
            set
            {
                var old = _hasAnyChangesSinceSnapshot;
                _hasAnyChangesSinceSnapshot = value;
                if (old != value)
                {
                    _hasAnyChangedSinceSnapshotChanged.EmitIfExists(() =>
                        new ValueChangedEvent<bool, IEntityStateBase>(old, value, this));
                    //if (DataTracker != null)
                    //{
                    //    DataTracker.NotifyEntityHasAnyChangedSinceSnapshotChanged(this);
                    //}
                }
            }
        }

        public EventEmitter<ValueChangedEvent<bool, IEntityStateBase>> HasAnyChangesChanged => _hasAnyChangedChanged =
            _hasAnyChangedChanged ?? new EventEmitter<ValueChangedEvent<bool, IEntityStateBase>>();

        public EventEmitter<ValueChangedEvent<bool, IEntityStateBase>> HasAnyChangesSinceSnapshotChanged =>
            _hasAnyChangedSinceSnapshotChanged =
                _hasAnyChangedSinceSnapshotChanged ?? new EventEmitter<ValueChangedEvent<bool, IEntityStateBase>>();

        public EventEmitter<ValueChangedEvent<bool, IEntityStateBase>> AttachedToTrackerChanged =>
            _attachedToTrackerChanged =
                _attachedToTrackerChanged ?? new EventEmitter<ValueChangedEvent<bool, IEntityStateBase>>();

        public bool PendingInsert
        {
            get => _pendingInsert;
            private set
            {
                var old = _pendingInsert;
                _pendingInsert = value;
                if (old != value)
                {
                    _pendingInsertChanged.EmitIfExists(() =>
                        new ValueChangedEvent<bool, IEntityStateBase>(old, value, this));
                }
            }
        }

        public EventEmitter<ValueChangedEvent<bool, IEntityStateBase>> PendingInsertChanged => _pendingInsertChanged =
            _pendingInsertChanged ?? new EventEmitter<ValueChangedEvent<bool, IEntityStateBase>>();

        public EventEmitter<ValueChangedEvent<bool, IEntityStateBase>> PendingDeleteChanged => _pendingDeleteChanged =
            _pendingDeleteChanged ?? new EventEmitter<ValueChangedEvent<bool, IEntityStateBase>>();

        public bool IsAttachedToGraph
        {
            get => _isAttachedToGraph;
            set
            {
                var old = _isAttachedToGraph;
                _isAttachedToGraph = value;
                if (old != value)
                {
                    _isAttachedToGraphChanged.EmitIfExists(() =>
                        new ValueChangedEvent<bool, IEntityStateBase>(old, value, this));
                    UpdatePendingInsert();
                }
            }
        }

        public EventEmitter<ValueChangedEvent<bool, IEntityStateBase>> IsAttachedToGraphChanged =>
            _isAttachedToGraphChanged =
                _isAttachedToGraphChanged ?? new EventEmitter<ValueChangedEvent<bool, IEntityStateBase>>();

        public EventEmitter<IEntityStateBase> Disposed =>
            _disposed =
                _disposed ?? new EventEmitter<IEntityStateBase>();

        public EventEmitter<IEntityStateBase> Fetched =>
            _fetched =
                _fetched ?? new EventEmitter<IEntityStateBase>();

        public AsyncEventEmitter<IEntityStateBase> FetchedAsync =>
            _fetchedAsync =
                _fetchedAsync ?? new AsyncEventEmitter<IEntityStateBase>();

        public Guid Id { get; set; }

        //private CompositeKey _remoteKey;
        public bool IsNew
        {
            get => _isNew;
            set
            {
                var old = _isNew;
                _isNew = value;
                if (old != value)
                {
                    _isNewChanged.EmitIfExists(() => new ValueChangedEvent<bool, IEntityStateBase>(old, value, this));
                    UpdateStatus();
                    if (DataTracker != null)
                    {
                        DataTracker.NotifyEntityIsNewChanged(this);
                        TrackingSet.NotifyEntityIsNewChanged(this);
                    }
                }
            }
        }

        public EventEmitter<MarkedForDeletionChangeEvent> MarkedForDeletionChanged => _markedForDeletionChanged =
            _markedForDeletionChanged ?? new EventEmitter<MarkedForDeletionChangeEvent>();

        public EventEmitter<MarkedForDeletionChangeEvent> MarkedForAnyDeletionChanged => _markedForAnyDeletionChanged =
            _markedForAnyDeletionChanged ?? new EventEmitter<MarkedForDeletionChangeEvent>();

        public EventEmitter<MarkedForDeletionChangeEvent> MarkedForAdditionChanged => _markedForAdditionChanged =
            _markedForAdditionChanged ?? new EventEmitter<MarkedForDeletionChangeEvent>();

        public EventEmitter<ValueChangedEvent<bool, IEntityStateBase>> IsNewChanged =>
            _isNewChanged = _isNewChanged ?? new EventEmitter<ValueChangedEvent<bool, IEntityStateBase>>();

        public string StateKey { get; set; }


        public bool MarkedForDeletion
        {
            get => _markedForDeletion;
            set
            {
                var changed = _markedForDeletion != value;
                _markedForDeletion = value;
                if (changed)
                {
                    var markedForDeletionChangeEvent = new MarkedForDeletionChangeEvent(this, value);
                    _markedForDeletionChanged.EmitIfExists(() => markedForDeletionChangeEvent);
                    if(TrackingSet != null)
                    {
                        TrackingSet.NotifyMarkedForDeletionChanged(_markedForDeletion, this);
                    }
                }

                UpdateStatus();
            }
        }

        public bool MarkedForCascadeDeletion
        {
            get => _markedForCascadeDeletion;
            set
            {
                _markedForCascadeDeletion = value;
                UpdateStatus();
            }
        }

        public bool MarkedForAnyDeletion => _markedForAnyDeletion;
        public bool PendingDelete => _pendingDelete;

        public void UnmarkForDeletion()
        {
            if (MarkedForAnyDeletion)
            {
                MarkedForDeletion = false;
                MarkedForCascadeDeletion = false;
                if (DataTracker != null)
                {
                    DataTracker.RelationshipObserver.RestoreRelationships(Entity, EntityType);
                }
            }
        }

        public void AbandonChanges()
        {
            AbandonPropertyChanges(null);
        }

        public void AbandonPropertyChanges(IProperty[] properties)
        {
            var ev = new AbandonChangeEvent(this, null);
            AbandonEvents.EmitStartedAsync(() => ev);
            StateEvents.AbandoningEntityChanges.EmitIfExists(() => ev);
            for (var i = 0; i < Properties.Count; i++)
            {
                var state = Properties[i];
                if (properties != null && !properties.Contains(state.Property))
                {
                    continue;
                }

                state.AbandonChanges();
            }

            MarkedForDeletion = false;
            MarkedForCascadeDeletion = false;
            AbandonEvents.EmitCompletedAsync(() => ev);
            AbandonEvents.EmitSuccessAsync(() => ev);
            ClearStatefulEvents();
            StateEvents.AbandonedEntityChanges.EmitIfExists(() => ev);
        }

        public void AddSnapshot()
        {
            SetSnapshotValue(Status);
        }

        /// <summary>
        ///     For internal use only.
        /// </summary>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        /// <param name="propertyState"></param>
        public void NotifyPropertyLocalValueChange(object oldValue, object newValue, IPropertyState propertyState)
        {
            _propertyLocalValueChanged.EmitIfExists(() =>
                new ValueChangedEvent<object, IPropertyState>(oldValue, newValue, propertyState));
        }

        public void ClearSnapshotValue()
        {
            SnapshotStatus = Status;
        }

        public void SetSnapshotValue(EntityStatus snapshotStatus)
        {
            SnapshotStatus = snapshotStatus;
            EventSubscription subscription = null;
            subscription = DataTracker.HasSnapshotChanged.Subscribe(_ =>
            {
                if (!DataTracker.HasSnapshot)
                {
                    ClearSnapshotValue();
                    subscription.Unsubscribe();
                }
            });
        }

        public void ClearStatefulEvents()
        {
            StatefulSaveEvents.UnsubscribeAll();
            foreach (var propertyState in PropertyStates)
            {
                propertyState.ClearStatefulEvents();
            }
        }

        public void MarkForCascadeDeletion(object from, IRelationship relationship)
        {
            MarkedForCascadeDeletion = true;
            CascadeDeletedBy.Add(new CascadeDeletion(relationship, from));
        }

        public void HardReset()
        {
            IsNew = false;
            for (var i = 0; i < Properties.Count; i++)
            {
                var propertyState = Properties[i];
                propertyState.HardReset();
            }

            //_remoteKey = EntityConfiguration.GetCompositeKey(Entity);
        }

        public void SoftReset(bool markAsNotNew)
        {
            if (markAsNotNew)
            {
                IsNew = false;
            }

            for (var i = 0; i < Properties.Count; i++)
            {
                var propertyState = Properties[i];
                propertyState.SoftReset();
            }
        }

        public CompositeKey RemoteKey => IsNew ? null : _remoteKey = _remoteKey ?? KeyBeforeChanges();

        public CompositeKey LocalKey
        {
            get => _localKey;
            set
            {
                _localKey = value;
                _remoteKey = null;
            }
        }

        //public CompositeKey RemoteKey => _remoteKey ?? CurrentKey;

        public CompositeKey KeyBeforeChanges()
        {
            var compositeKey =
                new CompositeKey(EntityConfiguration.TypeName, EntityConfiguration.Key.Properties.Length);
            for (var i = 0; i < EntityConfiguration.Key.Properties.Length; i++)
            {
                var property = EntityConfiguration.Key.Properties[i];
                compositeKey.Keys[i] = new KeyValue(
                    ((IMetadata) property).Name,
                    GetPropertyState(((IMetadata) property).Name).RemoteValue,
                    property.TypeDefinition);
            }

            return compositeKey;
        }

        public Guid? PersistenceKey { get; set; }

        public List<CascadeDeletion> CascadeDeletedBy =>
            _cascadeDeletedBy = _cascadeDeletedBy ?? new List<CascadeDeletion>();

        //private readonly AsyncEventEmitter<IqlEntityEvent<T>> _savingEmitter = new AsyncEventEmitter<IqlEntityEvent<T>>();
        //public IAsyncEventSubscriber<IqlEntityEvent<T>> SavingAsync => _savingEmitter;
        //IAsyncEventSubscriber<IEntityEventBase> IEntityStateBase.SavingAsync => SavingAsync;


        //private readonly AsyncEventEmitter<IqlEntityEvent<T>> _savedEmitter = new AsyncEventEmitter<IqlEntityEvent<T>>();
        //public IAsyncEventSubscriber<IqlEntityEvent<T>> SavedAsync => _savedEmitter;
        //IAsyncEventSubscriber<IEntityEventBase> IEntityStateBase.SavedAsync => SavedAsync;


        public void Restore(SerializedEntityState state)
        {
            for (var i = 0; i < state.PropertyStates.Length; i++)
            {
                var deserializedPropertyState = state.PropertyStates[i];
                GetPropertyState(deserializedPropertyState.Property)
                    .Restore(deserializedPropertyState);
            }

            if (state.PersistenceKey != null)
            {
                PersistenceKey = state.PersistenceKey.EnsureGuid();
            }

            IsNew = state.IsNew;
            // This must occur after "IsNew" is set else it will reset MarkedForDeletion
            MarkedForDeletion = state.MarkedForDeletion;
            MarkedForCascadeDeletion = state.MarkedForCascadeDeletion;
            LocalKey = state.CurrentKey.ToCompositeKey(EntityConfiguration);
            for (var i = 0; i < EntityConfiguration.Key.Properties.Length; i++)
            {
                var key = EntityConfiguration.Key.Properties[i];
                key.SetValue(Entity, LocalKey.Keys.Single(_ => _.Name == key.Name).Value);
            }
        }

        public bool Floating { get; set; }

        public IOperationEvents<IQueuedCrudOperation, IEntityCrudResult> StatefulSaveEvents => _statefulSaveEvents =
            _statefulSaveEvents ?? new OperationEvents<IQueuedCrudOperation, IEntityCrudResult>();

        IOperationEventsBase IStateful.StatefulSaveEvents => StatefulSaveEvents;

        public IOperationEvents<IQueuedCrudOperation, IEntityCrudResult> SaveEvents => _saveEvents =
            _saveEvents ?? new OperationEvents<IQueuedCrudOperation, IEntityCrudResult>();

        IOperationEventsBase IStateful.SaveEvents => SaveEvents;

        public IOperationEvents<AbandonChangeEvent, AbandonChangeEvent> AbandonEvents => _abandonEvents =
            _abandonEvents ?? new OperationEvents<AbandonChangeEvent, AbandonChangeEvent>();

        IOperationEventsBase IStateful.AbandonEvents => AbandonEvents;
        public T Entity { get; }
        object IEntityStateBase.Entity => Entity;

        public Type EntityType { get; }

        //public IDataContext DataContext => DataTracker.DataContext;
        public IEntityConfiguration EntityConfiguration { get; }

        ITrackingSet IEntityStateBase.TrackingSet => TrackingSet;

        public bool IsTracked { get; }
        public bool IsIqlEntityState => true;

        public EventEmitter<ValueChangedEvent<object, IPropertyState>> PropertyLocalValueChanged =>
            _propertyLocalValueChanged = _propertyLocalValueChanged ??
                                         new EventEmitter<ValueChangedEvent<object, IPropertyState>>();

        public EventEmitter<ValueChangedEvent<EntityStatus, IEntityStateBase>> StatusChanged =>
            _statusChanged = _statusChanged ?? new EventEmitter<ValueChangedEvent<EntityStatus, IEntityStateBase>>();

        public EventEmitter<ValueChangedEvent<bool, IEntityStateBase>> StatusHasChangedChanged =>
            _statusHasChangedChanged =
                _statusHasChangedChanged ?? new EventEmitter<ValueChangedEvent<bool, IEntityStateBase>>();

        public EventEmitter<ValueChangedEvent<bool, IEntityStateBase>> StatusHasChangedSinceSnapshotChanged =>
            _statusHasChangedSinceSnapshotChanged =
                _statusHasChangedSinceSnapshotChanged ?? new EventEmitter<ValueChangedEvent<bool, IEntityStateBase>>();

        public bool StatusHasChanged { get; set; }

        public bool StatusHasChangedSinceSnapshot { get; set; }

        public EntityStatus Status
        {
            get => _status;
            set
            {
                var old = _status;
                _status = value;
                if (!_settingStatus)
                {
                    _settingStatus = true;
                    switch (_status)
                    {
                        case EntityStatus.Unattached:
                            AttachedToTracker = false;
                            break;
                        case EntityStatus.New:
                            AttachedToTracker = true;
                            IsNew = true;
                            UnmarkForDeletion();
                            PendingInsert = true;
                            if (TrackingSet != null)
                            {
                                TrackingSet.AddEntity(Entity);
                            }

                            break;
                        case EntityStatus.NewAndDeleted:
                            AttachedToTracker = true;
                            IsNew = true;
                            MarkedForDeletion = true;
                            PendingInsert = false;
                            //if (TrackingSet != null)
                            //{
                            //    TrackingSet.UntrackEntity(Entity);
                            //}
                            break;
                        case EntityStatus.Existing:
                            AttachedToTracker = true;
                            IsNew = false;
                            UnmarkForDeletion();
                            PendingInsert = false;
                            break;
                        case EntityStatus.ExistingAndPendingDelete:
                            AttachedToTracker = true;
                            IsNew = false;
                            MarkedForDeletion = true;
                            PendingInsert = false;
                            break;
                        case EntityStatus.ExistingAndDeleted:
                            IsNew = false;
                            AttachedToTracker = false;
                            UnmarkForDeletion();
                            PendingInsert = false;
                            if (TrackingSet != null)
                            {
                                TrackingSet.UntrackEntity(Entity);
                            }

                            break;
                    }

                    _settingStatus = false;
                }

                if (old != value)
                {
                    _statusChanged.EmitIfExists(() =>
                        new ValueChangedEvent<EntityStatus, IEntityStateBase>(old, value, this));
                    //if (DataTracker != null)
                    //{
                    //    DataTracker.NotifyStatusChanged(this, value);
                    //}
                }

                UpdateStatusHasChanged();
            }
        }

        public EntityStatus SnapshotStatus { get; protected set; } = EntityStatus.Unattached;

        public void UpdateHasChanges()
        {
            for (var i = 0; i < Properties.Count; i++)
            {
                var propertyState = Properties[i];
                if (propertyState.Property.TypeDefinition.Kind == IqlType.Collection)
                {
                    continue;
                }

                propertyState.UpdateHasChanged(true);
            }

            for (var i = 0; i < Properties.Count; i++)
            {
                var propertyState = Properties[i];
                if (propertyState.Property.TypeDefinition.Kind != IqlType.Collection)
                {
                    continue;
                }

                propertyState.UpdateHasChanged(true);
            }
        }

        public IPropertyState[] GetChangedProperties(IProperty[] properties = null)
        {
            var propertyStates = PropertyStates.Where(ps =>
                    ps.HasChanges && !ps.Property.Kind.HasFlag(IqlPropertyKind.Relationship) &&
                    (properties == null || properties.Contains(ps.Property)))
                .ToArray();
            return propertyStates;
        }

        object IEntityStateBase.EntityBeforeChanges()
        {
            return EntityBeforeChanges();
        }

        public IPropertyState[] PropertyStates => Properties.ToArray();

        public IPropertyState GetPropertyState(string name)
        {
            if (!_propertyStateByName.ContainsKey(name))
            {
                var state = Properties.SingleOrDefault(p => p.Property.Name == name);
                if (state != null)
                {
                    _propertyStateByName.Add(name, state);
                    return state;
                }

                return null;
            }

            return _propertyStateByName[name];
        }

        public IPropertyState FindPropertyState(string name)
        {
            var state = Properties.FirstOrDefault(p => p.Property.Name == name);
            state = state ?? Properties.FirstOrDefault(p => p.Property.PrimaryProperty.Name == name);
            state = state ?? Properties.FirstOrDefault(p => p.Property.PrimaryProperty.GetGroupProperties()
                        .Any(_ => _.Name == name));
            state = state ?? Properties.FirstOrDefault(p =>
                        p.Property.PropertyGroup != null && p.Property.PropertyGroup.Name == name);
            return state;
        }

        public bool HasValidKey()
        {
            return CompositeKey.IsValid(EntityConfiguration, Entity, IsNew);
        }

        public string SerializeToJson()
        {
            return this.ToJson();
        }

        public object PrepareForJson()
        {
            return new
            {
                Id,
                CurrentKey = LocalKey.PrepareForJson(),
                PersistenceKey,
                IsNew,
                MarkedForDeletion,
                MarkedForCascadeDeletion,
                PropertyStates = PropertyStates.Where(_ => IsNew || _.HasChanges).Select(_ => _.PrepareForJson())
                    .Where(_ => _ != null)
            };
        }

        public void Dispose()
        {
            MarkedForDeletionChanged?.Dispose();
            EventSubscriberManager?.Dispose();
            for (var i = 0; i < PropertyStates.Length; i++)
            {
                var propState = PropertyStates[i];
                propState.Dispose();
            }

            _disposed.EmitIfExists(() => this);
        }

        private void UpdatePendingInsert()
        {
            PendingInsert = IsNew && !MarkedForAnyDeletion;
            if (TrackingSet != null)
            {
                if (PendingInsert)
                {
                    TrackingSet.SetMarkedForAddition(this, true);
                }
                else
                {
                    TrackingSet.SetMarkedForAddition(this, false);
                }
            }
        }

        private void UpdateStatus()
        {
            UpdateMarkedForAnyDeletion();
            UpdatePendingInsert();
            CheckHasChanged();

            if (_settingStatus)
            {
                return;
            }

            _settingStatus = true;
            var status = EntityStatus.Unattached;
            if (AttachedToTracker)
            {
                if (IsNew)
                {
                    if (PendingInsert)
                    {
                        status = EntityStatus.New;
                    }
                    else
                    {
                        status = EntityStatus.NewAndDeleted;
                    }
                }
                else
                {
                    if (MarkedForDeletion)
                    {
                        status = EntityStatus.ExistingAndPendingDelete;
                    }
                    else if (AttachedToTracker)
                    {
                        status = EntityStatus.Existing;
                    }
                    else
                    {
                        status = EntityStatus.ExistingAndDeleted;
                    }
                }
            }
            else if (!IsNew)
            {
                status = EntityStatus.ExistingAndDeleted;
            }

            if(status != Status)
            {
                Status = status;
                UpdateMarkedForAnyDeletion();
                UpdatePendingInsert();
                CheckHasChanged();
            }
            _settingStatus = false;
        }
        private void UpdateMarkedForAnyDeletion()
        {
            var oldMarkedForAnyDeletion = _markedForAnyDeletion;
            _markedForAnyDeletion = MarkedForDeletion || MarkedForCascadeDeletion;
            if (oldMarkedForAnyDeletion != _markedForAnyDeletion)
            {
                _markedForAnyDeletionChanged.EmitIfExists(() =>
                    new MarkedForDeletionChangeEvent(this, _markedForAnyDeletion));
            }

            var oldPendingDelete = _pendingDelete;
            _pendingDelete = _markedForAnyDeletion && !IsNew && AttachedToTracker;
            if (oldPendingDelete != _pendingDelete)
            {
                _pendingDeleteChanged.EmitIfExists(() =>
                    new ValueChangedEvent<bool, IEntityStateBase>(oldPendingDelete, _pendingDelete, this));
                if (TrackingSet != null)
                {
                    if (_pendingDelete)
                    {
                        TrackingSet.SetMarkedForDeletion(this, true);
                    }
                    else
                    {
                        TrackingSet.SetMarkedForDeletion(this, false);
                    }
                }
            }
        }

        private void UpdateStatusHasChanged()
        {
            StatusHasChanged = AreStatusDifferent(SnapshotStatus, Status);
            NotifyStatusChange();
        }

        public static bool AreStatusDifferent(EntityStatus oldValue, EntityStatus newValue)
        {
            var hasChanged = false;
            if (oldValue == EntityStatus.Existing &&
                newValue == EntityStatus.ExistingAndPendingDelete)
            {
                hasChanged = true;
            }
            else if (oldValue == EntityStatus.ExistingAndPendingDelete &&
                     newValue == EntityStatus.Existing)
            {
                hasChanged = true;
            }
            else if (oldValue == EntityStatus.New &&
                     newValue == EntityStatus.NewAndDeleted)
            {
                hasChanged = true;
            }
            else if ((oldValue == EntityStatus.NewAndDeleted || oldValue == EntityStatus.Unattached) &&
                     newValue == EntityStatus.New)
            {
                hasChanged = true;
            }

            return hasChanged;
        }

        private void NotifyStatusChange()
        {
            if (DataTracker != null && CanNotifyStatusChange)
            {
                DataTracker.NotifyStatusChanged(this,
                    Status == EntityStatus.New ||
                    Status == EntityStatus.ExistingAndPendingDelete);
            }
        }

        private void MonitorSaveEvents()
        {
            EventSubscriberManager.SubscribeAsync(SaveEvents.StartedAsync,
                async _ =>
                {
                    if (_.Operation.Kind == IqlOperationKind.Update)
                    {
                        var updateOp = (IUpdateEntityOperation) _.Operation;
                        var changed = updateOp.GetChangedProperties();
                        for (var i = 0; i < changed.Length; i++)
                        {
                            var prop = changed[i];
                            foreach (var property in prop.Property.GetGroupProperties())
                            {
                                var propState = GetPropertyState(property.Name);
                                await propState.SaveEvents.EmitStartedAsync(() =>
                                    new IqlEntityPropertyEvent<T>(_.Operation, propState.Property, this, propState));
                            }
                        }

                        _operations.Add(_.Operation.Id, changed);
                    }
                    else if (_.Operation.Kind == IqlOperationKind.Add)
                    {
                        foreach (var propState in PropertyStates)
                        {
                            await propState.SaveEvents.EmitStartedAsync(() =>
                                new IqlEntityPropertyEvent<T>(_.Operation, propState.Property, this, propState));
                        }

                        _operations.Add(_.Operation.Id, PropertyStates.ToArray());
                    }
                });
            EventSubscriberManager.SubscribeAsync(SaveEvents.SuccessfulAsync,
                async _ =>
                {
                    if (_operations.ContainsKey(_.Operation.Id))
                    {
                        var properties = _operations[_.Operation.Id];
                        for (var i = 0; i < properties.Length; i++)
                        {
                            var prop = properties[i];
                            foreach (var property in prop.Property.GetGroupProperties())
                            {
                                var propState = GetPropertyState(property.Name);
                                await propState.SaveEvents.EmitSuccessAsync(() =>
                                    new IqlEntityPropertyEvent<T>(_.Operation, propState.Property, this, propState));
                            }
                        }
                    }
                });
            EventSubscriberManager.SubscribeAsync(SaveEvents.CompletedAsync,
                async _ =>
                {
                    if (_operations.ContainsKey(_.Operation.Id))
                    {
                        var properties = _operations[_.Operation.Id];
                        for (var i = 0; i < properties.Length; i++)
                        {
                            var prop = properties[i];
                            foreach (var property in prop.Property.GetGroupProperties())
                            {
                                var propState = GetPropertyState(property.Name);
                                await propState.SaveEvents.EmitCompletedAsync(() =>
                                    new IqlEntityPropertyEvent<T>(_.Operation, propState.Property, this, propState));
                            }
                        }

                        _operations.Remove(_.Operation.Id);
                    }
                });
        }

        public static IEntityStateBase New(object entity, Type entityType, IEntityConfiguration entityConfiguration)
        {
            return (IEntityStateBase)
                Activator.CreateInstance(typeof(EntityState<>).MakeGenericType(entityType), entity, entityType,
                    entityConfiguration);
        }

        public bool HasRelationshipChanged(IProperty relationshipProperty)
        {
            if (relationshipProperty.Relationship.ThisIsTarget)
            {
                return false;
            }

            if (relationshipProperty.Kind.HasFlag(IqlPropertyKind.Relationship))
            {
                var relationshipEntity = relationshipProperty.GetValue(Entity);
                if (relationshipEntity == null)
                {
                    return true;
                }

                var relationshipEntityState = DataTracker
                    .TrackingSetByType(relationshipProperty.Relationship.OtherEnd.Type)
                    .FindMatchingEntityState(relationshipEntity);

                if (!relationshipEntityState.IsNew)
                {
                    var thisEndConstraints = relationshipProperty.Relationship.ThisEnd.Constraints;
                    for (var i = 0; i < thisEndConstraints.Length; i++)
                    {
                        var constraint = thisEndConstraints[i];
                        var constraintState = GetPropertyState(((IMetadata) constraint).Name);
                        if (constraintState.HasChanges)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        public T EntityBeforeChanges()
        {
            if (IsNew)
            {
                return default(T);
            }

            var entity = Entity.Clone(EntityConfiguration.Builder, EntityType);
            foreach (var property in Properties)
            {
                if (property.Property.TypeDefinition.Kind != IqlType.Collection)
                {
                    property.Property.SetValue(entity, property.RemoteValue);
                }
            }

            return (T) entity;
        }
    }
}