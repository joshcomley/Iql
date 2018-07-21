#if !TypeScript
using Iql.Server.Serialization;
using Iql.Tests.Context;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            db.EntityConfigurationContext.EntityType<Client>()
                .HasGeographic(c => c.AverageIncome, c => c.AverageIncome, "MyGeographic");
            var json = db.EntityConfigurationContext.ToJson();
            var document = EntityConfigurationDocument.FromJson(json);
            db.EntityConfigurationContext.EntityType<Client>().Geographics.Clear();
        }
    }
}
#endif