#if !TypeScript
using Iql.Entities;
using Iql.Entities.NestedSets;
using Iql.Server.Serialization;
using Iql.Tests.Context;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Iql.Entities.PropertyGroups.Files;
using Tunnel.App.Data.Entities;

namespace Iql.Tests.Tests.MetadataSerialization
{
    [TestClass]
    public class MetadataSerializationTests
    {
        [TestMethod]
        public void TestSerializeDeserialize()
        {
            // Test succeeds if no exception is thrown
            var db = new AppDbContext();
            var clientConfig = db.EntityConfigurationContext.EntityType<Client>();
            var sitesConfig = db.EntityConfigurationContext.EntityType<Site>();
            var clientRelationship = sitesConfig.FindRelationship(r => r.Client);
            clientRelationship.Metadata.Set("NumberSeven", 7);
            clientRelationship.AllowInlineEditing = true;
            sitesConfig.FindPropertyByExpression(c => c.Client).IsInferredWith(_ => _.CreatedByUser.Client);
            var sitesRelationship = clientConfig.FindCollectionRelationship(r => r.Sites);
            sitesRelationship.AllowInlineEditing = true;
            clientConfig
                .HasGeographic(c => c.AverageIncome, c => c.AverageIncome, "MyGeographic");
            clientConfig
                .HasNestedSet(c => c.AverageIncome, c => c.AverageSales, setKey: "MyNestedSet");
            clientConfig.Geographics[0].SetHint(KnownHints.BigText);
            clientConfig.SetPropertyOrder(
                c => c.FindProperty(nameof(Client.Name)),
                c => c.PropertyCollection(
                    c1 => c1.FindProperty(nameof(Client.Id)),
                    c1 => c1.FindRelationship(c2 => c2.Type),
                    c1 => c1.Geographics[0],
                    c1 => c1.PropertyCollection(
                        c2 => c2.FindProperty(nameof(Client.Description)),
                        c2 => c2.FindProperty(nameof(Client.Category))))
                    .Configure(c3 =>
                    {
                        c3.SetHint(KnownHints.HelpTextBottom);
                        c3.ContentAlignment = ContentAlignment.Horizontal;
                    }),
                c => c.NestedSets[0]);
            clientConfig.HasFile(f => f.Description,
                f =>
                {
                    f.MediaKey.AddGroup(g => g.AddPropertyPath(p => p.CreatedByUser.Id).AddString("root-mk-test"));
                    f.Key = "my-file";
                    f.AddPreview(
                        _ => _.Name,
                        200,
                        configure:
                        fp => fp.MediaKey.AddGroup(
                            g => g.AddPropertyPath(p => p.CreatedByUser.Id).AddString("sub-mk-test")
                        )
                    );
                });
            clientConfig.Metadata.Set("abc", 123);
            // clientConfig.FindRelationshipByName().FindPropertyByExpression(c => c.Type).Relationship.ThisEnd.inf
            Assert.AreEqual(ContentAlignment.Horizontal, (clientConfig.PropertyOrder[1] as IPropertyCollection).ContentAlignment);

            var json = db.EntityConfigurationContext.ToJson();

            var document = EntityConfigurationDocument.FromJson(json);
            var clientContentParsed = document.EntityTypes.Single(et => et.Name == nameof(Client));
            var file = clientContentParsed.Files[0];
            Assert.IsNotNull(file.MediaKey);
            Assert.AreEqual(1, file.Previews.Count);
            var filePreview = file.Previews[0];
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