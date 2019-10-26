using System.Threading.Tasks;
using Iql.Data.Tracking;
using Iql.Entities;
using Iql.Tests.Context;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IqlSampleApp.Data.Entities;

namespace Iql.Tests.Tests.DataContextTests
{
    [TestClass]
    public class DataTrackerSnapshotTests : TestsBase
    {
        [TestMethod]
        public async Task TestNoChangesSnapshotShouldBeNull()
        {
            var id = 156187;
            var clientRemote = new Client
            {
                Name = "abc",
                Id = id
            };
            AppDbContext.InMemoryDb.Clients.Add(clientRemote);
            var entity1 = await Db.GetEntityAsync<Client>(156187);
            var snapshot = Db.AddSnapshot();
            Assert.IsNull(snapshot);
        }

        [TestMethod]
        public async Task TestSomeChangesSnapshotShouldNotBeNull()
        {
            var id = 156187;
            var clientRemote = new Client
            {
                Name = "abc",
                Id = id
            };
            AppDbContext.InMemoryDb.Clients.Add(clientRemote);
            var entity1 = await Db.GetEntityAsync<Client>(156187);
            entity1.Name = "def";
            var snapshot = Db.AddSnapshot();
            Assert.IsNotNull(snapshot);
        }

        [TestMethod]
        public async Task TestRevertSnapshotWithSimplePropertyChanges()
        {
            var id = 156187;
            var clientRemote = new Client
            {
                Name = "abc",
                Id = id
            };
            AppDbContext.InMemoryDb.Clients.Add(clientRemote);
            var entity1 = await Db.GetEntityAsync<Client>(156187);
            entity1.Name = "def";
            var snapshot = Db.AddSnapshot();
            Assert.AreEqual(snapshot, Db.CurrentSnapshot);
            entity1.Name = "ghi";
            Db.UndoChanges();
            Assert.AreEqual("def", entity1.Name);
            Db.UndoChanges();
            Assert.AreEqual("def", entity1.Name);
            Db.RemoveLastSnapshot(SnapshotRemoveKind.GoToPreSnapshotValues);
            Assert.AreEqual("abc", entity1.Name);
            Db.RestoreNextAbandonedSnapshot();
            Assert.AreEqual("def", entity1.Name);
        }

        [TestMethod]
        public async Task ShouldNotBeAbleToRestoreAbandonedSnapshotAfterChanges()
        {
            var id = 156187;
            var clientRemote = new Client
            {
                Name = "abc",
                Id = id
            };
            AppDbContext.InMemoryDb.Clients.Add(clientRemote);
            var entity1 = await Db.GetEntityAsync<Client>(156187);
            entity1.Name = "def";
            var snapshot = Db.AddSnapshot();
            Assert.AreEqual(snapshot, Db.CurrentSnapshot);
            entity1.Name = "ghi";
            Db.UndoChanges();
            Assert.AreEqual("def", entity1.Name);
            Db.UndoChanges();
            Assert.AreEqual("def", entity1.Name);
            Db.RemoveLastSnapshot(SnapshotRemoveKind.GoToPreSnapshotValues);
            Assert.AreEqual("abc", entity1.Name);
            entity1.Name = "xyz";
            Db.RestoreNextAbandonedSnapshot();
            Assert.AreEqual("xyz", entity1.Name);
        }

        [TestMethod]
        public async Task TestAddEntitySnapshot()
        {
            var entity1 = new Client
            {
                Name = "abc"
            };
            Db.Clients.Add(entity1);
            entity1.Name = "def";
            var snapshot1 = Db.AddSnapshot();

            var entity2 = new Client
            {
                Name = "abc"
            };
            Db.Clients.Add(entity2);
            entity2.Name = "def";
            var snapshot2 = Db.AddSnapshot();

            Assert.IsTrue(Db.IsTracked(entity2));

            Db.RemoveLastSnapshot(SnapshotRemoveKind.GoToPreSnapshotValues);

            Assert.IsFalse(Db.IsTracked(entity2));

            Db.RestoreNextAbandonedSnapshot();

            Assert.IsTrue(Db.IsTracked(entity2));
        }

