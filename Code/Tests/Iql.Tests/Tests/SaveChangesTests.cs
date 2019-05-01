﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Iql.Data.Context;
using Iql.Data.Crud.Operations;
using Iql.Data.Crud.Operations.Queued;
using Iql.Tests.Context;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IqlSampleApp.Data.Entities;

namespace Iql.Tests.Tests
{
    [TestClass]
    public class SaveChangesTests : TestsBase
    {
        [TestMethod]
        public async Task CrudEventsTest()
        {
            var db = new AppDbContext();
            var changesSavedAsyncCount = 0;
            var entityChangesSavedAsyncCount = 0;
            var entityAddSavedAsyncCount = 0;
            var entityUpdateSavedAsyncCount = 0;
            var entityDeleteSavedAsyncCount = 0;
            var changesSavingStartedAsyncCount = 0;
            var entityChangesSavingStartedAsyncCount = 0;
            var entityAddSavingStartedAsyncCount = 0;
            var entityUpdateSavingStartedAsyncCount = 0;
            var entityDeleteSavingStartedAsyncCount = 0;
            var changesSavingCompletedAsyncCount = 0;
            var entityChangesSavingCompletedAsyncCount = 0;
            var entityAddSavingCompletedAsyncCount = 0;
            var entityUpdateSavingCompletedAsyncCount = 0;
            var entityDeleteSavingCompletedAsyncCount = 0;

            db.Events.ContextEvents.SavedAsync.SubscribeAsync(async _ => { changesSavedAsyncCount++; });
            db.Events.ContextEvents.SavingStartedAsync.SubscribeAsync(async _ => { changesSavingStartedAsyncCount++; });
            db.Events.ContextEvents.SavingCompletedAsync.SubscribeAsync(async _ => { changesSavingCompletedAsyncCount++; });
            db.Events.EntityEvents.SavedAsync.SubscribeAsync(async _ => { entityChangesSavedAsyncCount++; });
            db.Events.EntityEvents.SavingStartedAsync.SubscribeAsync(async _ => { entityChangesSavingStartedAsyncCount++; });
            db.Events.EntityEvents.SavingCompletedAsync.SubscribeAsync(async _ => { entityChangesSavingCompletedAsyncCount++; });
            db.Events.AddEvents.SavedAsync.SubscribeAsync(async _ => { entityAddSavedAsyncCount++; });
            db.Events.AddEvents.SavingStartedAsync.SubscribeAsync(async _ => { entityAddSavingStartedAsyncCount++; });
            db.Events.AddEvents.SavingCompletedAsync.SubscribeAsync(async _ => { entityAddSavingCompletedAsyncCount++; });
            db.Events.UpdateEvents.SavedAsync.SubscribeAsync(async _ => { entityUpdateSavedAsyncCount++; });
            db.Events.UpdateEvents.SavingStartedAsync.SubscribeAsync(async _ => { entityUpdateSavingStartedAsyncCount++; });
            db.Events.UpdateEvents.SavingCompletedAsync.SubscribeAsync(async _ => { entityUpdateSavingCompletedAsyncCount++; });
            db.Events.DeleteEvents.SavedAsync.SubscribeAsync(async _ => { entityDeleteSavedAsyncCount++; });
            db.Events.DeleteEvents.SavingStartedAsync.SubscribeAsync(async _ => { entityDeleteSavingStartedAsyncCount++; });
            db.Events.DeleteEvents.SavingCompletedAsync.SubscribeAsync(async _ => { entityDeleteSavingCompletedAsyncCount++; });

            var changesSavedCount = 0;
            var entityChangesSavedCount = 0;
            var entityAddSavedCount = 0;
            var entityUpdateSavedCount = 0;
            var entityDeleteSavedCount = 0;
            var changesSavingStartedCount = 0;
            var entityChangesSavingStartedCount = 0;
            var entityAddSavingStartedCount = 0;
            var entityUpdateSavingStartedCount = 0;
            var entityDeleteSavingStartedCount = 0;
            var changesSavingCompletedCount = 0;
            var entityChangesSavingCompletedCount = 0;
            var entityAddSavingCompletedCount = 0;
            var entityUpdateSavingCompletedCount = 0;
            var entityDeleteSavingCompletedCount = 0;

            db.Events.ContextEvents.Saved.Subscribe(_ => { changesSavedCount++; });
            db.Events.ContextEvents.SavingStarted.Subscribe(_ => { changesSavingStartedCount++; });
            db.Events.ContextEvents.SavingCompleted.Subscribe(_ => { changesSavingCompletedCount++; });
            db.Events.EntityEvents.Saved.Subscribe(_ => { entityChangesSavedCount++; });
            db.Events.EntityEvents.SavingStarted.Subscribe(_ => { entityChangesSavingStartedCount++; });
            db.Events.EntityEvents.SavingCompleted.Subscribe(_ => { entityChangesSavingCompletedCount++; });
            db.Events.AddEvents.Saved.Subscribe(_ => { entityAddSavedCount++; });
            db.Events.AddEvents.SavingStarted.Subscribe(_ => { entityAddSavingStartedCount++; });
            db.Events.AddEvents.SavingCompleted.Subscribe(_ => { entityAddSavingCompletedCount++; });
            db.Events.UpdateEvents.Saved.Subscribe(_ => { entityUpdateSavedCount++; });
            db.Events.UpdateEvents.SavingStarted.Subscribe(_ => { entityUpdateSavingStartedCount++; });
            db.Events.UpdateEvents.SavingCompleted.Subscribe(_ => { entityUpdateSavingCompletedCount++; });
            db.Events.DeleteEvents.Saved.Subscribe(_ => { entityDeleteSavedCount++; });
            db.Events.DeleteEvents.SavingStarted.Subscribe(_ => { entityDeleteSavingStartedCount++; });
            db.Events.DeleteEvents.SavingCompleted.Subscribe(_ => { entityDeleteSavingCompletedCount++; });

            var clientType = new ClientType();
            var client = new Client();
            client.Type = clientType;
            client.Name = "My client";

            db.Clients.Add(client);

            var result = await db.SaveChangesAsync();

            Assert.AreEqual(true, result.Success);

            Assert.AreEqual(1, changesSavingStartedAsyncCount);
            Assert.AreEqual(1, changesSavingCompletedAsyncCount);
            Assert.AreEqual(1, changesSavedAsyncCount);

            Assert.AreEqual(1, changesSavingStartedCount);
            Assert.AreEqual(1, changesSavingCompletedCount);
            Assert.AreEqual(1, changesSavedCount);

            Assert.AreEqual(2, entityChangesSavingStartedAsyncCount);
            Assert.AreEqual(2, entityChangesSavingCompletedAsyncCount);
            Assert.AreEqual(2, entityChangesSavedAsyncCount);

            Assert.AreEqual(2, entityChangesSavingStartedCount);
            Assert.AreEqual(2, entityChangesSavingCompletedCount);
            Assert.AreEqual(2, entityChangesSavedCount);

            Assert.AreEqual(2, entityAddSavingStartedAsyncCount);
            Assert.AreEqual(2, entityAddSavingCompletedAsyncCount);
            Assert.AreEqual(2, entityAddSavedAsyncCount);

            Assert.AreEqual(2, entityAddSavingStartedCount);
            Assert.AreEqual(2, entityAddSavingCompletedCount);
            Assert.AreEqual(2, entityAddSavedCount);

            Assert.AreEqual(0, entityUpdateSavingStartedAsyncCount);
            Assert.AreEqual(0, entityUpdateSavingCompletedAsyncCount);
            Assert.AreEqual(0, entityUpdateSavedAsyncCount);

            Assert.AreEqual(0, entityUpdateSavingStartedCount);
            Assert.AreEqual(0, entityUpdateSavingCompletedCount);
            Assert.AreEqual(0, entityUpdateSavedCount);

            Assert.AreEqual(0, entityDeleteSavingStartedAsyncCount);
            Assert.AreEqual(0, entityDeleteSavingCompletedAsyncCount);
            Assert.AreEqual(0, entityDeleteSavedAsyncCount);

            Assert.AreEqual(0, entityDeleteSavingStartedCount);
            Assert.AreEqual(0, entityDeleteSavingCompletedCount);
            Assert.AreEqual(0, entityDeleteSavedCount);

            client.Name = "New name";

            result = await db.SaveChangesAsync();

            Assert.AreEqual(true, result.Success);

            Assert.AreEqual(2, changesSavingStartedAsyncCount);
            Assert.AreEqual(2, changesSavingCompletedAsyncCount);
            Assert.AreEqual(2, changesSavedAsyncCount);

            Assert.AreEqual(2, changesSavingStartedCount);
            Assert.AreEqual(2, changesSavingCompletedCount);
            Assert.AreEqual(2, changesSavedCount);

            Assert.AreEqual(3, entityChangesSavingStartedAsyncCount);
            Assert.AreEqual(3, entityChangesSavingCompletedAsyncCount);
            Assert.AreEqual(3, entityChangesSavedAsyncCount);

            Assert.AreEqual(3, entityChangesSavingStartedCount);
            Assert.AreEqual(3, entityChangesSavingCompletedCount);
            Assert.AreEqual(3, entityChangesSavedCount);

            Assert.AreEqual(2, entityAddSavingStartedAsyncCount);
            Assert.AreEqual(2, entityAddSavingCompletedAsyncCount);
            Assert.AreEqual(2, entityAddSavedAsyncCount);

            Assert.AreEqual(2, entityAddSavingStartedCount);
            Assert.AreEqual(2, entityAddSavingCompletedCount);
            Assert.AreEqual(2, entityAddSavedCount);

            Assert.AreEqual(1, entityUpdateSavingStartedAsyncCount);
            Assert.AreEqual(1, entityUpdateSavingCompletedAsyncCount);
            Assert.AreEqual(1, entityUpdateSavedAsyncCount);

            Assert.AreEqual(1, entityUpdateSavingStartedCount);
            Assert.AreEqual(1, entityUpdateSavingCompletedCount);
            Assert.AreEqual(1, entityUpdateSavedCount);

            Assert.AreEqual(0, entityDeleteSavingStartedAsyncCount);
            Assert.AreEqual(0, entityDeleteSavingCompletedAsyncCount);
            Assert.AreEqual(0, entityDeleteSavedAsyncCount);

            Assert.AreEqual(0, entityDeleteSavingStartedCount);
            Assert.AreEqual(0, entityDeleteSavingCompletedCount);
            Assert.AreEqual(0, entityDeleteSavedCount);

            db.DeleteEntity(client);

            result = await db.SaveChangesAsync();

            Assert.AreEqual(true, result.Success);

            Assert.AreEqual(3, changesSavingStartedAsyncCount);
            Assert.AreEqual(3, changesSavingCompletedAsyncCount);
            Assert.AreEqual(3, changesSavedAsyncCount);

            Assert.AreEqual(3, changesSavingStartedCount);
            Assert.AreEqual(3, changesSavingCompletedCount);
            Assert.AreEqual(3, changesSavedCount);

            Assert.AreEqual(4, entityChangesSavingStartedAsyncCount);
            Assert.AreEqual(4, entityChangesSavingCompletedAsyncCount);
            Assert.AreEqual(4, entityChangesSavedAsyncCount);

            Assert.AreEqual(4, entityChangesSavingStartedCount);
            Assert.AreEqual(4, entityChangesSavingCompletedCount);
            Assert.AreEqual(4, entityChangesSavedCount);

            Assert.AreEqual(2, entityAddSavingStartedAsyncCount);
            Assert.AreEqual(2, entityAddSavingCompletedAsyncCount);
            Assert.AreEqual(2, entityAddSavedAsyncCount);

            Assert.AreEqual(2, entityAddSavingStartedCount);
            Assert.AreEqual(2, entityAddSavingCompletedCount);
            Assert.AreEqual(2, entityAddSavedCount);

            Assert.AreEqual(1, entityUpdateSavingStartedAsyncCount);
            Assert.AreEqual(1, entityUpdateSavingCompletedAsyncCount);
            Assert.AreEqual(1, entityUpdateSavedAsyncCount);

            Assert.AreEqual(1, entityUpdateSavingStartedCount);
            Assert.AreEqual(1, entityUpdateSavingCompletedCount);
            Assert.AreEqual(1, entityUpdateSavedCount);

            Assert.AreEqual(1, entityDeleteSavingStartedAsyncCount);
            Assert.AreEqual(1, entityDeleteSavingCompletedAsyncCount);
            Assert.AreEqual(1, entityDeleteSavedAsyncCount);

            Assert.AreEqual(1, entityDeleteSavingStartedCount);
            Assert.AreEqual(1, entityDeleteSavingCompletedCount);
            Assert.AreEqual(1, entityDeleteSavedCount);
        }

