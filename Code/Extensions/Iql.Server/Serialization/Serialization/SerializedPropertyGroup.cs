using System.Collections.Generic;

namespace Iql.Server.Serialization
{
    public class SerializedPropertyGroup
    {
        public string Type { get; set; }
        public string Paths { get; set; }
        public PropertyGroupKind Kind { get; set; }
        public List<SerializedPropertyGroup> Children { get; set; }
    }
}