using System;
using System.Collections.Generic;
using System.Reflection;
using Iql.Queryable.Extensions;

namespace Iql.Queryable.Data.EntityConfiguration
{
    public abstract class PropertyBase : IPropertyMetadata
    {
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

        private string _friendlyName;
        private bool _friendlyNameSet;
        private string _resolvedFriendlyName;
        private string _title;
        public ITypeDefinition TypeDefinition { get; set; }

        private bool _titleSet;
        private string _name;
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

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                _resolvedFriendlyName = null;
            }
        }
        public string Placeholder { get; set; }
        public abstract Func<object, object> PropertyGetter { get; set; }
        public abstract Func<object, object, object> PropertySetter { get; set; }

        public string FriendlyName
        {
            get => _friendlyNameSet ? _friendlyName : Title;
            set
            {
                _friendlyName = value;
                _friendlyNameSet = true;
                _resolvedFriendlyName = null;
            }
        }

        public string Title
        {
            get => _titleSet ? _title : Name;
            set
            {
                _title = value;
                _titleSet = true;
            }
        }

        public string GroupPath { get; set; }
        public string Description { get; set; }
        public List<object> Helpers { get; set; }
        public List<string> Hints { get; set; }

        public MetadataHint FindHint(string name)
        {
            return HintHelper.FindHint(this, name);
        }

        public bool HasHint(string name)
        {
            return HintHelper.HasHint(this, name);
        }

        public void RemoveHint(string name)
        {
            HintHelper.RemoveHint(this, name);
        }

        public void SetHint(string name, string value = null)
        {
            HintHelper.SetHint(this, name, value);
        }


        public string ResolveFriendlyName()
        {
            return _resolvedFriendlyName ?? (_resolvedFriendlyName =
                       string.IsNullOrWhiteSpace(FriendlyName) || !_friendlyNameSet
                           ? IntelliSpace.Parse(Name)
                           : FriendlyName);
        }

        public string ResolveName()
        {
            return Name ?? Title ?? "Unknown";
        }
    }
}