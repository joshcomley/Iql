using System;
using System.Collections.Generic;
using System.Reflection;
using Iql.Entities.Extensions;
using Iql.Entities.Metadata;
using Iql.Extensions;

namespace Iql.Entities
{
    public abstract class PropertyBase : MetadataBase, IPropertyMetadata
    {
        public IEntityConfiguration EntityConfiguration { get; set; }
        public List<RelationshipMatch> RelationshipSources { get; set; } = new List<RelationshipMatch>();
        public bool Searchable { get; set; }

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

        public ITypeDefinition TypeDefinition { get; set; }

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
        public abstract Func<object, object> PropertyGetter { get; set; }
        public abstract Func<object, object, object> PropertySetter { get; set; }

        public List<object> Helpers { get; set; }
    }
}