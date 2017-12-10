using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Iql.Queryable.Data.Crud.Operations;
using Iql.Queryable.Data.Crud.Operations.Queued;
using Iql.Tests.Context;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Iql.Tests.Tests
{
    [TestClass]
    public class SaveChangesTests : TestsBase
    {
        [TestMethod]
        public async Task ChangeSinglePropertyAndRevertAndChangeAgainAndSave()
        {
            // Set up
            AppDbContext.InMemoryDb.Clients.Add(new Client { Id = 1, Name = "Test" });
            AppDbContext.InMemoryDb.Clients.Add(new Client { Id = 2, Name = "Test 2" });

            var clients = await Db.Clients.ToList();
            var client1 = clients.Single(c => c.Id == 1);
            List<IQueuedOperation> queue = null;
            void AssertQueue(string newName)
            {
                queue = Db.DataStore.GetQueue().ToList();
                Assert.AreEqual(1, queue.Count);
                var update = queue[0] as QueuedUpdateEntityOperation<Client>;
                Assert.IsNotNull(update);
                Assert.AreEqual(client1, update.Operation.Entity);
                var propertyChanges = update.Operation.EntityState.ChangedProperties;
                Assert.AreEqual(1, propertyChanges.Count);
                var propertyChange = propertyChanges[0];
                Assert.AreEqual(nameof(Client.Name), propertyChange.Property.Name);
                Assert.AreEqual("Test", propertyChange.OldValue);
                Assert.AreEqual(newName, propertyChange.NewValue);
            }

            void AssertQueueEmpty()
            {
                queue = Db.DataStore.GetQueue().ToList();
                Assert.AreEqual(0, queue.Count);
            }

            // Should be no changes so far
            AssertQueueEmpty();

            // Make a change
            client1.Name = "A new name";

            // Refresh the queue
            AssertQueue("A new name");

            // Reset the change
            client1.Name = "Test";

            // Should be no changes any more
            AssertQueueEmpty();

            // Change *back* again
            client1.Name = "A new name 2";

            // Should have one change now
            AssertQueue("A new name 2");

            await Db.SaveChanges();

            AssertQueueEmpty();

            var dbClient = AppDbContext.InMemoryDb.Clients.Single(c => c.Id == client1.Id);
            Assert.AreEqual(dbClient.Name, "A new name 2");
        }

        [TestMethod]
        public async Task ChangeTwoPropertiesAndRevertOneAndAndSave()
        {
            // Set up
            AppDbContext.InMemoryDb.Clients.Add(new Client { Id = 1, Name = "Test" });
            AppDbContext.InMemoryDb.Clients.Add(new Client { Id = 2, Name = "Test 2" });

            var clients = await Db.Clients.ToList();
            var client1 = clients.Single(c => c.Id == 1);

            void AssertDescriptionOnlyQueued(string newDescription)
            {
                var queue = Db.DataStore.GetQueue().ToList();
                Assert.AreEqual(1, queue.Count);
                var update = queue[0] as QueuedUpdateEntityOperation<Client>;
                Assert.IsNotNull(update);
                Assert.AreEqual(client1, update.Operation.Entity);
                var propertyChanges = update.Operation.EntityState.ChangedProperties;
                Assert.AreEqual(1, propertyChanges.Count);
                var propertyChange = propertyChanges[0];
                Assert.AreEqual(nameof(Client.Description), propertyChange.Property.Name);
                Assert.AreEqual(null, propertyChange.OldValue);
                Assert.AreEqual(newDescription, propertyChange.NewValue);
            }

            void AssertBothChangesQueued(string newName, string newDescription)
            {
                var queue = Db.DataStore.GetQueue().ToList();
                Assert.AreEqual(1, queue.Count);
                var update = queue[0] as QueuedUpdateEntityOperation<Client>;
                Assert.IsNotNull(update);
                Assert.AreEqual(client1, update.Operation.Entity);
                var propertyChanges = update.Operation.EntityState.ChangedProperties;
                Assert.AreEqual(2, propertyChanges.Count);

                var nameChange = propertyChanges.Single(p => p.Property.Name == nameof(Client.Name));
                Assert.AreEqual("Test", nameChange.OldValue);
                Assert.AreEqual(newName, nameChange.NewValue);

                var descriptionChange = propertyChanges.Single(p => p.Property.Name == nameof(Client.Description));
                Assert.AreEqual(null, descriptionChange.OldValue);
                Assert.AreEqual(newDescription, descriptionChange.NewValue);
            }

            // Should be no changes so far
            AssertQueueEmpty();

            // Make two changes
            client1.Name = "A new name";
            client1.Description = "A new description";

            // Refresh the queue
            AssertBothChangesQueued("A new name", "A new description");

            // Reset one of the changes
            client1.Name = "Test";

            // Should be no changes any more
            AssertDescriptionOnlyQueued("A new description");

            // Change *back* again
            client1.Name = "A new name 2";

            // Should have one change now
            AssertBothChangesQueued("A new name 2", "A new description");

            await Db.SaveChanges();

            AssertQueueEmpty();

            var dbClient = AppDbContext.InMemoryDb.Clients.Single(c => c.Id == client1.Id);
            Assert.AreEqual(dbClient.Name, "A new name 2");
            Assert.AreEqual(dbClient.Description, "A new description");
        }

        public void AssertQueueEmpty()
        {
            var queue = Db.DataStore.GetQueue().ToList();
            Assert.AreEqual(0, queue.Count);
        }

        [TestMethod]
        public async Task ChangeTwoUnrelatedEntitiesAndSave()
        {
            // Set up
            AppDbContext.InMemoryDb.Clients.Add(new Client { Id = 1, Name = "Client 1" });
            AppDbContext.InMemoryDb.Clients.Add(new Client { Id = 2, Name = "Client 2" });
            AppDbContext.InMemoryDb.Sites.Add(new Site { Id = 1, Name = "Site 1" });
            AppDbContext.InMemoryDb.Sites.Add(new Site { Id = 2, Name = "Site 2" });

            var client1 = await Db.Clients.WithKey(1);
            var sites = await Db.Sites.ToList();
            var site1 = sites.Single(s => s.Id == 1);

            site1.Name = "Site 1 - changed";
            client1.Name = "Client 1 - changed";

            var queue = Db.DataStore.GetQueue().ToList();
            Assert.AreEqual(2, queue.Count);

            var siteChange = queue.Single(
                    q =>
                        q.Operation is UpdateEntityOperation<Site> &&
                        (q.Operation as UpdateEntityOperation<Site>).Entity == site1)
                        .Operation
                as UpdateEntityOperation<Site>;
            Assert.AreEqual(1, siteChange.EntityState.ChangedProperties.Count);
            Assert.AreEqual(nameof(Site.Name), siteChange.EntityState.ChangedProperties[0].Property.Name);

            var clientChange = queue.Single(
                    q =>
                        q.Operation is UpdateEntityOperation<Client> &&
                        (q.Operation as UpdateEntityOperation<Client>).Entity == client1)
                    .Operation
                as UpdateEntityOperation<Client>;
            Assert.AreEqual(1, clientChange.EntityState.ChangedProperties.Count);
            Assert.AreEqual(nameof(Client.Name), clientChange.EntityState.ChangedProperties[0].Property.Name);

            await Db.SaveChanges();

            AssertQueueEmpty();

            Assert.AreEqual("Site 1 - changed", AppDbContext.InMemoryDb.Sites.Single(s => s.Id == site1.Id).Name);
            Assert.AreEqual("Client 1 - changed", AppDbContext.InMemoryDb.Clients.Single(s => s.Id == client1.Id).Name);
        }
    }
}