using System;
using System.Collections.Generic;

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
            return Properties.ToArray();
        }

        public PropertyCollection(IEntityConfiguration entityConfiguration) : base(entityConfiguration)
        {
            Properties = new List<IPropertyGroup>();
        }

        //IPropertyCollection IConfigurableProperty<IPropertyCollection>.Configure(Action<IPropertyCollection> configure)
        //{
        //    Configure(con)
        //}
    }
}