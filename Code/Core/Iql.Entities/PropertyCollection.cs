using System;
using System.Collections.Generic;
using System.Linq;

namespace Iql.Entities
{
    public interface IPropertyPath : IPropertyGroup, IConfigurable<IPropertyPath>
    {
        string Path { get; set; }
        ISimpleProperty Property { get; }
        IqlPropertyPath BuildPropertyPath();
    }

    public class PropertyPath : SimplePropertyGroupBase<IPropertyPath>, IPropertyPath
    {
        public override ISimpleProperty ResolvePrimaryProperty()
        {
            return Property;
        }

        protected ISimpleProperty _property;
        public override PropertyKind Kind { get; set; } = PropertyKind.SimpleCollection;
        public string Path { get; set; }

        public ISimpleProperty Property => _property;

        public PropertyPath(IEntityConfiguration configuration, string path, string key = null)
            : base(configuration, key)
        {
            Path = path;
            if (configuration != null)
            {
                _property = configuration.FindNestedProperty(path);
            }
        }

        public override IPropertyGroup[] GetGroupProperties()
        {
            return new[] { Property };
        }

        public IqlPropertyPath BuildPropertyPath()
        {
            return IqlPropertyPath.FromString(Path, EntityConfiguration);
        }
    }

    public class PropertyCollection : PropertyGroupBase<IPropertyCollection>, IPropertyCollection
    {
        public bool Enclose { get; set; } = true;
        public ContentAlignment ContentAlignment { get; set; } = ContentAlignment.Vertical;
        public override PropertyKind Kind
        {
            get => PropertyKind.GroupCollection;
            set { }
        }
        public List<IPropertyGroup> Properties { get; }
        public override IPropertyGroup[] GetGroupProperties()
        {
            return Properties.Where(p => p != null).Select(p =>
            {
                if (p.Kind.HasFlag(PropertyKind.Property))
                {
                    var pr = p as IProperty;
                    return pr.PropertyGroup ?? pr;
                }
                return p;
            }).ToArray();
        }

        public PropertyCollection(IEntityConfiguration entityConfiguration, string key = null) : base(entityConfiguration, null)
        {
            Properties = new List<IPropertyGroup>();
        }

        //IPropertyCollection IConfigurableProperty<IPropertyCollection>.Configure(Action<IPropertyCollection> configure)
        //{
        //    Configure(con)
        //}
    }
}