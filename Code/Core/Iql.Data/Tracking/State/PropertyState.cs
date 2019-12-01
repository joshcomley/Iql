using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
    [DebuggerDisplay("{Property.Name}")]
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

        private IqlEventManager EventManager { get; } = new IqlEventManager();

        public IOperationEvents<IEntityPropertyEvent, IEntityPropertyEvent> StatefulSaveEvents { get; } =
            new OperationEvents<IEntityPropertyEvent, IEntityPropertyEvent>();

        public IOperationEvents<IEntityPropertyEvent, IEntityPropertyEvent> SaveEvents { get; } =
            new OperationEvents<IEntityPropertyEvent, IEntityPropertyEvent>();

        public IOperationEvents<AbandonChangeEvent, AbandonChangeEvent> AbandonEvents { get; } =
            new OperationEvents<AbandonChangeEvent, AbandonChangeEvent>();

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
                    _isRelationshipCollection = Property.Kind.HasFlag(PropertyKind.Relationship) &&
                                                Property.Relationship.ThisIsTarget &&
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
                    _otherEndIsCollection = EntityState != null && Property.Kind.HasFlag(PropertyKind.Relationship) &&
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

        public bool HasChangedSinceSnapshot
        {
            get => _hasChangedSinceSnapshot;
            private set
            {
                var old = _hasChangedSinceSnapshot;
                _hasChangedSinceSnapshot = value;
                if (old != value)
                {
                    if (EntityState != null)
                    {
                        EntityState.DataTracker.NotifyChangedSinceSnapshotChanged(this, HasChanged, value);
                    }

                    HasChangedSinceSnapshotChanged.Emit(
                        () => new ValueChangedEvent<bool>(old, value));
                    if (EntityState != null)
                    {
                        EntityState.CheckHasChanged();
                    }
                }
                CanUndo = HasSnapshotValue && HasChangedSinceSnapshot || HasChanged;
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

        public bool HasChanged
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
                    HasChangedChanged.Emit(() => new ValueChangedEvent<bool>(old, value));
                    if (EntityState != null)
                    {
                        EntityState.CheckHasChanged();
                    }

                    if (!value)
                    {
                        HasChangedSinceSnapshot = false;
                    }
                }
            }
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
                if (HasChangedSinceSnapshot)
                {
                    AddSnapshotInternal();
                }
            }
            else if (HasChanged)
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
            //UpdateSnapshotRelatedListChanged();
            UpdateHasChanged();
        }

        public void ClearSnapshotValue()
        {
            ClearSnapshotRelationshipChanges();
            HasSnapshotValue = false;
            _snapshotValue = null;
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

        public object LocalValue
        {
            get => LocalValueSet ? _localValue : RemoteValue;
            set
            {
                LocalValueSet = true;
                var oldValue = _localValue;
                _localValue = value;
                UpdateHasChanged();
                if (DataTracker != null)
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
                        EventManager.UnsubscribeAll("LocalValue");
                    }

                    if (value != null)
                    {
                        var addedKey = $"{nameof(LocalValue)}_{nameof(RelatedListChangeKind.Added)}";
                        var relatedList = (IRelatedList)value;
                        var remoteList = (IEnumerable<object>)RemoteValue;
                        EventManager.Subscribe(relatedList.RelatedListChange, _ =>
                        {
                            var hasChanged = false;
                            if (EntityState != null)
                            {
                                switch (_.Kind)
                                {
                                    case RelatedListChangeKind.Added:
                                        {
                                            var itemState = DataTracker.GetEntityState(_.Item);
                                            if (itemState != null)
                                            {
                                                var watch = true;
                                                if (!IsLocked)
                                                {
                                                    if (itemState.IsNew && !ItemsAdded.Contains(itemState))
                                                    {
                                                        ItemsAdded.Add(itemState);
                                                    }
                                                    else if (ItemsRemoved.Contains(itemState))
                                                    {
                                                        ItemsRemoved.Remove(itemState);
                                                    }
                                                }
                                                if (watch)
                                                {
                                                    DataTracker.RegisterInterest(_.Item, GetInterestKey(addedKey),
                                                      entityState =>
                                                      {
                                                          if (!IsLocked)
                                                          {
                                                              //if (!entityState.HasChanged)
                                                              //{
                                                              //    ItemsAdded.Remove(entityState);
                                                              //    ItemsRemoved.Remove(entityState);
                                                              //    ItemsChanged.Remove(entityState);
                                                              //}
                                                              if (!entityState.HasChangedSinceSnapshot)
                                                              {
                                                                  ItemsAddedSinceSnapshot.Remove(entityState);
                                                                  ItemsRemovedSinceSnapshot.Remove(entityState);
                                                                  ItemsChangedSinceSnapshot.Remove(entityState);
                                                              }
                                                              if (relatedList.Contains(entityState.Entity) &&
                                                                  (entityState.IsNew || HasRelationshipSourceChanged(entityState, Property.Relationship.OtherEnd, ChangeCalculationKind.Remote, CheckMode.Match)))
                                                              {
                                                                  if (relatedList.Contains(entityState.Entity))
                                                                  {
                                                                      if (!ItemsRemoved.Contains(entityState) && !ItemsAdded.Contains(entityState))
                                                                      {
                                                                          ItemsAdded.Add(entityState);
                                                                      }
                                                                  }
                                                                  //else
                                                                  //{
                                                                  //    if (ItemsAdded.Contains(entityState))
                                                                  //    {
                                                                  //        ItemsAdded.Remove(entityState);
                                                                  //    }
                                                                  //}
                                                              }
                                                              else
                                                              {
                                                                  if (ItemsAdded.Contains(entityState))
                                                                  {
                                                                      ItemsAdded.Remove(entityState);
                                                                      //DataTracker.UnregisterInterest(_.Item, GetInterestKey(addedKey));
                                                                  }
                                                              }
                                                          }
                                                          UpdateSnapshotRelatedListChanged();
                                                      });
                                                }
                                                UpdateSnapshotRelatedListChanged();
                                            }

                                            break;
                                        }
                                    case RelatedListChangeKind.Removed:
                                        {
                                            var itemState = DataTracker.GetEntityState(_.Item);
                                            if (itemState != null)
                                            {
                                                if (!IsLocked)
                                                {
                                                    if (ItemsAdded.Contains(itemState))
                                                    {
                                                        ItemsAdded.Remove(itemState);
                                                        //DataTracker.UnregisterInterest(_.Item, GetInterestKey(addedKey));
                                                    }
                                                    else if (!itemState.IsNew)
                                                    {
                                                        if (!ItemsRemoved.Contains(itemState))
                                                        {
                                                            ItemsRemoved.Add(itemState);
                                                        }
                                                    }
                                                }
                                                var removedKey =
                                                    $"{nameof(LocalValue)}_{nameof(RelatedListChangeKind.Removed)}";
                                                DataTracker.RegisterInterest(_.Item, GetInterestKey(
                                                        removedKey),
                                                    entityState =>
                                                    {
                                                        if (!IsLocked)
                                                        {
                                                            //var wasAdded = ItemsAdded.Contains(entityState);
                                                            //if (!entityState.HasChanged)
                                                            //{
                                                            //    ItemsAdded.Remove(entityState);
                                                            //    ItemsRemoved.Remove(entityState);
                                                            //    ItemsChanged.Remove(entityState);
                                                            //}
                                                            if (!entityState.HasChangedSinceSnapshot)
                                                            {
                                                                ItemsAddedSinceSnapshot.Remove(entityState);
                                                                ItemsRemovedSinceSnapshot.Remove(entityState);
                                                                ItemsChangedSinceSnapshot.Remove(entityState);
                                                            }
                                                            if (HasRelationshipSourceChanged(entityState, Property.Relationship.OtherEnd, ChangeCalculationKind.Remote, CheckMode.NoMatch))
                                                            {
                                                                if (!relatedList.Contains(entityState.Entity))
                                                                {
                                                                    if (remoteList.Contains(entityState) && !ItemsAdded.Contains(entityState) && !ItemsRemoved.Contains(entityState))
                                                                    {
                                                                        ItemsRemoved.Add(entityState);
                                                                    }
                                                                }
                                                                //else
                                                                //{
                                                                //    if (ItemsRemoved.Contains(entityState))
                                                                //    {
                                                                //        ItemsRemoved.Remove(entityState);
                                                                //    }
                                                                //}
                                                            }
                                                            else
                                                            {
                                                                if (ItemsRemoved.Contains(entityState))
                                                                {
                                                                    ItemsRemoved.Remove(entityState);
                                                                }
                                                                //DataTracker.UnregisterInterest(_.Item, GetInterestKey(removedKey));
                                                            }
                                                        }
                                                        UpdateSnapshotRelatedListChanged();
                                                    });
                                                UpdateSnapshotRelatedListChanged();
                                            }
                                            break;
                                        }
                                }
                            }

                            //if (hasChanged)
                            {
                                UpdateHasChanged();
                            }
                        }, "LocalValue");
                    }
                }
            }
        }

        private void UpdateSnapshotRelatedListChanged()
        {
            if (IsLocked || !IsRelationshipCollection)
            {
                return;
            }
            var relatedList = ((IRelatedList)LocalValue);
            if (HasSnapshotValue)
            {
                var snapshotList = ((IEnumerable<object>)SnapshotValue).ToArray();
                var addedSinceSnapshot = new List<object>();
                var removedSinceSnapshot = new List<object>();
                var changedSinceSnapshot = new List<object>();
                var changed = new List<object>();
                foreach (var item in relatedList)
                {
                    var state = DataTracker.GetEntityState(item);
                    if (state != null && !snapshotList.Contains(item) && (state.IsNew || state.HasChangedSinceSnapshot))
                    {
                        addedSinceSnapshot.Add(item);
                    }

                    if (state != null && !state.IsNew && !ItemsAdded.Contains(item) && state.HasChanged &&
                        HasRelationshipSourceChanged(state, Property.Relationship.OtherEnd, ChangeCalculationKind.Remote, CheckMode.Match, false))
                    {
                        changed.Add(state.Entity);
                    }
                }
                foreach (var item in snapshotList)
                {
                    if (!relatedList.Contains(item))
                    {
                        removedSinceSnapshot.Add(item);
                    }
                    else if (DataTracker.GetEntityState(item)?.HasChangedSinceSnapshot == true)
                    {
                        changedSinceSnapshot.Add(item);
                    }
                }

                ChangeList(ItemsChanged, changed);
                ChangeList(ItemsAddedSinceSnapshot, addedSinceSnapshot);
                ChangeList(ItemsChangedSinceSnapshot, changedSinceSnapshot);
                ChangeList(ItemsRemovedSinceSnapshot, removedSinceSnapshot);
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
                            HasRelationshipSourceChanged(state, Property.Relationship.OtherEnd, ChangeCalculationKind.Remote, CheckMode.Match, false))
                        {
                            if (state.HasChanged)
                            {
                                changed.Add(state);
                            }
                            if (state.HasChangedSinceSnapshot)
                            {
                                changedSinceSnapshot.Add(state);
                            }
                        }
                    }
                }

                ChangeEntityStateList(ItemsChanged, changed);
                ChangeEntityStateList(ItemsAddedSinceSnapshot, ItemsAdded);
                ChangeEntityStateList(ItemsChangedSinceSnapshot, changedSinceSnapshot);
                ChangeEntityStateList(ItemsRemovedSinceSnapshot, ItemsRemoved);
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
                var old = _hasNestedChanges;
                _hasNestedChanges = value;
                if (old != value)
                {
                    HasNestedChangesChanged.Emit(() => new ValueChangedEvent<bool>(old, value));
                }
            }
        }

        public bool HasNestedChangesSinceSnapshot
        {
            get => _hasNestedChangesSinceStart;
            private set
            {
                var old = _hasNestedChangesSinceStart;
                _hasNestedChangesSinceStart = value;
                if (old != value)
                {
                    HasNestedChangesSinceSnapshotChanged.Emit(() => new ValueChangedEvent<bool>(old, value));
                }
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
                HasChanged = ItemsRemoved.Any() || ItemsAdded.Any();
                HasChangedSinceSnapshot = ItemsRemovedSinceSnapshot.Any() || ItemsAddedSinceSnapshot.Any();
                HasNestedChanges = ItemsChanged.Any();
                HasNestedChangesSinceSnapshot = ItemsChangedSinceSnapshot.Any();
                if (!HasSnapshotValue && HasNestedChanges)
                {
                    EventManager.Subscribe(DataTracker.SnapshotAdded, _ =>
                    {
                        ClearSnapshotRelationshipChanges();
                    }, "SnapshotCheck");
                }
                else
                {
                    EventManager.UnsubscribeAll("SnapshotCheck");
                }
            }
            else
            {
                UpdateHasChangedSinceStart();
                UpdateHasChangedSinceSnapshot();
                foreach (var groupState in GroupStates)
                {
                    groupState.UpdateHasChanged();
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

        public EventEmitter<ValueChangedEvent<bool>> CanUndoChanged { get; } =
            new EventEmitter<ValueChangedEvent<bool>>();

        public EventEmitter<ValueChangedEvent<bool>> HasSnapshotValueChanged { get; } =
            new EventEmitter<ValueChangedEvent<bool>>();

        public EventEmitter<ValueChangedEvent<bool>> HasNestedChangesChanged { get; } =
            new EventEmitter<ValueChangedEvent<bool>>();

        public EventEmitter<ValueChangedEvent<bool>> HasNestedChangesSinceSnapshotChanged { get; } =
            new EventEmitter<ValueChangedEvent<bool>>();

        public EventEmitter<ValueChangedEvent<bool>> HasChangedChanged { get; } =
            new EventEmitter<ValueChangedEvent<bool>>();

        public EventEmitter<ValueChangedEvent<bool>> HasChangedSinceSnapshotChanged { get; } =
            new EventEmitter<ValueChangedEvent<bool>>();

        public EventEmitter<ValueChangedEvent<object>> RemoteValueChanged { get; } =
            new EventEmitter<ValueChangedEvent<object>>();

        public EventEmitter<ValueChangedEvent<object>> LocalValueChanged { get; } =
            new EventEmitter<ValueChangedEvent<object>>();

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

                    if (Property.Kind.HasFlag(PropertyKind.Relationship) && Property.Relationship.ThisIsTarget)
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
        }

        public void SoftReset()
        {
            if (!HasChanged)
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
            if (HasChanged)
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

        public void UndoChange()
        {
            if (EntityState != null)
            {
                EntityState.DataTracker.UndoChanges(new[] { EntityState.Entity }, new[] { Property });
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
            if (Property.Kind.HasFlag(PropertyKind.Count) ||
                Property.Kind.HasFlag(PropertyKind.Relationship))
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
            HasChangedChanged?.Dispose();
            RemoteValueChanged?.Dispose();
            LocalValueChanged?.Dispose();
        }

        private bool CalculateHasChanged(object remoteValue, ChangeCalculationKind kind)
        {
            if (Property.Kind.HasFlag(PropertyKind.Count))
            {
                HasChangedText = "Count field";
                return false;
            }

            if (Property.Kind.HasFlag(PropertyKind.Relationship))
            {
                HasChangedText = "Relationship field";
                if (Property.Relationship.ThisIsSource)
                {
                    return RelationshipSourceHasChanged(remoteValue, kind);
                }

                if (Property.TypeDefinition.Kind == IqlType.Collection)
                {
                    return RelationshipTargetHasChanged(remoteValue, kind);
                }
            }

            HasChangedText = $"PropertyChanger: {PropertyChanger.GetType().Name}";
            return !PropertyChanger.AreEquivalent(remoteValue,
                EntityState == null || LocalValueSet ? LocalValue : Property.GetValue(EntityState.Entity));
        }

        private bool CalculateHasNestedChanges(object remoteValue, ChangeCalculationKind kind)
        {
            if (Property.Kind.HasFlag(PropertyKind.Relationship) && Property.TypeDefinition.Kind == IqlType.Collection)
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
                    if (EntityState.GetPropertyState(key.PropertyName).HasChanged)
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
                    if (kind == ChangeCalculationKind.Remote && propState.HasChanged)
                    {
                        return true;
                    }

                    if (kind == ChangeCalculationKind.Snapshot && propState.HasChangedSinceSnapshot)
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

            if (EntityState != null)
            {
                if (HasRelationshipSourceChanged(EntityState, Property.Relationship.ThisEnd, kind, CheckMode.NoCheck))
                {
                    return true;
                }
            }

            return false;
        }

        private bool HasRelationshipSourceChanged(IEntityStateBase entityState, IRelationshipDetail relationshipDetail,
            ChangeCalculationKind kind, CheckMode checkMode, bool hasChanged = true)
        {
            var constraints = relationshipDetail.Constraints;
            for (var i = 0; i < constraints.Length; i++)
            {
                var constraint = constraints[i];
                var propertyState = entityState.GetPropertyState(constraint.Name);
                var allow = false;
                if (checkMode == CheckMode.NoCheck)
                {
                    allow = true;
                }
                else
                {
                    allow = propertyState.PropertyChanger.AreEquivalent(
                        relationshipDetail.OtherSide.Constraints[i].GetValue(EntityState.Entity),
                        propertyState.LocalValue);
                    if (checkMode == CheckMode.NoMatch)
                    {
                        allow = !allow;
                    }
                }

                if (!allow)
                {
                    return false;
                }
                if (propertyState != null)
                {
                    var hasChangedValue = kind == ChangeCalculationKind.Remote
                        ? propertyState.HasChanged
                        : propertyState.HasChangedSinceSnapshot;
                    if (hasChanged)
                    {
                        return hasChangedValue;
                    }
                    else if (hasChangedValue)
                    {
                        return false;
                    }
                }
            }

            return !hasChanged;
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
            if (_remoteValue != oldValue)
            {
                DataTracker.NotifyRemoteValueChanged(this);
                RemoteValueChanged.Emit(() => new ValueChangedEvent<object>(oldValue, value));
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
            HasChanged = CalculateHasChanged(RemoteValue, ChangeCalculationKind.Remote);
        }

        private void UpdateHasChangedSinceSnapshot()
        {
            HasChangedSinceSnapshot = CalculateHasChanged(SnapshotValue, ChangeCalculationKind.Snapshot);
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

    internal enum CheckMode
    {
        Match,
        NoMatch,
        NoCheck
    }
}