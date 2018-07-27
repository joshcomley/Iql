using Iql.Entities.DisplayFormatting;
using Iql.Entities.Extensions;
using Iql.Entities.Geography;
using Iql.Entities.NestedSets;
using Iql.Entities.Relationships;
using Iql.Entities.Rules;
using Iql.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using Iql.Entities.Lists;

namespace Iql.Entities
{
    public abstract class EntityConfigurationBase : MetadataBase, IEntityMetadata
    {
        private IProperty _titleProperty;
        private IProperty _previewProperty;
        private string _titlePropertyName;
        private string _previewPropertyName;
        public IList<IProperty> Properties { get; set; }

        public IList<IRelationship> Relationships
        {
            get => _relationships;
        }

        public object GetVersion(object entity)
        {
            var versionProperty = Properties.FirstOrDefault(p => p.HasHint(KnownHints.Version));
            if (versionProperty != null)
            {
                return versionProperty.GetValue(entity);
            }

            return null;
        }


        /// <summary>
        /// Determines whether this entity type has any fields that aren't key fields
        /// </summary>
        /// <returns></returns>
        public bool HasNonKeyFields()
        {
            for (var i = 0; i < Properties.Count; i++)
            {
                var property = Properties[i];
                if (property.Kind.HasFlag(PropertyKind.Primitive) &&
                    !property.Kind.HasFlag(PropertyKind.Key) &&
                    !property.Kind.HasFlag(PropertyKind.Count))
                {
                    return true;
                }

                if (property.Kind.HasFlag(PropertyKind.Relationship))
                {
                    var constraints = property.Relationship.ThisEnd.Constraints();
                    if (constraints.Any(c => !c.Kind.HasFlag(PropertyKind.Key)))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool HasRelationshipKeys => Key != null && Key.HasRelationshipKeys;

        public IPropertyGroup[] GetGroupProperties()
        {
            if (PropertyOrder == null || !PropertyOrder.Any())
            {
                var all = new List<IPropertyGroup>();
                all.AddRange(Properties.Where(p => p.Geographic == null && p.NestedSet == null));
                if (Geographics != null)
                {
                    all.AddRange(Geographics);
                }
                if (NestedSets != null)
                {
                    all.AddRange(NestedSets);
                }
                return all.ToArray();
            }

            var flattened = PropertyOrder.FlattenAllToSimpleProperties().ToList();
            var ordered = PropertyOrder.ToList();
            for (var i = 0; i < Properties.Count; i++)
            {
                var property = Properties[i];
                if (!flattened.Contains(property))
                {
                    ordered.Add(property);
                }
            }
            return ordered.ToArray();
        }

        public IList<IGeographic> Geographics { get; set; } = new List<IGeographic>();
        public IList<INestedSet> NestedSets { get; set; } = new List<INestedSet>();
        public IDisplayFormatting DisplayFormatting { get; set; }
        public IRuleCollection<IBinaryRule> EntityValidation { get; set; }
        public IEntityKey Key { get; set; }

        public Type Type { get; set; }

        private bool _titlePropertyNameChanged = false;
        private bool _previewPropertyNameChanged = false;
        private string _setFriendlyName;

        public IProperty TitleProperty
        {
            get
            {
                if (_titlePropertyNameChanged)
                {
                    _titlePropertyNameChanged = false;
                    _titleProperty = Properties.SingleOrDefault(p => p.Name == TitlePropertyName);
                }

                return _titleProperty;
            }
        }
        public IProperty PreviewProperty
        {
            get
            {
                if (_previewPropertyNameChanged)
                {
                    _previewPropertyNameChanged = false;
                    _previewProperty = Properties.SingleOrDefault(p => p.Name == PreviewPropertyName);
                }

                return _previewProperty;
            }
        }

        public string TitlePropertyName
        {
            get => _titlePropertyName;
            set
            {
                _titlePropertyName = value;
                _titlePropertyNameChanged = true;
            }
        }

        public string PreviewPropertyName
        {
            get => _previewPropertyName;
            set
            {
                _previewPropertyName = value;
                _previewPropertyNameChanged = true;
            }
        }

        public EntityManageKind ManageKind { get; set; } = EntityManageKind.Full;

        private bool _setFriendlyNameSet = false;
        private string _setName;
        private readonly ObservableList<IRelationship> _relationships = new ObservableList<IRelationship>();

        public EntityConfigurationBase()
        {
            _relationships.Change.Subscribe(change => { _allRelationships = null; });
        }

        public string SetFriendlyName
        {
            get
            {
                if (!_setFriendlyNameSet && _setFriendlyName == null)
                {
                    _setFriendlyName = IntelliSpace.Parse(SetName);
                }
                return _setFriendlyName;
            }
            set
            {
                if (!SetNameSet)
                {
                    _setName = null;
                }
                _setFriendlyNameSet = true;
                _setFriendlyName = value;
            }
        }

        public bool SetNameSet { get; private set; }

        public string SetName
        {
            get
            {
                if (!SetNameSet && _setName == null)
                {
                    if (_setFriendlyNameSet)
                    {
                        _setName = _setFriendlyName;
                    }
                    else
                    {
                        _setName = ResolveName();
                    }
                }

                return _setName;
            }
            set
            {
                if (!_setFriendlyNameSet)
                {
                    _setFriendlyName = null;
                }
                SetNameSet = true;
                _setName = value;
            }
        }

        protected override string ResolveName()
        {
            return Type?.Name ?? _setName ?? _setFriendlyName;
        }

        public string DefaultSortExpression { get; set; }
        public bool DefaultSortDescending { get; set; }

        public static string DefaultRequiredAutoValidationFailureMessage { get; set; } = "This field is required";
        public static string DefaultRequiredAutoValidationFailureKey { get; set; } = "Auto";

        private List<EntityRelationship> _allRelationships = new List<EntityRelationship>();

        public List<EntityRelationship> AllRelationships()
        {
            if (_allRelationships == null)
            {
                var list = new List<EntityRelationship>();
                foreach (var relationship in Relationships)
                {
                    var ends = new[] { relationship.Source, relationship.Target };
                    for (var i = 0; i < ends.Length; i++)
                    {
                        if (Equals(ends[i].Configuration, this))
                        {
                            var relationshipMatch = new EntityRelationship(relationship, i == 1);
                            list.Add(relationshipMatch);
                        }
                    }
                }

                _allRelationships = list;
            }
            return _allRelationships;
        }

        public EntityRelationship FindRelationshipByName(string propertyName)
        {
            return AllRelationships().SingleOrDefault(r => r.ThisEnd.Property.Name == propertyName);
        }

        public bool EntityHasKey(object entity, CompositeKey key)
        {
            var isMatch = true;
            foreach (var id in Key.Properties)
            {
                var compositeKeyValue = key.Keys.SingleOrDefault(k => k.Name == id.Name);
                if (compositeKeyValue == null)
                {
                    return false;
                }
                if (!Equals(entity.GetPropertyValue(id), compositeKeyValue.Value))
                {
                    isMatch = false;
                    break;
                }
            }
            return isMatch;
        }

        public bool KeysMatch(object left, object right)
        {
            if (new[] { left, right }.Count(i => i == null) == 1)
            {
                return false;
            }
            if (left.GetType() != right.GetType())
            {
                return false;
            }
            if (left == right)
            {
                return true;
            }
            var isMatch = true;
            foreach (var id in Key.Properties)
            {
                if (!Equals(left.GetPropertyValue(id), right.GetPropertyValue(id)))
                {
                    isMatch = false;
                    break;
                }
            }
            return isMatch;
        }

        public CompositeKey GetCompositeKey(object entity)
        {
            var key = new CompositeKey(Key.Properties.Length);
            for (var i = 0; i < Key.Properties.Length; i++)
            {
                var property = Key.Properties[i];
                key.Keys[i] = new KeyValue(property.Name, entity.GetPropertyValue(property), property.TypeDefinition);
            }
            return key;
        }

        public IProperty FindNestedPropertyByIqlExpression(IqlPropertyExpression propertyExpression)
        {
            return IqlPropertyPath.FromPropertyExpression(this as IEntityConfiguration, propertyExpression).Property;
        }
    }
}