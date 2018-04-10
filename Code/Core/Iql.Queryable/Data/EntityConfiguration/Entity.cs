using System;
using System.Collections.Generic;
using Iql.Queryable.Data.Context;
using Iql.Queryable.Extensions;

namespace Iql.Queryable.Data.EntityConfiguration
{
    public class Entity
    {
        public static int FindIndexOfEntityInSetByKey<TEntity>(
            IDataContext dataContext,
            TEntity clone,
            IList<TEntity> data) where TEntity : class
        {
            var key = dataContext.EntityConfigurationContext.GetEntity<TEntity>().Key;
            if (key == null)
            {
                throw new Exception($"No key has been defined for entity \"{typeof(TEntity).Name}\"");
            }
            for (var i = 0; i < data.Count; i++)
            {
                var source = data[i];
                var match = true;
                for (var j = 0; j < key.Properties.Length; j++)
                {
                    if (!Equals(source.GetPropertyValue(key.Properties[j]),
                        clone.GetPropertyValue(key.Properties[j])))
                    {
                        match = false;
                        break;
                    }
                }
                if (match)
                {
                    return i;
                }
            }
            return -1;
        }
    }
}