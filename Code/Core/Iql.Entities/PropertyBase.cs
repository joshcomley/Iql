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
        public override IPropertyGroup ResolvePrimaryProperty()
        {
            var propertyGroup = PropertyGroup;
            if (propertyGroup == null)
            {
                return this;
            }
            return propertyGroup;
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
        public IFile File => EntityConfiguration?.Files.FirstOrDefault(dr => dr.GetPropertyKind(this as IProperty) != FilePropertyKind.None);
        public IDateRange DateRange => EntityConfiguration?.DateRanges.FirstOrDefault(dr => dr.GetPropertyKind(this as IProperty) != DateRangePropertyKind.None);
        public INestedSet NestedSet => EntityConfiguration?.NestedSets.FirstOrDefault(ns => ns.GetPropertyKind(this as IProperty) != NestedSetPropertyKind.None);
        public bool IsLongitudeProperty => Equals(GeographicPoint?.LongitudeProperty, this);
        public bool IsLatitudeProperty => Equals(GeographicPoint?.LatitudeProperty, this);
        public bool IsLongitudeOrLatitudeProperty => IsLongitudeProperty || IsLatitudeProperty;
        public bool IsTitleProperty => EntityConfiguration?.TitlePropertyName == Name || EntityConfiguration?.TitleProperty == this;
        public bool IsPreviewProperty => EntityConfiguration?.PreviewPropertyName == Name || EntityConfiguration?.PreviewProperty == this;
        public bool IsSubTitleProperty => HasHint(KnownHints.SubTitle);
        public override IEntityConfiguration EntityConfiguration => EntityConfigurationInternal;
        public IEntityConfiguration EntityConfigurationInternal { get; set; }
        public List<EntityRelationship> RelationshipSources { get; set; } = new List<EntityRelationship>();
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

        public IEnumerable<IRelationship> Relationships => RelationshipSources.Where(r => !r.ThisIsTarget).Select(r => r.Relationship);
        public ITypeDefinition TypeDefinition { get; set; }

        private PropertySearchKind _searchKind;
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

        public override PropertyKind Kind { get; set; }

        protected EntityRelationship _relationship;
        private IList<IInferredValueConfiguration> _inferredValueConfigurations = new List<IInferredValueConfiguration>();
        private bool IsSearchableType => TypeDefinition != null && (TypeDefinition.Type == typeof(string) &&
                                                                    string.IsNullOrWhiteSpace(TypeDefinition.ConvertedFromType));

        private PropertySearchKind? GetGroupSearchKind(IPropertyContainer groupDefinition)
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

        public virtual PropertySearchKind SearchKind
        {
            get
            {
                if (AutoSearchKind)
                {
                    if (!IsSearchableType || ReadKind == PropertyReadKind.Hidden)
                    {
                        return PropertySearchKind.None;
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
                    var groupMatches = new List<PropertySearchKind>();
                    for (var i = 0; i < groups.Length; i++)
                    {
                        var groupSearchKind = GetGroupSearchKind(groups[i]);
                        if (groupSearchKind != null)
                        {
                            groupMatches.Add(groupSearchKind.Value);
                        }
                    }
                    if (groupMatches.Any(_ => _ == PropertySearchKind.Primary))
                    {
                        return PropertySearchKind.Primary;
                    }
                    if (groupMatches.Any(_ => _ == PropertySearchKind.Secondary))
                    {
                        return PropertySearchKind.Primary;
                    }
                    if (groupMatches.Any())
                    {
                        return PropertySearchKind.None;
                    }

                    if (EntityConfiguration.TitleProperty == this || EntityConfiguration.PreviewProperty == this ||
                        Matches("name", "fullname", "title", "firstname", "lastname", "christianname", "forename", "surname"))
                    {
                        return PropertySearchKind.Primary;
                    }

                    var searchKind = Kind.HasFlag(PropertyKind.Primitive) &&
                                  !Kind.HasFlag(PropertyKind.RelationshipKey) &&
                                  !Kind.HasFlag(PropertyKind.Key)
                        ? PropertySearchKind.Secondary
                        : PropertySearchKind.None;
                    // TOOD: Add getter for all special property types this belongs to
                    // TODO: Move this logic into each special property group
                    var specialPropertyMetadata = PropertyGroup?.GetPropertyGroupMetadata().FirstOrDefault(_ => _.Property == this);
                    if (specialPropertyMetadata != null && specialPropertyMetadata.Kind != null)
                    {
                        searchKind = PropertySearchKind.None;
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

        public string PropertyName { get; set; }
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
        public ITypeProperty PropertyMetadata => _propertyMetadata = _propertyMetadata ?? new PropertyMetadataProvider(EntityConfiguration.TypeMetadata, this);
    }
}