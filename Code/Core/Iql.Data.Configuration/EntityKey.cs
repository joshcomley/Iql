using System;
using System.Collections.Generic;
using System.Linq;

namespace Iql.Queryable.Data.EntityConfiguration
{
    public class EntityKey<T, TKey> : IEntityKey
    {
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

        public void SetReadOnly(bool readOnly = true)
        {
            for (var i = 0; i < Properties.Length; i++)
            {
                var property = Properties[i];
                property.ReadOnly = readOnly;
            }
        }
    }
}