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
                    property.SetValue(localEntity,
                        property.GetValue(remoteEntity));
                }
            }
        }
    }
}