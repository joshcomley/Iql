using Iql.Entities.Extensions;
using Iql.Entities.Geography;
using Iql.Entities.NestedSets;
using Iql.Entities.Relationships;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Iql.Entities.InferredValues;
using Iql.Entities.PropertyGroups.Dates;
using Iql.Entities.PropertyGroups.Files;

namespace Iql.Entities
{
    public abstract class PropertyBase : SimplePropertyGroupBase<IProperty>, IPropertyMetadata, IPropertyMetadataProvider
    {
        public bool IgnoreChangesInSnapshots { get; set; }
        private IProperty[] _groupStates = null;
        public IProperty[] GroupProperties
        {
            get
            {
                if (_groupStates == null)
                {
                    if (Kind.HasFlag(IqlPropertyKind.Relationship) && Relationship.ThisIsTarget)
                    {
                        _groupStates = new IProperty[] { (IProperty) this };
                    }
                    else
                    {
                        if (PropertyGroup == null)
                        {
                            _groupStates = new IProperty[] { (IProperty)this };
                        }
                        else
                        {
                            var properties = PropertyGroup.GetGroupProperties();
                            _groupStates = properties.Where(_ => _.GroupKind == IqlPropertyGroupKind.Primitive)
                                .Select(_ => (IProperty) _)
                                .ToArray();
                        }
                    }
                }

                return _groupStates;
            }
        }

        public bool IsCount
        {
            get
            {
                if (_isCount == null)
                {
                    _isCount = Kind.HasFlag(IqlPropertyKind.Count);
                }
                return _isCount.Value;
            }
        }

        public bool HasRelationship
        {
            get
            {
                if (_hasRelationship == null)
                {
                    _hasRelationship = Kind.HasFlag(IqlPropertyKind.Relationship);
                }
                return _hasRelationship.Value;
            }
        }

        public override IProperty PrimaryProperty
        {
            get
            {
                var propertyGroup = PropertyGroup;
                if (propertyGroup == null)
                {
                    return (IProperty) this;
                }

                return propertyGroup.PrimaryProperty;
            }
        }

        public override IPropertyGroup PropertyGroup
        {
            get
            {
                if (Relationship != null)
                {
                    return Relationship.ThisEnd;
                }

                if (GeographicPoint != null)
                {
                    return GeographicPoint;
                }

                if (NestedSet != null)
                {
                    return NestedSet;
                }

                if (DateRange != null)
                {
                    return DateRange;
                }

                if (File != null)
                {
                    return File;
                }

                return null;
            }
        }
        public override IPropertyGroup[] GetGroupProperties()
        {
            return new[] { this as IProperty };
        }
        public IGeographicPoint GeographicPoint => EntityConfiguration != null && EntityConfiguration.Geographics != null
            ? EntityConfiguration.Geographics.FirstOrDefault(g =>
                Equals(g.LatitudeProperty, this) || Equals(g.LongitudeProperty, this))
            : null;
        public IFile File => EntityConfiguration?.Files.FirstOrDefault(dr => dr.GetPropertyKind(this as IProperty) != IqlFilePropertyKind.None);
        public IDateRange DateRange => EntityConfiguration?.DateRanges.FirstOrDefault(dr => dr.GetPropertyKind(this as IProperty) != DateRangePropertyKind.None);
        public INestedSet NestedSet => EntityConfiguration?.NestedSets.FirstOrDefault(ns => ns.GetPropertyKind(this as IProperty) != NestedSetPropertyKind.None);
        public bool IsLongitudeProperty => Equals(GeographicPoint?.LongitudeProperty, this);
        public bool IsLatitudeProperty => Equals(GeographicPoint?.LatitudeProperty, this);
        public bool IsLongitudeOrLatitudeProperty => IsLongitudeProperty || IsLatitudeProperty;
        public bool IsTitleProperty => EntityConfiguration?.TitlePropertyName == ((MetadataBase) this).Name || EntityConfiguration?.TitleProperty == this;
        public bool IsPreviewProperty => EntityConfiguration?.PreviewPropertyName != null && EntityConfiguration?.PreviewPropertyName == File?.Name;
        public bool IsSubTitleProperty => HasHint(KnownHints.SubTitle);
        public override IEntityConfiguration EntityConfiguration => EntityConfigurationInternal;
        public IEntityConfiguration EntityConfigurationInternal { get; set; }
        private bool _relationshipSourcesInitialized;
        private List<EntityRelationship> _relationshipSources;
        public List<EntityRelationship> RelationshipSources { get { if(!_relationshipSourcesInitialized) { _relationshipSourcesInitialized = true; _relationshipSources = new List<EntityRelationship>(); } return _relationshipSources; } set { _relationshipSourcesInitialized = true; _relationshipSources = value; } }
        public bool Searchable { get; set; } = true;

