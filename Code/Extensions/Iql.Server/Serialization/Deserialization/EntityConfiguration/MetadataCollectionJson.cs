using Iql.Entities;
using System.Collections.Generic;

namespace Iql.Server.Serialization
{
    internal class MetadataCollectionJson : MetadataCollectionBase, IMetadataCollection
    {
        public KeyValuePair<string, object>[] All { get; set; }
    }
}