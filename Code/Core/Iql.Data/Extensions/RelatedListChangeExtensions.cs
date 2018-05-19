using System.Collections.Generic;
using Iql.Data.Configuration;
using Iql.Data.Lists;

namespace Iql.Data.Extensions
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