        [TestMethod]
        public async Task UpdateSelectedPropertiesOnly()
        {
            AppDbContext.InMemoryDb.Clients.Add(new Client { Id = 1, Name = "Test", Discount = 60, TypeId = 1 });

            var dbClient = AppDbContext.InMemoryDb.Clients.Single(c => c.Id == 1);

            var clients = await Db.Clients.ToListAsync();
            var client1 = clients.Single(c => c.Id == 1);

            var nameProperty = Db.EntityConfigurationContext.EntityType<Client>().FindProperty(nameof(Client.Name));

            client1.Name = "Test Changed";
            client1.Discount = 50;

            var result = await Db.SaveChangesAsync(null, new[] { nameProperty });
            Assert.AreEqual(true, result.Success);
            Assert.AreEqual(dbClient.Name, "Test Changed");
            Assert.AreEqual(dbClient.Discount, 60);

            var discountProperty = Db.EntityConfigurationContext.EntityType<Client>().FindProperty(nameof(Client.Discount));

            client1.Name = "Test Changed Again";
            client1.Discount = 40;

            result = await Db.SaveChangesAsync(null, new[] { discountProperty });
            Assert.AreEqual(true, result.Success);
            Assert.AreEqual(dbClient.Name, "Test Changed");
            Assert.AreEqual(dbClient.Discount, 40);
        }

