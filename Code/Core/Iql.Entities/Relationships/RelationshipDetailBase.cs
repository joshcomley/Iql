using Iql.Entities.Extensions;
using Iql.Entities.Lists;
using System;
using System.Collections.Generic;
using System.Linq;
using Iql.Events;

namespace Iql.Entities.Relationships
{
    public abstract class RelationshipDetailBase : SimplePropertyGroupBase<IRelationshipDetail>, IRelationshipDetail
    {
        public override IProperty PrimaryProperty
        {
            get { return Property; }
        }

        private IProperty[] _constraints;

        private bool _readKindSet;
        private IqlPropertyReadKind _readKindTemp = IqlPropertyReadKind.Display;
        public override IqlPropertyReadKind ReadKind
        {
            get => Property?.ReadKind ?? _readKindTemp;
            set
            {
                _readKindSet = true;
                if (Property != null)
                {
                    Property.ReadKind = value;
                }
                _readKindTemp = value;
            }
        }
        private bool _editKindSet;
        private bool _editKindNeedsPersisting;
        private IqlPropertyEditKind _editKindTemp = IqlPropertyEditKind.Display;
        public override IqlPropertyEditKind EditKind
        {
            get
            {
                if (_editKindNeedsPersisting && Property != null)
                {
                    _editKindNeedsPersisting = false;
                    Property.EditKind = _editKindTemp;
                }
                return Property?.EditKind ?? _editKindTemp;
            }
            set
            {
                _editKindSet = true;
                _editKindNeedsPersisting = true;
                if (Property != null)
                {
                    Property.EditKind = value;
                }
                _editKindTemp = value;
            }
        }

        protected RelationshipDetailBase(
            IRelationship relationship,
            RelationshipSide relationshipSide) : base(null, null)
        {
            Relationship = relationship;
            RelationshipSide = relationshipSide;
            //if (relationship != null)
            //{
            //    switch (relationship.Kind)
            //    {
            //        case RelationshipKind.ManyToMany:
            //            IsCollection = true;
            //            break;
            //        case RelationshipKind.OneToMany:
            //            IsCollection = relationshipSide == RelationshipSide.Target;
            //            break;
            //    }
            //}
        }
        private bool _valueMappingsInitialized;
        private List<ValueMapping> _valueMappings;

        public List<ValueMapping> ValueMappings { get { if (!_valueMappingsInitialized) { _valueMappingsInitialized = true; _valueMappings = new List<ValueMapping>(); } return _valueMappings; } set { _valueMappingsInitialized = true; _valueMappings = value; } }
        private bool _relationshipMappingsInitialized;
        private List<RelationshipMapping> _relationshipMappings;
        public List<RelationshipMapping> RelationshipMappings { get { if (!_relationshipMappingsInitialized) { _relationshipMappingsInitialized = true; _relationshipMappings = new List<RelationshipMapping>(); } return _relationshipMappings; } set { _relationshipMappingsInitialized = true; _relationshipMappings = value; } }
        public IRelationshipDetail OtherSide =>
            RelationshipSide == RelationshipSide.Source ? Relationship?.Target : Relationship?.Source;
        public RelationshipSide RelationshipSide { get; }
        public IRelationship Relationship { get; }
        public Type Type => EntityConfiguration?.Type;
        public bool IsCollection => Property != null && Property.TypeDefinition.IsCollection;

        public IProperty Property
        {
            get => _property;
            set
            {
                if (_readKindSet && _property != null)
                {
                    _property.ReadKind = ReadKind;
                }
                if (_editKindSet && _property != null)
                {
                    _property.EditKind = EditKind;
                }
                _property = value;
            }
        }

        public IProperty CountProperty
        {
            get
            {
                var property = Property as PropertyBase;
                if (property != null)
                {
                    if (RelationshipSide == RelationshipSide.Target)
                    {
                        return property.CountRelationshipProperty;
                    }
                    return OtherSide?.CountProperty;
                }

                return null;
            }
        }
        private bool _beingMarkedAsDirtyDelayedInitialized;
        private List<object> _beingMarkedAsDirtyDelayed;

        private List<object> _beingMarkedAsDirty { get { if (!_beingMarkedAsDirtyDelayedInitialized) { _beingMarkedAsDirtyDelayedInitialized = true; _beingMarkedAsDirtyDelayed = new List<object>(); } return _beingMarkedAsDirtyDelayed; } set { _beingMarkedAsDirtyDelayedInitialized = true; _beingMarkedAsDirtyDelayed = value; } }
        private EventSubscription _constraintsSubscription;
        private IProperty _property;
        private IProperty[] _allProperties = null;

