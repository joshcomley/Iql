using System.Collections;
using System.Reflection;

namespace Iql.Queryable.Data.DataStores
{
    public class ObjectMerger
    {
        public static void Merge(object localEntity, object remoteEntity)
        {
            if (remoteEntity != null)
            {
                foreach (var property in remoteEntity.GetType().GetRuntimeProperties())
                {
                    var localValue = property.GetValue(localEntity);
                    var isCollection = localValue is IEnumerable && !(localValue is string);
                    if (Equals(localValue, null) || !isCollection)
                    {
                        property.SetValue(localEntity,
                            property.GetValue(remoteEntity));
                    }
                }
            }
        }
    }
}