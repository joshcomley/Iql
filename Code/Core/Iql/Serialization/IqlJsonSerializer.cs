using System.Collections.Generic;
using Newtonsoft.Json;

namespace Iql.Serialization
{
    public class IqlJsonSerializer
    {
        public static string Serialize(object obj, IqlJsonSerializerSettings settings = null)
        {
            var converters = new List<JsonConverter>();
            #if !TypeScript
            if (settings?.UseNumberConverter == true)
            {
                converters.Add(new NumberConverter());
            }
            #endif
            return JsonConvert.SerializeObject(obj, new JsonSerializerSettings
            {
                NullValueHandling = settings == null 
                    ? NullValueHandling.Include 
                    : NullValueHandling.Ignore,
                Converters = converters
            });
        }
    }
}