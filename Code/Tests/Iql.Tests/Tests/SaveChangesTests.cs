using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Iql.Data.Crud.Operations;
using Iql.Data.Crud.Operations.Queued;
using Iql.Tests.Context;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tunnel.App.Data.Entities;

namespace Iql.Tests.Tests
{
    [TestClass]
    public class SaveChangesTests : TestsBase
    {
        [TestMethod]
        public async Task AddingAnEntityWithANewChildEntityShouldPersistRelationshipKeys()
        {
            var db1 = new AppDbContext();

            var clienType = new ClientType();
            var client = new Client();
            client.Type = clienType;
            client.Name = "My client";

            db1.Clients.Add(client);
            var result = await db1.SaveChangesAsync();

            Assert.AreEqual(true, result.Success);
            Assert.AreEqual(1, AppDbContext.InMemoryDb.Clients[0].TypeId);
        }

        [TestMethod]
        public async Task ChangeSinglePropertyAndRevertAndChangeAgainAndSave()
        {
            // Set up
            AppDbContext.InMemoryDb.Clients.Add(new Client { Id = 1, Name = "Test", TypeId = 1});
            AppDbContext.InMemoryDb.Clients.Add(new Client { Id = 2, Name = "Test 2", TypeId = 1 });

            var clients = await Db.Clients.ToListAsync();
            var client1 = clients.Single(c => c.Id == 1);
            List<IQueuedOperation> queue;
            void AssertQueue(string newName)
            {
                queue = Db.DataStore.GetChanges().ToList();
                Assert.AreEqual(1, queue.Count);
                var update = queue[0] as QueuedUpdateEntityOperation<Client>;
                Assert.IsNotNull(update);
                Assert.AreEqual(client1, update.Operation.Entity);
                var propertyChanges = update.Operation.EntityState.GetChangedProperties();
                Assert.AreEqual(1, propertyChanges.Length);
                var propertyChange = propertyChanges[0];
                Assert.AreEqual(nameof(Client.Name), propertyChange.Property.Name);
                Assert.AreEqual("Test", propertyChange.OldValue);
                Assert.AreEqual(newName, propertyChange.NewValue);
            }

            void AssertQueueEmpty()
            {
                queue = Db.DataStore.GetChanges().ToList();
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

            var saveChangesResult = await Db.SaveChangesAsync();

            AssertQueueEmpty();

            var dbClient = AppDbContext.InMemoryDb.Clients.Single(c => c.Id == client1.Id);
            Assert.AreEqual(dbClient.Name, "A new name 2");
        }

        [TestMethod]
        public async Task ChangeTwoPropertiesAndRevertOneAndAndSave()
        {
            // Set up
            AppDbContext.InMemoryDb.Clients.Add(new Client { Id = 1, Name = "Test", TypeId = 1 });
            AppDbContext.InMemoryDb.Clients.Add(new Client { Id = 2, Name = "Test 2", TypeId = 1 });

            var clients = await Db.Clients.ToListAsync();
            var client1 = clients.Single(c => c.Id == 1);

            void AssertDescriptionOnlyQueued(string newDescription)
            {
                var queue = Db.DataStore.GetChanges().ToList();
                Assert.AreEqual(1, queue.Count);
                var update = queue[0] as QueuedUpdateEntityOperation<Client>;
                Assert.IsNotNull(update);
                Assert.AreEqual(client1, update.Operation.Entity);
                var propertyChanges = update.Operation.EntityState.GetChangedProperties();
                Assert.AreEqual(1, propertyChanges.Length);
                var propertyChange = propertyChanges[0];
                Assert.AreEqual(nameof(Client.Description), propertyChange.Property.Name);
                Assert.AreEqual(null, propertyChange.OldValue);
                Assert.AreEqual(newDescription, propertyChange.NewValue);
            }

            void AssertBothChangesQueued(string newName, string newDescription)
            {
                var queue = Db.DataStore.GetChanges().ToList();
                Assert.AreEqual(1, queue.Count);
                var update = queue[0] as QueuedUpdateEntityOperation<Client>;
                Assert.IsNotNull(update);
                Assert.AreEqual(client1, update.Operation.Entity);
                var propertyChanges = update.Operation.EntityState.GetChangedProperties();
                Assert.AreEqual(2, propertyChanges.Length);

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

            await Db.SaveChangesAsync();

            AssertQueueEmpty();

            var dbClient = AppDbContext.InMemoryDb.Clients.Single(c => c.Id == client1.Id);
            Assert.AreEqual(dbClient.Name, "A new name 2");
            Assert.AreEqual(dbClient.Description, "A new description");
        }

        public void AssertQueueEmpty()
        {
            var queue = Db.DataStore.GetChanges().ToList();
            Assert.AreEqual(0, queue.Count);
        }

        [TestMethod]
        public async Task ChangeTwoUnrelatedEntitiesAndSave()
        {
            // Set up
            AppDbContext.InMemoryDb.Clients.Add(new Client { Id = 1, Name = "Client 1", TypeId = 1 });
            AppDbContext.InMemoryDb.Clients.Add(new Client { Id = 2, Name = "Client 2", TypeId = 1 });
            AppDbContext.InMemoryDb.Sites.Add(new Site { Id = 1, Name = "Site 1" });
            AppDbContext.InMemoryDb.Sites.Add(new Site { Id = 2, Name = "Site 2" });

            var client1 = await Db.Clients.GetWithKeyAsync(1);
            var sites = await Db.Sites.ToListAsync();
            var site1 = sites.Single(s => s.Id == 1);

            site1.Name = "Site 1 - changed";
            client1.Name = "Client 1 - changed";

            var queue = Db.DataStore.GetChanges().ToList();
            Assert.AreEqual(2, queue.Count);

            var siteChange = queue.Single(
                    q =>
                        q.Operation is UpdateEntityOperation<Site> &&
                        (q.Operation as UpdateEntityOperation<Site>).Entity == site1)
                        .Operation
                as UpdateEntityOperation<Site>;
            Assert.AreEqual(1, siteChange.EntityState.GetChangedProperties().Length);
            Assert.AreEqual(nameof(Site.Name), siteChange.EntityState.GetChangedProperties()[0].Property.Name);

            var clientChange = queue.Single(
                    q =>
                        q.Operation is UpdateEntityOperation<Client> &&
                        (q.Operation as UpdateEntityOperation<Client>).Entity == client1)
                    .Operation
                as UpdateEntityOperation<Client>;
            Assert.AreEqual(1, clientChange.EntityState.GetChangedProperties().Length);
            Assert.AreEqual(nameof(Client.Name), clientChange.EntityState.GetChangedProperties()[0].Property.Name);

            await Db.SaveChangesAsync();

            AssertQueueEmpty();

            Assert.AreEqual("Site 1 - changed", AppDbContext.InMemoryDb.Sites.Single(s => s.Id == site1.Id).Name);
            Assert.AreEqual("Client 1 - changed", AppDbContext.InMemoryDb.Clients.Single(s => s.Id == client1.Id).Name);
        }


        [TestMethod]
        public async Task ChangeExpandedEntityShouldOnlyProduceUpdateForThatEntity()
        {
            // Set up
            AppDbContext.InMemoryDb.Clients.Add(new Client { Id = 1, Name = "Client 1" });
            AppDbContext.InMemoryDb.Clients.Add(new Client { Id = 2, Name = "Client 2" });
            AppDbContext.InMemoryDb.Sites.Add(new Site { Id = 1, Name = "Site 1", ClientId = 1 });
            AppDbContext.InMemoryDb.Sites.Add(new Site { Id = 2, Name = "Site 2", ClientId = 2 });

            var site = await Db.Sites.Expand(s => s.Client).GetWithKeyAsync(1);

            site.Client.Name = "Client 1 - changed";

            void AssertNameOnlyQueued(string newDescription)
            {
                var queue = Db.DataStore.GetChanges().ToList();
                Assert.AreEqual(1, queue.Count);
                var update = queue[0] as QueuedUpdateEntityOperation<Client>;
                Assert.IsNotNull(update);
                Assert.AreEqual(site.Client, update.Operation.Entity);
                var propertyChanges = update.Operation.EntityState.GetChangedProperties();
                Assert.AreEqual(1, propertyChanges.Length);
                var propertyChange = propertyChanges[0];
                Assert.AreEqual(nameof(Client.Name), propertyChange.Property.Name);
                Assert.AreEqual("Client 1", propertyChange.OldValue);
                Assert.AreEqual(newDescription, propertyChange.NewValue);
            }

            AssertNameOnlyQueued("Client 1 - changed");
        }


        [TestMethod]
        public async Task ChangeRelationship()
        {
            // Set up
            AppDbContext.InMemoryDb.Clients.Add(new Client { Id = 1, Name = "Client 1" });
            AppDbContext.InMemoryDb.Clients.Add(new Client { Id = 2, Name = "Client 2" });
            AppDbContext.InMemoryDb.Sites.Add(new Site { Id = 1, Name = "Site 1", ClientId = 1 });
            AppDbContext.InMemoryDb.Sites.Add(new Site { Id = 2, Name = "Site 2", ClientId = 2 });

            var site = await Db.Sites.Expand(s => s.Client).GetWithKeyAsync(1);
            var propertyReferenceState = Db.DataStore.Tracking.TrackingSet<Site>()
                .GetEntityState(site)
                .GetPropertyState(nameof(Site.Client));
            var client1 = site.Client;
            Assert.AreEqual(client1, propertyReferenceState.OldValue);
            var client2 = await Db.Clients.GetWithKeyAsync(2);

            site.Client = client2;

            void AssertQueue()
            {
                var queue = Db.DataStore.GetChanges().ToList();
                Assert.AreEqual(1, queue.Count);
                var update = queue[0] as QueuedUpdateEntityOperation<Site>;
                Assert.IsNotNull(update);
                Assert.AreEqual(site, update.Operation.Entity);

                var propertyChanges = update.Operation.EntityState.GetChangedProperties();
                Assert.AreEqual(2, propertyChanges.Length);
                var propertyKeyChange = propertyChanges.First(p => p.Property.Name == nameof(Site.ClientId));//.Single(p => p.Property.Name == nameof(Site.ClientId));
                var propertyReferenceChange = propertyChanges.First(p => p.Property.Name == nameof(Site.Client));//.Single(p => p.Property.Name == nameof(Site.ClientId));

                Assert.AreEqual(nameof(Site.ClientId), propertyKeyChange.Property.Name);
                Assert.AreEqual(client1.Id, propertyKeyChange.OldValue);
                Assert.AreEqual(client2.Id, propertyKeyChange.NewValue);
                Assert.AreEqual(nameof(Site.Client), propertyReferenceChange.Property.Name);
                Assert.AreEqual(client1, propertyReferenceChange.OldValue);
                Assert.AreEqual(client2, propertyReferenceChange.NewValue);
            }

            AssertQueue();
        }
        // Test inserts
        // Test direct deletions
        // Test floating entities are not inserted
    }
}