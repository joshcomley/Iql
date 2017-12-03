using Iql.Queryable.Data;
using Iql.Queryable.Operations;

namespace Iql.Queryable.Extensions
{
    public static class CompositeKeyExtensions
    {
        public static bool MatchesEntity(this CompositeKey compositeKey, object entity)
        {
            var isMatch = true;
            foreach (var key in compositeKey.Keys)
            {
                if (!Equals(entity.GetPropertyValue(key.Name), key.Value))
                {
                    isMatch = false;
                    break;
                }
            }
            return isMatch;
        }
    }
}