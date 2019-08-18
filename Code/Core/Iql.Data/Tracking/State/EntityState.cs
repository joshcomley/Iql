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
using Iql.Entities.Relationships;
using Iql.Events;

namespace Iql.Data.Tracking.State
{
    [DebuggerDisplay("{EntityType.Name}")]
    public class EntityState<T> : IEntityState<T>
        where T : class
    {
        private bool _markedForDeletion;
        private bool _isNew = true;
        private CompositeKey _remoteKey;
        private CompositeKey _localKey;

        //private CompositeKey _remoteKey;
        public bool IsNew
        {
            get => _isNew;
            set
            {
                _isNew = value;
                if (value)
                {
                    //_remoteKey = null;
                    MarkedForDeletion = false;
                }
            }
        }

        public EventEmitter<MarkedForDeletionChangeEvent> MarkedForDeletionChanged { get; } = new EventEmitter<MarkedForDeletionChangeEvent>();
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
                }
            }
        }

        public bool MarkedForCascadeDeletion { get; set; }

        public bool MarkedForAnyDeletion => MarkedForDeletion || MarkedForCascadeDeletion;

        public void UnmarkForDeletion()
        {
            MarkedForDeletion = false;
            MarkedForCascadeDeletion = false;
        }

        public void AbandonChanges()
        {
            var ev = new AbandonChangeEvent(this, null);
            AbandonEvents.EmitStartedAsync(() => ev);
            StateEvents.AbandoningEntityChanges.Emit(() => ev);
            for (var i = 0; i < Properties.Count; i++)
            {
                var state = Properties[i];
                state.AbandonChanges();
            }
            MarkedForDeletion = false;
            MarkedForCascadeDeletion = false;
            AbandonEvents.EmitCompletedAsync(() => ev);
            AbandonEvents.EmitSuccessAsync(() => ev);
            ClearStatefulEvents();
            StateEvents.AbandonedEntityChanges.Emit(() => ev);
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

            MarkedForDeletion = state.MarkedForDeletion;
            MarkedForCascadeDeletion = state.MarkedForCascadeDeletion;
            if (state.PersistenceKey != null)
            {
                PersistenceKey = state.PersistenceKey.EnsureGuid();
            }

            IsNew = state.IsNew;
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
            IEntityConfiguration entityConfiguration)
        {
            DataTracker = dataTracker;
            Entity = entity;
            EntityType = entityType;
            EntityConfiguration = entityConfiguration;
            Properties = new List<IPropertyState>();
            foreach (var property in EntityConfiguration.Properties)
            {
                Properties.Add(new PropertyState(property, this));
            }
            LocalKey = entityConfiguration.GetCompositeKey(entity);
            MonitorSaveEvents();
        }

        private Dictionary<Guid, IPropertyState[]> _operations = new Dictionary<Guid, IPropertyState[]>();
        private async Task EmitPropertyEventAsync(ICrudOperation operation, Func<IPropertyState, IEntityPropertyEvent, Task> fn)
        {
        }

        private void MonitorSaveEvents()
        {
            EventManager = new IqlEventManager();
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
                            var propState = GetPropertyState(prop.Property.PropertyName);
                            await propState.SaveEvents.EmitStartedAsync(() =>
                                new IqlEntityPropertyEvent<T>(propState.Property, this, null));
                        }
                        _operations.Add(_.Operation.Id, changed);
                    }
                    else if (_.Operation.Kind == IqlOperationKind.Add)
                    {
                        foreach (var propState in PropertyStates)
                        {
                            await propState.SaveEvents.EmitStartedAsync(() =>
                                new IqlEntityPropertyEvent<T>(propState.Property, this, null));
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
                            await prop.SaveEvents.EmitSuccessAsync(() =>
                                new IqlEntityPropertyEvent<T>(prop.Property, this, null));
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
                            await prop.SaveEvents.EmitCompletedAsync(() =>
                                new IqlEntityPropertyEvent<T>(prop.Property, this, null));
                        }

                        _operations.Remove(_.Operation.Id);
                    }
                });
        }

        private IqlEventManager EventManager { get; set; }

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
                property.Property.SetValue(entity, property.RemoteValue);
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
            var state = Properties.SingleOrDefault(p => p.Property.Name == name);
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
            foreach(var propState in PropertyStates)
            {
                propState.Dispose();
            }
        }
    }
}