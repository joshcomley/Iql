#if !TypeScript
using Iql.Entities.NestedSets;
using Iql.Server.Serialization;
using Iql.Tests.Context;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Iql.Entities;
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
            clientRelationship.AllowInlineEditing = true;
            clientRelationship.IsInferredWith(_ => _.CreatedByUser.Client);
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
            clientConfig.Metadata.Set("abc", 123);
            // clientConfig.FindRelationshipByName().FindPropertyByExpression(c => c.Type).Relationship.ThisEnd.inf
            Assert.AreEqual(ContentAlignment.Horizontal, (clientConfig.PropertyOrder[1] as IPropertyCollection).ContentAlignment);
            var json = db.EntityConfigurationContext.ToJson();
            var document = EntityConfigurationDocument.FromJson(json);
            var clientContentParsed = document.EntityTypes.Single(et => et.Name == nameof(Client));
            var sitesContentParsed = document.EntityTypes.Single(et => et.Name == nameof(Site));
            var clientRelationshipParsed = sitesContentParsed.Relationships.First(r => r.Constraints.Any(c => c.SourceKeyProperty.Name == nameof(Site.ClientId)));
            Assert.IsNotNull(clientRelationshipParsed.Source);
            Assert.IsNotNull(clientRelationshipParsed.Target);
            Assert.AreEqual(123L, clientContentParsed.Metadata.Get("abc"));
            var averageIncomeProperty = clientConfig.FindProperty(nameof(Client.AverageIncome));
            var averageSalesProperty = clientConfig.FindProperty(nameof(Client.AverageSales));
            Assert.AreEqual(averageIncomeProperty.NestedSet.NestedSet, clientConfig.NestedSets.First());
            Assert.AreEqual(averageIncomeProperty.NestedSet.Kind, NestedSetPropertyKind.Left);
            Assert.AreEqual(averageSalesProperty.NestedSet.NestedSet, clientConfig.NestedSets.First());
            Assert.AreEqual(averageSalesProperty.NestedSet.Kind, NestedSetPropertyKind.Right);
            clientConfig.Geographics.Clear();
            clientConfig.NestedSets.Clear();
        }
    }
}
#endif