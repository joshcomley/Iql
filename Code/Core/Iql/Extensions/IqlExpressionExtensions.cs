#if TypeScript
using Iql.Serialization;
#endif
using Newtonsoft.Json;

namespace Iql.Extensions
{
    public static class IqlExpressionExtensions
    {
        public static TIql CloneIql<TIql>(this TIql iql)
            where TIql : IqlExpression
        {
            return (TIql) iql.Clone();
//#if !TypeScript
//            JsonSerializerSettings jss = new JsonSerializerSettings();
//            jss.TypeNameHandling = TypeNameHandling.All;
//            var json = JsonConvert.SerializeObject(iql, null, jss);
//            var clone = JsonConvert.DeserializeObject<TIql>(json, jss);
//            return clone;
//#else
//            var json = JsonConvert.SerializeObject(iql);
//            return IqlJsonDeserializer.DeserializeJson<TIql>(json);
//#endif
        }

        public static IqlPropertyExpression TryGetPropertyExpression(this IqlExpression expression)
        {
            if (expression is IqlLambdaExpression)
            {
                return (expression as IqlLambdaExpression).Body as IqlPropertyExpression;
            }
            return expression as IqlPropertyExpression;
        }
    }
}