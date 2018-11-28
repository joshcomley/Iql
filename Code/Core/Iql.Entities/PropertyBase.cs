using Iql.Entities.Extensions;
using Iql.Entities.Geography;
using Iql.Entities.NestedSets;
using Iql.Entities.Relationships;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Iql.Conversion;
using Iql.Entities.PropertyGroups.Dates;
using Iql.Entities.PropertyGroups.Files;

namespace Iql.Entities
{
    public abstract class PropertyBase : SimplePropertyGroupBase<IProperty>, IPropertyMetadata
    {
        public override ISimpleProperty ResolvePrimaryProperty()
        {
            var propertyGroup = PropertyGroup;
            if (propertyGroup == null)
            {
                return this;
            }
            return propertyGroup;
        }

        public override bool Internal => PropertyGroup != null;

        public ISimpleProperty PropertyGroup
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

        public void SetInferredWithExpression(LambdaExpression value)
        {
            // This expression is lazy converted to IQL on demand
            _inferredWithExpression = value;
        }

        public IqlExpression InferredWithIql
        {
            get
            {
                // Lazy convert the expression
                if (_inferredWithExpression != null)
                {
                    _inferredWithIql = IqlConverter.Instance.ConvertLambdaExpressionToIqlByType(
                        _inferredWithExpression, EntityConfiguration.Type).Expression;
                    _inferredWithExpression = null;
                }
                return _inferredWithIql;
            }
            set
            {
                _inferredWithExpression = null;
                _inferredWithIql = value;
            }
        }

        // Help avoid triggering lazy evaluation of inferred IQL expression
        public bool HasInferredWith => _inferredWithExpression != null || _inferredWithIql != null;

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
        public EntityRelationship Relationship
        {
            get
            {
                if (_relationship != null)
                {
                    return _relationship;
                }
                return CountRelationship?.Relationship;
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
                    _relationship = value;
                    if (_relationship != null && _relationship.ThisEnd.Property == this)
                    {
                        _relationship.ThisEnd.ValidationRules = ValidationRules;
                        _relationship.ThisEnd.DisplayRules = DisplayRules;
                        _relationship.ThisEnd.RelationshipFilterRules = RelationshipFilterRules;
                    }
                }
            }
        }

        public override PropertyKind Kind { get; set; }

        private bool _searchKindSet;
        private bool? _readOnly;
        private EntityRelationship _relationship;
        private LambdaExpression _inferredWithExpression;
        private IqlExpression _inferredWithIql;

        public PropertySearchKind SearchKind
        {
            get
            {
                if (!_searchKindSet)
                {
                    _searchKindSet = true;
                    _searchKind = Kind.HasFlag(PropertyKind.Primitive) &&
                                  !Kind.HasFlag(PropertyKind.RelationshipKey) &&
                                  !Kind.HasFlag(PropertyKind.Key) &&
                                  TypeDefinition.Type == typeof(string) && string.IsNullOrWhiteSpace(TypeDefinition.ConvertedFromType)
                        ? PropertySearchKind.Secondary
                        : PropertySearchKind.None;
                }

                return _searchKind;
            }
            set
            {
                _searchKind = value;
                _searchKindSet = true;
            }
        }

        internal IProperty CountRelationship { get; set; }

        public string PropertyName { get; set; }

        public abstract Func<object, object> GetValue { get; set; }
        public abstract Func<object, object, object> SetValue { get; set; }

        public List<object> Helpers { get; set; }

        public IProperty SetNullable(bool nullable = true)
        {
            Nullable = nullable;
            return (IProperty)this;
        }

        protected override bool ResolveAutoReadOnly()
        {
            if (HasInferredWith)
            {
                return true;
            }
            if (Kind.HasFlag(PropertyKind.Relationship) && Relationship?.ThisIsTarget == true && !Relationship.ThisEnd.EntityConfiguration.Key.IsPivot())
            {
                return true;
            }
            return base.ResolveAutoReadOnly();
        }

        protected PropertyBase() : base(null, null)
        {
        }
    }
}