using System;
using System.Collections.Generic;

namespace Iql.Entities
{
    public class PropertyCollection : IPropertyGroup
    {
        public IEntityConfiguration EntityConfiguration { get; }
        public List<IPropertyGroup> Properties { get; }
        public IPropertyGroup[] GetProperties()
        {
            return Properties.ToArray();
        }

        public PropertyCollection(IEntityConfiguration entityConfiguration)
        {
            EntityConfiguration = entityConfiguration;
            Properties = new List<IPropertyGroup>();
        }
    }
}