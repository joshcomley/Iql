using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Brandless.ObjectSerializer;
using Iql.Conversion;
using Iql.DotNet;
using Iql.Entities;
using Iql.Entities.Metadata;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using TypeSharp;
using TypeSharp.Conversion;

namespace Iql.Server.ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            IqlExpressionConversion.DefaultExpressionConverter = () => new DotNetExpressionConverter();
            var builder = new EntityConfigurationBuilder();
            var somethingType = builder.EntityType<Something>();
            somethingType.DefineProperty(_ => _.Name)
                .SetHint(KnownHints.BigText);
            somethingType.Name = "Hello";
            var json = somethingType.ToJson<IEntityMetadata>().Dump();
//            var obj = json.FromJson<EntityConfiguration<Something>>();
//            var s = new CSharpObjectSerializer();
//            var cSharp = s.Serialize(obj);
//            var cSharpClass = @"using Iql.Entities.Metadata;
//using Iql.Entities.Relationships;
//using Iql.Entities;
//using Iql.Server.ConsoleApp;
//" + cSharp.Class;
//            var settings = new DefaultConversionSettings
//            {
//                Compile = false,
//                GenerateImports = true,
//                WriteToDisk = false,
//                ResolveCircularDependencies = false,
//            };
//            settings.MetadataReferences.AddReference<Something>();
//            settings.MetadataReferences.AddReference<IEntityMetadata>();
//            settings.MetadataReferences.AddReference<HelpText>();
//            (await CSharpToTypescriptConverter.ConvertToTypeScript(cSharpClass, settings)).Dump();

        }
    }
    //class DTOJsonConverter : Newtonsoft.Json.JsonConverter
    //{
    //    private static readonly string ISCALAR_FULLNAME = typeof(Interfaces.IScalar).FullName;
    //    private static readonly string IENTITY_FULLNAME = typeof(Interfaces.IEntity).FullName;


    //    public override bool CanConvert(Type objectType)
    //    {
    //        if (objectType.FullName == ISCALAR_FULLNAME
    //            || objectType.FullName == IENTITY_FULLNAME)
    //        {
    //            return true;
    //        }
    //        return false;
    //    }

    //    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    //    {
    //        if (objectType == typeof(IProperty))
    //            return serializer.Deserialize(reader, typeof(Proper));
    //        else if (objectType.FullName == IENTITY_FULLNAME)
    //            return serializer.Deserialize(reader, typeof(DTO.ClientEntity));

    //        throw new NotSupportedException(string.Format("Type {0} unexpected.", objectType));
    //    }

    //    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    //    {
    //        serializer.Serialize(writer, value);
    //    }
    //}
    public static class StringExtensions
    {
        public static string Dump(this string str)
        {
            Console.WriteLine(str);
            return str;
        }
    }
    // Define other methods and classes here
    public interface ISomething
    {
        int Number { get; set; }
    }

    public class Something : ISomething
    {
        public string Name { get; set; }
        public int Number { get; set; }
    }

    public static class SerializationExtensions
    {
        public static string ToXml<T>(this T toSerialize)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(toSerialize.GetType());

            using (StringWriter textWriter = new StringWriter())
            {
                xmlSerializer.Serialize(textWriter, toSerialize);
                return textWriter.ToString();
            }
        }

        public static T FromJson<T>(this string json)
        {
            var settings = new JsonSerializerSettings();
            return (T)JsonConvert.DeserializeObject(json, typeof(T));
        }

        public static string ToJson<T>(this T entity)
        {
            var indented = Newtonsoft.Json.Formatting.Indented;
            var settings = new JsonSerializerSettings()
            {
                ContractResolver = new InterfaceContractResolver()
            };
            var serialized = JsonConvert.SerializeObject(entity, typeof(T), indented, settings);
            return serialized;
        }

        class InterfaceContractResolver : DefaultContractResolver
        {
            public InterfaceContractResolver()
            {
            }

            protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
            {
                if (typeof(IEntityConfiguration).IsAssignableFrom(type))
                {
                    return base.CreateProperties(typeof(IEntityMetadata), memberSerialization);
                }
                if (typeof(IProperty).IsAssignableFrom(type))
                {
                    return base.CreateProperties(typeof(IPropertyMetadata), memberSerialization);
                }
                //IList<JsonProperty> properties = base.CreateProperties(type, memberSerialization);
                return base.CreateProperties(type, memberSerialization);
            }
        }
    }
}
