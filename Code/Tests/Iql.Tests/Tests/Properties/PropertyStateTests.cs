using System.Threading.Tasks;
using Iql.Tests.Context;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IqlSampleApp.Data.Entities;

namespace Iql.Tests.Tests.Properties
{
    [TestClass]
    public class PropertyStateTests : TestsBase
    {
        [TestMethod]
        public async Task PauseEventManagerTest()
        {
            AppDbContext.InMemoryDb.Clients.Add(new Client
            {
                Id = 1,
                Name = "Some client"
            });
            var clients = await Db.Clients.ToListAsync();
            for (var i = 0; i < clients.Count; i++)
            {
                var client = clients[i];
                var propertyStates = Db.GetEntityState(client).PropertyStates;
                for (var j = 0; j < propertyStates.Length; j++)
                {
                    var propertyState = propertyStates[j];
                    // Fetching this forces it to load, thus checking for any errors.
                    // No errors on this test means pass
                    var siblings = propertyState.SiblingStates;
                }
            }
        }

        [TestMethod]
        public async Task UnchangedLocalValueShouldMatchRemoteValue()
        {
            var person = new Person();
            Assert.AreEqual(false, person.IsComplete);
            Db.People.Add(person);
            var state = Db.GetEntityState(person);
            var propertyState = state.GetPropertyState(nameof(Person.IsComplete));
            Assert.AreEqual(false, propertyState.LocalValue);
            Assert.AreEqual(propertyState.RemoteValue, propertyState.LocalValue);
        }
    }
}