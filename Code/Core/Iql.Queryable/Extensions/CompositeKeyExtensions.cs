using Iql.Queryable.Data;
using Iql.Queryable.Operations;

namespace Iql.Queryable.Extensions
{
    public static class CompositeKeyExtensions
    {
        public static string AsKeyString(this CompositeKey compositeKey)
        {
            var str = "";
            foreach (var key in compositeKey.Keys)
            {
                str += key.Name + ":" + key.Value;
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