using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Iql.Conversion;
using Iql.Conversion.State;
using Iql.Data.Context;
using Iql.Data.Crud.Operations;
using Iql.Data.Events;
using Iql.Data.Lists;
using Iql.Entities;
using Iql.Entities.Events;
using Iql.Entities.Extensions;
using Iql.Entities.Lists;
using Iql.Entities.PropertyChangers;
using Iql.Entities.Relationships;
using Iql.Events;
using Iql.Extensions;

namespace Iql.Data.Tracking.State
{
    [DebuggerDisplay("{Property.Name} - changes: (ac: {HasAnyChanges}, acss: {HasAnyChangesSinceSnapshot}, c: {HasChanges}, css: {HasChangesSinceSnapshot})")]
    public class PropertyState : IPropertyState
    {
        private bool _canUndo;
        private IPropertyState[] _groupStates;
        private bool _hasChanged;
        private string _id;
        private bool _isLocked;
        private bool _isUpdatingHasChanged;
        private object _localValue;
        private bool _nestedHasChanged;
        private bool? _isRelationshipCollection;
        private bool? _otherEndIsCollection;
        private List<IProperty> _otherSideProperties;
        private PropertyChanger _propertyChanger;
        //private object _relationshipEntityLocal;
        //private object _relationshipEntityRemote;
        //private object _relationshipEntitySnapshot;
        private IPropertyState _relationshipPropertyState;
        //private IPropertyState _relationshipPropertyStateLocal;
        //private IPropertyState _relationshipPropertyStateRemote;
        //private IPropertyState _relationshipPropertyStateSnapshot;
        private object _remoteValue;
        private object _remoteValueClone;
        private bool _remoteValueSet;
        private IPropertyState[] _siblingStates;
        private object _snapshotValue;
        private bool _hasSnapshotValue;
        private ObservableList<IEntityStateBase> _itemsChanged;
        private ObservableList<IEntityStateBase> _itemsRemoved;
        private ObservableList<IEntityStateBase> _itemsAdded;
        private ObservableList<IEntityStateBase> _itemsChangedSinceSnapshot;
        private ObservableList<IEntityStateBase> _itemsRemovedSinceSnapshot;
        private ObservableList<IEntityStateBase> _itemsAddedSinceSnapshot;
        private bool _hasNestedChanges;
        private bool _hasNestedChangesSinceStart;
        private bool _hasChangedSinceSnapshot;

        public PropertyState(
            IProperty property,
            IEntityStateBase entityState)
        {
            Guid = Guid.NewGuid();
            Property = property;
            EntityState = entityState;
            EnsureRemoteValue();
        }

        public object DebugKey { get; set; }

        public void PauseEvents()
        {
            _eventEmitterManager.Pause();
        }

        public void ResumeEvents()
        {
            _eventEmitterManager.Resume();
        }

        private string Id
        {
            get
            {
                if (_id == null)
                {
                    _id = Guid.NewGuid().ToString();
                }

                return _id;
            }
        }

        public bool HasSnapshotValue
        {
            get => _hasSnapshotValue;
            private set
            {
                var old = _hasSnapshotValue;
                _hasSnapshotValue = value;
                if (old != value)
                {
                    HasSnapshotValueChanged.Emit(() => new ValueChangedEvent<bool>(old, value));
                }
            }
        }

        public ObservableList<IEntityStateBase> ItemsChanged => _itemsChanged = _itemsChanged ?? new ObservableList<IEntityStateBase>();
        public ObservableList<IEntityStateBase> ItemsRemoved => _itemsRemoved = _itemsRemoved ?? new ObservableList<IEntityStateBase>();
        public ObservableList<IEntityStateBase> ItemsAdded => _itemsAdded = _itemsAdded ?? new ObservableList<IEntityStateBase>();
        public ObservableList<IEntityStateBase> ItemsChangedSinceSnapshot => _itemsChangedSinceSnapshot = _itemsChangedSinceSnapshot ?? new ObservableList<IEntityStateBase>();
        public ObservableList<IEntityStateBase> ItemsRemovedSinceSnapshot => _itemsRemovedSinceSnapshot = _itemsRemovedSinceSnapshot ?? new ObservableList<IEntityStateBase>();
        public ObservableList<IEntityStateBase> ItemsAddedSinceSnapshot => _itemsAddedSinceSnapshot = _itemsAddedSinceSnapshot ?? new ObservableList<IEntityStateBase>();
        
        private IqlEventSubscriberManager _eventSubscriptionManager;
        private IqlEventSubscriberManager EventSubscriberManager => _eventSubscriptionManager = _eventSubscriptionManager ?? new IqlEventSubscriberManager();

        private IOperationEvents<IEntityPropertyEvent, IEntityPropertyEvent> _statefulSaveEvents;
        public IOperationEvents<IEntityPropertyEvent, IEntityPropertyEvent> StatefulSaveEvents => _statefulSaveEvents = _statefulSaveEvents ?? new OperationEvents<IEntityPropertyEvent, IEntityPropertyEvent>();
        private IOperationEvents<IEntityPropertyEvent, IEntityPropertyEvent> _saveEvents;

        public IOperationEvents<IEntityPropertyEvent, IEntityPropertyEvent> SaveEvents => _saveEvents = _saveEvents ?? new OperationEvents<IEntityPropertyEvent, IEntityPropertyEvent>();
        private IOperationEvents<AbandonChangeEvent, AbandonChangeEvent> _abandonEvents;

        public IOperationEvents<AbandonChangeEvent, AbandonChangeEvent> AbandonEvents => _abandonEvents = _abandonEvents ?? new OperationEvents<AbandonChangeEvent, AbandonChangeEvent>();

        public PropertyChanger PropertyChanger
        {
            get { return _propertyChanger = _propertyChanger ?? Property.TypeDefinition.ResolveChanger(); }
        }

        public string HasChangedText { get; private set; }

        public bool IsRelationshipCollection
        {
            get
            {
                if (_isRelationshipCollection == null)
                {
                    _isRelationshipCollection = Property.Relationship != null &&
                                                Property.Relationship.ThisEnd.Property.TypeDefinition.Kind ==
                                                IqlType.Collection;
                }

                return _isRelationshipCollection.Value;
            }
        }
        private bool OtherEndIsCollection
        {
            get
            {
                if (_otherEndIsCollection == null)
                {
                    _otherEndIsCollection = EntityState != null && Property.Kind.HasFlag(IqlPropertyKind.Relationship) &&
                                            Property.Relationship.ThisIsSource &&
                                            Property.Relationship.OtherEnd.Property.TypeDefinition.Kind ==
                                            IqlType.Collection;
                }

                return _otherEndIsCollection.Value;
            }
        }

        public IPropertyState RelationshipPropertyState
        {
            get
            {
                if (Property.Relationship != null && _relationshipPropertyState == null)
                {
                    _relationshipPropertyState = Property.Relationship.ThisEnd.Property == Property
                        ? this
                        : EntityState.GetPropertyState(Property.Relationship.ThisEnd.Property.PropertyName);
                }

                return _relationshipPropertyState;
            }
        }

