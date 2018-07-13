using System.Collections.Generic;

namespace Iql.Entities
{
    public static class HintHelper
    {
        public static MetadataHint FindHint(IMetadata metadata, string name)
        {
            if (metadata.Hints == null)
            {
                return null;
            }
            name = name.ToLower();
            foreach (var hint in metadata.Hints)
            {
                var lower = hint.ToLower();
                if (name == lower)
                {
                    return new MetadataHint(hint);
                }

                var startsWith = $"{name}:";
                if (lower.StartsWith(startsWith))
                {
                    return new MetadataHint(hint, hint.Substring(startsWith.Length));
                }
            }
            return null;
        }

        public static bool HasHint(IConfiguration metadata, string name)
        {
            return metadata.FindHint(name) != null;
        }

        public static void SetHint(IConfiguration metadata, string name, string value = null)
        {
            var hint = FindHint(metadata, name);
            if (hint != null)
            {
                RemoveHint(metadata, name);
            }

            metadata.Hints = metadata.Hints ?? new List<string>();
            metadata.Hints.Add(new MetadataHint(name, value).Formatted());
        }

        public static void RemoveHint(IMetadata metadata, string name)
        {
            if (metadata.Hints == null)
            {
                return;
            }
            var hint = FindHint(metadata, name);
            if (hint != null)
            {
                metadata.Hints.Remove(hint.Formatted());
            }
        }
    }
}