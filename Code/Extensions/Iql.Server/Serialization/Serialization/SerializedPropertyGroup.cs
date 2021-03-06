﻿using System.Collections.Generic;

namespace Iql.Server.Serialization.Serialization
{
    public class SerializedPropertyGroup
    {
        public string Type { get; set; }
        public string Paths { get; set; }
        public IqlPropertyGroupKind Kind { get; set; }
        public List<SerializedPropertyGroup> Children { get; set; }
    }
}