        public ILockable Parent => EntityState;
        public bool IsLocked => _isLocked || Parent == null || Parent.IsLocked;

        public void Lock()
        {
            _isLocked = true;
        }

        public void Unlock()
        {
            _isLocked = false;
        }

        public DataTracker DataTracker => EntityState == null ? null : EntityState.DataTracker;
        IOperationEventsBase IStateful.StatefulSaveEvents => StatefulSaveEvents;
        IOperationEventsBase IStateful.SaveEvents => SaveEvents;
        IOperationEventsBase IStateful.AbandonEvents => AbandonEvents;

        public bool HasAnyChanges
        {
            get => _hasAnyChanges;
            protected set
            {
                if (value == _hasAnyChanges)
                {
                    return;
                }
                _hasAnyChanges = value;
                HasAnyChangesChanged.Emit(() => new ValueChangedEvent<bool>(!_hasAnyChanges, value));
            }
        }


        public bool HasAnyChangesSinceSnapshot
        {
            get => _hasAnyChangesSinceSnapshot;
            protected set
            {
                if (value == _hasAnyChangesSinceSnapshot)
                {
                    return;
                }
                _hasAnyChangesSinceSnapshot = value;
                HasAnyChangesSinceSnapshotChanged.Emit(() => new ValueChangedEvent<bool>(!_hasAnyChangesSinceSnapshot, value));
            }
        }

        public bool HasChangesSinceSnapshot
        {
            get => _hasChangedSinceSnapshot;
            private set
            {
                var old = _hasChangedSinceSnapshot;
                _hasChangedSinceSnapshot = value;
                if (old != value)
                {
                    UpdateHasAnyChanges();
                    if (EntityState != null)
                    {
                        EntityState.DataTracker.NotifyChangedSinceSnapshotChanged(this, HasChanges, value);
                    }

                    HasChangesSinceSnapshotChanged.Emit(
                        () => new ValueChangedEvent<bool>(old, value));
                    if (EntityState != null)
                    {
                        EntityState.CheckHasChanged();
                    }
                }
                CanUndo = HasSnapshotValue && HasChangesSinceSnapshot || HasChanges;
            }
        }

        public bool CanUndo
        {
            get => _canUndo;
            set
            {
                var old = _canUndo;
                _canUndo = value;
                if (value != old)
                {
                    CanUndoChanged.Emit(() => new ValueChangedEvent<bool>(old, value));
                }
            }
        }

        public bool HasChanges
        {
            get
            {
                var forceUpdate = false;
                if (!LocalValueSet && EntityState != null)
                {
                    LocalValue = Property.GetValue(EntityState.Entity);
                    forceUpdate = true;
                }

                if (PropertyChanger.CanSilentlyChange)
                {
                    forceUpdate = true;
                }

                if (forceUpdate && !_nestedHasChanged)
                {
                    _nestedHasChanged = true;
                    UpdateHasChanged();
                }

                _nestedHasChanged = false;
                return _hasChanged;
            }
            private set
            {
                var old = _hasChanged;
                _hasChanged = value;
                if (old != value)
                {
                    if (EntityState != null)
                    {
                        EntityState.DataTracker.NotifyChangedChanged(this, _hasChanged);
                    }
                    HasChangesChanged.Emit(() => new ValueChangedEvent<bool>(old, value));
                    if (EntityState != null)
                    {
                        EntityState.CheckHasChanged();
                    }

                    if (!value)
                    {
                        HasChangesSinceSnapshot = false;
                    }

                    UpdateHasAnyChanges();
                }
            }
        }

        private void UpdateHasAnyChanges()
        {
            HasAnyChanges = HasChanges || HasNestedChanges;
            HasAnyChangesSinceSnapshot = HasChangesSinceSnapshot || HasNestedChangesSinceSnapshot;
        }

        public object RemoteValue
        {
            get
            {
                EnsureRemoteValue();
                return _remoteValueClone;
            }
            set
            {
                _remoteValueSet = true;
                SetRemoteValue(value);
                UpdateHasChanged();
            }
        }

        public object SnapshotValue => HasSnapshotValue ? _snapshotValue : RemoteValue;

        public void AddSnapshot()
        {
            if (HasSnapshotValue)
            {
                if (HasChangesSinceSnapshot)
                {
                    AddSnapshotInternal();
                }
            }
            else if (HasChanges)
            {
                AddSnapshotInternal();
            }
        }