        [TestMethod]
        public async Task AddingAnEntityWithANewChildEntityShouldPersistRelationshipKeys()
        {
            var db1 = new AppDbContext();

            var clientType = new ClientType();
            var client = new Client();
            client.Type = clientType;
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
            AppDbContext.InMemoryDb.Clients.Add(new Client { Id = 1, Name = "Test", TypeId = 1 });
            AppDbContext.InMemoryDb.Clients.Add(new Client { Id = 2, Name = "Test 2", TypeId = 1 });

            var clients = await Db.Clients.ToListAsync();
            var client1 = clients.Single(c => c.Id == 1);
            IqlDataChanges queue;
            void AssertQueue(string newName)
            {
                queue = Db.GetChanges();
                Assert.AreEqual(1, queue.Count);
                var update = queue.AllChanges[0] as QueuedUpdateEntityOperation<Client>;
                Assert.IsNotNull(update);
                Assert.AreEqual(client1, update.Operation.Entity);
                var propertyChanges = update.Operation.EntityState.GetChangedProperties();
                Assert.AreEqual(1, propertyChanges.Length);
                var propertyChange = propertyChanges[0];
                Assert.AreEqual(nameof(Client.Name), propertyChange.Property.Name);
                Assert.AreEqual("Test", propertyChange.RemoteValue);
                Assert.AreEqual(newName, propertyChange.LocalValue);
            }

            void AssertQueueEmpty()
            {
                queue = Db.GetChanges();
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
                var queue = Db.GetChanges();
                Assert.AreEqual(1, queue.Count);
                var update = queue.AllChanges[0] as QueuedUpdateEntityOperation<Client>;
                Assert.IsNotNull(update);
                Assert.AreEqual(client1, update.Operation.Entity);
                var propertyChanges = update.Operation.EntityState.GetChangedProperties();
                Assert.AreEqual(1, propertyChanges.Length);
                var propertyChange = propertyChanges[0];
                Assert.AreEqual(nameof(Client.Description), propertyChange.Property.Name);
                Assert.AreEqual(null, propertyChange.RemoteValue);
                Assert.AreEqual(newDescription, propertyChange.LocalValue);
            }

            void AssertBothChangesQueued(string newName, string newDescription)
            {
                var queue = Db.GetChanges();
                Assert.AreEqual(1, queue.Count);
                var update = queue.AllChanges[0] as QueuedUpdateEntityOperation<Client>;
                Assert.IsNotNull(update);
                Assert.AreEqual(client1, update.Operation.Entity);
                var propertyChanges = update.Operation.EntityState.GetChangedProperties();
                Assert.AreEqual(2, propertyChanges.Length);

                var nameChange = propertyChanges.Single(p => p.Property.Name == nameof(Client.Name));
                Assert.AreEqual("Test", nameChange.RemoteValue);
                Assert.AreEqual(newName, nameChange.LocalValue);

                var descriptionChange = propertyChanges.Single(p => p.Property.Name == nameof(Client.Description));
                Assert.AreEqual(null, descriptionChange.RemoteValue);
                Assert.AreEqual(newDescription, descriptionChange.LocalValue);
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
            var queue = Db.GetChanges();
            Assert.AreEqual(0, queue.Count);
        }

        [TestMethod]
        public async Task ChangeTwoUnrelatedEntitiesAndSave()
        {
            // Set up
            AppDbContext.InMemoryDb.Clients.Add(new Client { Id = 1, Name = "Client 1", TypeId = 1 });
            AppDbContext.InMemoryDb.Clients.Add(new Client { Id = 2, Name = "Client 2", TypeId = 1 });
            AppDbContext.InMemoryDb.Sites.Add(new Site { Id = 1, Name = "Site 1", ClientId = 1 });
            AppDbContext.InMemoryDb.Sites.Add(new Site { Id = 2, Name = "Site 2", ClientId = 2 });

            var client1 = await Db.Clients.GetWithKeyAsync(1);
            var sites = await Db.Sites.ToListAsync();
            var site1 = sites.Single(s => s.Id == 1);

            site1.Name = "Site 1 - changed";
            client1.Name = "Client 1 - changed";

            var queue = Db.GetChanges();
            Assert.AreEqual(2, queue.Count);

            var siteChange = queue.AllChanges.Single(
                    q =>
                        q.Operation is UpdateEntityOperation<Site> &&
                        (q.Operation as UpdateEntityOperation<Site>).Entity == site1)
                        .Operation
                as UpdateEntityOperation<Site>;
            Assert.AreEqual(1, siteChange.EntityState.GetChangedProperties().Length);
            Assert.AreEqual(nameof(Site.Name), siteChange.EntityState.GetChangedProperties()[0].Property.Name);

            var clientChange = queue.AllChanges.Single(
                    q =>
                        q.Operation is UpdateEntityOperation<Client> &&
                        (q.Operation as UpdateEntityOperation<Client>).Entity == client1)
                    .Operation
                as UpdateEntityOperation<Client>;
            Assert.AreEqual(1, clientChange.EntityState.GetChangedProperties().Length);
            Assert.AreEqual(nameof(Client.Name), clientChange.EntityState.GetChangedProperties()[0].Property.Name);

            var result = await Db.SaveChangesAsync();

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
                var queue = Db.GetChanges();
                Assert.AreEqual(1, queue.Count);
                var update = queue.AllChanges[0] as QueuedUpdateEntityOperation<Client>;
                Assert.IsNotNull(update);
                Assert.AreEqual(site.Client, update.Operation.Entity);
                var propertyChanges = update.Operation.EntityState.GetChangedProperties();
                Assert.AreEqual(1, propertyChanges.Length);
                var propertyChange = propertyChanges[0];
                Assert.AreEqual(nameof(Client.Name), propertyChange.Property.Name);
                Assert.AreEqual("Client 1", propertyChange.RemoteValue);
                Assert.AreEqual(newDescription, propertyChange.LocalValue);
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
            var propertyReferenceState = Db
                .TemporalDataTracker
                .TrackingSet<Site>()
                .FindMatchingEntityState(site)
                .GetPropertyState(nameof(Site.Client));
            var client1 = site.Client;
            Assert.AreEqual(client1, propertyReferenceState.RemoteValue);
            var client2 = await Db.Clients.GetWithKeyAsync(2);

            site.Client = client2;

            void AssertQueue()
            {
                var queue = Db.GetChanges();
                Assert.AreEqual(1, queue.Count);
                var update = queue.AllChanges[0] as QueuedUpdateEntityOperation<Site>;
                Assert.IsNotNull(update);
                Assert.AreEqual(site, update.Operation.Entity);

                var propertyChanges = update.Operation.EntityState.GetChangedProperties();
                Assert.AreEqual(1, propertyChanges.Length);
                var propertyKeyChange = propertyChanges.First(p => p.Property.Name == nameof(Site.ClientId));//.Single(p => p.Property.Name == nameof(Site.ClientId));
                //var propertyReferenceChange = propertyChanges.First(p => p.Property.Name == nameof(Site.Client));//.Single(p => p.Property.Name == nameof(Site.ClientId));

                Assert.AreEqual(nameof(Site.ClientId), propertyKeyChange.Property.Name);
                Assert.AreEqual(client1.Id, propertyKeyChange.RemoteValue);
                Assert.AreEqual(client2.Id, propertyKeyChange.LocalValue);
                //Assert.AreEqual(nameof(Site.Client), propertyReferenceChange.Property.Name);
                //Assert.AreEqual(client1, propertyReferenceChange.RemoteValue);
                //Assert.AreEqual(client2, propertyReferenceChange.LocalValue);
            }

            AssertQueue();
        }
        // Test inserts
        // Test direct deletions
        // Test floating entities are not inserted
    }
}