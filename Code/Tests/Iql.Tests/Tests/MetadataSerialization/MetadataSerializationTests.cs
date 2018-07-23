#if !TypeScript
using Iql.Entities.NestedSets;
using Iql.Server.Serialization;
using Iql.Tests.Context;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
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
            clientConfig
                .HasGeographic(c => c.AverageIncome, c => c.AverageIncome, "MyGeographic");
            clientConfig
                .HasNestedSet(c => c.AverageIncome, c => c.AverageSales, setKey: "MyNestedSet");
            clientConfig.SetPropertyOrder(
                c => c.FindProperty(nameof(Client.Name)),
                c => c.PropertyCollection(
                    c1 => c1.FindProperty(nameof(Client.Id)), 
                    c1 => c1.Geographics[0], 
                    c1 => c1.PropertyCollection(
                        c2 => c2.FindProperty(nameof(Client.Description)),
                        c2 => c2.FindProperty(nameof(Client.Category)))),
                c => c.NestedSets[0]);
            var json = db.EntityConfigurationContext.ToJson();
            var document = EntityConfigurationDocument.FromJson(json);
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