        public void SetSnapshotValue(object value)
        {
            var newSnapshotValue = PropertyChanger.CloneValue(value);
            ObserveIfNecessary(_snapshotValue, newSnapshotValue, ChangeCalculationKind.Snapshot);
            _snapshotValue = newSnapshotValue;
            ClearSnapshotRelationshipChanges();
            HasSnapshotValue = true;
            UpdateSnapshotRelatedListChanged(true);
            UpdateHasChanged();
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

        public void ClearSnapshotValue()
        {
            ClearSnapshotRelationshipChanges();
            HasSnapshotValue = false;
            _snapshotValue = null;
            UpdateSnapshotRelatedListChanged();
            UpdateHasChanged();
        }

        private void ClearSnapshotRelationshipChanges()
        {
            if (IsRelationshipCollection)
            {
                ItemsChangedSinceSnapshot.Clear();
                ItemsRemovedSinceSnapshot.Clear();
                ItemsAddedSinceSnapshot.Clear();
            }
        }

        public bool LocalValueSet { get; private set; }

        string addedKey = $"{nameof(LocalValue)}_{nameof(RelatedListChangeKind.Added)}";
        string removedKey = $"{nameof(LocalValue)}_{nameof(RelatedListChangeKind.Removed)}";
        private IProperty _relationshipOtherEndProperty;
        private bool? _isCount;
        private bool _hasAnyChanges;
        private bool _hasAnyChangesSinceSnapshot;

        private IProperty RelationshipOtherEndProperty => _relationshipOtherEndProperty = _relationshipOtherEndProperty ?? Property.Relationship.OtherEnd.Property;

        public object LocalValue
        {
            get => LocalValueSet ? _localValue : RemoteValue;
            set
            {
                LocalValueSet = true;
                var oldValue = _localValue;
                _localValue = value;

                UpdateHasChanged();
                if (DataTracker != null && Property.Relationship != null && Property.Relationship.ThisIsSource)
                {
                    DataTracker.NotifyLocalValueChanged(this);
                }

                if (oldValue != value)
                {
                    LocalValueChanged.Emit(() => new ValueChangedEvent<object>(oldValue, value));
                }

                if (Property.Relationship != null && Property.TypeDefinition.Kind == IqlType.Collection)
                {
                    if (oldValue != null)
                    {
                        EventSubscriberManager.UnsubscribeAll("LocalValue");
                    }

                    if (value != null)
                    {
                        var relatedList = (IRelatedList)value;
                        var remoteList = (IEnumerable<object>)RemoteValue;
                        if (DataTracker != null)
                        {
                            foreach (var item in relatedList)
                            {
                                Watch(DataTracker.GetEntityState(item));
                            }
                        }
                        EventSubscriberManager.Subscribe(relatedList.RelatedListChange, _ =>
                        {
                            RelatedListUpdated(_, relatedList, remoteList);
                        }, "LocalValue");
                    }
                }
                UpdateHasChanged();
            }
        }

        private Dictionary<IEntityStateBase, IqlEventSubscriberManager> _stateWatchers = new Dictionary<IEntityStateBase, IqlEventSubscriberManager>();
        private Dictionary<IEntityStateBase, IqlEventSubscriberManager> _stateSinceSnapshotWatchers = new Dictionary<IEntityStateBase, IqlEventSubscriberManager>();

        private void Watch(IEntityStateBase state)
        {
            if (state != null)
            {
                if (!_stateWatchers.ContainsKey(state))
                {
                    var manager = new IqlEventSubscriberManager();
                    _stateWatchers.Add(state, manager);
                    var states = GetRelationshipPropertyStates(state);
                    manager.Subscribe(state.HasChangedChanged, _ =>
                    {
                        if (!DataTracker.AddingPropertySnapshots)
                        {
                            UpdateSnapshotRelatedListChanged();
                        }
                    });
                    foreach (var propertyState in state.PropertyStates)
                    {
                        if (states.Contains(propertyState))
                        {
                            manager.SubscribeAll(
                                new IEventSubscriberBase[]
                                {
                                    propertyState.HasChangesChanged,
                                    propertyState.OnReset
                                },
                                (caller, _) =>
                            {
                                if (!DataTracker.AddingPropertySnapshots)
                                {
                                    UpdateState(state, ChangeCalculationKind.Remote, ItemsAdded, ItemsRemoved, ItemsChanged, manager, _stateWatchers);
                                }
                            });
                        }
                        else
                        {
                            manager.Subscribe(propertyState.HasChangesChanged, _ =>
                            {
                                if (!DataTracker.AddingPropertySnapshots)
                                {
                                    UpdateSnapshotRelatedListChanged();
                                }
                            });
                        }
                    }
                }
                if (!_stateSinceSnapshotWatchers.ContainsKey(state))
                {
                    var manager = new IqlEventSubscriberManager();
                    _stateSinceSnapshotWatchers.Add(state, manager);
                    var states = GetRelationshipPropertyStates(state);
                    manager.Subscribe(state.HasChangedSinceSnapshotChanged, _ =>
                    {
                        if (!DataTracker.AddingPropertySnapshots)
                        {
                            UpdateSnapshotRelatedListChanged();
                        }
                    });
                    foreach (var propertyState in state.PropertyStates)
                    {
                        if (states.Contains(propertyState))
                        {
                            manager.SubscribeAll(
                                new IEventSubscriberBase[]
                                {
                                    propertyState.HasChangesSinceSnapshotChanged,
                                    propertyState.SnapshotValueChanged,
                                    propertyState.OnReset
                                }, (caller, _) =>
                             {
                                 if (!DataTracker.AddingPropertySnapshots)
                                 {
                                     UpdateState(state, ChangeCalculationKind.Snapshot, ItemsAddedSinceSnapshot, ItemsRemovedSinceSnapshot, ItemsChangedSinceSnapshot, manager, _stateSinceSnapshotWatchers);
                                 }
                             });
                        }
                        else
                        {
                            manager.SubscribeAll(new IEventSubscriberBase[] { propertyState.HasChangesSinceSnapshotChanged, propertyState.SnapshotValueChanged }, (caller, _) =>
                            {
                                if (!DataTracker.AddingPropertySnapshots)
                                {
                                    UpdateSnapshotRelatedListChanged();
                                }
                            });
                        }
                    }
                }
            }
        }

        private IPropertyState[] GetRelationshipPropertyStates(IEntityStateBase state)
        {
            var properties = GetRelationshipProperties();
            var states = properties.Select(_ => state.GetPropertyState(_.PropertyName))
                .Where(_ => _ != null)
                .ToArray();
            return states;
        }

        private IProperty[] GetRelationshipProperties()
        {
            return Property.Relationship.OtherEnd.AllProperties;
        }

        private void UpdateState(IEntityStateBase state, ChangeCalculationKind changeCalculationKind,
            ObservableList<IEntityStateBase> itemsAdded,
            ObservableList<IEntityStateBase> itemsRemoved,
            ObservableList<IEntityStateBase> itemsChanged,
            IqlEventSubscriberManager subscriptionManager,
            Dictionary<IEntityStateBase, IqlEventSubscriberManager> stateWatchers)
        {
            if (SanityCheckRelationshipSource(state, Property.Relationship.OtherEnd))
            {
                if (DoesRelationshipSourceMatch(state, Property.Relationship.OtherEnd))
                {
                    if (state.IsNew || HasRelationshipSourceChanged(state, Property.Relationship.OtherEnd,
                        changeCalculationKind))
                    {
                        if (!itemsAdded.Contains(state))
                        {
                            itemsAdded.Add(state);
                        }
                    }
                    else
                    {
                        itemsAdded.Remove(state);
                        itemsRemoved.Remove(state);
                    }
                }
                else
                {
                    if (DoesRelationshipSourceMatch(state, Property.Relationship.OtherEnd, null,
                            changeCalculationKind == ChangeCalculationKind.Remote ? CheckValueKind.Remote : CheckValueKind.Snapshot))
                    {
                        var isDisallowed = changeCalculationKind == ChangeCalculationKind.Remote && state.IsNew;
                        if (!isDisallowed && !itemsRemoved.Contains(state))
                        {
                            itemsRemoved.Add(state);
                        }
                    }
                    else
                    {
                        itemsChanged.Remove(state);
                        itemsAdded.Remove(state);
                        itemsRemoved.Remove(state);
                        subscriptionManager.UnsubscribeAll();
                        stateWatchers.Remove(state);
                    }
                }
                UpdateSnapshotRelatedListChanged();
            }
        }

        private void RelatedListUpdated(IRelatedListChangeEvent _, IRelatedList relatedList, IEnumerable<object> remoteList)
        {
            if (EntityState != null)
            {
                var state = DataTracker.GetEntityState(_.Item);
                if (state != null)
                {
                    switch (_.Kind)
                    {
                        case RelatedListChangeKind.Added:
                            if (state.IsNew || HasRelationshipSourceChanged(state, Property.Relationship.OtherEnd, ChangeCalculationKind.Remote))
                            {
                                ItemsAdded.Add(state);
                            }
                            if (state.IsNew || HasRelationshipSourceChanged(state, Property.Relationship.OtherEnd, ChangeCalculationKind.Snapshot))
                            {
                                ItemsAddedSinceSnapshot.Add(state);
                            }
                            Watch(state);
                            UpdateHasChanged();
                            //RelatedListItemAdded(_, relatedList);
                            break;
                        case RelatedListChangeKind.Removed:
                            if (state.IsNew)
                            {
                                ItemsAdded.Remove(state);
                                ItemsAddedSinceSnapshot.Remove(state);
                            }
                            else
                            {
                                ItemsRemoved.Add(state);
                                ItemsRemovedSinceSnapshot.Add(state);
                            }
                            Watch(state);
                            UpdateHasChanged();
                            //RelatedListItemRemoved(_, relatedList, remoteList);
                            break;
                    }
                }
            }
            UpdateSnapshotRelatedListChanged();
            UpdateHasChanged();
        }

        private void RelatedListItemRemoved(IRelatedListChangeEvent _, IRelatedList relatedList, IEnumerable<object> remoteList)
        {
            UpdateSnapshotRelatedListChanged();
        }

        private void RelatedListItemAdded(IRelatedListChangeEvent _, IRelatedList relatedList)
        {
            UpdateSnapshotRelatedListChanged();
        }

        public void UpdateSnapshotRelatedListChanged(bool updateAddedAndRemoved = false)
        {
            if (IsLocked || !IsRelationshipCollection)
            {
                return;
            }

            //updateAddedAndRemoved = false;
            var relatedList = ((IRelatedList)LocalValue);
            if (HasSnapshotValue)
            {
                var snapshotList = ((IEnumerable<object>)SnapshotValue).ToArray();
                var addedSinceSnapshot = new List<object>();
                var removedSinceSnapshot = new List<object>();
                var changedSinceSnapshot = new List<object>();
                var changed = new List<object>();
                for (var i = 0; i < relatedList.Count; i++)
                {
                    var item = relatedList[i];
                    var state = DataTracker.GetEntityState(item);
                    if (state != null &&
                        !snapshotList.Contains(item) &&
                        DoesRelationshipSourceMatch(state, Property.Relationship.OtherEnd) &&
                        (state.IsNew || HasRelationshipSourceChanged(state, Property.Relationship.OtherEnd, ChangeCalculationKind.Snapshot))
                        )
                    {
                        addedSinceSnapshot.Add(item);
                        Watch(state);
                    }

                    if (state != null && !state.IsNew && !ItemsAdded.Contains(item) && state.HasChanged &&
                        !HasRelationshipSourceChanged(state, Property.Relationship.OtherEnd,
                            ChangeCalculationKind.Remote) &&
                        DoesRelationshipSourceMatch(state, Property.Relationship.OtherEnd))
                    {
                        changed.Add(state.Entity);
                        Watch(state);
                    }
                }

                for (var i = 0; i < snapshotList.Length; i++)
                {
                    var item = snapshotList[i];
                    var state = DataTracker.GetEntityState(item);
                    if (!relatedList.Contains(item))
                    {
                        removedSinceSnapshot.Add(item);
                        Watch(state);
                    }
                    else if (state != null &&
                             HasRelationshipSourceOtherPropertyChanged(state, Property.Relationship.OtherEnd, ChangeCalculationKind.Snapshot) &&
                             !HasRelationshipSourceChanged(state, Property.Relationship.OtherEnd, ChangeCalculationKind.Snapshot) &&
                             DoesRelationshipSourceMatch(state, Property.Relationship.OtherEnd))
                    {
                        changedSinceSnapshot.Add(item);
                        Watch(state);
                    }
                }

                ChangeList(ItemsChanged, changed);
                ChangeList(ItemsChangedSinceSnapshot, changedSinceSnapshot);
                if (updateAddedAndRemoved)
                {
                    ChangeList(ItemsAddedSinceSnapshot, addedSinceSnapshot);
                    ChangeList(ItemsRemovedSinceSnapshot, removedSinceSnapshot);
                }
            }
            else
            {
                var changed = new List<IEntityStateBase>();
                var changedSinceSnapshot = new List<IEntityStateBase>();
                foreach (var item in relatedList)
                {
                    var state = DataTracker.GetEntityState(item);
                    if (state != null)
                    {
                        if (!state.IsNew && !ItemsAdded.Contains(item) &&
                            DoesRelationshipSourceMatch(state, Property.Relationship.OtherEnd))
                        {
                            if (!HasRelationshipSourceChanged(state, Property.Relationship.OtherEnd, ChangeCalculationKind.Remote) && state.HasChanged)
                            {
                                changed.Add(state);
                                Watch(state);
                            }
                            if (!HasRelationshipSourceChanged(state, Property.Relationship.OtherEnd, ChangeCalculationKind.Snapshot) && state.HasChangedSinceSnapshot)
                            {
                                changedSinceSnapshot.Add(state);
                                Watch(state);
                            }
                        }
                    }
                }

                ChangeEntityStateList(ItemsChanged, changed);
                ChangeEntityStateList(ItemsChangedSinceSnapshot, changedSinceSnapshot);
                if (updateAddedAndRemoved)
                {
                    ChangeEntityStateList(ItemsAddedSinceSnapshot, ItemsAdded);
                    ChangeEntityStateList(ItemsRemovedSinceSnapshot, ItemsRemoved);
                }

                //if (!DataTracker.HasSnapshot)
                //{
                //    ChangeEntityStateList(ItemsAddedSinceSnapshot, ItemsAdded);
                //    ChangeEntityStateList(ItemsChangedSinceSnapshot, ItemsChanged);
                //    ChangeEntityStateList(ItemsRemovedSinceSnapshot, ItemsRemoved);
                //}
                //else
                //{
                //    var entityStateBases = new List<IEntityStateBase>();
                //    ChangeEntityStateList(ItemsAddedSinceSnapshot, entityStateBases);
                //    ChangeEntityStateList(ItemsChangedSinceSnapshot, entityStateBases);
                //    ChangeEntityStateList(ItemsRemovedSinceSnapshot, entityStateBases);
                //}
            }

            UpdateHasChanged();
        }

        private void ChangeList(ObservableList<IEntityStateBase> source, List<object> setTo)
        {
            var copy = source.ToList();
            foreach (var item in setTo)
            {
                var state = DataTracker.GetEntityState(item);
                if (state != null && !source.Contains(state))
                {
                    source.Add(state);
                }
            }
            foreach (var item in copy)
            {
                if (!setTo.Contains(item.Entity))
                {
                    source.Remove(item);
                }
            }
        }

        private void ChangeEntityStateList(ObservableList<IEntityStateBase> source, IList<IEntityStateBase> setTo)
        {
            var copy = source.ToList();
            foreach (var state in setTo)
            {
                if (state != null && !source.Contains(state))
                {
                    source.Add(state);
                }
            }
            foreach (var item in copy)
            {
                if (!setTo.Contains(item))
                {
                    source.Remove(item);
                }
            }
        }

        public bool HasNestedChanges
        {
            get => _hasNestedChanges;
            private set
            {
                if (value == _hasNestedChanges)
                {
                    return;
                }
                _hasNestedChanges = value;
                HasNestedChangesChanged.Emit(() => new ValueChangedEvent<bool>(!_hasNestedChanges, value));
                UpdateHasAnyChanges();
            }
        }

        public bool HasNestedChangesSinceSnapshot
        {
            get => _hasNestedChangesSinceStart;
            private set
            {
                if (value == _hasNestedChangesSinceStart)
                {
                    return;
                }
                _hasNestedChangesSinceStart = value;
                HasNestedChangesSinceSnapshotChanged.Emit(() => new ValueChangedEvent<bool>(!_hasNestedChangesSinceStart, value));
                UpdateHasAnyChanges();
            }
        }

        public void UpdateHasChanged(bool? ignoreRelationshipOtherSide = null)
        {
            if (_isUpdatingHasChanged)
            {
                return;
            }

            if (IsLocked)
            {
                return;
            }

            _isUpdatingHasChanged = true;
            if (IsRelationshipCollection)
            {
                HasChanges = ItemsRemoved.Count > 0 || ItemsAdded.Count > 0;
                HasChangesSinceSnapshot = ItemsRemovedSinceSnapshot.Count > 0 || ItemsAddedSinceSnapshot.Count > 0;
                HasNestedChanges = ItemsChanged.Count > 0;
                HasNestedChangesSinceSnapshot = ItemsChangedSinceSnapshot.Count > 0;
                //if (!HasSnapshotValue && HasNestedChanges)
                //{
                //    EventManager.Subscribe(DataTracker.SnapshotAdded, _ =>
                //    {
                //        ClearSnapshotRelationshipChanges();
                //    }, "SnapshotCheck");
                //}
                //else
                //{
                //    EventManager.UnsubscribeAll("SnapshotCheck");
                //}
            }
            else
            {
                UpdateHasChangedSinceStart();
                UpdateHasChangedSinceSnapshot();
                UpdateHasAnyChanges();
                if (ignoreRelationshipOtherSide != true)
                {
                    for (var i = 0; i < SiblingStates.Length; i++)
                    {
                        var groupState = SiblingStates[i];
                        groupState.UpdateHasChanged(true);
                    }
                }
            }
            //if (ignoreRelationshipOtherSide != true && OtherEndIsCollection && RelationshipPropertyState != null)
            //{
            //    var oldRelatedList = _relationshipEntityRemote == RelationshipPropertyState.RemoteValue
            //        ? _relationshipPropertyStateRemote
            //        : ResolveRelatedListStateFromSource(RelationshipPropertyState.RemoteValue);
            //    var newRelatedList = _relationshipEntityLocal == RelationshipPropertyState.LocalValue
            //        ? _relationshipPropertyStateLocal
            //        : ResolveRelatedListStateFromSource(RelationshipPropertyState.LocalValue);
            //    var snapshotRelatedList = _relationshipEntitySnapshot == RelationshipPropertyState.SnapshotValue
            //        ? _relationshipPropertyStateSnapshot
            //        : ResolveRelatedListStateFromSource(RelationshipPropertyState.LocalValue);
            //    _relationshipPropertyStateLocal = newRelatedList;
            //    _relationshipEntityLocal = RelationshipPropertyState.LocalValue;
            //    _relationshipPropertyStateRemote = newRelatedList;
            //    _relationshipEntityRemote = RelationshipPropertyState.RemoteValue;
            //    _relationshipPropertyStateSnapshot = snapshotRelatedList;
            //    _relationshipEntitySnapshot = RelationshipPropertyState.SnapshotValue;
            //    if (oldRelatedList != null)
            //    {
            //        oldRelatedList.UpdateHasChanged();
            //    }

            //    if (newRelatedList != oldRelatedList && newRelatedList != null)
            //    {
            //        newRelatedList.UpdateHasChanged();
            //    }

            //    if (snapshotRelatedList != oldRelatedList && snapshotRelatedList != newRelatedList &&
            //        snapshotRelatedList != null)
            //    {
            //        snapshotRelatedList.UpdateHasChanged();
            //    }
            //}

            _isUpdatingHasChanged = false;
        }


        private EventEmitter<IPropertyState> _onReset;
        public EventEmitter<IPropertyState> OnReset => _onReset = _onReset ?? new EventEmitter<IPropertyState>().RegisterWith(_eventEmitterManager);


        private readonly IqlEventEmitterManager _eventEmitterManager = new IqlEventEmitterManager();
        private EventEmitter<ValueChangedEvent<bool>> _canUndoChanged;

        public EventEmitter<ValueChangedEvent<bool>> CanUndoChanged => _canUndoChanged = _canUndoChanged ?? new EventEmitter<ValueChangedEvent<bool>>().RegisterWith(_eventEmitterManager);
        private EventEmitter<ValueChangedEvent<object>> _snapshotValueChanged;

        public EventEmitter<ValueChangedEvent<object>> SnapshotValueChanged => _snapshotValueChanged = _snapshotValueChanged ?? new EventEmitter<ValueChangedEvent<object>>().RegisterWith(_eventEmitterManager);
        private EventEmitter<ValueChangedEvent<bool>> _hasSnapshotValueChanged;

        public EventEmitter<ValueChangedEvent<bool>> HasSnapshotValueChanged => _hasSnapshotValueChanged = _hasSnapshotValueChanged ?? new EventEmitter<ValueChangedEvent<bool>>().RegisterWith(_eventEmitterManager);
        private EventEmitter<ValueChangedEvent<bool>> _hasNestedChangesChanged;

        public EventEmitter<ValueChangedEvent<bool>> HasNestedChangesChanged => _hasNestedChangesChanged = _hasNestedChangesChanged ?? new EventEmitter<ValueChangedEvent<bool>>().RegisterWith(_eventEmitterManager);
        private EventEmitter<ValueChangedEvent<bool>> _hasNestedChangesSinceSnapshotChanged;

        public EventEmitter<ValueChangedEvent<bool>> HasNestedChangesSinceSnapshotChanged => _hasNestedChangesSinceSnapshotChanged = _hasNestedChangesSinceSnapshotChanged ?? new EventEmitter<ValueChangedEvent<bool>>().RegisterWith(_eventEmitterManager);

        private EventEmitter<ValueChangedEvent<bool>> _hasChangesChanged;
        public EventEmitter<ValueChangedEvent<bool>> HasChangesChanged => _hasChangesChanged = _hasChangesChanged ?? new EventEmitter<ValueChangedEvent<bool>>().RegisterWith(_eventEmitterManager);
        
        private EventEmitter<ValueChangedEvent<bool>> _hasAnyChangesChanged;

        public EventEmitter<ValueChangedEvent<bool>> HasAnyChangesChanged => _hasAnyChangesChanged = _hasAnyChangesChanged ?? new EventEmitter<ValueChangedEvent<bool>>().RegisterWith(_eventEmitterManager);
        private EventEmitter<ValueChangedEvent<bool>> _hasAnyChangesSinceSnapshotChanged;

        public EventEmitter<ValueChangedEvent<bool>> HasAnyChangesSinceSnapshotChanged => _hasAnyChangesSinceSnapshotChanged = _hasAnyChangesSinceSnapshotChanged ?? new EventEmitter<ValueChangedEvent<bool>>().RegisterWith(_eventEmitterManager);
        private EventEmitter<ValueChangedEvent<bool>> _hasChangesSinceSnapshotChanged;

        public EventEmitter<ValueChangedEvent<bool>> HasChangesSinceSnapshotChanged => _hasChangesSinceSnapshotChanged = _hasChangesSinceSnapshotChanged ?? new EventEmitter<ValueChangedEvent<bool>>().RegisterWith(_eventEmitterManager);
        private EventEmitter<ValueChangedEvent<object>> _remoteValueChanged;

        public EventEmitter<ValueChangedEvent<object>> RemoteValueChanged => _remoteValueChanged = _remoteValueChanged ?? new EventEmitter<ValueChangedEvent<object>>().RegisterWith(_eventEmitterManager);
        private EventEmitter<ValueChangedEvent<object>> _localValueChanged;

        public EventEmitter<ValueChangedEvent<object>> LocalValueChanged => _localValueChanged = _localValueChanged ?? new EventEmitter<ValueChangedEvent<object>>().RegisterWith(_eventEmitterManager);

        public IPropertyState[] SiblingStates
        {
            get
            {
                if (_siblingStates == null)
                {
                    if (GroupStates == null)
                    {
                        return null;
                    }

                    _siblingStates = GroupStates.Where(_ => _ != this).ToArray();
                }

                return _siblingStates;
            }
        }

        public IPropertyState[] GroupStates
        {
            get
            {
                if (_groupStates == null)
                {
                    if (EntityState == null)
                    {
                        return null;
                    }

                    if (Property.Kind.HasFlag(IqlPropertyKind.Relationship) && Property.Relationship.ThisIsTarget)
                    {
                        _groupStates = new IPropertyState[] { this };
                    }
                    else
                    {
                        if (Property.PropertyGroup == null)
                        {
                            _groupStates = new IPropertyState[] { this };
                        }
                        else
                        {
                            var properties = Property.PropertyGroup.GetGroupProperties();
                            _groupStates = properties.Where(_ => _.GroupKind == IqlPropertyGroupKind.Primitive)
                                .Select(_ => EntityState.GetPropertyState(((IProperty)_).PropertyName))
                                .Where(_ => _ != null)
                                .ToArray();
                        }
                    }
                }

                return _groupStates;
            }
        }

        public IEntityStateBase EntityState { get; }
        public IProperty Property { get; }
        public string Data { get; set; }

        public Guid Guid { get; set; }

        public void HardReset()
        {
            var newValue = Property.GetValue(EntityState.Entity);
            _remoteValueSet = false;
            LocalValueSet = false;
            EnsureRemoteValue();
            LocalValue = newValue;
            UpdateHasChanged();
            OnReset.Emit(() => this);
            DataTracker.NotifyHardReset(this);
        }

        public void SoftReset()
        {
            if (!HasChanges)
            {
                HardReset();
            }
        }

        public IPropertyState Copy()
        {
            return new PropertyState(Property, null)
            {
                RemoteValue = RemoteValue,
                LocalValue = LocalValue,
                Data = Data
            };
        }

        public void AbandonChanges()
        {
            if (HasChanges)
            {
                var ev = new AbandonChangeEvent(EntityState, this);
                AbandonEvents.EmitStartedAsync(() => ev);
                StateEvents.AbandoningPropertyChange.Emit(() => ev);
                // If collection: restore the original entities to the current list
                if (Property.TypeDefinition.Kind == IqlType.Collection)
                {
                    RestoreList((IList)_remoteValueClone, (IList)LocalValue);
                }
                else
                {
                    EntityState.Entity.SetPropertyValue(Property, _remoteValueClone);
                }

                HardReset();
                AbandonEvents.EmitSuccessAsync(() => ev);
                AbandonEvents.EmitCompletedAsync(() => ev);
                StateEvents.AbandonedPropertyChange.Emit(() => ev);
                //PropertyChanger.ApplyTo(_oldObjectClone, _oldObject);
            }

            ClearStatefulEvents();
            //_hasChanged = false;
        }

        public void ClearStatefulEvents()
        {
            Data = null;
            StatefulSaveEvents.UnsubscribeAll();
        }

        public void Restore(SerializedPropertyState state)
        {
            LocalValue = Property.TypeDefinition.EnsureValueType(state.LocalValue);
            RemoteValue = Property.TypeDefinition.EnsureValueType(state.RemoteValue);
            Property.SetValue(EntityState.Entity, LocalValue);
            Data = state.Data;
            if (!string.IsNullOrWhiteSpace(state.Guid))
            {
                Guid = new Guid(state.Guid);
            }
        }

        public void UndoChanges(bool? undoNestedChanges = null)
        {
            if (EntityState != null)
            {
                if (IsRelationshipCollection)
                {
                    var all = ItemsAddedSinceSnapshot
                        .Concat(ItemsRemovedSinceSnapshot)
                        .Concat((undoNestedChanges == true ? ItemsChangedSinceSnapshot.ToArray() : new IEntityStateBase[] { }))
                        .ToArray();

                    var properties = Property.Relationship.OtherEnd.Property.GroupProperties
                        .Concat((undoNestedChanges == true ? Property.Relationship.OtherEnd.EntityConfiguration.Properties.ToArray() : new IProperty[] { }))
                        .ToArray();
                    EntityState.DataTracker.UndoChanges(all, properties);
                }
                else
                {
                    EntityState.DataTracker.UndoChanges(new[] { EntityState.Entity }, new[] { Property });
                }
            }
            else
            {
                LocalValue = RemoteValue;
            }
        }

        public string SerializeToJson()
        {
            return this.ToJson();
        }

        public object PrepareForJson()
        {
            if (Property.Kind.HasFlag(IqlPropertyKind.Count) ||
                Property.Kind.HasFlag(IqlPropertyKind.Relationship))
            {
                return null;
            }

            return new
            {
                RemoteValue,
                LocalValue = Property.GetValue(EntityState.Entity),
                Property = Property.PropertyName,
                Data,
                Guid
            };
        }

        public void Dispose()
        {
            //DataTracker.UnregisterInterest(_.Item, GetInterestKey(addedKey));
            //DataTracker.UnregisterInterest(_.Item, GetInterestKey(addedKey));
            _eventEmitterManager?.Dispose();
        }

        private bool CalculateHasChanged(object remoteValue, ChangeCalculationKind kind)
        {
            if (Property.IsCount)
            {
                //HasChangedText = "Count field";
                return false;
            }

            if (Property.HasRelationship)
            {
                //HasChangedText = "Relationship field";
                if (Property.Relationship.ThisIsSource)
                {
                    return RelationshipSourceHasChanged(remoteValue, kind);
                }

                if (Property.TypeDefinition.Kind == IqlType.Collection)
                {
                    return RelationshipTargetHasChanged(remoteValue, kind);
                }
            }

            //HasChangedText = $"PropertyChanger: {PropertyChanger.GetType().Name}";
            return !PropertyChanger.AreEquivalent(remoteValue,
                EntityState == null || LocalValueSet ? LocalValue : Property.GetValue(EntityState.Entity));
        }

        private bool CalculateHasNestedChanges(object remoteValue, ChangeCalculationKind kind)
        {
            if (Property.Kind.HasFlag(IqlPropertyKind.Relationship) && Property.TypeDefinition.Kind == IqlType.Collection)
            {
                return RelationshipTargetHasNestedChanges(remoteValue, kind);
            }

            return false;
        }

        private void EnsureRemoteValue()
        {
            if (EntityState != null &&
                _remoteValueSet &&
                _remoteValue == null &&
                _localValue != null &&
                Property.Relationship != null &&
                !Property.Relationship.ThisIsTarget &&
                Property == Property.Relationship.ThisEnd.Property)
            {
                var canMatchToKey = true;
                for (var i = 0; i < Property.Relationship.ThisEnd.Constraints.Length; i++)
                {
                    var key = Property.Relationship.ThisEnd.Constraints[i];
                    if (EntityState.GetPropertyState(key.PropertyName).HasChanges)
                    {
                        canMatchToKey = false;
                    }
                }

                if (canMatchToKey)
                {
                    var objectKey = Property.Relationship.OtherEnd.GetCompositeKey(_localValue, true);
                    var relationshipKey = Property.Relationship.ThisEnd.GetCompositeKey(EntityState.Entity);
                    if (objectKey.Matches(relationshipKey))
                    {
                        SetRemoteValue(_localValue);
                        _remoteValueSet = true;
                    }
                }
            }

            if (!_remoteValueSet)
            {
                _remoteValueSet = true;
                if (EntityState != null)
                {
                    SetRemoteValue(Property.GetValue(EntityState.Entity));
                }
            }
        }

        private bool RelationshipTargetHasNestedChanges(object remoteValue, ChangeCalculationKind kind)
        {
            var remoteList = (IList)remoteValue;
            var localList = (IList)LocalValue;
            if (remoteList == null && localList == null)
            {
                return false;
            }

            if (remoteList != null)
            {
                for (var i = 0; i < remoteList.Count; i++)
                {
                    var item = remoteList[i];
                    if (HasItemChanged(item, kind))
                    {
                        return true;
                    }
                }
            }

            if (localList != null)
            {
                for (var i = 0; i < localList.Count; i++)
                {
                    var item = localList[i];
                    if (HasItemChanged(item, kind))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private bool HasItemChanged(object item, ChangeCalculationKind kind)
        {
            var state = DataTracker.GetEntityState(item);
            if (state == null)
            {
                return false;
            }

            return kind == ChangeCalculationKind.Remote ? state.HasChanged : state.HasChangedSinceSnapshot;
        }

        private bool RelationshipTargetHasChanged(object remoteValue, ChangeCalculationKind kind)
        {
            var remoteList = (IList)remoteValue;
            var remoteCount = 0;
            if (remoteList != null)
            {
                remoteCount = remoteList.Count;
            }

            var localCount = 0;
            var localList = (IList)LocalValue;
            if (localList != null)
            {
                localCount = localList.Count;
            }

            if (remoteCount == 0 && localCount == 0)
            {
                return false;
            }

            if (remoteCount > 0 && localCount < remoteCount)
            {
                return true;
            }

            if (remoteCount == 0 && localList != null)
            {
                for (var i = 0; i < localList.Count; i++)
                {
                    var item = localList[i];
                    if (HasRelationshipItemChanged(item, kind))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private bool HasRelationshipItemChanged(object item, ChangeCalculationKind kind)
        {
            var state = DataTracker.GetEntityState(item);
            if (state == null)
            {
                return false;
            }

            if (_otherSideProperties == null)
            {
                _otherSideProperties = Property.Relationship.OtherEnd.Constraints.ToList();
                _otherSideProperties.Add(Property.Relationship.OtherEnd.Property);
            }

            for (var i = 0; i < _otherSideProperties.Count; i++)
            {
                var prop = _otherSideProperties[i];
                var propState = state.GetPropertyState(prop.PropertyName);
                if (propState != null)
                {
                    if (kind == ChangeCalculationKind.Remote && propState.HasChanges)
                    {
                        return true;
                    }

                    if (kind == ChangeCalculationKind.Snapshot && propState.HasChangesSinceSnapshot)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private bool RelationshipSourceHasChanged(object remoteValue, ChangeCalculationKind kind)
        {
            if (remoteValue != null && LocalValue != null && remoteValue != LocalValue)
            {
                return true;
            }

            if (remoteValue == null && LocalValue != null)
            {
                return true;
            }

            if (remoteValue != null && LocalValue == null)
            {
                return true;
            }

            if (remoteValue == LocalValue)
            {
                return false;
            }

            if (EntityState != null)
            {
                if (HasRelationshipSourceChanged(EntityState, Property.Relationship.ThisEnd, kind))
                {
                    return true;
                }
            }

            return false;
        }

        private bool HasRelationshipSourceChanged(IEntityStateBase entityState, IRelationshipDetail relationshipDetail,
            ChangeCalculationKind kind)
        {
            var constraints = relationshipDetail.Constraints.ToList();
            constraints.Add(relationshipDetail.Property);
            for (var i = 0; i < constraints.Count; i++)
            {
                var constraint = constraints[i];
                var propertyState = entityState.GetPropertyState(constraint.Name);
                if (propertyState != null)
                {
                    var hasChangedValue = kind == ChangeCalculationKind.Remote
                        ? propertyState.HasChanges
                        : propertyState.HasChangesSinceSnapshot;
                    if (hasChangedValue)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private bool HasRelationshipSourceOtherPropertyChanged(IEntityStateBase entityState, IRelationshipDetail relationshipDetail,
            ChangeCalculationKind kind)
        {
            var groupProperties = relationshipDetail.Property.GroupProperties;

            foreach (var property in entityState.PropertyStates)
            {
                if (!groupProperties.Contains(property.Property) && (kind == ChangeCalculationKind.Remote ? property.HasChanges : property.HasChangesSinceSnapshot))
                {
                    return true;
                }
            }
            return false;
        }

        private bool SanityCheckRelationshipSource(IEntityStateBase entityState, IRelationshipDetail relationshipDetail)
        {
            var entityValue = entityState.GetPropertyState(relationshipDetail.Property.PropertyName).LocalValue;
            if (entityValue == null)
            {
                return true;
            }

            if (DataTracker != null)
            {
                var state = DataTracker.GetEntityState(entityValue);
                if (state.IsNew)
                {
                    var constraints = relationshipDetail.Constraints;
                    foreach (var constraint in constraints)
                    {
                        if (!entityState.GetPropertyState(constraint.PropertyName).IsDefaultValue(constraint.TypeDefinition))
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    return DoesRelationshipSourceMatch(entityState, relationshipDetail, state.Entity);
                }
            }

            return false;
        }

        private bool DoesRelationshipSourceMatch(
            IEntityStateBase entityState,
            IRelationshipDetail relationshipDetail,
            object usEntity = null,
            CheckValueKind checkRemoteValue = CheckValueKind.Local)
        {
            usEntity = usEntity ?? EntityState.Entity;
            var usEntityState = DataTracker.GetEntityState(usEntity);
            if (entityState.GetPropertyState(relationshipDetail.Property.PropertyName).LocalValue == usEntity)
            {
                return true;
            }
            var constraints = relationshipDetail.Constraints;
            for (var i = 0; i < constraints.Length; i++)
            {
                var constraint = constraints[i];
                var propertyState = entityState.GetPropertyState(constraint.Name);
                if (propertyState != null)
                {
                    object oldValue;
                    switch (checkRemoteValue)
                    {
                        case CheckValueKind.Local:
                            oldValue = propertyState.LocalValue;
                            break;
                        case CheckValueKind.Remote:
                            oldValue = propertyState.RemoteValue;
                            break;
                        case CheckValueKind.Snapshot:
                            oldValue = propertyState.HasSnapshotValue ? propertyState.SnapshotValue : propertyState.RemoteValue;
                            break;
                        default:
                            oldValue = propertyState.LocalValue;
                            break;
                    }
                    if (!propertyState.PropertyChanger.AreEquivalent(
                        usEntityState.GetPropertyState(relationshipDetail.OtherSide.Constraints[i].PropertyName).LocalValue,
                        oldValue))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private void ObserveIfNecessary(object oldValue, object newValue, ChangeCalculationKind kind)
        {
            if (Property.Relationship != null && Property.TypeDefinition.Kind == IqlType.Collection)
            {
                ObserveList((IList)oldValue, (IList)newValue, kind);
            }
        }

        private void ObserveList(IList oldValue, IList newValue, ChangeCalculationKind kind)
        {
            //var key = GetInterestKey($"Collection-{(kind == ChangeCalculationKind.Remote ? "Remote" : "Snapshot")}");
            //if (oldValue != null)
            //{
            //    for (var i = 0; i < oldValue.Count; i++)
            //    {
            //        var value = oldValue[i];
            //        DataTracker.UnregisterInterest(value, key);
            //    }
            //}
            //if (newValue != null)
            //{
            //    for (var i = 0; i < newValue.Count; i++)
            //    {
            //        var value = newValue[i];
            //        DataTracker.RegisterInterest(value, key, () =>
            //        {
            //            UpdateHasChanged();
            //        });
            //    }
            //}
        }

        private void AddSnapshotInternal()
        {
            SetSnapshotValue(LocalValue);
        }

        private void SetRemoteValue(object value)
        {
            var oldValue = _remoteValue;
            _remoteValueClone = PropertyChanger.CloneValue(value);
            if (IsRelationshipCollection)
            {
                ItemsChanged.Clear();
                ItemsRemoved.Clear();
                ItemsAdded.Clear();
            }
            ObserveIfNecessary(oldValue, _remoteValueClone, ChangeCalculationKind.Remote);
            _remoteValue = value;
            UpdateHasChanged();
            var areEquivalent = PropertyChanger.AreEquivalent(_remoteValue, oldValue);
            if (!areEquivalent)
            {
                DataTracker.NotifyRemoteValueChanged(this);
                if(_remoteValueChanged != null)
                {
                    RemoteValueChanged.Emit(() => new ValueChangedEvent<object>(oldValue, value));
                }
            }
        }

        private IPropertyState ResolveRelatedListStateFromSource(object relationshipPropertyValue)
        {
            //var state = DataTracker.FindTrackedEntity(compositeKey);
            if (relationshipPropertyValue == null)
            {
                return null;
            }

            var state = DataTracker.GetEntityState(relationshipPropertyValue);
            if (state != null)
            {
                return state.GetPropertyState(Property.Relationship.OtherEnd.Property.PropertyName);
            }

            return null;
        }

        //private void UpdateHasNestedChangesSinceStart()
        //{
        //    var oldValue = HasNestedChanges;
        //    HasNestedChanges = CalculateHasNestedChanges(LocalValue, ChangeCalculationKind.Remote);
        //    if (oldValue != HasNestedChanges)
        //    {
        //        HasNestedChangesChanged.Emit(() => new ValueChangedEvent<bool>(oldValue, HasNestedChanges));
        //        if (EntityState != null)
        //        {
        //            EntityState.CheckHasChanged();
        //        }
        //    }
        //}

        //private void UpdateHasNestedChangesSinceSnapshot()
        //{
        //    var oldValue = HasNestedChangesSinceSnapshot;
        //    HasNestedChangesSinceSnapshot = CalculateHasNestedChanges(LocalValue, ChangeCalculationKind.Snapshot);
        //    if (oldValue != HasNestedChangesSinceSnapshot)
        //    {
        //        HasNestedChangesSinceSnapshotChanged.Emit(() =>
        //            new ValueChangedEvent<bool>(oldValue, HasNestedChangesSinceSnapshot));
        //        if (EntityState != null)
        //        {
        //            EntityState.CheckHasChanged();
        //        }
        //    }
        //}

        private void UpdateHasChangedSinceStart()
        {
            HasChanges = CalculateHasChanged(RemoteValue, ChangeCalculationKind.Remote);
        }

        private void UpdateHasChangedSinceSnapshot()
        {
            HasChangesSinceSnapshot = CalculateHasChanged(SnapshotValue, ChangeCalculationKind.Snapshot);
        }

        private void RestoreList(IList source, IList destination)
        {
            if (source == null && destination != null)
            {
                destination.Clear();
            }

            if (source != null && destination == null)
            {
                throw new NotSupportedException("Cannot merge non-empty list with null list.");
            }

            var toRemove = new List<object>();
            for (var i = 0; i < source.Count; i++)
            {
                var item = source[i];
                if (!destination.Contains(item))
                {
                    toRemove.Add(item);
                }
            }

            var toAdd = new List<object>();
            for (var i = 0; i < destination.Count; i++)
            {
                var item = destination[i];
                if (!source.Contains(item))
                {
                    toAdd.Add(item);
                }
            }

            for (var i = 0; i < toRemove.Count; i++)
            {
                var item = toRemove[i];
                destination.Remove(item);
            }

            for (var i = 0; i < toAdd.Count; i++)
            {
                var item = toAdd[i];
                destination.Add(item);
            }
        }

        private string GetInterestKey(string key)
        {
            return $"{Id}-{key}";
        }
    }

    internal enum CheckValueKind
    {
        Remote,
        Local,
        Snapshot
    }

    internal enum CheckMode
    {
        Match,
        NoMatch,
        NoCheck
    }
}