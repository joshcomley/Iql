using System.Linq;
using Iql.Data.Configuration;

namespace Iql.Data.Extensions
{
    public static class MetadataExtensions
    {
        public static string[] GroupPathParts(this IMetadata metadata)
        {
            var groupPath = metadata.GroupPath;
            return GetGroupPathParts(groupPath);
        }

        public static string[] GetGroupPathParts(string groupPath)
        {
            if (string.IsNullOrWhiteSpace(groupPath))
            {
                return new string[] { };
            }
            var parts = groupPath.Split('/');
            return parts.Where(part => !string.IsNullOrWhiteSpace(part)).ToArray();
        }
    }
}