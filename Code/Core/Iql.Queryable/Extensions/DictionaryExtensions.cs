using System;
using System.Collections.Generic;

namespace Iql.Queryable.Extensions
{
    public static class DictionaryExtensions
    {
        public static TValue Ensure<TKey, TValue>(
            this Dictionary<TKey, TValue> dictionary, 
            TKey key,
            Func<TValue> instantiator)
        {
            if (dictionary.ContainsKey(key))
            {
                return dictionary[key];
            }

            var value = instantiator();
            dictionary.Add(key, value);
            return value;
        }
    }
}