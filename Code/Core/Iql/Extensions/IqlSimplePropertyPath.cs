using System.Collections.Generic;
using System.Linq;

namespace Iql.Extensions
{
    public class IqlSimplePropertyPath
    {
        public string RootReferenceName { get; set; }
        public string[] PathParts { get; set; }
        public string Path => PathParts == null ? "" : string.Join(".", PathParts);

        public string FullPath
        {
            get
            {
                var parts = new List<string>();
                if (!string.IsNullOrWhiteSpace(RootReferenceName))
                {
                    parts.Add(RootReferenceName);
                }

                if (PathParts != null)
                {
                    parts.AddRange(PathParts);
                }

                return string.Join(".", parts);
            }
        }

        public string PathAfter(int skip, bool includeRoot = false)
        {
            var sourceParts = (PathParts ?? new string[] { }).ToList();
            var parts = new List<string>();
            if (includeRoot && !string.IsNullOrWhiteSpace(RootReferenceName))
            {
                parts.Add(RootReferenceName);
            }
            parts.AddRange(sourceParts);
            var newParts = new List<string>();
            for (var i = skip; i < parts.Count; i++)
            {
                newParts.Add(parts[i]);
            }
            return string.Join(".", newParts);
        }

        public IqlSimplePropertyPath(string rootReferenceName, string[] pathParts)
        {
            RootReferenceName = rootReferenceName;
            PathParts = pathParts;
        }
    }
}