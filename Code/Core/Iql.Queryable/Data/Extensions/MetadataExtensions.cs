using System.Collections.Generic;
using System.Linq;
using Iql.Queryable.Data.EntityConfiguration;

namespace Iql.Queryable.Data.Extensions
{
    public static class MetadataExtensions
    {
        public static string[] GroupPathParts(this IMetadata metadata)
        {
            if (string.IsNullOrWhiteSpace(metadata.GroupPath))
            {
                return new string[] { };
            }
            var parts = metadata.GroupPath.Split('/');
            return parts.Where(part => !string.IsNullOrWhiteSpace(part)).ToArray();
        }
    }
}