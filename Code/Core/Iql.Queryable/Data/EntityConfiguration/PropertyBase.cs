using System;
using System.Collections.Generic;
using System.Reflection;
using Iql.Queryable.Extensions;

namespace Iql.Queryable.Data.EntityConfiguration
{
    public abstract class PropertyBase : IPropertyMetadata
    {
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
        public RelationshipMatch Relationship { get; set; }
        public PropertyKind Kind { get; set; }

        private bool _searchKindSet;
        public PropertySearchKind SearchKind
        {
            get
            {
                if (!_searchKindSet)
                {
                    _searchKindSet = true;
                    _searchKind = Kind == PropertyKind.Primitive && TypeDefinition.Type == typeof(string) && string.IsNullOrWhiteSpace(TypeDefinition.ConvertedFromType)
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

        public IProperty CountRelationship { get; protected set; }
        public bool ReadOnly { get; set; }

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