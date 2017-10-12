using TypeSharp.Extensions;

namespace Iql.Queryable.Data.DataStores
{
    [DoNotConvert]
    public class ObjectNullifier
    {
        public static void ClearProperty(object entity, string property)
        {
            entity.SetPropertyValue(property, null);
        }
    }
}