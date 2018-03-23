using System;
using System.Collections.Generic;
using System.Reflection;
using Iql.Queryable.Extensions;

namespace Iql.Queryable.Data.EntityConfiguration
{
    public abstract class PropertyBase : IPropertyMetadata
    {
        private string _friendlyName;
        private bool _friendlyNameSet;
        private string _resolvedFriendlyName;
        private string _title;

        private bool _titleSet;
        private string _name;
        private PropertySearchKind _searchKind;
#if !TypeScript
        public PropertyInfo PropertyInfo { get; set; }
#endif
        public bool Nullable { get; set; }
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
                    _searchKind = Kind == PropertyKind.Primitive && Type == typeof(string) && string.IsNullOrWhiteSpace(ConvertedFromType)
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

        public IProperty CountRelationship { get; private set; }
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
        public Type ElementType { get; set; }
        public Type Type { get; set; }
        public bool IsCollection { get; internal set; }
        public Type DeclaringType { get; internal set; }
        public string ConvertedFromType { get; set; }
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

        internal void Configure(
            string name,
            Type declaringType,
            Type propertyType,
            Type elementType,
            string convertedFromType,
            bool isCollection,
            bool readOnly,
            IProperty countRelationship
        )
        {
            Name = name;
            DeclaringType = declaringType;
            ElementType = elementType;
            Type = propertyType;
            IsCollection = isCollection;
            ConvertedFromType = convertedFromType;
            ReadOnly = readOnly;
            CountRelationship = countRelationship;
            Kind = PropertyKind.Primitive;
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