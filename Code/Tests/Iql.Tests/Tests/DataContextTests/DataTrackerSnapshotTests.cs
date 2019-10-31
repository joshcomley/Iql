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
            Assert.IsNotNull(snapshot);
            snapshot = Db.AddSnapshot(true);
            Assert.IsNull(snapshot);
        }

        [TestMethod]
        public async Task TestUndoChangesAfterSave()
        {
            var id1 = 156187;
            var clientRemote = new Client
            {
                Name = "abc",
                Id = id1,
                TypeId = 1
            };
            AppDbContext.InMemoryDb.Clients.Add(clientRemote);
            var id2 = 156188;
            var clientRemote2 = new Client
            {
                Name = "xyz",
                Id = id2,
                TypeId = 1
            };
            AppDbContext.InMemoryDb.Clients.Add(clientRemote2);
            var entity1 = await Db.GetEntityAsync<Client>(id1);
            var entity2 = await Db.GetEntityAsync<Client>(id2);
            var entity1PropertyState = Db.GetEntityState(entity1).GetPropertyState(nameof(Client.Name));
            var entity2PropertyState = Db.GetEntityState(entity2).GetPropertyState(nameof(Client.Name));
            Assert.IsFalse(entity1PropertyState.CanUndo);
            Assert.IsFalse(entity2PropertyState.CanUndo);
            entity1.Name = "def";
            entity2.Name = "123";
            Assert.IsTrue(entity1PropertyState.CanUndo);
            Assert.IsTrue(entity2PropertyState.CanUndo);
            Db.AbandonChanges();
            Assert.IsFalse(entity1PropertyState.CanUndo);
            Assert.IsFalse(entity2PropertyState.CanUndo);
            Assert.AreEqual("abc", entity1.Name);
            Assert.AreEqual("xyz", entity2.Name);
            entity1.Name = "def";
            entity2.Name = "123";
            Assert.IsTrue(entity1PropertyState.CanUndo);
            Assert.IsTrue(entity2PropertyState.CanUndo);
            Db.UndoChanges();
            Assert.AreEqual("abc", entity1.Name);
            Assert.AreEqual("xyz", entity2.Name);
            Assert.IsFalse(entity1PropertyState.CanUndo);
            Assert.IsFalse(entity2PropertyState.CanUndo);
            var snapshot = Db.AddSnapshot();
            Assert.IsFalse(entity1PropertyState.CanUndo);
            Assert.IsFalse(entity2PropertyState.CanUndo);
            entity1.Name = "def";
            entity2.Name = "123";
            Assert.IsTrue(entity1PropertyState.CanUndo);
            Assert.IsTrue(entity2PropertyState.CanUndo);
            Db.UndoChanges();
            Assert.IsFalse(entity1PropertyState.CanUndo);
            Assert.IsFalse(entity2PropertyState.CanUndo);
            Assert.AreEqual("abc", entity1.Name);
            Assert.AreEqual("xyz", entity2.Name);
            entity1.Name = "def";
            entity2.Name = "123";
            Assert.IsTrue(entity1PropertyState.CanUndo);
            Assert.IsTrue(entity2PropertyState.CanUndo);
            var result = await Db.SaveChangesAsync(new[] { entity1 });
            Assert.IsTrue(result.Success);
            Assert.IsFalse(entity1PropertyState.CanUndo);
            Assert.IsTrue(entity2PropertyState.CanUndo);
            Db.UndoChanges();
            Assert.IsFalse(entity1PropertyState.CanUndo);
            Assert.IsFalse(entity2PropertyState.CanUndo);
            Assert.AreEqual("def", entity1.Name);
            Assert.AreEqual("xyz", entity2.Name);
            Assert.IsNotNull(snapshot);
        }

        [TestMethod]
        public async Task TestReplaceSnapshotAfterSave()
        {
            var id1 = 156187;
            var clientRemote = new Client
            {
                Name = "abc",
                Id = id1,
                TypeId = 1
            };
            AppDbContext.InMemoryDb.Clients.Add(clientRemote);
            var id2 = 156188;
            var clientRemote2 = new Client
            {
                Name = "xyz",
                Id = id2,
                TypeId = 1
            };
            AppDbContext.InMemoryDb.Clients.Add(clientRemote2);
            var entity1 = await Db.GetEntityAsync<Client>(id1);
            var entity2 = await Db.GetEntityAsync<Client>(id2);
            entity1.Name = "def";
            entity2.Name = "123";
            var snapshot = Db.AddSnapshot();
            entity1.Name = "xxx";
            entity2.Name = "yyy";
            Db.UndoChanges();
            Assert.AreEqual(1, Db.SnapshotsCount);
            Assert.AreEqual("def", entity1.Name);
            Assert.AreEqual("123", entity2.Name);
            entity1.Name = "xxx";
            entity2.Name = "yyy";
            snapshot = Db.ReplaceLastSnapshot();
            Db.UndoChanges();
            Assert.AreEqual(1, Db.SnapshotsCount);
            Assert.AreEqual("xxx", entity1.Name);
            Assert.AreEqual("yyy", entity2.Name);
            Db.RemoveLastSnapshot();
            Assert.AreEqual(0, Db.SnapshotsCount);
            Assert.AreEqual("xxx", entity1.Name);
            Assert.AreEqual("yyy", entity2.Name);
        }

        [TestMethod]
        public async Task TestPropertyUndo()
        {
            var id1 = 156187;
            var clientRemote = new Client
            {
                Name = "abc",
                Id = id1,
                TypeId = 1
            };
            AppDbContext.InMemoryDb.Clients.Add(clientRemote);
            var id2 = 156188;
            var clientRemote2 = new Client
            {
                Name = "xyz",
                Id = id2,
                TypeId = 1
            };
            AppDbContext.InMemoryDb.Clients.Add(clientRemote2);
            var entity1 = await Db.GetEntityAsync<Client>(id1);
            var entity2 = await Db.GetEntityAsync<Client>(id2);
            entity1.Name = "def";
            entity2.Name = "123";
            var entity1State = Db.GetEntityState(entity1);
            var entity1PropertyState = entity1State.GetPropertyState(nameof(Client.Name));
            entity1PropertyState.UndoChange();
            Assert.AreEqual(0, Db.SnapshotsCount);
            Assert.AreEqual("abc", entity1.Name);
            Assert.AreEqual("123", entity2.Name);
            entity1.Name = "def";
            var snapshot = Db.AddSnapshot();
            entity1PropertyState.UndoChange();
            Assert.AreEqual(1, Db.SnapshotsCount);
            Assert.AreEqual("def", entity1.Name);
            Assert.AreEqual("123", entity2.Name);
            entity1PropertyState.UndoChange();
            Assert.AreEqual(1, Db.SnapshotsCount);
            Assert.AreEqual("def", entity1.Name);
            Assert.AreEqual("123", entity2.Name);
            entity1PropertyState.UndoChange();
            Assert.AreEqual(1, Db.SnapshotsCount);
            Assert.AreEqual("def", entity1.Name);
            Assert.AreEqual("123", entity2.Name);
            Db.RemoveLastSnapshot();
            Assert.AreEqual(0, Db.SnapshotsCount);
            Assert.AreEqual("def", entity1.Name);
            Assert.AreEqual("123", entity2.Name);
            entity1PropertyState.UndoChange();
            Assert.AreEqual(0, Db.SnapshotsCount);
            Assert.AreEqual("abc", entity1.Name);
            Assert.AreEqual("123", entity2.Name);
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
            var prop = Db.GetEntityState(client).GetPropertyState(nameof(Client.Type));
            client.Type = clientType;
            var snapshot = Db.AddSnapshot();
            Assert.AreEqual(3, snapshot.Values.Count);
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