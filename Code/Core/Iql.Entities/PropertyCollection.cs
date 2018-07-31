using System;
using System.Collections.Generic;
using System.Linq;

namespace Iql.Entities
{
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
            return Properties.Select(p =>
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