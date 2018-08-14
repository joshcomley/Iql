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

        public abstract IProperty ResolvePropertyMap(string internalPropertyName);

        public IPropertyGroup[] GetGroupProperties()
        {
            if (_cachedProperties != null)
            {
                return _cachedProperties.ToArray();
            }
            var all = new List<IPropertyGroup>();
            all.Add(IdProperty);
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