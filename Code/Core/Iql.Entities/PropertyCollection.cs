using System;
using System.Collections.Generic;

namespace Iql.Entities
{
    public class PropertyCollection : PropertyGroupBase<PropertyCollection>
    {
        public List<IPropertyGroup> Properties { get; }
        public override IPropertyGroup[] GetProperties()
        {
            return Properties.ToArray();
        }

        public PropertyCollection(IEntityConfiguration entityConfiguration) : base(entityConfiguration)
        {
            Properties = new List<IPropertyGroup>();
        }
    }
}