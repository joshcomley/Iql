using System;
using System.Collections.Generic;
using System.Linq;

namespace Iql.Entities.Extensions
{
    public static class IqlEntitiesListExtensions
    {
        public static IEnumerable<T> OrderByWithIndex<T, TKey>(this IEnumerable<T> collection, Func<T, int, TKey> keySelector)
        {
            if (collection == null)
            {
                return null;
            }
            var array = collection as T[] ?? collection.ToArray();
            var dic = new Dictionary<T, int>();
            for (var i = 0; i < array.Length; i++)
            {
                var item = array[i];
                dic.Add(item, i);
            }
            return collection.OrderBy(_ => keySelector(_, dic[_]));
        }
    }
}