        [TestMethod]
        public async Task TestDeleteEntitySnapshot()
        {
            var entity1 = new Client
            {
                Name = "abc"
            };
            Db.Clients.Add(entity1);

            var additions = Db.GetAdditions();
            Assert.AreEqual(1, additions.Length);

            entity1.Name = "def";
            var snapshot1 = Db.AddSnapshot();
            var state = Db.GetEntityState(entity1);

            Db.DeleteEntity(entity1);

            additions = Db.GetAdditions();
            Assert.AreEqual(0, additions.Length);

            var snapshot2 = Db.AddSnapshot();

            Assert.IsTrue(state.MarkedForAnyDeletion);

            Db.RemoveLastSnapshot(SnapshotRemoveKind.GoToPreSnapshotValues);

            additions = Db.GetAdditions();
            Assert.AreEqual(1, additions.Length);

            Assert.IsFalse(state.MarkedForAnyDeletion);

            Db.RestoreNextAbandonedSnapshot();

            additions = Db.GetAdditions();
            Assert.AreEqual(0, additions.Length);

            Assert.IsTrue(state.MarkedForAnyDeletion);
        }

        [TestMethod]
        public async Task TestUndoOnePropertyWithNoSnapshot()
        {
            AppDbContext.InMemoryDb.Clients.Add(new Client
            {
                Id = 1212,
                Name = "abc",
                Description = "def"
            });
            var client = await Db.Clients.GetWithKeyAsync(1212);
            client.Name = "123";
            client.Description = "456";
            Db.UndoChanges(null, new IProperty[] { Db.EntityConfigurationContext.EntityType<Client>().FindProperty(nameof(Client.Name)) });
            Assert.AreEqual("abc", client.Name);
            Assert.AreEqual("456", client.Description);
            Db.UndoChanges(null, new IProperty[] { Db.EntityConfigurationContext.EntityType<Client>().FindProperty(nameof(Client.Name)) });
            Assert.AreEqual("abc", client.Name);
            Assert.AreEqual("456", client.Description);
            Db.UndoChanges();
            Assert.AreEqual("abc", client.Name);
            Assert.AreEqual("def", client.Description);
        }

        [TestMethod]
        public async Task TestUndoOnePropertyWithSnapshot()
        {
            AppDbContext.InMemoryDb.Clients.Add(new Client
            {
                Id = 1212,
                Name = "abc",
                Description = "def"
            });
            var client = await Db.Clients.GetWithKeyAsync(1212);
            client.Name = "123";
            client.Description = "456";
            Db.AddSnapshot();
            client.Name = "foo";
            client.Description = "bar";
            Db.UndoChanges(null, new IProperty[] { Db.EntityConfigurationContext.EntityType<Client>().FindProperty(nameof(Client.Name)) });
            Assert.AreEqual("123", client.Name);
            Assert.AreEqual("bar", client.Description);
            Db.UndoChanges(null, new IProperty[] { Db.EntityConfigurationContext.EntityType<Client>().FindProperty(nameof(Client.Name)) });
            Assert.AreEqual("123", client.Name);
            Assert.AreEqual("bar", client.Description);
            Db.UndoChanges();
            Assert.AreEqual("123", client.Name);
            Assert.AreEqual("456", client.Description);
            Db.UndoChanges();
            Assert.AreEqual("123", client.Name);
            Assert.AreEqual("456", client.Description);
            Db.RemoveLastSnapshot(SnapshotRemoveKind.GoToPreSnapshotValues);
            Assert.AreEqual("abc", client.Name);
            Assert.AreEqual("def", client.Description);
        }

