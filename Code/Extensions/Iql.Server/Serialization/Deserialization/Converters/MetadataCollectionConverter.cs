using Iql.Entities;
using Newtonsoft.Json;
using System;

namespace Iql.Server.Serialization
{
    public class MetadataCollectionConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var mcj = new MetadataCollectionJson();
            serializer.Populate(reader, mcj);
            var mc = new MetadataCollection();
            foreach (var item in mcj.All)
            {
                mc.Set(item.Key, item.Value);
            }
            return mc;
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(IMetadataCollection).IsAssignableFrom(objectType);
        }
    }
}