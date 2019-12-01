using System;
using System.Collections.Generic;
using System.Data;
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
        private bool _markedForDeletion;
        private bool _attachedToTracker = true;
        private bool _pendingInsert;
        private bool _isNew;
        private CompositeKey _remoteKey;
        private CompositeKey _localKey;

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
                    AttachedToTrackerChanged.Emit(() => new ValueChangedEvent<bool>(old, value));
                }
                UpdateStatus();
            }
        }

        private bool _isLocked = false;
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
            HasChanged = IsNew || Properties.Any(_ => _.HasChanged);
            var hasSnapshots = DataTracker != null && DataTracker.HasSnapshot;
            HasChangedSinceSnapshot = (!hasSnapshots && HasChanged) || Properties.Any(_ => _.HasChangedSinceSnapshot);
        }

        public bool HasChanged
        {
            get => _hasChanged;
            set
            {
                var old = _hasChanged;
                _hasChanged = value;
                if (old != value)
                {
                    HasChangedChanged.Emit(() => new ValueChangedEvent<bool>(old, value));
                    if (DataTracker != null)
                    {
                        DataTracker.NotifyEntityHasChangedChanged(this);
                    }
                }
            }
        }

        public bool HasChangedSinceSnapshot
        {
            get => _hasChangedSinceSnapshot;
            set
            {
                var old = _hasChangedSinceSnapshot;
                _hasChangedSinceSnapshot = value;
                if (old != value)
                {
                    HasChangedSinceSnapshotChanged.Emit(() => new ValueChangedEvent<bool>(old, value));
                    if (DataTracker != null)
                    {
                        DataTracker.NotifyEntityHasChangedSinceSnapshotChanged(this);
                    }
                }
            }
        }

        public EventEmitter<ValueChangedEvent<bool>> HasChangedChanged { get; } = new EventEmitter<ValueChangedEvent<bool>>();
        public EventEmitter<ValueChangedEvent<bool>> HasChangedSinceSnapshotChanged { get; } = new EventEmitter<ValueChangedEvent<bool>>();

        public EventEmitter<ValueChangedEvent<bool>> AttachedToTrackerChanged { get; } = new EventEmitter<ValueChangedEvent<bool>>();

        public bool PendingInsert
        {
            get => _pendingInsert;
            private set
            {
                var old = _pendingInsert;
                _pendingInsert = value;
                if (old != value)
                {
                    PendingInsertChanged.Emit(() => new ValueChangedEvent<bool>(old, value));
                }
            }
        }

        public EventEmitter<ValueChangedEvent<bool>> PendingInsertChanged { get; } = new EventEmitter<ValueChangedEvent<bool>>();

        public bool IsAttachedToGraph
        {
            get => _isAttachedToGraph;
            set
            {
                var old = _isAttachedToGraph;
                _isAttachedToGraph = value;
                if (old != value)
                {
                    IsAttachedToGraphChanged.Emit(() => new ValueChangedEvent<bool>(old, value));
                    UpdatePendingInsert();
                }
            }
        }

        private void UpdatePendingInsert()
        {
            PendingInsert = IsNew && !MarkedForAnyDeletion;
        }

        public EventEmitter<ValueChangedEvent<bool>> IsAttachedToGraphChanged { get; } = new EventEmitter<ValueChangedEvent<bool>>();
        public Guid Id { get; set; }

        //private CompositeKey _remoteKey;
        public bool IsNew
        {
            get => _isNew;
            set
            {
                var old = _isNew;
                _isNew = value;
                if (value)
                {
                    // TODO: Remove this..?
                    //_remoteKey = null;
                    MarkedForDeletion = false;
                }
                if (old != value)
                {
                    IsNewChanged.Emit(() => new ValueChangedEvent<bool>(old, value));
                    UpdatePendingInsert();
                    CheckHasChanged();
                    if (DataTracker != null)
                    {
                        DataTracker.NotifyEntityIsNewChanged(this);
                    }
                }

                UpdateStatus();
            }
        }

        private void UpdateStatus()
        {
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

            Status = status;
            _settingStatus = false;
        }

        public EventEmitter<MarkedForDeletionChangeEvent> MarkedForDeletionChanged { get; } = new EventEmitter<MarkedForDeletionChangeEvent>();
        public EventEmitter<ValueChangedEvent<bool>> IsNewChanged { get; } = new EventEmitter<ValueChangedEvent<bool>>();
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
                    MarkedForDeletionChanged.Emit(() => new MarkedForDeletionChangeEvent(this, value));
                    UpdatePendingInsert();
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
                UpdatePendingInsert();
                UpdateStatus();
            }
        }

        public bool MarkedForAnyDeletion => MarkedForDeletion || MarkedForCascadeDeletion;

        public void UnmarkForDeletion()
        {
            MarkedForDeletion = false;
            MarkedForCascadeDeletion = false;
        }

        public void AbandonChanges()
        {
            AbandonPropertyChanges(null);
        }

        public void AbandonPropertyChanges(IProperty[] properties)
        {
            var ev = new AbandonChangeEvent(this, null);
            AbandonEvents.EmitStartedAsync(() => ev);
            StateEvents.AbandoningEntityChanges.Emit(() => ev);
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
            StateEvents.AbandonedEntityChanges.Emit(() => ev);
        }

        public void AddSnapshot()
        {
            SetSnapshotValue(Status);
        }

        public void ClearSnapshotValue()
        {
            SnapshotStatus = Status;
        }

        public void SetSnapshotValue(EntityStatus snapshotStatus)
        {
            SnapshotStatus = snapshotStatus;
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
            var compositeKey = new CompositeKey(EntityConfiguration.TypeName, EntityConfiguration.Key.Properties.Length);
            for (var i = 0; i < EntityConfiguration.Key.Properties.Length; i++)
            {
                var property = EntityConfiguration.Key.Properties[i];
                compositeKey.Keys[i] = new KeyValue(
                    property.Name,
                    GetPropertyState(property.Name).RemoteValue,
                    property.TypeDefinition);
            }
            return compositeKey;
        }

        public Guid? PersistenceKey { get; set; }
        public List<CascadeDeletion> CascadeDeletedBy { get; } = new List<CascadeDeletion>();

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
                key.SetValue(Entity, LocalKey.Keys.Single(_ => _.Name == key.PropertyName).Value);
            }
        }

        public bool Floating { get; set; }
        public DataTracker DataTracker { get; }
        public IOperationEvents<IQueuedCrudOperation, IEntityCrudResult> StatefulSaveEvents { get; } = new OperationEvents<IQueuedCrudOperation, IEntityCrudResult>();
        IOperationEventsBase IStateful.StatefulSaveEvents => StatefulSaveEvents;
        public IOperationEvents<IQueuedCrudOperation, IEntityCrudResult> SaveEvents { get; } = new OperationEvents<IQueuedCrudOperation, IEntityCrudResult>();
        IOperationEventsBase IStateful.SaveEvents => SaveEvents;
        public IOperationEvents<AbandonChangeEvent, AbandonChangeEvent> AbandonEvents { get; } = new OperationEvents<AbandonChangeEvent, AbandonChangeEvent>();
        IOperationEventsBase IStateful.AbandonEvents => AbandonEvents;
        public T Entity { get; }
        object IEntityStateBase.Entity => Entity;
        public Type EntityType { get; }
        //public IDataContext DataContext => DataTracker.DataContext;
        public IEntityConfiguration EntityConfiguration { get; }

        public EntityState(
            DataTracker dataTracker,
            T entity,
            Type entityType,
            IEntityConfiguration entityConfiguration,
            bool isNew)
        {
            Id = Guid.NewGuid();
            DataTracker = dataTracker;
            if (DataTracker != null)
            {
                EventManager.Subscribe(DataTracker.HasSnapshotChanged, _ =>
                {
                    CheckHasChanged();
                });
            }
            Entity = entity;
            EntityType = entityType;
            EntityConfiguration = entityConfiguration;
            Properties = new List<IPropertyState>();
            for (var i = 0; i < EntityConfiguration.Properties.Count; i++)
            {
                var property = EntityConfiguration.Properties[i];
                var propertyState = new PropertyState(property, this);
                Properties.Add(propertyState);
                propertyState.HasChangedChanged.Subscribe(_ => CheckHasChanged());
                propertyState.HasChangedSinceSnapshotChanged.Subscribe(_ => CheckHasChanged());
                propertyState.HasSnapshotValueChanged.Subscribe(_ => CheckHasChanged());
                ;
            }

            UpdateHasChanges();
            LocalKey = entityConfiguration.GetCompositeKey(entity);
            if (isNew)
            {
                PendingInsert = true;
            }
            IsNew = isNew;
            _snapshotStatus = Status;
            MonitorSaveEvents();
            CanNotifyStatusChange = true;
            NotifyStatusChange();
        }

        public EventEmitter<ValueChangedEvent<EntityStatus>> StatusChanged { get; } = new EventEmitter<ValueChangedEvent<EntityStatus>>();
        public EventEmitter<ValueChangedEvent<bool>> StatusHasChangedChanged { get; } = new EventEmitter<ValueChangedEvent<bool>>();
        public EventEmitter<ValueChangedEvent<bool>> StatusHasChangedSinceSnapshotChanged { get; } = new EventEmitter<ValueChangedEvent<bool>>();

        public bool StatusHasChanged
        {
            get { return _statusHasChanged; }
            set { _statusHasChanged = value; }
        }

        public bool StatusHasChangedSinceSnapshot
        {
            get { return _statusHasChangedSinceSnapshot; }
            set { _statusHasChangedSinceSnapshot = value; }
        }

        private TrackingSet<T> TrackingSet
        {
            get
            {
                if (_trackingSet == null && DataTracker != null)
                {
                    _trackingSet = DataTracker.TrackingSet<T>();
                }
                return _trackingSet;
            }
        }

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
                    StatusChanged.Emit(() => new ValueChangedEvent<EntityStatus>(old, value));
                    //if (DataTracker != null)
                    //{
                    //    DataTracker.NotifyStatusChanged(this, value);
                    //}
                }

                UpdateStatusHasChanged();
            }
        }

        private void UpdateStatusHasChanged()
        {
            if (SnapshotStatus == EntityStatus.Existing &&
                Status == EntityStatus.ExistingAndPendingDelete)
            {
                StatusHasChanged = true;
            }
            else if (SnapshotStatus == EntityStatus.ExistingAndPendingDelete &&
                Status == EntityStatus.Existing)
            {
                StatusHasChanged = true;
            }
            else if (SnapshotStatus == EntityStatus.New &&
                Status == EntityStatus.NewAndDeleted)
            {
                StatusHasChanged = true;
            }
            else if (SnapshotStatus == EntityStatus.NewAndDeleted &&
                Status == EntityStatus.New)
            {
                StatusHasChanged = true;
            }
            else
            {
                StatusHasChanged = false;
            }
            NotifyStatusChange();
        }

        private bool CanNotifyStatusChange { get; set; }

        private void NotifyStatusChange()
        {
            if (DataTracker != null && CanNotifyStatusChange)
            {
                DataTracker.NotifyStatusChanged(this,
                    Status == EntityStatus.New ||
                    Status == EntityStatus.ExistingAndPendingDelete);
            }
        }

        public EntityStatus SnapshotStatus
        {
            get => _snapshotStatus;
            protected set => _snapshotStatus = value;
        }

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

        private readonly Dictionary<Guid, IPropertyState[]> _operations = new Dictionary<Guid, IPropertyState[]>();
        private bool _isAttachedToGraph;
        private bool _hasChanged;
        private bool _hasChangedSinceSnapshot;
        private IqlEventManager _eventManager;
        private EntityStatus _status = EntityStatus.Unattached;
        private bool _settingStatus = false;
        private bool _markedForCascadeDeletion;
        private bool _statusHasChanged;
        private bool _statusHasChangedSinceSnapshot;
        private EntityStatus _snapshotStatus = EntityStatus.Unattached;
        private TrackingSet<T> _trackingSet;

        private void MonitorSaveEvents()
        {
            EventManager.SubscribeAsync(SaveEvents.StartedAsync,
                async _ =>
                {
                    if (_.Operation.Kind == IqlOperationKind.Update)
                    {
                        var updateOp = (IUpdateEntityOperation)_.Operation;
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
            EventManager.SubscribeAsync(SaveEvents.SuccessfulAsync,
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
            EventManager.SubscribeAsync(SaveEvents.CompletedAsync,
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

        private IqlEventManager EventManager => _eventManager = _eventManager ?? new IqlEventManager();

        public static IEntityStateBase New(object entity, Type entityType, IEntityConfiguration entityConfiguration)
        {
            return (IEntityStateBase)
                Activator.CreateInstance(typeof(EntityState<>).MakeGenericType(entityType),
                new object[] { entity, entityType, entityConfiguration });
        }

        public IPropertyState[] GetChangedProperties(IProperty[] properties = null)
        {
            var propertyStates = PropertyStates.Where(ps =>
                    ps.HasChanged && !ps.Property.Kind.HasFlag(PropertyKind.Relationship) && (properties == null || properties.Contains(ps.Property)))
                .ToArray();
            return propertyStates;
        }

        public bool HasRelationshipChanged(IProperty relationshipProperty)
        {
            if (relationshipProperty.Relationship.ThisIsTarget)
            {
                return false;
            }
            if (relationshipProperty.Kind.HasFlag(PropertyKind.Relationship))
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
                        var constraintState = GetPropertyState(constraint.Name);
                        if (constraintState.HasChanged)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }
        private List<IPropertyState> Properties { get; }

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
            return (T)entity;
        }

        object IEntityStateBase.EntityBeforeChanges()
        {
            return EntityBeforeChanges();
        }

        public IPropertyState[] PropertyStates => Properties.ToArray();

        public IPropertyState GetPropertyState(string name)
        {
            var state = Properties.SingleOrDefault(p => p.Property.PropertyName == name);
            return state;
        }

        public IPropertyState FindPropertyState(string name)
        {
            var state = Properties.FirstOrDefault(p => p.Property.PropertyName == name);
            state = state ?? Properties.FirstOrDefault(p => p.Property.PrimaryProperty.PropertyName == name);
            state = state ?? Properties.FirstOrDefault(p => p.Property.PrimaryProperty.GetGroupProperties()
                        .Any(_ => _.Name == name));
            state = state ?? Properties.FirstOrDefault(p => p.Property.PropertyGroup != null && p.Property.PropertyGroup.Name == name);
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
                PropertyStates = PropertyStates.Where(_ => IsNew || _.HasChanged).Select(_ => _.PrepareForJson()).Where(_ => _ != null)
            };
        }

        public void Dispose()
        {
            MarkedForDeletionChanged?.Dispose();
            EventManager?.Dispose();
            foreach (var propState in PropertyStates)
            {
                propState.Dispose();
            }
        }
    }
}