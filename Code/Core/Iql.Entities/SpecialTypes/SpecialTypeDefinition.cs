using System;
using System.Collections.Generic;
using System.Linq;

namespace Iql.Entities.SpecialTypes
{
    public abstract class SpecialTypeDefinition : MetadataBase, IPropertyContainer
    {
        public abstract Type InternalType { get; }
        private List<IPropertyGroup> _cachedProperties;
        public IProperty IdProperty { get; set; }

        private IEntityConfiguration _entityConfiguration = null;
        public IEntityConfiguration EntityConfiguration => _entityConfiguration = _entityConfiguration ??
            GetGroupProperties().Select(_ => _.EntityConfiguration).FirstOrDefault();

        protected SpecialTypeDefinition(IProperty idProperty)
        {
            IdProperty = idProperty;
        }

        public virtual PropertyMap ResolvePropertyMap(string internalPropertyName)
        {
            return PropertyMaps.SingleOrDefault(_ => _.InternalPropertyName == internalPropertyName);
        }

        public virtual PropertyMap ResolvePropertyMapInverse(string customPropertyName)
        {
            return PropertyMaps.SingleOrDefault(_ => _.CustomProperty.Name == customPropertyName);
        }

        public abstract PropertyMap[] PropertyMaps { get; }

        public IPropertyGroup[] GetGroupProperties()
        {
            if (_cachedProperties != null)
            {
                return _cachedProperties.ToArray();
            }

            var all = new List<IPropertyGroup>();
            if (IdProperty != null)
            {
                all.Add(IdProperty);
            }

            all.AddRange(GetSpecialTypeProperties());
            if (all.All(_ => _ != null))
            {
                _cachedProperties = all;
            }
            return all.ToArray();
        }

        public abstract IPropertyGroup[] GetSpecialTypeProperties();
    }
}