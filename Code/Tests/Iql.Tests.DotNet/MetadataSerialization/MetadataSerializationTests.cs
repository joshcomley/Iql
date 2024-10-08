﻿#if !TypeScript && !CustomEvaluate
using System;
using Iql.Server.Serialization;
using Iql.Entities;
using Iql.Entities.NestedSets;
using Iql.Tests.Context;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Iql.Entities.PropertyGroups.Files;
using Iql.Forms;
using Iql.Server;
using IqlSampleApp.Data.Entities;
using Iql.Server.Serialization.Serialization;

namespace Iql.Tests.Tests.MetadataSerialization
{
    [TestClass]
    // [Ignore]
    public class MetadataSerializationTests
    {
        [TestMethod]
        public void TestSerializeDeserialize()
        {
            var db = new AppDbContext();
            var json2 =
                false
                    // For speedy debugging
                    ? new Result<string>(MetadataSerializationJsonCache.Json)
                    : db.EntityConfigurationContext.ToJson();
            Assert.IsNotNull(json2.Value, json2.Errors);
            return;
            var clientConfig = db.EntityConfigurationContext.EntityType<Client>();
            var sitesConfig = db.EntityConfigurationContext.EntityType<Site>();
            sitesConfig.FindRelationship(_ => _.Client)
                .CreateWithRelationshipValue(_ => _.Type, ctx => _ => ctx.Owner.Client.Type);
            sitesConfig.FindRelationship(_ => _.Client).CreateWithPropertyValue(_ => _.Name, _ => "test@123.com");
            var clientRelationship = sitesConfig.FindRelationship(r => r.Client);
            var sitesRelationship = clientConfig.FindCollectionRelationship(r => r.Sites);
            Assert.AreEqual(clientRelationship.OtherSide, sitesRelationship);
            Assert.AreEqual(clientRelationship, sitesRelationship.OtherSide);
            Assert.AreEqual(clientRelationship.Relationship, sitesRelationship.Relationship);
            Assert.AreNotEqual(clientRelationship, sitesRelationship);
            Assert.AreEqual(clientRelationship, clientRelationship.OtherSide.OtherSide);
            Assert.AreEqual(clientRelationship.Relationship, clientRelationship.OtherSide.Relationship);
            Assert.AreNotEqual(clientRelationship, clientRelationship.OtherSide);
            clientRelationship.Metadata.Set("NumberSeven", 7);
            clientRelationship.CanWrite = true;
            sitesConfig.FindPropertyByExpression(c => c.Client).IsInferredWith(_ => _.CurrentEntityState.CreatedByUser.Client);
            sitesRelationship.CanWrite = true;
            clientConfig
                .HasGeographic(c => c.AverageIncome, c => c.AverageIncome, "MyGeographic");
            clientConfig
                .HasNestedSet(c => c.AverageIncome, c => c.AverageSales, setKey: "MyNestedSet");
            clientConfig.Geographics[0].SetHint(FormHints.BigText);
            clientConfig.SetEditDisplay(
                (entityConfiguration, displayConfiguration) => displayConfiguration.SetProperties(
                    entityConfiguration,
                    c => c.FindProperty(nameof(Client.Name)),
                    c => c.PropertyCollection(
                            c1 => c1.PropertyPath(_ => _.Type.Name),
                            c1 => c1.FindProperty(nameof(Client.Id)),
                            c1 => c1.FindRelationship(c2 => c2.Type),
                            c1 => c1.Geographics[0],
                            c1 => c1.PropertyCollection(
                                c2 => c2.FindProperty(nameof(Client.Description)),
                                c2 => c2.FindProperty(nameof(Client.Category))))
                        .Configure(c3 =>
                        {
                            c3.SetHint(FormHints.HelpTextBottom);
                            c3.ContentAlignment = ContentAlignment.Horizontal;
                        }),
                    c => c.NestedSets[0]));
            clientConfig.HasFile(f => f.Description,
                new Guid("264781e3-2630-4a22-8f8e-9c861ead521d"),
                f =>
                {
                    f.MediaKey.AddGroup(g => g.AddPropertyPath(p => p.CreatedByUser.Id).AddString("root-mk-test"));
                    f.Key = "my-file";
                    f.AddPreview(
                        _ => _.Name,
                        new Guid("6b4ce757-2b64-4092-a537-f697dcea433f"),
                        IqlPreviewKind.Image,
                        200,
                        configure:
                        fp => fp.MediaKey.AddGroup(
                            g => g.AddPropertyPath(p => p.CreatedByUser.Id).AddString("sub-mk-test")
                        )
                    );
                });
            clientConfig.Metadata.Set("abc", 123);
            // clientConfig.FindRelationshipByName().FindPropertyByExpression(c => c.Type).Relationship.ThisEnd.inf
            Assert.AreEqual(ContentAlignment.Horizontal, (clientConfig.GetDisplayConfiguration(
                DisplayConfigurationKind.Edit,
                DisplayConfigurationKeys.Default).Properties[1] as IPropertyCollection).ContentAlignment);
            var json =
                false
                // For speedy debugging
                ? MetadataSerializationJsonCache.Json
                : db.EntityConfigurationContext.ToJson().Value;
            // return;
            var document = EntityConfigurationDocument.FromJson(json);
            var clientContentParsed = document.EntityTypes.Single(et => et.Name == nameof(Client));
            var siteContentParsed = document.EntityTypes.Single(et => et.Name == nameof(Site));
            var siteClientRelationship = siteContentParsed.FindRelationshipByName(nameof(Site.Client)).ThisEnd;
            var clientTypeRelationship = clientContentParsed.FindRelationshipByName(nameof(Client.Type)).ThisEnd;
            var valueMappings = siteClientRelationship.ValueMappings;
            Assert.AreEqual(1, valueMappings.Count);
            var valueMapping = valueMappings[0];
            var clientNameProperty = clientContentParsed.FindProperty(nameof(Site.Name));
            Assert.AreEqual(clientNameProperty, valueMapping.Property);
            Assert.IsNotNull(valueMapping.Expression);
            var relationshipMappings = siteClientRelationship.RelationshipMappings;
            Assert.AreEqual(1, relationshipMappings.Count);
            Assert.AreEqual(clientTypeRelationship, relationshipMappings[0].Property);
            var propertyPath = (clientContentParsed.GetDisplayConfiguration(
                DisplayConfigurationKind.Edit,
                DisplayConfigurationKeys.Default).Properties[1] as PropertyCollection).Properties[0];
            Assert.IsTrue(propertyPath is PropertyPath);
            Assert.AreEqual("Type/Name", (propertyPath as PropertyPath).Path);
            var file = clientContentParsed.Files[0];
            Assert.IsNotNull(file.MediaKey);
            Assert.AreEqual(1, file.Previews.Count);
            var filePreview = file.Previews[0];
            Assert.IsNotNull(filePreview.UrlProperty);
            Assert.IsNotNull(filePreview.MediaKey);
            Assert.AreEqual("sub-mk-test", filePreview.MediaKey.Groups[0].Parts[1].Key);
            var sitesContentParsed = document.EntityTypes.Single(et => et.Name == nameof(Site));
            var clientRelationshipParsed = sitesContentParsed.Relationships.First(r => r.Constraints.Any(c => c.SourceKeyProperty.Name == nameof(Site.ClientId)));
            Assert.AreEqual(7, clientRelationship.Metadata.Get("NumberSeven"));
            Assert.IsNotNull(clientRelationshipParsed.Source);
            Assert.IsNotNull(clientRelationshipParsed.Target);
            Assert.AreEqual(123L, clientContentParsed.Metadata.Get("abc"));
            var averageIncomeProperty = clientConfig.FindProperty(nameof(Client.AverageIncome));
            var averageSalesProperty = clientConfig.FindProperty(nameof(Client.AverageSales));
            Assert.AreEqual(averageIncomeProperty.NestedSet, clientConfig.NestedSets.First());
            Assert.AreEqual(averageIncomeProperty.NestedSet.GetPropertyKind(averageIncomeProperty), NestedSetPropertyKind.Left);
            Assert.AreEqual(averageSalesProperty.NestedSet, clientConfig.NestedSets.First());
            Assert.AreEqual(averageSalesProperty.NestedSet.GetPropertyKind(averageSalesProperty), NestedSetPropertyKind.Right);
            clientConfig.Geographics.Clear();
            clientConfig.NestedSets.Clear();
        }
    }
}
#endif