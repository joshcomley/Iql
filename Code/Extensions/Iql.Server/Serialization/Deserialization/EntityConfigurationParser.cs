using Iql.DotNet.Serialization;
using Iql.Entities;
using Iql.Entities.DisplayFormatting;
using Iql.Entities.Enums;
using Iql.Entities.Geography;
using Iql.Entities.NestedSets;
using Iql.Entities.Relationships;
using Iql.Entities.Rules;
using Iql.Entities.Rules.Display;
using Iql.Entities.Rules.Relationship;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Iql.Server.Serialization
{
    public class EntityConfigurationParser
    {
        public static EntityConfigurationDocument FromJson(string json)
        {
            var settings = new JsonSerializerSettings();
            var interfaceTypeResolver = new InterfaceConverter();
            settings.Converters.Add(interfaceTypeResolver);
            var lambdaExpressionConverter = new LambdaExpressionConverter();
            settings.Converters.Add(lambdaExpressionConverter);
            settings.Converters.Add(new MetadataCollectionConverter());
            var configurationDocument = JsonConvert.DeserializeObject<EntityConfigurationDocument>(json, settings);
            settings.Converters.Add(new TypeConverter());
            interfaceTypeResolver.Finalise(configurationDocument);
            lambdaExpressionConverter.Finalise(configurationDocument);
            return configurationDocument;
        }
    }
}