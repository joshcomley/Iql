using Newtonsoft.Json;

namespace Iql.Serialization
{
    public static class IqlJsonDeserializationExtensions
    {
        public static IqlExpression EnsureIsIql(this IqlExpression expression)
        {
            var instance = new IqlJsonDeserializerInstance<IqlExpression>(JsonConvert.SerializeObject(expression));
            return instance.Deserialize();
        }
    }
}