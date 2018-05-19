using Iql.Extensions;

namespace Iql.Entities.Extensions
{
    public static class CompositeKeyExtensions
    {
        public static int AsKeyStringCount;
        public static string AsKeyString(this CompositeKey compositeKey, bool? includeName = null)
        {
            AsKeyStringCount++;
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
            if (compositeKey.Keys.Length == 1)
            {
                return $"{(shouldIncludeName ? compositeKey.Keys[0].Name + ":" : "")}{compositeKey.Keys[0].Value ?? "NULL"}";
            }
            if (compositeKey.Keys.Length == 2)
            {
                return $"{(shouldIncludeName ? compositeKey.Keys[0].Name + ":" : "")}{compositeKey.Keys[0].Value ?? "NULL"}" +
                    $";{(shouldIncludeName ? compositeKey.Keys[1].Name + ":" : "")}:{compositeKey.Keys[1].Value ?? "NULL"}";
            }
            var str = "";
            foreach (var key in compositeKey.Keys)
            {
                if (shouldIncludeName)
                {
                    str += key.Name + ":";
                }
                str += key.Value + ";";
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