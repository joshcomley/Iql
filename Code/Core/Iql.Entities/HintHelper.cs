using System.Collections.Generic;

namespace Iql.Entities
{
    public static class HintHelper
    {
        public static HintHelperResult FindHintAndResolveProperty(IMetadata metadata, string name, bool? onlySelf = null)
        {
            if (onlySelf != true)
            {
                var hint = FindHint(metadata, name, true);
                if (hint != null)
                {
                    return new HintHelperResult(hint, metadata);
                }
                if (metadata is IPropertyGroup group)
                {
                    if (group.PrimaryProperty != null)
                    {
                        hint = HintHelper.FindHint(group.PrimaryProperty, name, true);
                        if (hint != null)
                        {
                            return new HintHelperResult(hint, group.PrimaryProperty);
                        }
                    }
                    if (group.PropertyGroup != null)
                    {
                        hint = HintHelper.FindHint(group.PropertyGroup, name, true);
                        {
                            return new HintHelperResult(hint, group.PropertyGroup);
                        }
                    }
                    if (group.PrimaryProperty != null && group.PrimaryProperty.PropertyGroup != null)
                    {
                        hint = HintHelper.FindHint(group.PrimaryProperty.PropertyGroup, name, true);
                        if (hint != null)
                        {
                            return new HintHelperResult(hint, group.PrimaryProperty.PropertyGroup);
                        }
                    }
                }
                return null;
            }
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
                    return new HintHelperResult(new MetadataHint(hint), metadata);
                }

                var startsWith = $"{name}:";
                if (lower.StartsWith(startsWith))
                {
                    return new HintHelperResult(new MetadataHint(name, hint.Substring(startsWith.Length)), metadata);
                }
            }
            return null;
        }

        public static MetadataHint FindHint(IMetadata metadata, string name, bool? onlySelf = null)
        {
            return FindHintAndResolveProperty(metadata, name, onlySelf)?.Hint;
        }

        public static bool HasHint(IConfiguration metadata, string name, bool? onlySelf = null)
        {
            return metadata.FindHint(name, onlySelf) != null;
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

        public static void RemoveHint(IMetadata metadata, string name, bool? onlySelf = null)
        {
            if (metadata.Hints == null)
            {
                return;
            }
            HintHelperResult result = FindHintAndResolveProperty(metadata, name, onlySelf);
            while(result != null)
            {
                var length = result.Metadata.Hints.Count;
                result.Metadata.Hints.Remove(result.Hint.Formatted());
                if (result.Metadata.Hints.Count == length)
                {
                    // Something went wrong
                    break;
                }
                result = FindHintAndResolveProperty(metadata, name, onlySelf);
            }
        }
    }
}