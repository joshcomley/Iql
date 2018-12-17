using System.Collections.Generic;

namespace Iql.Entities
{
    public class DisplayConfiguration
    {
        public DisplayConfigurationKind Kind { get; set; }
        public IList<IPropertyGroup> Properties { get; set; }
        public string Key { get; set; }

        public DisplayConfiguration(DisplayConfigurationKind kind = DisplayConfigurationKind.Read, IList<IPropertyGroup> properties = null, string key = null)
        {
            Kind = kind;
            Properties = properties;
            Key = key;
        }

        public DisplayConfiguration()
        {
            
        }
    }
}