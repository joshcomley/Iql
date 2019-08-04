using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;
using Iql.Entities;
using Iql.Entities.Rules.Display;
using Iql.Entities.Rules.Relationship;
using Iql.Extensions;
using Iql.Server.Serialization;

namespace Iql.OData.TypeScript.Generator.EntityConfiguration
{
    public class EntityConfigurationParser
    {
        public static EntityConfigurationDocument GetEntityConfigurations(string xml)
        {
            return null;
            //Dictionary<string, EntityConfiguration> entityConfigurations = new Dictionary<string, EntityConfiguration>();
            //var doc = XDocument.Parse(xml);
            ////var ns = doc.Root.GetDefaultNamespace();
            //var entityTypes = doc.Descendants().Where(d => d.Name.LocalName == "EntityType");
            //foreach (var entityType in entityTypes)
            //{
            //    var entityTypeName = entityType.Attribute("Name").Value;
            //    EntityConfiguration entityConfiguration = new EntityConfiguration();
            //    entityConfigurations.Add(entityTypeName, entityConfiguration);
            //    var configurationAnnotation = entityType.Elements().SingleOrDefault(e => e.Attributes().Any(a => a.Name.LocalName == "Term" && a.Value == "Iql.Configuration"));
            //    if (configurationAnnotation != null)
            //    {
            //        foreach (var elm in configurationAnnotation.DescendantsAndSelf())
            //        {
            //            elm.Name = elm.Name.LocalName;
            //        }
            //        var configuration = configurationAnnotation.ToString();//.DeserializeFromXml<Annotation>();
            //        try
            //        {
            //            var annotation = configuration.DeserializeFromXml<Annotation>();
            //            ToEntityConfiguration(entityConfiguration, annotation);
            //        }
            //        catch
            //        {
            //            Console.WriteLine("Failed to deserialize");
            //        }
            //    }

            //    //configurationAnnotation.Dump();
            //    var properties = entityType.Descendants().Where(d => d.Name.LocalName == "Property" || d.Name.LocalName == "NavigationProperty");
            //    foreach (var property in properties)
            //    {
            //        var propertyName = property.Attribute("Name").Value;
            //        var propertyConfigurationItem = new PropertyConfiguration();
            //        var propertyConfigurationAnnotation = property.Elements().SingleOrDefault(e => e.Attributes().Any(a => a.Name.LocalName == "Term" && a.Value == "Iql.Configuration"));
            //        if (propertyConfigurationAnnotation != null)
            //        {
            //            foreach (var elm in propertyConfigurationAnnotation.DescendantsAndSelf())
            //            {
            //                elm.Name = elm.Name.LocalName;
            //            }
            //            var propertyConfiguration = propertyConfigurationAnnotation.ToString();//.DeserializeFromXml<Annotation>();
            //            try
            //            {
            //                var annotation = propertyConfiguration.DeserializeFromXml<Annotation>();
            //                ToEntityConfiguration(propertyConfigurationItem, annotation);
            //            }
            //            catch
            //            {
            //                Console.WriteLine("Failed to deserialize");
            //            }
            //        }
            //        entityConfiguration.PropertyConfigurations.Add(propertyName, propertyConfigurationItem);
            //    }
            //}
            //return entityConfigurations;
        }

        //public static ConfigurationBase ToEntityConfiguration(ConfigurationBase configuration, Annotation annotation)
        //{
        //    var isEntity = configuration is EntityConfiguration;
        //    foreach (var coll in annotation.Collection)
        //    {
        //        switch (coll.Name)
        //        {
        //            case "HelpTexts":
        //                break;
        //            case "Rules":
        //                foreach (var rule in coll.Collection)
        //                {
        //                    Rule r = null;
        //                    switch (rule.Name)
        //                    {
        //                        case "ValidationRule":
        //                            var validationRule = new ValidationRule();
        //                            configuration.Validations.Add(validationRule);
        //                            r = validationRule;
        //                            break;
        //                        case "DisplayRule":
        //                            var displayRule = new DisplayRule();
        //                            displayRule.Kind = (DisplayRuleKind)rule.Collection.LabeledElement
        //                                .Single(c => c.Name == "Kind").Number;
        //                            displayRule.AppliesToKind = (DisplayRuleAppliesToKind)rule.Collection.LabeledElement
        //                                .Single(c => c.Name == "AppliesToKind").Number;
        //                            configuration.DisplayRules.Add(displayRule);
        //                            r = displayRule;
        //                            break;
        //                        case "RelationshipFilter":
        //                            var relationshipFilterRule = new RelationshipFilterRule();
        //                            configuration.RelationshipFilterRules.Add(relationshipFilterRule);
        //                            r = relationshipFilterRule;
        //                            break;
        //                    }
        //                    r.Iql = rule.Collection.LabeledElement.Single(c => c.Name == "Expression").String;
        //                    r.Message = rule.Collection.LabeledElement.Single(c => c.Name == "Message").String;
        //                    r.Key = rule.Collection.LabeledElement.Single(c => c.Name == "Key").String;
        //                }
        //                break;
        //            case "DisplayFormatters":
        //                foreach (var displayFormatterSource in coll.Collection)
        //                {
        //                    var displayFormatter = new DisplayFormatter();
        //                    displayFormatter.FormatterExpressionIql = displayFormatterSource.String;
        //                    displayFormatter.Name = displayFormatterSource.Name;
        //                    (configuration as EntityConfiguration).DisplayFormatters.Add(displayFormatter);
        //                }
        //                break;
        //            case "Metadata":
        //                IMetadata metadata;
        //                if (isEntity)
        //                {
        //                    var entityMetadata = new EntityMetadata();
        //                    (configuration as EntityConfiguration).Metadata = entityMetadata;
        //                    metadata = entityMetadata;
        //                }
        //                else
        //                {
        //                    var propertyMetadata = new PropertyMetadata();
        //                    (configuration as PropertyConfiguration).Metadata = propertyMetadata;
        //                    metadata = propertyMetadata;
        //                }
        //                var metadataType = metadata.GetType();
        //                foreach (var key in coll.Collection)
        //                {
        //                    var property = metadataType.GetProperty(key.Name);
        //                    object value = null;
        //                    if (property.PropertyType == typeof(string))
        //                    {
        //                        value = key.String;
        //                    }
        //                    if (property.PropertyType == typeof(bool) || property.PropertyType == typeof(bool?))
        //                    {
        //                        value = key.Bool;
        //                    }
        //                    if (property.PropertyType.IsEnum)
        //                    {
        //                        value = Enum.Parse(property.PropertyType, key.String);
        //                    }
        //                    if (property.PropertyType.IsEnumerableType())
        //                    {
        //                        var elementType = property.PropertyType.GetGenericArguments()[0];
        //                        if (elementType.IsClass && elementType != typeof(string))
        //                        {
        //                            var serializer = new XmlSerializer(property.PropertyType);
        //                            using (TextReader reader = new StringReader(key.String))
        //                            {
        //                                value = serializer.Deserialize(reader);
        //                            }
        //                        }
        //                        else
        //                        {
        //                            var list = new List<string>();
        //                            foreach (var item in key.Collection.String)
        //                            {
        //                                list.Add(item);
        //                            }
        //                            value = list;
        //                        }
        //                    }
        //                    metadata.SetPropertyValueByName(key.Name, value);
        //                }
        //                break;
        //        }
        //    }

        //    return configuration;
        //}
    }
}