using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tunnel.App.Data.Entities;

namespace Iql.Tests.Tests
{
    [TestClass]
    public class ChangeQueueTests : TestsBase
    {
        [TestMethod]
        public async Task TestAddingNestedItem2()
        {
            var clientType2 = new ClientType
            {
                //                Id = 3,
                Name = "Another",
            };
            clientType2.Clients.AddRange(new[]
            {
                new Client {
                    //                  Id = 2,
                    Name = "Client 2"}
            });
            Db.ClientTypes.Add(clientType2);
            await Db.SaveChangesAsync();
        }
        [TestMethod]
        public async Task TestAddingNestedItem()
        {
            var clientType1 = new ClientType
            {
                //Id = 2,
                Name = "Something else",
            };
            var client1 = new Client
            {
                //Id = 1,
                Name = "Client 1"
            };
            clientType1.Clients.Add(client1);
            Db.ClientTypes.Add(clientType1);
            Assert.AreEqual(clientType1, client1.Type);
            Assert.AreEqual(clientType1.Id, client1.TypeId);
            //await Db.SaveChanges();
        }
    }
}