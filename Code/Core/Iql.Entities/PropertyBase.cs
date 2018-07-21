using Iql.Entities.Extensions;
using Iql.Entities.Geography;
using Iql.Entities.Relationships;
using Iql.Entities.Rules;
using Iql.Entities.Rules.Display;
using Iql.Entities.Rules.Relationship;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Iql.Entities
{
    public abstract class PropertyBase : MetadataBase, IPropertyMetadata
    {
        public IGeographic Geographic => EntityConfiguration != null && EntityConfiguration.Geographics != null
            ? EntityConfiguration.Geographics.FirstOrDefault(g =>
                Equals(g.LatitudeProperty, this) || Equals(g.LongitudeProperty, this))
            : null;
        public bool IsLongitudeProperty => Equals(Geographic?.LongitudeProperty, this);
        public bool IsLatitudeProperty => Equals(Geographic?.LatitudeProperty, this);
        public bool IsLongitudeOrLatitudeProperty => IsLongitudeProperty || IsLatitudeProperty;
        public bool IsTitleProperty => EntityConfiguration?.TitlePropertyName == Name;
        public bool IsPreviewProperty => EntityConfiguration?.PreviewPropertyName == Name;
        public bool IsSubTitleProperty => HasHint(KnownHints.SubTitle);
        public IEntityConfiguration EntityConfiguration { get; set; }
        public List<RelationshipMatch> RelationshipSources { get; set; } = new List<RelationshipMatch>();
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

        public virtual IRuleCollection<IBinaryRule> ValidationRules { get; set; }
        public virtual IRuleCollection<IDisplayRule> DisplayRules { get; set; }
        public virtual IRuleCollection<IRelationshipRule> RelationshipFilterRules { get; set; }
        public IEnumerable<IRelationship> Relationships => RelationshipSources.Where(r => !r.ThisIsTarget).Select(r => r.Relationship);
        public ITypeDefinition TypeDefinition { get; set; }

        protected abstract IMediaKey GetMediaKey();
        protected abstract void SetMediaKey(IMediaKey value);
        public bool HasMediaKey => MediaKey?.Groups?.Any(g => g.Parts?.Any() == true) == true;

        public IMediaKey MediaKey
        {
            get => GetMediaKey();
            set => SetMediaKey(value);
        }

        private PropertySearchKind _searchKind;
#if !TypeScript
        public PropertyInfo PropertyInfo { get; set; }
#endif
        public RelationshipMatch Relationship
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

        public PropertyKind Kind { get; set; }

        private bool _searchKindSet;
        private bool? _readOnly;
        private RelationshipMatch _relationship;

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

        public bool ReadOnly
        {
            get => _readOnly ?? ResolveAutoReadOnly();
            set => _readOnly = value;
        }

        public bool Hidden { get; set; } = false;
        public bool Sortable { get; set; } = true;

        private bool ResolveAutoReadOnly()
        {
            if (Kind.HasFlag(PropertyKind.Key) && !Kind.HasFlag(PropertyKind.RelationshipKey))
            {
                return true;
            }

            if (Kind.HasFlag(PropertyKind.Relationship) && Relationship?.ThisIsTarget == true && !Relationship.ThisEnd.Configuration.Key.IsPivot())
            {
                return true;
            }

            return false;
        }

        public string Placeholder { get; set; }
        public abstract Func<object, object> GetValue { get; set; }
        public abstract Func<object, object, object> SetValue { get; set; }

        public List<object> Helpers { get; set; }

        public IProperty SetReadOnlyAndHidden(bool readOnlyAndHidden = true)
        {
            SetReadOnly(readOnlyAndHidden).SetHidden(readOnlyAndHidden);
            return (IProperty)this;
        }

        public IProperty SetReadOnly(bool readOnly = true)
        {
            ReadOnly = readOnly;
            return (IProperty)this;
        }

        public IProperty SetHidden(bool hidden = true)
        {
            Hidden = hidden;
            return (IProperty)this;
        }

        public IProperty SetNullable(bool nullable = true)
        {
            Nullable = nullable;
            return (IProperty)this;
        }
    }
}