using System.Collections.Generic;
using Iql.Queryable.Operations;

namespace Iql.Queryable.Extensions
{
    public static class RelatedListChangeExtensions
    {
        public static RelatedListChange<TSource, T> FindMatchingChange<TSource, T>(this IEnumerable<RelatedListChange<TSource, T>> list, CompositeKey itemKey) where T : class where TSource : class
        {
            foreach (var otherChange in list)
            {
                if (itemKey.Matches(otherChange.ItemKey))
                {
                    return otherChange;
                }
            }
            return null;
        }
    }
}