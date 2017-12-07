using Iql.Tests.Context;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Iql.Tests.Tests
{
    public class TestsBase
    {
        protected static AppDbContext Db { get; set; } = new AppDbContext();

        [ClassInitialize]
        public static void SetUp(TestContext textContext)
        {
        }

        [ClassCleanup]
        public static void CleanUp()
        {

        }

        [TestCleanup]
        public void TestCleanUp()
        {
            AppDbContext.InMemoryDb.ClientTypes.Clear();
            AppDbContext.InMemoryDb.Clients.Clear();
            AppDbContext.InMemoryDb.Sites.Clear();
            AppDbContext.InMemoryDb.SiteInspections.Clear();
            AppDbContext.InMemoryDb.RiskAssessments.Clear();
            AppDbContext.InMemoryDb.RiskAssessmentSolutions.Clear();
            Db = new AppDbContext();
        }

        protected static ClientTypes AddClientTypes()
        {
            // Create two new client types
            var clientType1 = new ClientType
            {
                Id = 2,
                Name = "Something else",
            };
            clientType1.Clients.AddRange(new[]
            {
                new Client {Id = 1, Name = "Client 1"}
            });
            Db.ClientTypes.Add(clientType1);
            var clientType2 = new ClientType
            {
                Id = 3,
                Name = "Another",
            };
            clientType2.Clients.AddRange(new[]
            {
                new Client {Id = 2, Name = "Client 2"}
            });
            Db.ClientTypes.Add(clientType2);
            var clientType3 = new ClientType
            {
                Id = 41,
                Name = "Another",
            };
            Db.ClientTypes.Add(clientType3);
            var clientType4 = new ClientType
            {
                Id = 42,
                Name = "A fourth",
            };
            clientType4.Clients.AddRange(new[]
            {
                new Client {Id = 21, Name = "Client 21"}
            });
            Db.ClientTypes.Add(clientType4);
            var clientType5 = new ClientType
            {
                Id = 43,
                Name = "A fifth",
            };
            clientType5.Clients.AddRange(new[]
            {
                new Client {Id = 22, Name = "Client 22"},
                new Client {Id = 23, Name = "Client 23"}
            });
            Db.ClientTypes.Add(clientType5);
            return new ClientTypes(clientType1, clientType2, clientType3, clientType4, clientType5);
        }
    }
}