        public void MarkDirty(object entity)
        {
            if (entity != null && !_beingMarkedAsDirty.Contains(entity))
            {
                _beingMarkedAsDirty.Add(entity);
                //CompositeKeys.Remove(entity);
                //InverseCompositeKeys.Remove(entity);
                if (!OtherSide.IsCollection)
                {
                    var referencedEntity = Property.GetValue(entity);
                    if (referencedEntity != null)
                    {
                        OtherSide.MarkDirty(referencedEntity);
                    }
                }
                _beingMarkedAsDirty.Remove(entity);
            }
        }

        public IProperty[] AllProperties
        {
            get
            {
                if(_allProperties == null)
                {
                    var all = new List<IProperty>();
                    all.AddRange(Constraints);
                    all.Add(Property);
                    _allProperties = all.ToArray();
                }
                return _allProperties;
            }
        }

        public IProperty[] Constraints
        {
            get
            {
                if (_constraints != null)
                {
                    return _constraints;
                }

                if (Relationship == null)
                {
                    return new IProperty[] { };
                }
                if (_constraintsSubscription == null && Relationship.Constraints is ObservableList<IRelationshipConstraint>)
                {
                    _constraintsSubscription = (Relationship.Constraints as ObservableList<IRelationshipConstraint>).Change.Subscribe(e =>
                    {
                        _constraints = null;
                    });
                }
                switch (RelationshipSide)
                {
                    case RelationshipSide.Target:
                        return _constraints = Relationship.Constraints.Select(rc => rc.TargetKeyProperty).ToArray();
                    case RelationshipSide.Source:
                        return _constraints = Relationship.Constraints.Select(rc => rc.SourceKeyProperty).ToArray();
                }
                throw new NotSupportedException();
            }
        }

        //internal Dictionary<object, CompositeKey> CompositeKeys { get; }
        //    = new Dictionary<object, CompositeKey>();
        //internal Dictionary<object, CompositeKey> InverseCompositeKeys { get; }
        //    = new Dictionary<object, CompositeKey>();

        public CompositeKey GetCompositeKey(object entity, bool inverse = false)
        {
            //var dic = inverse
            //    ? InverseCompositeKeys
            //    : CompositeKeys;
            //if (dic.ContainsKey(entity))
            //{
            //    return dic[entity];
            //}
            return GetCompositeKeyInternal(entity, inverse);
        }

        private CompositeKey GetCompositeKeyInternal(object entity, bool inverse)
        {
            var entityIsCompositeKey = entity is CompositeKey;
            if (entityIsCompositeKey && !inverse)
            {
                return (CompositeKey)entity;
            }
            var constraints = Constraints;
            var inverseConstraints = RelationshipSide == RelationshipSide.Source
                ? Relationship.Target.Constraints : Relationship.Source.Constraints;
            inverseConstraints = inverse ? inverseConstraints : constraints;
            var compositeKey = new CompositeKey(EntityConfiguration.TypeName, constraints.Length);
            compositeKey.Entity = entity;
            if (entityIsCompositeKey)
            {
                for (var i = 0; i < constraints.Length; i++)
                {
                    var constraint = constraints[i];
                    var value = ((CompositeKey)entity).GetValue(constraint.Name);
                    var keyValue = new KeyValue(inverseConstraints[i].Name,
                        value,
                        constraint.TypeDefinition);
                    compositeKey.Keys[i] = keyValue;
                }
            }
            else
            {
                for (var i = 0; i < constraints.Length; i++)
                {
                    var constraint = constraints[i];
                    var value = entity.GetPropertyValue(constraint);
                    var keyValue = new KeyValue(inverseConstraints[i].Name,
                        value,
                        constraint.TypeDefinition);
                    compositeKey.Keys[i] = keyValue;
                }
            }

            return compositeKey;
        }

        public override IPropertyGroup[] GetGroupProperties()
        {
            var list = new List<IPropertyGroup>();
            list.AddRange(Constraints);
            list.Add(Property);
            return list.ToArray();
        }

        public override IqlPropertyKind Kind { get; set; } = IqlPropertyKind.SimpleCollection;
        IRelationshipDetail IConfigurable<IRelationshipDetail>.Configure(Action<IRelationshipDetail> configure)
        {
            if (configure != null)
            {
                configure(this);
            }
            return this;
        }
    }
}