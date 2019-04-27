using Iql.Extensions;

namespace Iql.Entities.Extensions
{
    public static class CompositeKeyExtensions
    {
        public static string AsLegacyKeyString(this CompositeKey compositeKey, bool? includeName = null)
        {
            return compositeKey.AsKeyString(includeName, false);
        }

        public static string AsKeyString(this CompositeKey compositeKey, bool? includeName = null, bool includeTypeName = true)
        {
            // Shortcuts for speed
            bool shouldIncludeName;
            switch (includeName)
            {
                case true:
                    shouldIncludeName = true;
                    break;
                case false:
                    shouldIncludeName = false;
                    break;
                default:
                    shouldIncludeName = compositeKey.Keys.Length > 1;
                    break;
            }
            var str = "";
            if (compositeKey.Keys.Length == 1)
            {
                str = $"{(shouldIncludeName ? compositeKey.Keys[0].Name + ":" : "")}{compositeKey.Keys[0].Value ?? "NULL"}";
            }
            else if (compositeKey.Keys.Length == 2)
            {
                str = $"{(shouldIncludeName ? compositeKey.Keys[0].Name + ":" : "")}{compositeKey.Keys[0].Value ?? "NULL"}" +
                    $";{(shouldIncludeName ? compositeKey.Keys[1].Name + ":" : "")}:{compositeKey.Keys[1].Value ?? "NULL"}";
            }
            else
            {
                foreach (var key in compositeKey.Keys)
                {
                    if (shouldIncludeName)
                    {
                        str += key.Name + ":";
                    }
                    str += key.Value + ";";
                }
            }

            if (includeTypeName == true && compositeKey.TypeName != null)
            {
                str = $"{compositeKey.TypeName}>{str}";
            }
            return str;
        }

        public static bool MatchesEntity(this CompositeKey compositeKey, object entity)
        {
            var isMatch = true;
            foreach (var key in compositeKey.Keys)
            {
                if (!Equals(entity.GetPropertyValueByName(key.Name), key.Value))
                {
                    isMatch = false;
                    break;
                }
            }
            return isMatch;
        }
    }
}