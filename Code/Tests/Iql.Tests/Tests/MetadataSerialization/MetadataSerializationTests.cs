#if !TypeScript
using Iql.Server.Serialization;
using Iql.Tests.Context;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
            var json = db.EntityConfigurationContext.ToJson();
            var document = EntityConfigurationDocument.FromJson(json);
        }
    }
}
#endif