        [TestMethod]
        public async Task TestSaveChangesAndUndoChanges()
        {
            AppDbContext.InMemoryDb.Clients.Add(new Client
            {
                Id = 1212,
                Name = "abc",
                Description = "def"
            });
            var client = await Db.Clients.GetWithKeyAsync(1212);
            client.Name = "123";
            client.Description = "456";
            Db.AddSnapshot();
            client.Name = "foo";
            client.Description = "bar";
            Db.UndoChanges(null, new IProperty[] { Db.EntityConfigurationContext.EntityType<Client>().FindProperty(nameof(Client.Name)) });
            Assert.AreEqual("123", client.Name);
            Assert.AreEqual("bar", client.Description);
            Db.UndoChanges(null, new IProperty[] { Db.EntityConfigurationContext.EntityType<Client>().FindProperty(nameof(Client.Name)) });
            Assert.AreEqual("123", client.Name);
            Assert.AreEqual("bar", client.Description);
            Db.UndoChanges();
            Assert.AreEqual("123", client.Name);
            Assert.AreEqual("456", client.Description);
            Db.UndoChanges();
            Assert.AreEqual("123", client.Name);
            Assert.AreEqual("456", client.Description);
            Db.RemoveLastSnapshot(SnapshotRemoveKind.GoToPreSnapshotValues);
            Assert.AreEqual("abc", client.Name);
            Assert.AreEqual("def", client.Description);
        }

        [TestMethod]
        public async Task TestFetchWithExternalPropertyChangesShouldClearRestorableSnapshots()
        {
            var dbClient = new Client
            {
                Id = 1212,
                Name = "abc",
                Description = "def"
            };
            AppDbContext.InMemoryDb.Clients.Add(dbClient);
            var client = await Db.Clients.GetWithKeyAsync(1212);
            client.Name = "123";
            client.Description = "456";
            Db.AddSnapshot();
            client.Name = "foo";
            client.Description = "bar";
            Db.RemoveLastSnapshot(SnapshotRemoveKind.GoToPreSnapshotValues);
            Assert.AreEqual("abc", client.Name);
            Assert.AreEqual("def", client.Description);
            Assert.IsTrue(Db.HasRestorableSnapshot);
            var client2 = await Db.Clients.GetWithKeyAsync(1212);
            Assert.AreEqual(client, client2);
            Assert.IsTrue(Db.HasRestorableSnapshot);
            dbClient.Name = "new name";
            var client3 = await Db.Clients.GetWithKeyAsync(1212);
            Assert.AreEqual(client, client2);
            Assert.IsFalse(Db.HasRestorableSnapshot);
        }

        [TestMethod]
        public async Task TestFetchEntirelyNewEntityShouldNotClearRestorableSnapshots()
        {
            var dbClient = new Client
            {
                Id = 1212,
                Name = "abc",
                Description = "def"
            };
            var dbClient2 = new Client
            {
                Id = 1213,
                Name = "xxx",
                Description = "yyy"
            };
            AppDbContext.InMemoryDb.Clients.Add(dbClient);
            AppDbContext.InMemoryDb.Clients.Add(dbClient2);
            var client = await Db.Clients.GetWithKeyAsync(1212);
            client.Name = "123";
            client.Description = "456";
            Db.AddSnapshot();
            client.Name = "foo";
            client.Description = "bar";
            Db.RemoveLastSnapshot(SnapshotRemoveKind.GoToPreSnapshotValues);
            Assert.AreEqual("abc", client.Name);
            Assert.AreEqual("def", client.Description);
            Assert.IsTrue(Db.HasRestorableSnapshot);
            var client2 = await Db.Clients.GetWithKeyAsync(1212);
            Assert.AreEqual(client, client2);
            Assert.IsTrue(Db.HasRestorableSnapshot);
            var client2b = await Db.Clients.GetWithKeyAsync(1213);
            Assert.IsTrue(Db.HasRestorableSnapshot);
            dbClient.Name = "new name";
            var client3 = await Db.Clients.GetWithKeyAsync(1212);
            Assert.AreEqual(client, client2);
            Assert.IsFalse(Db.HasRestorableSnapshot);
        }

