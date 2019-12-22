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
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using Iql.Conversion;
using Iql.Data.Types;
using Iql.Entities.Functions;
using Iql.Parsing.Types;

namespace Iql.Entities
{
    public abstract class EntityConfigurationBase : MetadataBase, IEntityMetadata
    {
        private List<string> _aliases;
        public List<string> Aliases
        {
            get
            {
                _aliases = _aliases ?? new List<string>();
                return _aliases;
            }
            set => _aliases = value;
        }

        protected EntityConfigurationBase AddAliasInternal(string name)
        {
            var match = this.ConfigurationContainer.GetEntityByTypeName(name);
            if (match != null)
            {
                // Redundant cast is for TypeScript
                // ReSharper disable once RedundantCast
                if (match != (object)this)
                {
                    throw new Exception($@"""{name}"" is already mapped to entity of type {match.Name}");
                }
            }
            Aliases.Add(name);
            return this;
        }

        public bool HasNameOrAlias(string name)
        {
            if(Name == name)
            {
                return true;
            }

            if(Aliases != null && Aliases.Any(_ => _ == name))
            {
                return true;
            }

            if(Type.Name == name)
            {
                return true;
            }

            return false;
        }

        public override IEntityConfiguration EntityConfiguration => (IEntityConfiguration)this;
        private bool _thumbnailPropertyResolved = false;
	    private IFile _resolvedThumbnailProperty = null;
	    public IFile ResolvePreviewProperty()
	    {
		    if (!_thumbnailPropertyResolved)
		    {
			    CalculatePreviewProperty();
		    }
		    return _resolvedThumbnailProperty;
	    }

	    public IFile CalculatePreviewProperty()
	    {
		    _thumbnailPropertyResolved = true;
		    _resolvedThumbnailProperty = PreviewProperty ?? Properties.OrderBy(_ => _.GroupOrder).Where(_ => _.File != null)
			    .Select(_ => _.File)
			    .FirstOrDefault();
		    return _resolvedThumbnailProperty;
	    }

	    public bool IsPivot
        {
            get
            {
                return Key.Properties.Length == 2 &&
                       Key.Properties.All(_ => _.Relationship != null && _.Relationship.ThisIsSource);
            }
        }


        private IIqlTypeMetadata _typeMetadata = null;
        public IIqlTypeMetadata TypeMetadata => _typeMetadata = _typeMetadata ?? new EntityConfigurationTypeProvider((IEntityConfiguration)this);
        private bool _methodsInitialized;
        private List<IqlMethod> _methods;

        public List<IqlMethod> Methods { get { if(!_methodsInitialized) { _methodsInitialized = true; _methods = new List<IqlMethod>(); } return _methods; } set { _methodsInitialized = true; _methods = value; } }

        public IEntityConfiguration AddMethod(IqlMethod method)
        {
            if (!Methods.Contains(method))
            {
                Methods.Add(method);
            }

            return (IEntityConfiguration)this;
        }
        public IEntityConfiguration ConfigureMethod(IqlMethod method)
        {
            if (!Methods.Contains(method))
            {
                Methods.Add(method);
            }

            return (IEntityConfiguration)this;
        }

        public IqlMethod FindMethod(string name, bool? ensure = null, Action<IqlMethod> configure = null)
        {
            Methods = Methods ?? new List<IqlMethod>();
            var result = Methods.FirstOrDefault(_ => _.Name == name);
            if (result == null && ensure != null && ensure == true)
            {
                result = new IqlMethod(IqlMethodScopeKind.EntitySet, name, null, null, null, (IEntityConfiguration)this);
                Methods.Add(result);
            }

            if (result != null && configure != null)
            {
                configure(result);
            }
            return result;
        }
        protected virtual IEntityConfigurationContainer ConfigurationContainer { get; }
        private bool _propertiesMapDelayedInitialized;
        private Dictionary<string, IProperty> _propertiesMapDelayed;
        protected Dictionary<string, IProperty> _propertiesMap { get { if(!_propertiesMapDelayedInitialized) { _propertiesMapDelayedInitialized = true; _propertiesMapDelayed = new Dictionary<string, IProperty>(); } return _propertiesMapDelayed; } set { _propertiesMapDelayedInitialized = true; _propertiesMapDelayed = value; } }
        private IFile _previewProperty;
        private string _titlePropertyName;
        private string _previewPropertyName;
        public IList<IProperty> Properties { get; set; }
        public IProperty PersistenceKeyProperty { get; set; }

        public IList<IRelationship> Relationships => _relationships;

        public IProperty[] TryMatchProperty(params string[] names)
        {
            List<IProperty> properties = null;
            // TODO: Convert name to lower case and remove all non-alpha characters (including numbers)
            for (var i = 0; i < names.Length; i++)
            {
                var name = names[i];
                var property = Properties.FirstOrDefault(p => p.Matches(name));
                if (property != null && property.TypeDefinition.ToIqlType() == IqlType.String)
                {
                    properties = properties ?? new List<IProperty>();
                    properties.Add(property);
                }
            }
            return properties?.ToArray();
        }

        protected EntityConfigurationBase(IEntityConfigurationContainer configurationContainer)
        {
            ConfigurationContainer = configurationContainer;
            _relationships.Change.Subscribe(change => { _allRelationships = null; });
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
                if (property.Kind.HasFlag(IqlPropertyKind.Primitive) &&
                    !property.Kind.HasFlag(IqlPropertyKind.Key) &&
                    !property.Kind.HasFlag(IqlPropertyKind.Count))
                {
                    return true;
                }

                if (property.Kind.HasFlag(IqlPropertyKind.Relationship))
                {
                    var constraints = property.Relationship.ThisEnd.Constraints;
                    if (constraints.Any(c => !c.Kind.HasFlag(IqlPropertyKind.Key)))
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
                p.PropertyGroup == null || (p.Kind.HasFlag(IqlPropertyKind.Key) && !p.Kind.HasFlag(IqlPropertyKind.RelationshipKey))));
            var ordered = all.PrioritizeForReading().ToArray();
            return ordered;
        }
        private bool _displayConfigurationsInitialized;
        private IList<DisplayConfiguration> _displayConfigurations;

        public IList<DisplayConfiguration> DisplayConfigurations { get { if(!_displayConfigurationsInitialized) { _displayConfigurationsInitialized = true; _displayConfigurations = new List<DisplayConfiguration>(); } return _displayConfigurations; } set { _displayConfigurationsInitialized = true; _displayConfigurations = value; } }

        public IEnumerable<DisplayConfiguration> DisplayConfigurationsFor(DisplayConfigurationKind kind)
        {
            return DisplayConfigurations == null
                ? new DisplayConfiguration[] { }
                : DisplayConfigurations.Where(_ => _.Kind == kind);
        }

        private DisplayConfiguration _fullReadDisplayConfiguration = null;
        private DisplayConfiguration _fullEditDisplayConfiguration = null;

        public DisplayConfiguration GetFullDisplayConfiguration(
            DisplayConfigurationKind? kind = null)
        {
            var resolvedKind = kind ?? DisplayConfigurationKind.Read;
            if (resolvedKind == DisplayConfigurationKind.Read)
            {
                _fullReadDisplayConfiguration =
                    _fullReadDisplayConfiguration ?? BuildFullDisplayConfiguration(resolvedKind);
                return _fullReadDisplayConfiguration;
            }

            _fullEditDisplayConfiguration =
                _fullEditDisplayConfiguration ?? BuildFullDisplayConfiguration(resolvedKind);
            return _fullEditDisplayConfiguration;
        }

        private DisplayConfiguration BuildFullDisplayConfiguration(DisplayConfigurationKind kind)
        {
            var final = new List<IPropertyGroup>();
            var properties = BuildDisplayConfiguration(null);
            for (int i = 0; i < properties.Length; i++)
            {
                var property = properties[i];
                if (property.Kind.HasFlag(IqlPropertyKind.RelationshipKey) ||
                    (kind == DisplayConfigurationKind.Edit && property.Kind.HasFlag(IqlPropertyKind.Count)))
                {
                    continue;
                }

                var resolved = property.PropertyGroup ?? property;
                if (!final.Contains(resolved))
                {
                    final.Add(resolved);
                }
            }

            return new DisplayConfiguration(kind, final, null, true);
        }

        public DisplayConfiguration GetDisplayConfiguration(DisplayConfigurationKind kind, params string[] keys)
        {
            var displayConfigurations = DisplayConfigurationsFor(kind).ToList();
            if ((keys == null || keys.Length == 0) &&
                displayConfigurations.Count > 0)
            {
                if (displayConfigurations.Count == 1)
                {
                    return displayConfigurations[0];
                }

                if (displayConfigurations.Any())
                {
                    return GetDisplayConfiguration(kind, DisplayConfigurationKeys.Default) ?? displayConfigurations[0];
                }
            }

            if (keys != null)
            {
                for (var i = 0; i < keys.Length; i++)
                {
                    var key = keys[i];
                    var config = displayConfigurations.SingleOrDefault(_ => _.Key == key);
                    if (config != null)
                    {
                        return config;
                    }
                }
            }
            return GetFullDisplayConfiguration(kind);
        }

        public DisplayConfiguration FindDisplayConfiguration(DisplayConfigurationKind? kind = null)
        {
            var resolvedKind = kind ?? DisplayConfigurationKind.Read;
            DisplayConfiguration config = null;
            if (DisplayConfigurations != null)
            {
                config = DisplayConfigurations.FirstOrDefault(_ => _.Kind == resolvedKind);
            }
            config = config ?? GetFullDisplayConfiguration(resolvedKind);
            return config;
        }

        [DebuggerDisplay("{Property.Name} - {Order}")]
        class OrderedProperty
        {
            public IPropertyGroup Property { get; }
            public int Order { get; }

            public OrderedProperty(IPropertyGroup property, int order)
            {
                Property = property;
                Order = order;
            }
        }

        public virtual IPropertyGroup[] BuildDisplayConfiguration(
            DisplayConfiguration configuration,
            bool? doNotAppendMissingProperties = null,
            bool? includeReadHiddenProperties = null)
        {
            if (configuration == null || !configuration.Properties.Any())
            {
                return GetGroupProperties().Where(_ => includeReadHiddenProperties == true || !_.IsHiddenFromRead).ToArray();
            }

            var properties = configuration.Properties.ToList();
            if (doNotAppendMissingProperties != true)
            {
                var flattened = configuration.Properties.FlattenAllToSimpleProperties().ToList();
                var groupProperties = GetGroupProperties().Where(_ => includeReadHiddenProperties == true || !_.IsHiddenFromRead).ToArray().FlattenAllToSimpleProperties().ToList();
                for (var i = 0; i < groupProperties.Count; i++)
                {
                    var property = groupProperties[i];
                    if (!flattened.Contains(property) && !properties.Contains(property))
                    {
                        properties.Add(property);
                    }
                }
            }

            return properties.ToArray();
        }
        private bool _geographicsInitialized;
        private IList<IGeographicPoint> _geographics;

        public IList<IGeographicPoint> Geographics { get { if(!_geographicsInitialized) { _geographicsInitialized = true; _geographics = new List<IGeographicPoint>(); } return _geographics; } set { _geographicsInitialized = true; _geographics = value; } }
        private bool _nestedSetsInitialized;
        private IList<INestedSet> _nestedSets;
        public IList<INestedSet> NestedSets { get { if(!_nestedSetsInitialized) { _nestedSetsInitialized = true; _nestedSets = new List<INestedSet>(); } return _nestedSets; } set { _nestedSetsInitialized = true; _nestedSets = value; } }
        private bool _dateRangesInitialized;
        private IList<IDateRange> _dateRanges;
        public IList<IDateRange> DateRanges { get { if(!_dateRangesInitialized) { _dateRangesInitialized = true; _dateRanges = new List<IDateRange>(); } return _dateRanges; } set { _dateRangesInitialized = true; _dateRanges = value; } }
        private bool _filesInitialized;
        private IList<IFile> _files;
        public IList<IFile> Files { get { if(!_filesInitialized) { _filesInitialized = true; _files = new List<IFile>(); } return _files; } set { _filesInitialized = true; _files = value; } }
        public IDisplayFormatting DisplayFormatting { get; set; }
        public IRuleCollection<IBinaryRule> EntityValidation { get; set; }
        public IEntityKey Key { get; set; }

        public Type Type { get; set; }

        private bool _previewPropertyNameChanged = false;
        private string _setFriendlyName;

        public IProperty TitleProperty
        {
            get
            {
                var name = TitlePropertyName;
                if (!string.IsNullOrWhiteSpace(name))
                {
                    return Properties.SingleOrDefault(p => ((IMetadata) p).Name == TitlePropertyName);
                }
                return null;
            }
        }

        public IFile PreviewProperty
        {
            get
            {
                if (_previewPropertyNameChanged)
                {
                    _previewPropertyNameChanged = false;
                    var prop = Properties.FirstOrDefault(p => p.File != null && p.File.Name == PreviewPropertyName);
					_previewProperty = prop?.File;
                }

                return _previewProperty;
            }
        }

        public bool AutoTitleProperty { get; set; } = true;
        public bool AutoTitlePropertyResolved { get; set; } = false;
        private string _autoTitlePropertyName = null;
        public virtual string TitlePropertyName
        {
            get
            {
                if (AutoTitleProperty)
                {
                    if (AutoTitlePropertyResolved)
                    {
                        return _autoTitlePropertyName;
                    }

                    if (Properties != null)
                    {
                        AutoTitlePropertyResolved = true;
                        double GetOrder(IProperty p, double d)
                        {
                            if (
                                p.Kind.HasFlag(IqlPropertyKind.RelationshipKey) ||
                                p.Kind.HasFlag(IqlPropertyKind.Key))
                            {
                                return d + 1000;
                            }

                            if (p.SearchKind == IqlPropertySearchKind.Primary)
                            {
                                if (p.Matches("name", "title"))
                                {
                                    return d + 10;
                                }

                                return d + 20;
                            }

                            if (p.SearchKind == IqlPropertySearchKind.Secondary)
                            {
                                if (p.Matches("description"))
                                {
                                    return d + 30;
                                }

                                return d + 40;
                            }

                            return 100;
                        }

                        var increment = 0.0001;
                        var index = increment;
                        //var titlePropertyCandidates = Properties.Where(p =>
                        //        p.TypeDefinition.Kind == IqlType.String && p.Kind.HasFlag(IqlPropertyKind.Primitive))
                        //    .Select(p =>
                        //    {
                        //        index += increment;
                        //        return new
                        //        {
                        //            Order = GetOrder(p, index),
                        //            Property = p,
                        //            Name = p.Name
                        //        };
                        //    }).ToArray();
                        _autoTitlePropertyName = Properties.Where(p =>
                                 p.TypeDefinition.Kind == IqlType.String && p.Kind.HasFlag(IqlPropertyKind.Primitive))
                            .OrderBy(p =>
                            {
                                index += increment;
                                return GetOrder(p, index);
                            })
                            .FirstOrDefault()?.Name;
                        return _autoTitlePropertyName;
                    }

                    return null;
                }

                return _titlePropertyName;
            }
            set
            {
                _titlePropertyName = value;
                AutoTitleProperty = false;
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
        private bool _relationshipsDelayedInitialized;
        private ObservableList<IRelationship> _relationshipsDelayed;
        private ObservableList<IRelationship> _relationships { get { if(!_relationshipsDelayedInitialized) { _relationshipsDelayedInitialized = true; _relationshipsDelayed = new ObservableList<IRelationship>(); } return _relationshipsDelayed; } set { _relationshipsDelayedInitialized = true; _relationshipsDelayed = value; } }

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

        public string DefaultBrowseSortExpression { get; set; }
        public bool DefaultBrowseSortDescending { get; set; }
        public string DefaultSearchSortExpression { get; set; }
        public bool DefaultSearchSortDescending { get; set; }
        private bool _allRelationshipsDelayedInitialized;
        private List<EntityRelationship> _allRelationshipsDelayed;


        private List<EntityRelationship> _allRelationships { get { if(!_allRelationshipsDelayedInitialized) { _allRelationshipsDelayedInitialized = true; _allRelationshipsDelayed = new List<EntityRelationship>(); } return _allRelationshipsDelayed; } set { _allRelationshipsDelayedInitialized = true; _allRelationshipsDelayed = value; } }

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
                            var relationshipMatch = EntityRelationship.Get(relationship, i == 1);
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
            return AllRelationships().SingleOrDefault(r => ((IMetadata) r.ThisEnd.Property).Name == propertyName);
        }

        public bool EntityHasKey(object entity, CompositeKey key)
        {
            var isMatch = true;
            foreach (var id in Key.Properties)
            {
                var compositeKeyValue = key.Keys.SingleOrDefault(k => k.Name == ((IMetadata) id).Name);
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
            return CompositeKey.Ensure(entity, (IEntityConfiguration)this);
            //var key = new CompositeKey(Key.Properties.Length);
            //for (var i = 0; i < Key.Properties.Length; i++)
            //{
            //    var property = Key.Properties[i];
            //    key.Keys[i] = new KeyValue(property.Name, entity.GetPropertyValue(property), property.TypeDefinition);
            //}

            //return key;
        }

        public string GetCompositeKeyString(object entity)
        {
            return GetCompositeKey(entity).AsLegacyKeyString();
        }

        public IProperty FindNestedPropertyByIqlExpression(IqlPropertyExpression propertyExpression)
        {
            var iqlPropertyPath = IqlPropertyPath.FromPropertyExpression(
                (this as IEntityConfiguration).Builder,
                (this as IEntityConfiguration).TypeMetadata,
                propertyExpression);
            if (iqlPropertyPath.Property == null)
            {
                throw new Exception($"Unable to find property: {iqlPropertyPath.PathToHere}");
            }
            return iqlPropertyPath.Property.EntityProperty();
        }

        public IProperty FindNestedProperty(string name)
        {
            // TODO: Use IqlPropertyPath
            var chars = new[] { '/', '.' };
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
                result = Properties.SingleOrDefault(_ => ((IMetadata) _).Name == name);
            }

            return result;
        }

        public IProperty FindNestedPropertyByLambdaExpression(LambdaExpression property)
        {
            if (property == null)
            {
                return null;
            }

            var iql = IqlConverter.Instance.ConvertLambdaExpressionToIqlByType(property, (this as IEntityConfiguration).Builder, Type).Expression;
            return FindNestedPropertyByIqlExpression((iql as IqlLambdaExpression).Body as IqlPropertyExpression);
        }
    }
}