        public virtual bool? Nullable
        {
            get => TypeDefinition?.Nullable;
            set
            {
                if (value.HasValue && TypeDefinition != null)
                {
                    TypeDefinition = TypeDefinition.ChangeNullable(value.Value);
                }
            }
        }

        public IList<IInferredValueConfiguration> InferredValueConfigurations
        {
            get => _inferredValueConfigurations;
            set
            {
                _inferredValueConfigurations = value;
                if (value != null)
                {
                    for (var i = 0; i < value.Count; i++)
                    {
                        var item = value[i];
                        item.Property = this;
                    }
                }
            }
        }

        public void SetInferredWithExpression(LambdaExpression value, bool onlyIfNew = false, InferredValueKind kind = InferredValueKind.Always, bool canOverride = false, params string[] onlyWhenPropertyChanges)
        {
            InferredValueConfigurations.Add(new InferredValueConfiguration(this)
                .SetInferredWithExpression(value, onlyIfNew, kind, canOverride, onlyWhenPropertyChanges));
        }

        public void SetConditionallyInferredWithExpression(
            LambdaExpression expression, LambdaExpression condition)
        {
            InferredValueConfigurations.Add(new InferredValueConfiguration(this)
                .SetConditionallyInferredWithExpression(expression, condition));
        }

        // Help avoid triggering lazy evaluation of inferred IQL expression
        public bool HasInferredWith => InferredValueConfigurations.Any();
        public bool HasInferredWithCondition => InferredValueConfigurations.Any(_ => _.HasCondition);

        //private LambdaExpression _inferredWithPathResolvedWith;
        //private IqlPropertyPath _inferredWithPath;
        //public IqlPropertyPath GetInferredWithPath()
        //{
        //    if (GetInferredWithExpression() == null)
        //    {
        //        return null;
        //    }

        //    if (_inferredWithPathResolvedWith != GetInferredWithExpression())
        //    {
        //        _inferredWithPathResolvedWith = GetInferredWithExpression();
        //        _inferredWithPath = IqlPropertyPath.FromLambdaExpression(GetInferredWithExpression(), EntityConfiguration);
        //    }
        //    return _inferredWithPath;
        //}

        public IqlCanTranslateKind CanTranslate { get; set; }
        public string CanTranslateProperty { get; set; }
        public IEnumerable<IRelationship> Relationships => RelationshipSources.Where(r => !r.ThisIsTarget).Select(r => r.Relationship);
        public ITypeDefinition TypeDefinition { get; set; }

        private IqlPropertySearchKind _searchKind;
#if !TypeScript
        public PropertyInfo PropertyInfo { get; set; }
#endif

        public EntityRelationship RelationshipDirect => _relationship;

        public EntityRelationship Relationship
        {
            get
            {
                if (_relationship != null)
                {
                    return _relationship;
                }

                return CountRelationshipProperty?.RelationshipDirect;
            }
            set
            {
                if (_relationship != null)
                {
                    if (_relationship != value)
                    {
                        throw new ArgumentException("A relationship can only be assigned once.");
                    }
                }
                else
                {
                    var canWrite = CanWriteInternal;
                    var canWriteSet = CanWriteSet;
                    _relationship = value;
                    if (_relationship != null && _relationship.ThisEnd.Property == this)
                    {
                        _relationship.ThisEnd.ValidationRules = ValidationRules;
                        _relationship.ThisEnd.DisplayRules = DisplayRules;
                        _relationship.ThisEnd.RelationshipFilterRules = RelationshipFilterRules;
                        if (canWriteSet && canWrite.HasValue)
                        {
                            _relationship.ThisEnd.CanWrite = canWrite.Value;
                        }
                    }
                }
            }
        }

        public override IqlPropertyKind Kind { get; set; }

