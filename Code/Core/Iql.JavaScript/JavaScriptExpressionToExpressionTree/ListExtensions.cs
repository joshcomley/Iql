using System.Collections;
using System.Collections.Generic;

namespace Iql.JavaScript.JavaScriptExpressionToExpressionTree
{
    public static class ListExtensions
    {
        public static T Pop<T>(this IList<T> list)
        {
            var item = list[list.Count - 1];
            list.RemoveAt(list.Count - 1);
            return item;
        }

        public static T PopAs<T>(this IList list)
            where T : class
        {
            var item = list[list.Count - 1];
            list.RemoveAt(list.Count - 1);
            return item as T;
        }
    }
}