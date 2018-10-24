using Iql.Entities.DisplayFormatting;
using Iql.Entities.Extensions;
using Iql.Entities.Geography;
using Iql.Entities.Lists;
using Iql.Entities.NestedSets;
using Iql.Entities.PropertyGroups.Dates;
using Iql.Entities.PropertyGroups.Files;
using Iql.Entities.Relationships;
using Iql.Entities.Rules;
using Iql.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using Iql.Conversion;

namespace Iql.Entities
{
    public abstract class EntityConfigurationBase : MetadataBase, IEntityMetadata
    {
        protected virtual IEntityConfigurationContainer ConfigurationContainer { get; }
        protected readonly Dictionary<string, IProperty> _propertiesMap = new Dictionary<string, IProperty>();
        private IProperty _titleProperty;
        private IProperty _previewProperty;
        private string _titlePropertyName;
        private string _previewPropertyName;
        public IList<IProperty> Properties { get; set; }

        public IList<IRelationship> Relationships => _relationships;

        protected EntityConfigurationBase(IEntityConfigurationContainer configurationContainer)
        {
            ConfigurationContainer = configurationContainer;
            _relationships.Change.Subscribe(change =>
            {
                _allRelationships = null;
            });
        }
        public IPropertyGroup[] AllPropertyGroups()
        {
            var list = new List<IPropertyGroup>();
            var relationships = AllRelationships();
            if (relationships != null)
            {
                foreach (var relationship in relationships)
                {
                    list.Add(relationship.ThisEnd);
                }
            }

            var others = new IEnumerable<IPropertyGroup>[] { NestedSets, Geographics, DateRanges, Files };
            for (var i = 0; i < others.Length; i++)
            {
                var group = others[i];
                if (group != null)
                {
                    list.AddRange(group);
                }
            }
            return list.Distinct().ToArray();
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
                    var constraints = property.Relationship.ThisEnd.Constraints;
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
            var all = new List<IPropertyGroup>();
            all.AddRange(AllPropertyGroups());
            all.AddRange(Properties.Where(p =>
                p.PropertyGroup == null));
            return all.PrioritizeForReading().ToArray();
        }

        public virtual IPropertyGroup[] GetDisplayConfiguration(DisplayConfigurationKind kind, bool appendMissingProperties = true)
        {
            var orderedDisplaySetting = kind == DisplayConfigurationKind.Edit ? EditDisplay : ReadDisplay;
            if (orderedDisplaySetting == null || !orderedDisplaySetting.Any())
            {
                return GetGroupProperties();
            }
            var ordered = orderedDisplaySetting.ToList();
            if (appendMissingProperties)
            {
                var flattened = orderedDisplaySetting.FlattenAllToSimpleProperties().ToList();
                var groupProperties = GetGroupProperties();
                for (var i = 0; i < groupProperties.Length; i++)
                {
                    var property = groupProperties[i];
                    if (!flattened.Contains(property) && !ordered.Contains(property))
                    {
                        ordered.Add(property);
                    }
                }
            }
            return ordered.ToArray();
        }

        public IList<IGeographic> Geographics { get; set; } = new List<IGeographic>();
        public IList<INestedSet> NestedSets { get; set; } = new List<INestedSet>();
        public IList<IDateRange> DateRanges { get; set; } = new List<IDateRange>();
        public IList<IFile> Files { get; set; } = new List<IFile>();
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
            get
            {
                if (_titlePropertyName == null && Properties != null)
                {
                    _titlePropertyName = Properties.Where(p => p.TypeDefinition.Kind == IqlType.String && p.Kind.HasFlag(PropertyKind.Primitive))
                        .OrderBy(p => p.HasHint(KnownHints.EmailAddress) || p.Kind.HasFlag(PropertyKind.RelationshipKey) || p.Kind.HasFlag(PropertyKind.Key))
                        .FirstOrDefault()?.PropertyName;
                }
                return _titlePropertyName;
            }
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
                    _setName = _setFriendlyNameSet ? _setFriendlyName : ResolveName();
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
                        if (Equals(ends[i].EntityConfiguration, this))
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

        public string GetCompositeKeyString(object entity)
        {
            return GetCompositeKey(entity).AsKeyString();
        }
        
        public IProperty FindNestedPropertyByIqlExpression(IqlPropertyExpression propertyExpression)
        {
            return IqlPropertyPath.FromPropertyExpression(this as IEntityConfiguration, propertyExpression).Property;
        }

        public IProperty FindNestedProperty(string name)
        {
            // TODO: Use IqlPropertyPath
            var chars = new[] {'/', '.'};
            for (var j = 0; j < chars.Length; j++)
            {
                var ch = chars[j];
                if (name.IndexOf(ch) != -1)
                {
                    var parts = name.Split(ch);
                    IEntityConfiguration config = this as IEntityConfiguration;
                    IProperty property = null;
                    for (var i = 0; i < parts.Length; i++)
                    {
                        var part = parts[i];
                        property = config.FindNestedProperty(part);
                        if (i == parts.Length - 1)
                        {
                            break;
                        }
                        config = property.Relationship.OtherEnd.EntityConfiguration;
                    }
                    return property;
                }
            }

            var result = _propertiesMap.ContainsKey(name) ? _propertiesMap[name] : null;
            if (result == null)
            {
                result = Properties.SingleOrDefault(_ => _.Name == name);
            }

            return result;
        }

        public IProperty FindNestedPropertyByLambdaExpression(LambdaExpression property)
        {
            if (property == null)
            {
                return null;
            }
            var iql = IqlConverter.Instance.ConvertLambdaExpressionToIqlByType(property, Type).Expression;
            return FindNestedPropertyByIqlExpression((iql as IqlLambdaExpression).Body as IqlPropertyExpression);
        }
    }
}