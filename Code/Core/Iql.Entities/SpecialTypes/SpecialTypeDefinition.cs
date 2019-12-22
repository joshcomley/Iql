using System;
using System.Collections.Generic;
using System.Linq;

namespace Iql.Entities.SpecialTypes
{
    public abstract class SpecialTypeDefinition : MetadataBase, IPropertyContainer
    {
        public override IUserPermission ParentPermissions => EntityConfiguration;
        public abstract Type InternalType { get; }
        private List<IPropertyGroup> _cachedProperties;
        public IProperty IdProperty { get; set; }

        private IEntityConfiguration _entityConfiguration = null;
        public override IEntityConfiguration EntityConfiguration => _entityConfiguration = _entityConfiguration ??
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
            return PropertyMaps.SingleOrDefault(_ => ((IMetadata) _.CustomProperty).Name == customPropertyName);
        }

        public abstract PropertyMap[] PropertyMaps { get; }

        public IProperty PrimaryProperty
        {
            get { return IdProperty; }
        }

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

        public bool IsTypeGroup => false;

        public IqlPropertyGroupKind GroupKind { get; } = IqlPropertyGroupKind.SpecialType;
        public abstract PropertyGroupMetadata[] GetPropertyGroupMetadata();

        public abstract IPropertyGroup[] GetSpecialTypeProperties();
    }
}