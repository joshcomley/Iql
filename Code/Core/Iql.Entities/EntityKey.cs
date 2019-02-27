using System;
using System.Collections.Generic;
using System.Linq;

namespace Iql.Entities
{
    public class EntityKeyBase : IEntityKey
    {
        private readonly IList<IProperty> _propertiesInternal;
        private IProperty[] _properties;

        public EntityKeyBase()
        {
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

        public bool HasRelationshipKeys => Properties.Any(p =>
            p.Relationship != null && !p.Relationship.ThisIsTarget);

        public bool Editable { get; set; }

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

        public void SetReadKind(PropertyReadKind readKind)
        {
            for (var i = 0; i < Properties.Length; i++)
            {
                var property = Properties[i];
                property.ReadKind = readKind;
            }
        }
        public void SetEditKind(PropertyEditKind editKind)
        {
            for (var i = 0; i < Properties.Length; i++)
            {
                var property = Properties[i];
                property.EditKind = editKind;
            }
        }

        public Type Type { get; set; }
        public Type KeyType { get; set; }
    }
    public class EntityKey<T, TKey> : EntityKeyBase, IEntityKey
    {
        public EntityKey()
        {
            Type = typeof(T);
            KeyType = typeof(TKey);
        }
    }
}