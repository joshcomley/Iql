using System.Collections.Generic;
using Iql.Extensions;
using Iql.Queryable.Data;
using Iql.Queryable.Data.EntityConfiguration;
using Iql.Queryable.Data.EntityConfiguration.Relationships;
using Iql.Queryable.Operations;

namespace Iql.Queryable.Extensions
{
    public static class CompositeKeyExtensions
    {
        public static int AsKeyStringCount = 0;
        public static string AsKeyString(this CompositeKey compositeKey, bool includeName = true)
        {
            AsKeyStringCount++;
            if (compositeKey.Keys.Length == 1)
            {
                return compositeKey.Keys[0].Value?.ToString() ?? "NULL";
            }
            if (compositeKey.Keys.Length == 2)
            {
                return (compositeKey.Keys[0].Value?.ToString() ?? "NULL") + ";"
                                                                          +
                                                                          (compositeKey.Keys[1].Value?.ToString() ??
                                                                           "NULL");
            }
            var str = "";
            foreach (var key in compositeKey.Keys)
            {
                if (includeName)
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