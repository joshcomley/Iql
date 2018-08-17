using System.Collections.Generic;
using Iql.Entities;

namespace Iql.Server.Serialization.Deserialization.EntityConfiguration
{
    internal class MetadataCollectionJson : MetadataCollectionBase, IMetadataCollection
    {
        public KeyValuePair<string, object>[] All { get; set; }
    }
}