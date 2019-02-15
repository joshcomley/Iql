using System;
using System.Collections.Generic;
using System.Linq;

namespace Iql.Entities
{
    public class DisplayConfiguration : IDisplayConfiguration
    {
        public RemainingPropertiesMode RemainingPropertiesMode { get; set; } = RemainingPropertiesMode.Append;
        public DisplayConfigurationKind Kind { get; set; }
        public IList<IPropertyGroup> Properties { get; set; }
        public string Key { get; set; }

        public DisplayConfiguration(
            DisplayConfigurationKind kind = DisplayConfigurationKind.Read,
            IList<IPropertyGroup> properties = null,
            string key = null)
        {
            Kind = kind;
            Properties = properties;
            Key = key;
        }

        public DisplayConfiguration()
        {
            
        }

        public DisplayConfiguration SetProperties<T>(
            EntityConfiguration<T> entityConfiguration,
            params Func<EntityConfiguration<T>, IPropertyGroup>[] properties)
            where T : class
        {
            var coll = new List<IPropertyGroup>();
            for (var i = 0; i < properties.Length; i++)
            {
                var property = properties[i];
                coll.Add(property(entityConfiguration));
            }

            var result = coll.ToList();
            Properties = result;
            return this;
        }
    }
}