        protected EntityRelationship _relationship;
        private bool _inferredValueConfigurationsDelayedInitialized;
        private IList<IInferredValueConfiguration> _inferredValueConfigurationsDelayed;
        private IList<IInferredValueConfiguration> _inferredValueConfigurations { get { if(!_inferredValueConfigurationsDelayedInitialized) { _inferredValueConfigurationsDelayedInitialized = true; _inferredValueConfigurationsDelayed = new List<IInferredValueConfiguration>(); } return _inferredValueConfigurationsDelayed; } set { _inferredValueConfigurationsDelayedInitialized = true; _inferredValueConfigurationsDelayed = value; } }
        private bool IsSearchableType => TypeDefinition != null && (TypeDefinition.Type == typeof(string) &&
                                                                    string.IsNullOrWhiteSpace(TypeDefinition.ConvertedFromType));

        private IqlPropertySearchKind? GetGroupSearchKind(IPropertyContainer groupDefinition)
        {
            var match = groupDefinition.GetPropertyGroupMetadata()
                .FirstOrDefault(_ => _.Property == this);
            if (match != null && match.Kind != null)
            {
                return match.Kind;
            }
            return null;
        }

        public bool AutoSearchKind { get; set; } = true;

        public virtual IqlPropertySearchKind SearchKind
        {
            get
            {
                if (AutoSearchKind)
                {
                    if (!IsSearchableType || ReadKind == IqlPropertyReadKind.Hidden)
                    {
                        return IqlPropertySearchKind.None;
                    }

                    if (EntityConfiguration.SpecialTypeDefinition != null)
                    {
                        var groupSearchKind = GetGroupSearchKind(EntityConfiguration.SpecialTypeDefinition);
                        if (groupSearchKind != null)
                        {
                            return groupSearchKind.Value;
                        }
                    }

                    var groups = EntityConfiguration.AllPropertyGroups();
                    var groupMatches = new List<IqlPropertySearchKind>();
                    for (var i = 0; i < groups.Length; i++)
                    {
                        var groupSearchKind = GetGroupSearchKind(groups[i]);
                        if (groupSearchKind != null)
                        {
                            groupMatches.Add(groupSearchKind.Value);
                        }
                    }
                    if (groupMatches.Any(_ => _ == IqlPropertySearchKind.Primary))
                    {
                        return IqlPropertySearchKind.Primary;
                    }
                    if (groupMatches.Any(_ => _ == IqlPropertySearchKind.Secondary))
                    {
                        return IqlPropertySearchKind.Primary;
                    }
                    if (groupMatches.Any())
                    {
                        return IqlPropertySearchKind.None;
                    }

                    if (EntityConfiguration.TitleProperty == this || EntityConfiguration.PreviewProperty == this ||
                        Matches("name", "fullname", "title", "firstname", "lastname", "christianname", "forename", "surname"))
                    {
                        return IqlPropertySearchKind.Primary;
                    }

                    var searchKind = Kind.HasFlag(IqlPropertyKind.Primitive) &&
                                  !Kind.HasFlag(IqlPropertyKind.RelationshipKey) &&
                                  !Kind.HasFlag(IqlPropertyKind.Key)
                        ? IqlPropertySearchKind.Secondary
                        : IqlPropertySearchKind.None;
                    // TOOD: Add getter for all special property types this belongs to
                    // TODO: Move this logic into each special property group
                    var specialPropertyMetadata = PropertyGroup?.GetPropertyGroupMetadata().FirstOrDefault(_ => _.Property == this);
                    if (specialPropertyMetadata != null && specialPropertyMetadata.Kind != null)
                    {
                        searchKind = IqlPropertySearchKind.None;
                    }
                    _searchKind = searchKind;
                }

                return _searchKind;
            }
            set
            {
                _searchKind = value;
                AutoSearchKind = false;
            }
        }

        public bool IsPersistenceKey => EntityConfiguration.PersistenceKeyProperty == this;

        internal IProperty CountRelationshipProperty { get; set; }

        public abstract Func<object, object> GetValue { get; set; }
        public abstract Func<object, object, object> SetValue { get; set; }

        public List<object> Helpers { get; set; }

        public IProperty SetNullable(bool nullable = true)
        {
            Nullable = nullable;
            return (IProperty)this;
        }

        protected PropertyBase() : base(null, null)
        {
        }

        private ITypeProperty _propertyMetadata = null;
        private bool? _isCount;
        private bool? _hasRelationship;
        public ITypeProperty PropertyMetadata => _propertyMetadata = _propertyMetadata ?? new PropertyMetadataProvider(EntityConfiguration.TypeMetadata, this);
    }
}