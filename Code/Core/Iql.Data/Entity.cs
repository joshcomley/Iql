using System.Collections.Generic;
using Iql.Data.Context;
using Iql.Entities;
using Iql.Extensions;

namespace Iql.Data
{
    public class Entity
    {
        public static int FindIndexOfEntityInSetByEntity<TEntity>(
            IDataContext dataContext,
            TEntity clone,
            IList<TEntity> data) where TEntity : class
        {
            if (clone == null)
            {
                return -1;
            }
            return FindIndexOfEntityByKey(data, dataContext.EntityConfigurationContext.GetEntity<TEntity>().GetCompositeKey(clone));
        }

        public static int FindIndexOfEntityByKey<TEntity>(
            IList<TEntity> data, 
            CompositeKey compositeKey) where TEntity : class
        {
            if (compositeKey == null)
            {
                return -1;
            }
            //if (key == null)
            //{
            //    throw new Exception($"No key has been defined for entity \"{typeof(TEntity).Name}\"");
            //}

            for (var i = 0; i < data.Count; i++)
            {
                var source = data[i];
                var match = true;
                for (var j = 0; j < compositeKey.Keys.Length; j++)
                {
                    if (!Equals(source.GetPropertyValueByName(compositeKey.Keys[j].Name),
                        compositeKey.Keys[j].Value))
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