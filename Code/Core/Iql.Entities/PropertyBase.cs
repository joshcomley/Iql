using Iql.Entities.Extensions;
using Iql.Entities.Geography;
using Iql.Entities.NestedSets;
using Iql.Entities.Relationships;
using Iql.Entities.Rules;
using Iql.Entities.Rules.Relationship;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Iql.Entities.PropertyGroups.Dates;
using Iql.Entities.PropertyGroups.Files;

namespace Iql.Entities
{
    public abstract class PropertyBase : SimplePropertyGroupBase<IProperty>, IPropertyMetadata
    {
        public override ISimpleProperty ResolvePrimaryProperty()
        {
            return PropertyGroup ?? this;
        }

        public bool Internal => PropertyGroup != null;
        public bool IsHiddenOrInternal => IsHidden || Internal;
        public ISimpleProperty PropertyGroup
        {
            get
            {
                if (Relationship != null)
                {
                    return Relationship.ThisEnd;
                }

                if (Geographic != null)
                {
                    return Geographic;
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
        public IGeographic Geographic => EntityConfiguration != null && EntityConfiguration.Geographics != null
            ? EntityConfiguration.Geographics.FirstOrDefault(g =>
                Equals(g.LatitudeProperty, this) || Equals(g.LongitudeProperty, this))
            : null;

        public IFile File => EntityConfiguration?.Files.FirstOrDefault(dr => dr.GetPropertyKind(this as IProperty) != FilePropertyKind.None);
        public IDateRange DateRange => EntityConfiguration?.DateRanges.FirstOrDefault(dr => dr.GetPropertyKind(this as IProperty) != DateRangePropertyKind.None);
        public INestedSet NestedSet => EntityConfiguration?.NestedSets.FirstOrDefault(ns => ns.GetPropertyKind(this as IProperty) != NestedSetPropertyKind.None);
        public bool IsLongitudeProperty => Equals(Geographic?.LongitudeProperty, this);
        public bool IsLatitudeProperty => Equals(Geographic?.LatitudeProperty, this);
        public bool IsLongitudeOrLatitudeProperty => IsLongitudeProperty || IsLatitudeProperty;
        public bool IsTitleProperty => EntityConfiguration?.TitlePropertyName == Name;
        public bool IsPreviewProperty => EntityConfiguration?.PreviewPropertyName == Name;
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

        public LambdaExpression InferredWith { get; set; }
        private LambdaExpression _inferredWithPathResolvedWith;
        private IqlPropertyPath _inferredWithPath;
        public IqlPropertyPath GetInferredWithPath()
        {
            if (InferredWith == null)
            {
                return null;
            }

            if (_inferredWithPathResolvedWith != InferredWith)
            {
                _inferredWithPathResolvedWith = InferredWith;
                _inferredWithPath = IqlPropertyPath.FromLambdaExpression(InferredWith, EntityConfiguration);
            }
            return _inferredWithPath;
        }

        public virtual IRuleCollection<IRelationshipRule> RelationshipFilterRules { get; set; }
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
            set => _relationship = value;
        }

        public override PropertyKind Kind { get; set; }

        private bool _searchKindSet;
        private bool? _readOnly;
        private EntityRelationship _relationship;

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

        public bool IsReadOnly => ResolveAutoReadOnly();

        public bool IsHidden => EditKind == PropertyEditKind.Hidden && ReadKind == PropertyReadKind.Hidden;


        private bool ResolveAutoReadOnly()
        {
            if (EditKind == PropertyEditKind.Display || EditKind == PropertyEditKind.Hidden)
            {
                return true;
            }

            if (Kind.HasFlag(PropertyKind.Key) && !Kind.HasFlag(PropertyKind.RelationshipKey))
            {
                return true;
            }

            if (Kind.HasFlag(PropertyKind.Relationship) && Relationship?.ThisIsTarget == true && !Relationship.ThisEnd.EntityConfiguration.Key.IsPivot())
            {
                return true;
            }

            return false;
        }

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
    }
}