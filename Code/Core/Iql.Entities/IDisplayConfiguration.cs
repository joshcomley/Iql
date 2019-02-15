using System.Collections.Generic;

namespace Iql.Entities
{
    public interface IDisplayConfiguration
    {
        RemainingPropertiesMode RemainingPropertiesMode { get; set; }
        DisplayConfigurationKind Kind { get; set; }
        IList<IPropertyGroup> Properties { get; set; }
    }
}