        [TestMethod]
        public async Task TestAddingNewEntityShouldClearRestorableSnapshots()
        {
            var dbClient = new Client
            {
                Id = 1212,
                Name = "abc",
                Description = "def"
            };
            AppDbContext.InMemoryDb.Clients.Add(dbClient);
            var client = await Db.Clients.GetWithKeyAsync(1212);
            client.Name = "123";
            client.Description = "456";
            Db.AddSnapshot();
            client.Name = "foo";
            client.Description = "bar";
            Db.RemoveLastSnapshot(SnapshotRemoveKind.GoToPreSnapshotValues);
            Assert.AreEqual("abc", client.Name);
            Assert.AreEqual("def", client.Description);
            Assert.IsTrue(Db.HasRestorableSnapshot);
            Db.Clients.Add(new Client());
            Assert.IsFalse(Db.HasRestorableSnapshot);
        }

        [TestMethod]
        public async Task TestDeletingEntityShouldClearRestorableSnapshots()
        {
            var dbClient = new Client
            {
                Id = 1212,
                Name = "abc",
                Description = "def"
            };
            AppDbContext.InMemoryDb.Clients.Add(dbClient);
            var client = await Db.Clients.GetWithKeyAsync(1212);
            client.Name = "123";
            client.Description = "456";
            Db.AddSnapshot();
            client.Name = "foo";
            client.Description = "bar";
            Db.RemoveLastSnapshot(SnapshotRemoveKind.GoToPreSnapshotValues);
            Assert.AreEqual("abc", client.Name);
            Assert.AreEqual("def", client.Description);
            Assert.IsTrue(Db.HasRestorableSnapshot);
            Db.Delete(client);
            Assert.IsFalse(Db.HasRestorableSnapshot);
        }

        [TestMethod]
        public async Task TestRestoringANewEntity()
        {
            var dbClient = new Client
            {
                Id = 1212,
                Name = "abc",
                Description = "def"
            };
            AppDbContext.InMemoryDb.Clients.Add(dbClient);
            var client = await Db.Clients.GetWithKeyAsync(1212);
            client.Name = "123";
            client.Description = "456";
            var clientType = new ClientType { Name = "Client type" };
            client.Type = clientType;
            Db.AddSnapshot();
            client.Name = "foo";
            client.Description = "bar";
            Db.RemoveLastSnapshot(SnapshotRemoveKind.GoToPreSnapshotValues);
            Assert.AreEqual("abc", client.Name);
            Assert.AreEqual("def", client.Description);
            Assert.AreEqual(null, client.Type);
            Assert.IsTrue(Db.HasRestorableSnapshot);
            Db.RestoreNextAbandonedSnapshot();
            Assert.AreEqual(clientType, client.Type);
            Assert.IsFalse(Db.HasRestorableSnapshot);
        }

        [TestMethod]
        public async Task TestRestoringANewEntity2()
        {
            var dbClient = new Client
            {
                Id = 1212,
                TypeId = 1313,
                Name = "abc",
                Description = "def"
            };
            AppDbContext.InMemoryDb.Clients.Add(dbClient);
            var dbClientType = new ClientType
            {
                Id = 1313,
                Name = "abc"
            };
            AppDbContext.InMemoryDb.ClientTypes.Add(dbClientType);
            var client = await Db.Clients.GetWithKeyAsync(1212);
            client.Name = "123";
            client.Description = "456";
            var oldTypeId = client.TypeId;
            var clientType = new ClientType { Name = "Client type" };
            client.Type = clientType;
            var firstSnapshot = Db.AddSnapshot();
            client.TypeId = oldTypeId;
            var secondSnapshot = Db.AddSnapshot();
        }

        // TODO: Test removing a new entity (aka not deleting)
        // TODO: Test restoring more than one abandoned snapshot
    }
}