using System.Collections.Generic;

namespace Iql.Queryable
{
    static class ExtendedListExtensions
    {
        internal static void Initialize<T>(this List<T> list, IEnumerable<T> source) where T : class
        {
            if (source != null)
            {
#if TypeScript
                if (source.GetType() == typeof(int))
                {
                    var count = (int)(object)source;
                    for (var i = 0; i < count; i++)
                    {
                        list.Add(null);
                    }
                }
                else
                {
#endif
                list.AddRange(source);
#if TypeScript
                }
#endif
            }
        }
    }
}