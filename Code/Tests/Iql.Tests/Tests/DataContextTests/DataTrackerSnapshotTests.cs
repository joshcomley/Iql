using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using Iql.Data.Tracking;
using Iql.Data.Tracking.State;
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
        public async Task TestDeleteNewEntity()
        {
            var id = 156187;
            var clientRemote = new Client
            {
                Name = "abc",
                Id = id
            };
            AppDbContext.InMemoryDb.Clients.Add(clientRemote);
            var client = new Client
            {
                Name = "abc"
            };
            Db.Clients.Add(client);
            var state = Db.GetEntityState(client);
            Assert.AreEqual(EntityStatus.New, state.Status);
            Assert.IsTrue(state.AttachedToTracker);
            Db.Clients.Delete(client);
            Assert.AreEqual(EntityStatus.NewAndDeleted, state.Status);
            Assert.IsTrue(state.AttachedToTracker);
            var remoteClient = await Db.Clients.GetWithKeyAsync(id);
            var remoteState = Db.GetEntityState(remoteClient);
            Assert.AreEqual(EntityStatus.Existing, remoteState.Status);
            Db.Clients.Delete(remoteClient);
            Assert.AreEqual(EntityStatus.ExistingAndPendingDelete, remoteState.Status);
            var result = await Db.SaveChangesAsync();
            Assert.IsTrue(result.Success);
            Assert.AreEqual(EntityStatus.ExistingAndDeleted, remoteState.Status);
        }

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
        public async Task TestRevertSnapshot()
        {
            var id1 = 156187;
            var clientRemote = new Client
            {
                Name = "abc",
                Id = id1,
                TypeId = 1
            };
            AppDbContext.InMemoryDb.Clients.Add(clientRemote);
            var entity1 = await Db.GetEntityAsync<Client>(id1);
            var snapshot = Db.AddSnapshot();
            Assert.IsFalse(Db.HasChangesSinceSnapshot);
            entity1.Name = "def";
            Assert.IsTrue(Db.HasChangesSinceSnapshot);
            Db.RevertToSnapshot();
            Assert.AreEqual("abc", entity1.Name);
            Assert.IsFalse(Db.HasChangesSinceSnapshot);
        }

        [TestMethod]
        public async Task TestRevertSnapshotWithNewEntity()
        {
            var id1 = 156187;
            var clientTypeRemote = new ClientType
            {
                Name = "abc",
                Id = id1
            };
            AppDbContext.InMemoryDb.ClientTypes.Add(clientTypeRemote);
            var entity1 = await Db.GetEntityAsync<ClientType>(id1);
            var snapshot = Db.AddSnapshot();
            var propertyState = Db.GetEntityState(entity1).GetPropertyState(nameof(ClientType.Clients));
            var newClient = new Client
            {
                Name = "New client",
                Description = "abc"
            };
            Assert.IsFalse(propertyState.HasChangedSinceSnapshot);
            Assert.IsFalse(Db.HasChangesSinceSnapshot);
            Db.Clients.Add(newClient);
            newClient.TypeId = id1;
            //            entity1.Clients.Add(newClient);
            Assert.IsTrue(propertyState.HasChangedSinceSnapshot);
            Assert.AreEqual(id1, newClient.TypeId);
            Assert.AreEqual(1, entity1.Clients.Count);
            Assert.IsTrue(Db.HasChangesSinceSnapshot);
            Db.RevertToSnapshot();
            Assert.IsFalse(propertyState.HasChangedSinceSnapshot);
            Assert.AreEqual(0, newClient.TypeId);
            Assert.AreEqual(0, entity1.Clients.Count);
            Assert.IsFalse(Db.HasChangesSinceSnapshot);
        }


        [TestMethod]
        public async Task TestUndoSinglePropertyFromSnapshot()
        {
            var id1 = 156187;
            var clientRemote = new Client
            {
                Name = "abc",
                Id = id1
            };
            AppDbContext.InMemoryDb.Clients.Add(clientRemote);
            var entity1 = await Db.GetEntityAsync<Client>(id1);
            var entityState = Db.GetEntityState(entity1);
            var snapshot = Db.AddSnapshot();
            Assert.IsFalse(Db.HasChangesSinceSnapshot);
            entity1.Name = "def";
            entity1.Description = "123";
            Assert.IsTrue(Db.HasChangesSinceSnapshot);
            Assert.IsTrue(Db.HasChanges);
            var nameProperty = Db.EntityConfigurationContext.EntityType<Client>().FindProperty(nameof(Client.Name));
            var descriptionPropertyState = entityState.GetPropertyState(nameof(Client.Description));
            Assert.IsTrue(descriptionPropertyState.HasChangedSinceSnapshot);
            Db.HasChangesSinceSnapshotChanged.Subscribe(_ =>
            {
                var a = 0;
            });
            Db.UndoChanges(new[] {entity1}, new[] {nameProperty});
            Assert.AreEqual("abc", entity1.Name);
            Assert.AreEqual("123", entity1.Description);
            Assert.IsTrue(Db.HasChangesSinceSnapshot);
            Assert.IsTrue(descriptionPropertyState.HasChangedSinceSnapshot);
            entity1.Description = null;
            Assert.IsFalse(descriptionPropertyState.HasChangedSinceSnapshot);
            Assert.IsFalse(Db.HasChangesSinceSnapshot);
        }



        [TestMethod]
        public async Task TestUndoSinglePropertyFromSnapshotShouldNotAffectInsertedEntities()
        {
            var id1 = 156187;
            var clientRemote1 = new Client
            {
                Name = "abc",
                Id = id1
            };
            var id2 = 156188;
            var clientRemote2 = new Client
            {
                Name = "abc",
                Id = id2
            };
            var id3 = 156189;
            var clientRemoteUndoDelete3 = new Client
            {
                Name = "clientRemoteUndoDelete3",
                Id = id3
            };
            AppDbContext.InMemoryDb.Clients.Add(clientRemote1);
            AppDbContext.InMemoryDb.Clients.Add(clientRemote2);
            AppDbContext.InMemoryDb.Clients.Add(clientRemoteUndoDelete3);
            var entity1 = await Db.GetEntityAsync<Client>(id1);
            var entity2KeepDelete = await Db.GetEntityAsync<Client>(id2);
            var entity3UndoDelete = await Db.GetEntityAsync<Client>(id3);
            var entity2KeepDeleteState = Db.GetEntityState(entity2KeepDelete);
            var entity3UndoDeleteState = Db.GetEntityState(entity3UndoDelete);
            var newEntityKeep = new Client()
            {
                Name = "New Client"
            };
            var newEntityUndo = new Client()
            {
                Name = "New Client Undo"
            };
            var snapshot = Db.AddSnapshot();
            Assert.IsFalse(Db.HasChangesSinceSnapshot);
            Assert.IsFalse(Db.HasChanges);
            Db.Clients.Add(newEntityKeep);
            Db.Clients.Add(newEntityUndo);
            Db.Clients.Delete(entity2KeepDelete);
            Db.Clients.Delete(entity3UndoDelete);
            Assert.IsTrue(entity2KeepDeleteState.MarkedForAnyDeletion);
            Assert.IsTrue(Db.HasChangesSinceSnapshot);
            Assert.IsTrue(Db.HasChanges);
            Assert.IsTrue(Db.IsTracked(newEntityKeep));
            var entityState = Db.GetEntityState(entity1);
            var newEntityKeepState = Db.GetEntityState(newEntityKeep);
            var newEntityUndoState = Db.GetEntityState(newEntityUndo);
            entity1.Name = "def";
            entity1.Description = "123";
            Assert.IsTrue(Db.HasChangesSinceSnapshot);
            Assert.IsTrue(Db.HasChanges);
            var nameProperty = Db.EntityConfigurationContext.EntityType<Client>().FindProperty(nameof(Client.Name));
            var descriptionPropertyState = entityState.GetPropertyState(nameof(Client.Description));
            Assert.IsTrue(descriptionPropertyState.HasChangedSinceSnapshot);
            Assert.IsTrue(Db.IsTracked(newEntityKeep));
            Assert.IsTrue(entity3UndoDeleteState.MarkedForAnyDeletion);
            Db.UndoChanges(new[] { entity3UndoDelete, newEntityUndo, entity1 }, new[] { nameProperty });
            Assert.IsTrue(entity2KeepDeleteState.MarkedForAnyDeletion);
            Assert.IsFalse(entity3UndoDeleteState.MarkedForAnyDeletion);
            Assert.IsTrue(Db.IsTracked(newEntityKeep));
            Assert.IsTrue(Db.IsTracked(newEntityUndo));
            Assert.AreEqual("abc", entity1.Name);
            Assert.AreEqual("123", entity1.Description);
            Assert.IsTrue(Db.HasChangesSinceSnapshot);
            Assert.IsTrue(descriptionPropertyState.HasChangedSinceSnapshot);
            entity1.Description = null;
            Assert.IsFalse(descriptionPropertyState.HasChangedSinceSnapshot);
            Assert.IsTrue(Db.HasChangesSinceSnapshot);
            Assert.IsTrue(Db.IsTracked(newEntityKeep));
            Db.Clients.Delete(newEntityKeep);
            var isTracked = Db.IsTracked(newEntityKeep);
            var isDeleted = newEntityKeepState.MarkedForAnyDeletion;
        }

        [TestMethod]
        public async Task TestNestedSnapshots()
        {
            var id1 = 156187;
            var clientRemote = new Client
            {
                Name = "abc",
                Description = "def",
                Id = id1
            };
            AppDbContext.InMemoryDb.Clients.Add(clientRemote);
            var client = await Db.Clients.GetWithKeyAsync(id1);
            var clientState = Db.GetEntityState(client);
            var snapshot1 = Db.AddSnapshot();
            var snapshot2 = Db.AddSnapshot();

            client.Description = "456";
            Db.DeleteEntity(client);
            Db.ReplaceLastSnapshot();
            Assert.IsTrue(Db.HasChanges);
            Assert.IsFalse(Db.HasChangesSinceSnapshot);
            Db.UndoChanges();
            Assert.IsTrue(Db.HasChanges);
            Assert.IsFalse(Db.HasChangesSinceSnapshot);
            Db.RemoveLastSnapshot();
            Assert.IsTrue(Db.HasChanges);
            Assert.IsTrue(Db.HasChangesSinceSnapshot);
            Assert.AreEqual("456", client.Description);
            Assert.IsTrue(Db.HasChanges);
            Assert.IsTrue(Db.HasChangesSinceSnapshot);
        }

        [TestMethod]
        public async Task TestReplaceLastSnapshot()
        {
            var id1 = 156187;
            var clientTypeRemote = new ClientType
            {
                Name = "abc",
                Id = id1
            };
            AppDbContext.InMemoryDb.ClientTypes.Add(clientTypeRemote);
            var entity = await Db.ClientTypes.GetWithKeyAsync(id1);
            Assert.AreEqual(0, Db.SnapshotsCount);
            Db.AddSnapshot();
            Assert.AreEqual(1, Db.SnapshotsCount);
            entity.Name = "def";
            Assert.IsTrue(Db.HasChangesSinceSnapshot);
            var snapshot = Db.ReplaceLastSnapshot();
            Assert.AreEqual(1, Db.SnapshotsCount);
            Assert.IsFalse(Db.HasChangesSinceSnapshot);
            Assert.AreEqual("def", entity.Name);
        }

        [TestMethod]
        public async Task TestRevertSnapshotWithNewEntityViaRelationshipList()
        {
            var id1 = 156187;
            var clientTypeRemote = new ClientType
            {
                Name = "abc",
                Id = id1
            };
            AppDbContext.InMemoryDb.ClientTypes.Add(clientTypeRemote);
            var entity1 = await Db.GetEntityAsync<ClientType>(id1);
            var snapshot = Db.AddSnapshot();
            var propertyState = Db.GetEntityState(entity1).GetPropertyState(nameof(ClientType.Clients));
            var newClient = new Client
            {
                Name = "New client",
                Description = "abc"
            };
            Assert.IsFalse(propertyState.HasChangedSinceSnapshot);
            Assert.IsFalse(Db.HasChangesSinceSnapshot);
            entity1.Clients.Add(newClient);
            Assert.IsTrue(propertyState.HasChangedSinceSnapshot);
            Assert.AreEqual(id1, newClient.TypeId);
            Assert.AreEqual(1, entity1.Clients.Count);
            Assert.IsTrue(Db.HasChangesSinceSnapshot);
            Db.RevertToSnapshot();
            Assert.IsFalse(propertyState.HasChangedSinceSnapshot);
            Assert.AreEqual(0, newClient.TypeId);
            Assert.AreEqual(0, entity1.Clients.Count);
            Assert.IsFalse(Db.HasChangesSinceSnapshot);
        }

        [TestMethod]
        public async Task TestNewEntityStillReceivesChangedSinceSnapshot()
        {
            var newClient = new Client
            {
                Name = "New client",
                Description = "abc"
            };
            var snapshot = Db.AddSnapshot();
            Db.Clients.Add(newClient);
            var entityState = Db.GetEntityState(newClient);
            Assert.IsFalse(entityState.HasChangedSinceSnapshot);
            for (var i = 0; i < entityState.PropertyStates.Length; i++)
            {
                var propertyState = entityState.PropertyStates[i];
                Assert.IsFalse(propertyState.HasChangedSinceSnapshot);
            }

            var namePropertyState = entityState.GetPropertyState(nameof(Client.Name));
            newClient.Name = "def";
            Assert.IsTrue(entityState.HasChangedSinceSnapshot);
            Assert.IsTrue(namePropertyState.HasChangedSinceSnapshot);
        }

        [TestMethod]
        public async Task TestRelationshipListEntityChange()
        {
            var id1 = 156187;
            var clientTypeRemote = new ClientType
            {
                Name = "abc",
                Id = id1
            };
            AppDbContext.InMemoryDb.ClientTypes.Add(clientTypeRemote);
            var entity1 = await Db.GetEntityAsync<ClientType>(id1);
            var propertyState = Db.GetEntityState(entity1).GetPropertyState(nameof(ClientType.Clients));
            var newClient = new Client
            {
                Name = "New client",
                Description = "abc"
            };
            entity1.Clients.Add(newClient);
            var snapshot = Db.AddSnapshot();
            newClient.Name = "New";
            Assert.IsFalse(propertyState.HasChangedSinceSnapshot);
            Assert.IsTrue(Db.HasChangesSinceSnapshot);
            Db.RevertToSnapshot();
            Assert.IsFalse(propertyState.HasChangedSinceSnapshot);
            Assert.IsFalse(Db.HasChangesSinceSnapshot);
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
            var entity2State = Db.GetEntityState(entity2);
            Assert.AreEqual(EntityStatus.New, entity2State.Status);

            Db.RemoveLastSnapshot(SnapshotRemoveKind.GoToPreSnapshotValues);

            Assert.AreEqual(EntityStatus.NewAndDeleted, entity2State.Status);

            Db.RestoreNextAbandonedSnapshot();

            Assert.AreEqual(EntityStatus.New, entity2State.Status);
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
            dbClient.AverageIncome = 12;
            var client3 = await Db.Clients.GetWithKeyAsync(1212);
            Assert.AreEqual(client, client3);
            Assert.IsTrue(Db.HasRestorableSnapshot);
            dbClient.Name = "new name";
            client3 = await Db.Clients.GetWithKeyAsync(1212);
            Assert.AreEqual(client, client3);
            Assert.IsFalse(Db.HasRestorableSnapshot);
        }

        [TestMethod]
        public async Task AddingSnapshotShouldResetSnapshotChangeStatus()
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
            var propertyState = Db.GetEntityState(client).GetPropertyState(nameof(Client.Name));
            Assert.IsTrue(propertyState.HasChanged);
            Assert.IsTrue(propertyState.HasChangedSinceSnapshot);
            Db.AddSnapshot();
            Assert.IsTrue(propertyState.HasChanged);
            Assert.IsFalse(propertyState.HasChangedSinceSnapshot);
            client.Name = "456";
            Assert.IsTrue(propertyState.HasChanged);
            Assert.IsTrue(propertyState.HasChangedSinceSnapshot);
            Db.RevertToSnapshot();
            Assert.AreEqual("123", client.Name);
            Assert.IsTrue(propertyState.HasChanged);
            Assert.IsFalse(propertyState.HasChangedSinceSnapshot);
            client.Name = "456";
            Assert.IsTrue(propertyState.HasChanged);
            Assert.IsTrue(propertyState.HasChangedSinceSnapshot);
            Db.AddSnapshot();
            Assert.IsTrue(propertyState.HasChanged);
            Assert.IsFalse(propertyState.HasChangedSinceSnapshot);
        }

        [TestMethod]
        public async Task TestRelationshipCollectionNestedChanges()
        {
            var dbClient = new Client
            {
                Id = 1212,
                Name = "abc",
                Description = "def",
                TypeId = 77
            };
            var dbClientType1 = new ClientType
            {
                Id = 77
            };
            var dbClientType2 = new ClientType
            {
                Id = 78
            };
            AppDbContext.InMemoryDb.ClientTypes.Add(dbClientType1);
            AppDbContext.InMemoryDb.ClientTypes.Add(dbClientType2);
            AppDbContext.InMemoryDb.Clients.Add(dbClient);
            var types = await Db.ClientTypes.Expand(_ => _.Clients).ToListAsync();
            var clientType1 = types.Single(_ => _.Id == 77);
            var clientType2 = types.Single(_ => _.Id == 78);
            var client = clientType1.Clients[0];
            var relationshipCollection1 = Db.GetEntityState(clientType1).GetPropertyState(nameof(ClientType.Clients));
            var relationshipCollection2 = Db.GetEntityState(clientType2).GetPropertyState(nameof(ClientType.Clients));
            Assert.IsFalse(relationshipCollection1.HasChanged);
            Assert.IsFalse(relationshipCollection1.HasChangedSinceSnapshot);
            Assert.IsFalse(relationshipCollection1.HasNestedChanges);
            Assert.IsFalse(relationshipCollection1.HasNestedChangesSinceSnapshot);
            Assert.IsFalse(relationshipCollection2.HasChanged);
            Assert.IsFalse(relationshipCollection2.HasChangedSinceSnapshot);
            Assert.IsFalse(relationshipCollection2.HasNestedChanges);
            Assert.IsFalse(relationshipCollection2.HasNestedChangesSinceSnapshot);
            var clientState = Db.GetEntityState(client);
            var nameState = clientState.GetPropertyState(nameof(Client.Name));
            Assert.IsFalse(clientState.HasChanged);
            Assert.IsFalse(clientState.HasChangedSinceSnapshot);
            Assert.IsFalse(nameState.HasChanged);
            Assert.IsFalse(nameState.HasChangedSinceSnapshot);
            client.Name = "def";
            Assert.IsTrue(clientState.HasChanged);
            Assert.IsTrue(clientState.HasChangedSinceSnapshot);
            Assert.IsTrue(nameState.HasChanged);
            Assert.IsTrue(nameState.HasChangedSinceSnapshot);
            Assert.IsFalse(relationshipCollection1.HasChanged);
            Assert.IsFalse(relationshipCollection1.HasChangedSinceSnapshot);
            Assert.IsTrue(relationshipCollection1.HasNestedChanges);
            Assert.IsTrue(relationshipCollection1.HasNestedChangesSinceSnapshot);
            Assert.IsFalse(relationshipCollection2.HasChanged);
            Assert.IsFalse(relationshipCollection2.HasChangedSinceSnapshot);
            Assert.IsFalse(relationshipCollection2.HasNestedChanges);
            Assert.IsFalse(relationshipCollection2.HasNestedChangesSinceSnapshot);
            client.Name = "abc";
            Assert.IsFalse(clientState.HasChanged);
            Assert.IsFalse(clientState.HasChangedSinceSnapshot);
            Assert.IsFalse(nameState.HasChanged);
            Assert.IsFalse(nameState.HasChangedSinceSnapshot);
            Assert.IsFalse(relationshipCollection1.HasChanged);
            Assert.IsFalse(relationshipCollection1.HasChangedSinceSnapshot);
            Assert.IsFalse(relationshipCollection1.HasNestedChanges);
            Assert.IsFalse(relationshipCollection1.HasNestedChangesSinceSnapshot);
            Assert.IsFalse(relationshipCollection2.HasChanged);
            Assert.IsFalse(relationshipCollection2.HasChangedSinceSnapshot);
            Assert.IsFalse(relationshipCollection2.HasNestedChanges);
            Assert.IsFalse(relationshipCollection2.HasNestedChangesSinceSnapshot);
            client.Name = "def";
            Assert.IsFalse(relationshipCollection1.HasChanged);
            Assert.IsFalse(relationshipCollection1.HasChangedSinceSnapshot);
            Assert.IsTrue(relationshipCollection1.HasNestedChanges);
            Assert.IsTrue(relationshipCollection1.HasNestedChangesSinceSnapshot);
            Assert.IsFalse(relationshipCollection2.HasChanged);
            Assert.IsFalse(relationshipCollection2.HasChangedSinceSnapshot);
            Assert.IsFalse(relationshipCollection2.HasNestedChanges);
            Assert.IsFalse(relationshipCollection2.HasNestedChangesSinceSnapshot);
            var snapshot = Db.AddSnapshot();
            Assert.IsFalse(relationshipCollection1.HasChanged);
            Assert.IsFalse(relationshipCollection1.HasChangedSinceSnapshot);
            Assert.IsTrue(relationshipCollection1.HasNestedChanges);
            Assert.IsFalse(relationshipCollection1.HasNestedChangesSinceSnapshot);
            Assert.IsFalse(relationshipCollection2.HasChanged);
            Assert.IsFalse(relationshipCollection2.HasChangedSinceSnapshot);
            Assert.IsFalse(relationshipCollection2.HasNestedChanges);
            Assert.IsFalse(relationshipCollection2.HasNestedChangesSinceSnapshot);
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
        }

        [TestMethod]
        public async Task TestRelationshipCollectionStatus()
        {
            var dbClientType1 = new ClientType
            {
                Id = 1212,
                Name = "abc",
            };
            var dbClientType2 = new ClientType
            {
                Id = 1213,
                Name = "abc",
            };
            var dbClient1 = new Client
            {
                Id = 4545,
                Name = "abc",
                Description = "def",
                TypeId = 1212
            };
            var dbClient2 = new Client
            {
                Id = 4546,
                Name = "def",
                Description = "def",
                TypeId = 1212
            };
            var dbClient3 = new Client
            {
                Id = 4547,
                Name = "ghi",
                Description = "def",
                TypeId = 1212
            };
            var dbClient4 = new Client
            {
                Id = 4548,
                Name = "jkl",
                Description = "def",
                TypeId = 1212
            };
            var dbClient5 = new Client
            {
                Id = 4549,
                Name = "mno",
                Description = "def",
                TypeId = 1212
            };
            var dbClient6 = new Client
            {
                Id = 4550,
                Name = "pqr",
                Description = "def",
                TypeId = 1212
            };
            var dbClient7 = new Client
            {
                Id = 4551,
                Name = "stu",
                Description = "def",
                TypeId = 1213
            };
            AppDbContext.InMemoryDb.ClientTypes.Add(dbClientType1);
            AppDbContext.InMemoryDb.ClientTypes.Add(dbClientType2);
            AppDbContext.InMemoryDb.Clients.Add(dbClient1);
            AppDbContext.InMemoryDb.Clients.Add(dbClient2);
            AppDbContext.InMemoryDb.Clients.Add(dbClient3);
            AppDbContext.InMemoryDb.Clients.Add(dbClient4);
            AppDbContext.InMemoryDb.Clients.Add(dbClient5);
            AppDbContext.InMemoryDb.Clients.Add(dbClient6);
            AppDbContext.InMemoryDb.Clients.Add(dbClient7);
            var clientTypes = await Db.ClientTypes.Expand(_ => _.Clients).ToListAsync();
            var clientType1 = clientTypes.Single(_ => _.Id == 1212);
            var clientType2 = clientTypes.Single(_ => _.Id == 1213);
            var client1 = clientType1.Clients.Single(_ => _.Id == 4545);
            var client2 = clientType1.Clients.Single(_ => _.Id == 4546);
            var client3 = clientType1.Clients.Single(_ => _.Id == 4547);
            var client4 = clientType1.Clients.Single(_ => _.Id == 4548);
            var client5 = clientType1.Clients.Single(_ => _.Id == 4549);
            var client6 = clientType1.Clients.Single(_ => _.Id == 4550);
            var client7 = clientType2.Clients.Single(_ => _.Id == 4551);
            var client1State = Db.GetEntityState(client1);
            var client2State = Db.GetEntityState(client2);
            var client3State = Db.GetEntityState(client3);
            var client4State = Db.GetEntityState(client4);
            var client5State = Db.GetEntityState(client5);
            var client6State = Db.GetEntityState(client6);
            var client7State = Db.GetEntityState(client7);
            Assert.AreEqual(6, clientType1.Clients.Count);
            var state = Db.GetEntityState(clientType1);
            var clientType1ClientsState = state.GetPropertyState(nameof(ClientType.Clients));
            Assert.AreEqual(0, clientType1ClientsState.ItemsChanged.Count);
            Assert.AreEqual(0, clientType1ClientsState.ItemsAdded.Count);
            Assert.AreEqual(0, clientType1ClientsState.ItemsRemoved.Count);
            Assert.AreEqual(0, clientType1ClientsState.ItemsChangedSinceSnapshot.Count);
            Assert.AreEqual(0, clientType1ClientsState.ItemsAddedSinceSnapshot.Count);
            Assert.AreEqual(0, clientType1ClientsState.ItemsRemovedSinceSnapshot.Count);
            client1.Name = "a new name";
            Assert.AreEqual(6, clientType1.Clients.Count);
            client2.TypeId = 7878;
            Assert.AreEqual(5, clientType1.Clients.Count);
            var newClient1 = new Client
            {
                Name = "A new client 1",
                Description = "def",
                TypeId = clientType1.Id
            };
            Db.Clients.Add(newClient1);
            var newClient1State = Db.GetEntityState(newClient1);
            Assert.AreEqual(6, clientType1.Clients.Count);
            AssertCollection(clientType1ClientsState.ItemsChanged, client1State);
            AssertCollection(clientType1ClientsState.ItemsAdded, newClient1State);
            AssertCollection(clientType1ClientsState.ItemsRemoved, client2State);
            AssertCollection(clientType1ClientsState.ItemsChangedSinceSnapshot, client1State);
            AssertCollection(clientType1ClientsState.ItemsAddedSinceSnapshot, newClient1State);
            AssertCollection(clientType1ClientsState.ItemsRemovedSinceSnapshot, client2State);
            clientType1ClientsState.ItemsChanged.Change.Subscribe(_ =>
            {
                int a = 0;
            });
            Db.AddSnapshot();
            AssertCollection(clientType1ClientsState.ItemsChanged, client1State);
            AssertCollection(clientType1ClientsState.ItemsAdded, newClient1State);
            AssertCollection(clientType1ClientsState.ItemsRemoved, client2State);
            Assert.AreEqual(0, clientType1ClientsState.ItemsChangedSinceSnapshot.Count);
            Assert.AreEqual(0, clientType1ClientsState.ItemsAddedSinceSnapshot.Count);
            Assert.AreEqual(0, clientType1ClientsState.ItemsRemovedSinceSnapshot.Count);
            client4.Name = "Another new name";
            AssertCollection(clientType1ClientsState.ItemsChanged, client1State, client4State);
            AssertCollection(clientType1ClientsState.ItemsAdded, newClient1State);
            AssertCollection(clientType1ClientsState.ItemsRemoved, client2State);
            AssertCollection(clientType1ClientsState.ItemsChangedSinceSnapshot, client4State);
            Assert.AreEqual(0, clientType1ClientsState.ItemsAddedSinceSnapshot.Count);
            Assert.AreEqual(0, clientType1ClientsState.ItemsRemovedSinceSnapshot.Count);
            var newClient2 = new Client
            {
                Name = "A new client 2",
                Description = "def",
                TypeId = clientType1.Id
            };
            Db.Clients.Add(newClient2);
            var newClient2State = Db.GetEntityState(newClient2);
            AssertCollection(clientType1ClientsState.ItemsChanged, client1State, client4State);
            AssertCollection(clientType1ClientsState.ItemsAdded, newClient1State, newClient2State);
            AssertCollection(clientType1ClientsState.ItemsRemoved, client2State);
            AssertCollection(clientType1ClientsState.ItemsChangedSinceSnapshot, client4State);
            AssertCollection(clientType1ClientsState.ItemsAddedSinceSnapshot, newClient2State);
            Assert.AreEqual(0, clientType1ClientsState.ItemsRemovedSinceSnapshot.Count);
            client7.TypeId = clientType1.Id;
            AssertCollection(clientType1ClientsState.ItemsChanged, client1State, client4State);
            AssertCollection(clientType1ClientsState.ItemsAdded, newClient1State, newClient2State, client7State);
            AssertCollection(clientType1ClientsState.ItemsRemoved, client2State);
            AssertCollection(clientType1ClientsState.ItemsChangedSinceSnapshot, client4State);
            AssertCollection(clientType1ClientsState.ItemsAddedSinceSnapshot, newClient2State, client7State);
            Assert.AreEqual(0, clientType1ClientsState.ItemsRemovedSinceSnapshot.Count);
            var result = await Db.SaveChangesAsync();
            Assert.IsTrue(result.Success);
            Assert.AreEqual(0, clientType1ClientsState.ItemsChanged.Count);
            Assert.AreEqual(0, clientType1ClientsState.ItemsAdded.Count);
            Assert.AreEqual(0, clientType1ClientsState.ItemsRemoved.Count);
            Assert.AreEqual(0, clientType1ClientsState.ItemsChangedSinceSnapshot.Count);
            Assert.AreEqual(0, clientType1ClientsState.ItemsAddedSinceSnapshot.Count);
            Assert.AreEqual(0, clientType1ClientsState.ItemsRemovedSinceSnapshot.Count);
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