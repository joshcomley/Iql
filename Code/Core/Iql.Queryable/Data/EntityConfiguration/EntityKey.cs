using System;
using System.Collections.Generic;
using System.Linq;
using Iql.Queryable.Data.Lists;

namespace Iql.Queryable.Data.EntityConfiguration
{
    public class EntityKey<T, TKey> : IEntityKey
    {
        private bool _isGeneratedRemotely = true;
        private IList<IProperty> _propertiesInternal;
        private IProperty[] _properties;

        public EntityKey()
        {
            Type = typeof(T);
            KeyType = typeof(TKey);
            _propertiesInternal = new List<IProperty>();
            UpdateProperties();
        }

        public void AddProperty(IProperty property)
        {
            _propertiesInternal.Add(property);
            UpdateProperties();
        }

        private void UpdateProperties()
        {
            _properties = _propertiesInternal.ToArray();
            for (var i = 0; i < _propertiesInternal.Count; i++)
            {
                var property = _properties[i];
                property.ReadOnly = IsGeneratedRemotely;
            }
        }

        public bool IsGeneratedRemotely
        {
            get => _isGeneratedRemotely;
            set
            {
                _isGeneratedRemotely = value;
                UpdateProperties();
            }
        }

        public Type Type { get; set; }
        public bool HasRelationshipKeys => Properties.Any(p =>
            p.Relationship != null && !p.Relationship.ThisIsTarget);
        public Type KeyType { get; set; }

        public IProperty[] Properties
        {
            get => _properties;
        }

        public bool IsPivot()
        {
            for (var i = 0; i < _properties.Length; i++)
            {
                var property = _properties[i];
                if (!property.Kind.HasFlag(PropertyKind.RelationshipKey))
                {
                    return false;
                }
            }
            return true;
        }
    }
}