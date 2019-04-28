using Iql.Extensions;
using Newtonsoft.Json;

namespace Iql.Serialization
{
    public static class IqlJsonDeserializationExtensions
    {
        public static bool ClaimsToBeIql(this object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (Equals(obj.GetPropertyValueByName(nameof(IqlExpression.IsIqlExpression)), true))
            {
                return true;
            }
            return false;
        }

        public static IqlExpression EnsureIsIql(this IqlExpression expression)
        {
            var instance = new IqlJsonDeserializerInstance<IqlExpression>(JsonConvert.SerializeObject(expression));
            return instance.Deserialize();
        }
    }
}