using System.Linq;
using System.Threading.Tasks;
using Iql.Data.Context;
using Iql.Data.Crud.Operations;
using Iql.Data.DataStores;
using Iql.Data.Serialization;
using Iql.Entities;
using Iql.JavaScript.Extensions;
#if TypeScript
using Iql.Parsing;
#endif
using Iql.Tests.Context;
using IqlSampleApp.Data.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Iql.Tests.Tests.Offline
{
    [TestClass]
    public class OfflineTests
    {
        [TestInitialize]
        public void TestInitialize()
        {
            Db.Reset();
        }

        [TestCleanup]
        public void TestCleanUp()
        {
        }

        private static OfflineAppDbContext _db = new OfflineAppDbContext();
        public static OfflineAppDbContext Db => _db;

        [TestMethod]
        public async Task RestoringStateAndFetchingPreChangedEntitiesShouldLeaveChangesIntact()
        {
            Db.IsOffline = false;
            Db.Reset();
            var changes = Db.GetChanges();
            Assert.AreEqual(0, changes.Length);
            Db.TemporalDataTracker.RestoreFromJson(@"{""Sets"":[{""Type"":""Client"",""EntityStates"":[{""CurrentKey"":{""Keys"":[{""Name"":""Id"",""Value"":1}]},""IsNew"":false,""MarkedForDeletion"":false,""MarkedForCascadeDeletion"":false,""PropertyStates"":[{""RemoteValue"":1,""LocalValue"":2,""Property"":""TypeId""},{""RemoteValue"":""Coca-Cola"",""LocalValue"":""Changed"",""Property"":""Name""}]}]},{""Type"":""ClientType"",""EntityStates"":[{""CurrentKey"":{""Keys"":[{""Name"":""Id"",""Value"":2}]},""IsNew"":false,""MarkedForDeletion"":false,""MarkedForCascadeDeletion"":false,""PropertyStates"":[{""RemoteValue"":""Software"",""LocalValue"":""A new name"",""Property"":""Name""}]}]}]}");
            changes = Db.GetChanges();
            Assert.AreEqual(2, Db.GetChanges().Length);
            var clients = await Db.Clients.ToListAsync();
            Assert.AreEqual(2, Db.GetChanges().Length);
            var client = clients.Single(_ => _.Id == 1);
            Assert.AreEqual("Changed", client.Name);
            Assert.AreEqual(2, Db.GetChanges().Length);
        }

        [TestMethod]
        public async Task SerializeAndDeserializeState()
        {
            Db.IsOffline = false;
            var clients = await Db.Clients.Expand(_ => _.Type).ToListAsync();
            var client = clients.First(_ => _.Id == 1);
            var oldClientTypeId = client.TypeId;
            var oldClientName = client.Name;
            var oldClientType = client.Type;
            client.TypeId = 2;
            var oldClientTypeName = client.Type.Name;
            void AssertChanges()
            {
                var changes = Db.GetChanges();
                Assert.AreEqual(2, changes.Length);
                var clientChange = changes.FirstOrDefault(_ => _.Operation.EntityType == typeof(Client));
                var clientTypeChange = changes.FirstOrDefault(_ => _.Operation.EntityType == typeof(ClientType));
                Assert.IsNotNull(clientChange);
                Assert.IsNotNull(clientTypeChange);

                var clientUpdate = clientChange.Operation as IUpdateEntityOperation;
                Assert.IsNotNull(clientUpdate);
                var clientChangedProperties = clientUpdate.GetChangedProperties();
                Assert.AreEqual(2, clientChangedProperties.Length);

                var clientNameChange =
                    clientChangedProperties.SingleOrDefault(_ => _.Property.PropertyName == nameof(Client.Name));
                Assert.IsNotNull(clientNameChange);

                Assert.AreEqual(oldClientName, clientNameChange.RemoteValue);
                Assert.AreEqual("Changed", clientNameChange.LocalValue);

                var clientTypeIdChange =
                    clientChangedProperties.SingleOrDefault(_ => _.Property.PropertyName == nameof(Client.TypeId));
                Assert.IsNotNull(clientTypeIdChange);
                Assert.AreEqual(oldClientTypeId, clientTypeIdChange.RemoteValue);
                Assert.AreEqual(2, clientTypeIdChange.LocalValue);

                var clientTypeUpdate = clientTypeChange.Operation as IUpdateEntityOperation;
                Assert.IsNotNull(clientTypeUpdate);
                var clientTypeChangedProperties = clientTypeUpdate.GetChangedProperties();
                Assert.AreEqual(1, clientTypeChangedProperties.Length);

                var clientTypeNameChange =
                    clientTypeChangedProperties.SingleOrDefault(_ => _.Property.PropertyName == nameof(ClientType.Name));
                Assert.IsNotNull(clientTypeNameChange);
                Assert.AreEqual(oldClientTypeName, clientTypeNameChange.RemoteValue);
                Assert.AreEqual("A new name", clientTypeNameChange.LocalValue);
            }

            client.Name = "Changed";
            var newClientTypeId = 2;
            Assert.AreNotEqual(oldClientType, client.Type);
            client.Type.Name = "A new name";
            var entityState = Db.GetEntityState(client);
            var jsonWithChanges = Db.TemporalDataTracker.SerializeToJson();
            Assert.AreEqual(
                @"{""Sets"":[{""Type"":""Client"",""EntityStates"":[{""CurrentKey"":{""Keys"":[{""Name"":""Id"",""Value"":1}]},""IsNew"":false,""MarkedForDeletion"":false,""MarkedForCascadeDeletion"":false,""PropertyStates"":[{""RemoteValue"":1,""LocalValue"":2,""Property"":""TypeId""},{""RemoteValue"":""Coca-Cola"",""LocalValue"":""Changed"",""Property"":""Name""}]}]},{""Type"":""ClientType"",""EntityStates"":[{""CurrentKey"":{""Keys"":[{""Name"":""Id"",""Value"":2}]},""IsNew"":false,""MarkedForDeletion"":false,""MarkedForCascadeDeletion"":false,""PropertyStates"":[{""RemoteValue"":""Software"",""LocalValue"":""A new name"",""Property"":""Name""}]}]}]}",
                jsonWithChanges.NormalizeJson());

            AssertChanges();

            Db.TemporalDataTracker.AbandonChanges();

            Assert.AreEqual(oldClientType, client.Type);

            var jsonWithoutChanges = Db.TemporalDataTracker.SerializeToJson();
            Assert.AreEqual(@"{}", jsonWithoutChanges.NormalizeJson());

            Db.TemporalDataTracker.RestoreFromJson(jsonWithChanges);

            Assert.AreEqual(client.TypeId, newClientTypeId);
            Assert.AreNotEqual(oldClientType, client.Type);

            var jsonWithChanges2 = Db.TemporalDataTracker.SerializeToJson();
            Assert.AreEqual(jsonWithChanges, jsonWithChanges2);

            AssertChanges();
            // TODO: Now restore state to a new data context and verify GetChanges()
            // TODO: Add a delete and an add to the changes and ensure they serialize/deserialize correctly
        }
        [TestMethod]
        public async Task SerializeAndDeserializeStore()
        {
            var json = Db.DataStore.SerializeEntitiesToJson();
            var normalizedJson = json.NormalizeJson();
            Assert.AreEqual(@"[{""Type"":""Client"",""Entities"":[{""Id"":1,""TypeId"":1,""Name"":""Coca-Cola"",""AverageSales"":0,""AverageIncome"":12,""Category"":0,""Discount"":0,""Guid"":""00000000-0000-0000-0000-000000000000"",""CreatedDate"":""0001-01-01T00:00:00.0+00:00"",""PersistenceKey"":""00000000-0000-0000-0000-000000000000""},{""Id"":2,""TypeId"":1,""Name"":""Pepsi"",""AverageSales"":0,""AverageIncome"":33,""Category"":0,""Discount"":0,""Guid"":""00000000-0000-0000-0000-000000000000"",""CreatedDate"":""0001-01-01T00:00:00.0+00:00"",""PersistenceKey"":""00000000-0000-0000-0000-000000000000""},{""Id"":3,""TypeId"":2,""Name"":""Microsoft"",""AverageSales"":0,""AverageIncome"":97,""Category"":0,""Discount"":0,""Guid"":""00000000-0000-0000-0000-000000000000"",""CreatedDate"":""0001-01-01T00:00:00.0+00:00"",""PersistenceKey"":""00000000-0000-0000-0000-000000000000""}]},{""Type"":""ClientType"",""Entities"":[{""Id"":1,""Name"":""Beverages""},{""Id"":2,""Name"":""Software""}]},{""Type"":""Site"",""Entities"":[{""Id"":0,""Location"":{""type"":""Point"",""coordinates"":[13.2846516,52.5069704]},""Name"":""Berlin"",""Left"":0,""Right"":0,""Guid"":""00000000-0000-0000-0000-000000000000"",""CreatedDate"":""0001-01-01T00:00:00.0+00:00"",""PersistenceKey"":""00000000-0000-0000-0000-000000000000""}]}]",
                normalizedJson);
            var sets = JsonDataSerializer.DeserializeEntitySets(Db.EntityConfigurationContext, json);
            Assert.AreEqual(3, sets.Count);
            var clients = sets.Set<Client>();
            Assert.AreEqual(3, clients.Count);
            Assert.AreEqual("Coca-Cola", clients[0].Name);
            var sites = sets.Set<Site>();
            var site = sites.First();
            Assert.AreEqual(13.2846516, site.Location.X);
            Assert.AreEqual(52.5069704, site.Location.Y);
            // DONE: Check Geographic properties serialize correctly
            // TODO: Create "Restore" method on DataStore
            // TODO: OfflineDataStore should persist to file the state upon every change commit
        }

        [TestMethod]
        public async Task GetDataWhenOnlineAndRefetchWhenOffline()
        {
            Db.IsOffline = true;
            var clientsOffline1 = await Db.Clients.ToListAsync();
            Assert.AreEqual(0, clientsOffline1.Count);

            Db.IsOffline = false;
            var clients = await Db.Clients.Expand(_ => _.Type).ToListAsync();
            Assert.AreEqual(3, clients.Count);

            Db.IsOffline = true;
            var clientsOffline2 = await Db.Clients.Expand(_ => _.Type).ToListAsync();
            Assert.AreEqual(3, clientsOffline2.Count);
        }

        [TestMethod]
        public async Task DeleteEntityWhenOfflineAndResyncWhenOnline()
        {
            var onlineDataSet = (Db.DataStore as IOfflineDataStore).DataSet<Client>();
            bool RemoteClientExists()
            {
                var remoteClient = onlineDataSet.FirstOrDefault(_ => _.Id == 1);
                return remoteClient != null;
            }

            Assert.IsTrue(RemoteClientExists());
            Db.IsOffline = false;
            var client = await Db.Clients.GetWithKeyAsync(1);

            Assert.IsNotNull(client);

            Db.IsOffline = true;
            var client2 = await Db.Clients.GetWithKeyAsync(1);
            Assert.IsNotNull(client);
            Assert.AreEqual(client, client2);

            var entityState = Db.Delete(client);
            Assert.IsTrue(entityState.MarkedForDeletion);

            client2 = await Db.Clients.GetWithKeyAsync(1);
            Assert.IsNotNull(client);
            Assert.AreEqual(client, client2);

            Assert.AreEqual(1, Db.GetChanges().Length);
            Assert.AreEqual(0, Db.GetOfflineChanges().Length);

            var result = await Db.SaveChangesAsync();
            Assert.IsTrue(result.Success);
            Assert.IsTrue(RemoteClientExists());
            Assert.AreEqual(0, Db.GetChanges().Length);
            Assert.AreEqual(1, Db.GetOfflineChanges().Length);

            Db.IsOffline = false;

            var offlineResyncResult = await Db.SaveOfflineChangesAsync();
            Assert.IsTrue(offlineResyncResult.Success);
            Assert.AreEqual(0, Db.GetChanges().Length);
            Assert.AreEqual(0, Db.GetOfflineChanges().Length);
            Assert.IsFalse(RemoteClientExists());
        }

        [TestMethod]
        public async Task NonTrackedResultsShouldNotPropagateToOffline()
        {
            // Go online
            Db.IsOffline = false;

            // Fetch all three clients
            var clients = await Db.Clients.NoTracking().Expand(_ => _.Type).ToListAsync();
            Assert.AreEqual(3, clients.Count);

            // Should be no changes
            var changes = Db.GetChanges();
            Assert.AreEqual(0, changes.Length);

            var offlineChanges = Db.GetOfflineChanges();
            Assert.AreEqual(0, offlineChanges.Length);

            // Go offline
            Db.IsOffline = true;
            var client = clients.First();

            // Change the "Name" property
            client.Name = "Changed";

            // Should be one change queued in the data context
            changes = Db.GetChanges();
            Assert.AreEqual(0, changes.Length);

            // Should be no changes in the offline context
            offlineChanges = Db.GetOfflineChanges();
            Assert.AreEqual(0, offlineChanges.Length);

            // Save changes should persist to offline data store and tracker
            var result = await Db.SaveChangesAsync();
            Assert.IsTrue(result.Success);

            // Should still be no temporal changes
            changes = Db.GetChanges();
            Assert.AreEqual(0, changes.Length);

            // As we are not tracking, there should be no changes propagated to offline
            offlineChanges = Db.GetOfflineChanges();
            Assert.AreEqual(0, offlineChanges.Length);
        }

        [TestMethod]
        public async Task AddingAnEntityWhenOnline()
        {
            var offlineDataSet = Db.DataStore.OfflineDataStore.DataSet<Client>();
            var onlineDataSet = (Db.DataStore as IOfflineDataStore).DataSet<Client>();

            Assert.AreEqual(0, offlineDataSet.Count);
            Assert.AreEqual(3, onlineDataSet.Count);

            Assert.IsFalse(Db.HasOfflineChanges());

            // Go offline
            Db.IsOffline = false;
            var newClient = new Client();
            var newClientName = "New Client 456";
            newClient.Name = newClientName;
            newClient.TypeId = 2;
            Db.Clients.Add(newClient);
            var result = await Db.SaveChangesAsync();

            Assert.IsFalse(Db.HasOfflineChanges());

            Assert.IsTrue(result.Success);

            var newClientFetched = await Db.Clients.Where(_ => _.Name == newClientName
#if TypeScript
                ,
                new EvaluateContext
                {
                    Context = this,
                    Evaluate = _ => newClientName
                }
#endif
            ).ToListAsync();
            Assert.AreEqual(1, newClientFetched.Count);
        }


        [TestMethod]
        public async Task AddingAnEntityWhenOffline()
        {
            var offlineDataSet = Db.DataStore.OfflineDataStore.DataSet<Client>();
            var onlineDataSet = (Db.DataStore as IOfflineDataStore).DataSet<Client>();

            Assert.AreEqual(0, offlineDataSet.Count);
            Assert.AreEqual(3, onlineDataSet.Count);

            Assert.IsFalse(Db.HasOfflineChanges());

            // Go offline
            Db.IsOffline = true;
            var newClient = new Client();
            var newClientName = "New Client 123";
            newClient.Name = newClientName;
            newClient.TypeId = 2;
            Db.Clients.Add(newClient);
            var result = await Db.SaveChangesAsync();

            Assert.IsTrue(Db.HasOfflineChanges());
            var offlineChanges = Db.GetOfflineChanges();
            Assert.AreEqual(1, offlineChanges.Length);
            Assert.IsTrue(offlineChanges[0].Type == QueuedOperationType.Add);

            Assert.IsTrue(result.Success);

            var newClientFetched = await Db.Clients.Where(_ => _.Name == newClientName
#if TypeScript
                ,
                new EvaluateContext
                {
                    Context = this,
                    Evaluate = _ => newClientName
                }
#endif
            ).ToListAsync();
            Assert.AreEqual(1, newClientFetched.Count);

            Db.IsOffline = false;
            result = await Db.SaveOfflineChangesAsync();
            Assert.IsTrue(result.Success);

            var onlineClient = onlineDataSet.SingleOrDefault(_ => _.Name == newClientName);

            Assert.IsNotNull(onlineClient);

            Assert.IsFalse(Db.HasOfflineChanges());
        }



        [TestMethod]
        public async Task AddingAnEntityWithANewDependencyWhenOffline()
        {
            var offlineDataSet = Db.DataStore.OfflineDataStore.DataSet<Client>();
            var onlineDataSet = (Db.DataStore as IOfflineDataStore).DataSet<Client>();

            Assert.AreEqual(0, offlineDataSet.Count);
            Assert.AreEqual(3, onlineDataSet.Count);

            Assert.IsFalse(Db.HasOfflineChanges());

            // Go offline
            Db.IsOffline = true;
            var newClient = new Client();
            var newClientName = "New Client 1234";
            var newClientType = new ClientType();
            var newClientTypeName = "New Client Type 1234";
            newClient.Name = newClientName;
            newClient.Type = newClientType;
            Db.Clients.Add(newClient);
            var result = await Db.SaveChangesAsync();

            Assert.IsTrue(Db.HasOfflineChanges());

            Assert.IsTrue(result.Success);

            var newClientFetched = await Db.Clients.Where(_ => _.Name == newClientName
#if TypeScript
                ,
                new EvaluateContext
                {
                    Context = this,
                    Evaluate = _ => newClientName
                }
#endif
            ).ToListAsync();
            Assert.AreEqual(1, newClientFetched.Count);

            Db.IsOffline = false;
            result = await Db.SaveOfflineChangesAsync();
            Assert.IsTrue(result.Success);

            var onlineClient = onlineDataSet.SingleOrDefault(_ => _.Name == newClientName);

            Assert.IsNotNull(onlineClient);

            Assert.IsFalse(Db.HasOfflineChanges());
        }

        [TestMethod]
        public async Task SaveChangeWhenOfflineAndResyncWhenOnline()
        {
            var offlineDataSet = Db.DataStore.OfflineDataStore.DataSet<Client>();
            var onlineDataSet = (Db.DataStore as IOfflineDataStore).DataSet<Client>();

            Assert.AreEqual(0, offlineDataSet.Count);
            Assert.AreEqual(3, onlineDataSet.Count);

            // Go online
            Db.IsOffline = false;

            // Fetch all three clients
            var clients = await Db.Clients.Expand(_ => _.Type).ToListAsync();
            Assert.AreEqual(3, clients.Count);
            Assert.AreEqual(3, offlineDataSet.Count);
            var offlineFirstClient = offlineDataSet.Single(_ => _.Id == 1);
            var onlineFirstClient = onlineDataSet.Single(_ => _.Id == 1);
            var offlineSecondClient = offlineDataSet.Single(_ => _.Id == 2);
            var onlineSecondClient = onlineDataSet.Single(_ => _.Id == 2);
            Assert.AreEqual(OfflineAppDbContext.Client1Name, offlineFirstClient.Name);
            Assert.AreEqual(OfflineAppDbContext.Client1Name, onlineFirstClient.Name);
            // Should be no changes
            var changes = Db.GetChanges();
            Assert.AreEqual(0, changes.Length);

            var offlineChanges = Db.GetOfflineChanges();
            Assert.AreEqual(0, offlineChanges.Length);

            // Go offline
            Db.IsOffline = true;
            var firstClient = clients.First();
            Assert.AreEqual(1, firstClient.Id);

            // Change the "Name" property
            var firstClientNewName = "Changed";
            firstClient.Name = firstClientNewName;

            // Should be one change queued in the data context
            changes = Db.GetChanges();
            Assert.AreEqual(1, changes.Length);
            AssertChangedProperties(changes, firstClient, nameof(Client.Name));

            // Should be no changes in the offline context
            offlineChanges = Db.GetOfflineChanges();
            Assert.AreEqual(0, offlineChanges.Length);

            // Save changes should persist to offline data store and tracker
            var result = await Db.SaveChangesAsync();
            Assert.IsTrue(result.Success);

            // Should be no more temporal changes
            changes = Db.GetChanges();
            Assert.AreEqual(0, changes.Length);

            // Changes should have moved to offline tracker
            offlineChanges = Db.GetOfflineChanges();
            Assert.AreEqual(1, offlineChanges.Length);

            // Making a fresh request should result in an entity returned with the name change propagated, via the offline data store
            var clients2 = await Db.Clients.Expand(_ => _.Type).ToListAsync();
            var client2 = clients2.First(_ => _.Id == 1);
            Assert.AreEqual(1, client2.Id);
            Assert.AreEqual(firstClientNewName, client2.Name);
            Assert.AreEqual(client2, firstClient);

            // The same should happen without tracking enabled
            var clientsUntracked = await Db.Clients.NoTracking().Expand(_ => _.Type).ToListAsync();
            var clientUntracked = clientsUntracked.First(_ => _.Id == 1);
            Assert.AreEqual(1, clientUntracked.Id);
            Assert.AreEqual(firstClientNewName, clientUntracked.Name);

            // The online client's "Name" should remain the same
            Assert.AreEqual(OfflineAppDbContext.Client1Name, onlineFirstClient.Name);

            // "Name" change should be propagated to the offline tracker
            offlineChanges = Db.GetOfflineChanges();
            Assert.AreEqual(1, offlineChanges.Length);
            AssertChangedProperties(offlineChanges, firstClient, nameof(Client.Name));
            Assert.AreEqual(firstClientNewName, offlineFirstClient.Name);

            // Make another change
            var firstClientNewTypeId = 2;
            firstClient.TypeId = firstClientNewTypeId;

            // Latest change should still be temporal
            offlineChanges = Db.GetOfflineChanges();
            Assert.AreEqual(1, offlineChanges.Length);
            AssertChangedProperties(offlineChanges, firstClient, nameof(Client.Name));

            // "TypeId" change should be in the temporal changes
            changes = Db.GetChanges();
            Assert.AreEqual(1, changes.Length);
            AssertChangedProperties(changes, firstClient, nameof(Client.TypeId));

            // Still offline, "TypeId" change should persist to offline
            result = await Db.SaveChangesAsync();
            Assert.IsTrue(result.Success);

            // Changes should be propagated to offline
            changes = Db.GetChanges();
            Assert.AreEqual(0, changes.Length);

            // Offline should now contain both "TypeId" and "Name" change
            offlineChanges = Db.GetOfflineChanges();
            Assert.AreEqual(1, offlineChanges.Length);
            AssertChangedProperties(offlineChanges, firstClient, nameof(Client.TypeId), nameof(Client.Name));

            // The online client's "Name" and "TypeId" should remain the same
            Assert.AreEqual(OfflineAppDbContext.Client1Name, onlineFirstClient.Name);
            Assert.AreEqual(OfflineAppDbContext.Client1TypeId, onlineFirstClient.TypeId);

            result = await Db.SaveChangesAsync();
            Assert.IsTrue(result.Success);

            // There should be no new temporal changes
            changes = Db.GetChanges();
            Assert.AreEqual(0, changes.Length);

            // There should be no new offline changes
            offlineChanges = Db.GetOfflineChanges();
            Assert.AreEqual(1, offlineChanges.Length);
            AssertChangedProperties(offlineChanges, firstClient, nameof(Client.TypeId), nameof(Client.Name));

            // The online client's "Name" and "TypeId" should remain the same
            Assert.AreEqual(OfflineAppDbContext.Client1Name, onlineFirstClient.Name);
            Assert.AreEqual(OfflineAppDbContext.Client1TypeId, onlineFirstClient.TypeId);

            // After going online again, we should only work offline until the offline changes are merged
            Db.IsOffline = false;
            var firstClientNewNameInternal = "Client Online";
            onlineFirstClient.Name = firstClientNewNameInternal;

            clients = await Db.Clients.Expand(_ => _.Type).ToListAsync();
            Assert.AreEqual(firstClientNewName, firstClient.Name);

            var secondClient = clients.Single(_ => _.Id == 2);
            var secondClientNewName = "Changed 2";
            secondClient.Name = secondClientNewName;

            // There should be one new temporal changes
            changes = Db.GetChanges();
            Assert.AreEqual(1, changes.Length);
            AssertChangedProperties(changes, secondClient, nameof(Client.Name));

            // There should be no new offline changes
            offlineChanges = Db.GetOfflineChanges();
            Assert.AreEqual(1, offlineChanges.Length);
            AssertChangedProperties(offlineChanges, firstClient, nameof(Client.TypeId), nameof(Client.Name));

            result = await Db.SaveChangesAsync();
            Assert.IsTrue(result.Success);

            // We should still be in offline mode, because we haven't resynced the offline changes
            // As such, there now should be no temporal changes but we should have a new offline change
            changes = Db.GetChanges();
            Assert.AreEqual(0, changes.Length);

            // There should be one new offline change
            offlineChanges = Db.GetOfflineChanges();
            Assert.AreEqual(2, offlineChanges.Length);
            AssertChangedProperties(offlineChanges, firstClient, nameof(Client.TypeId), nameof(Client.Name));
            AssertChangedProperties(offlineChanges, secondClient, nameof(Client.Name));

            Assert.AreEqual(OfflineAppDbContext.Client2Name, onlineSecondClient.Name);

            Assert.AreEqual(firstClientNewNameInternal, onlineFirstClient.Name);

            result = await Db.SaveOfflineChangesAsync();
            Assert.IsTrue(result.Success);

            Assert.AreEqual(firstClientNewName, onlineFirstClient.Name);
            Assert.AreEqual(firstClientNewTypeId, onlineFirstClient.TypeId);
            Assert.AreEqual(secondClientNewName, onlineSecondClient.Name);

            changes = Db.GetChanges();
            Assert.AreEqual(0, changes.Length);

            // There should be one new offline change
            offlineChanges = Db.GetOfflineChanges();
            Assert.AreEqual(0, offlineChanges.Length);
        }

        /* TODO:
         * Adding an entity
         * Deleting an entity
         */

        private void AssertChangedProperties(IQueuedOperation[] changes, object entity, params string[] properties)
        {
            var findEntityState = DataContext.FindEntityState(entity);
            var key = findEntityState.CurrentKey;
            var first = changes.FirstOrDefault(_ =>
                _.Operation is IUpdateEntityOperation &&
                (_.Operation as IUpdateEntityOperation).Entity.GetType() == findEntityState.EntityConfiguration.Type &&
                findEntityState.EntityConfiguration.GetCompositeKey((_.Operation as IUpdateEntityOperation).Entity).Matches(key));
            Assert.IsNotNull(first);
            var updateEntityOperation = first.Operation as IUpdateEntityOperation;
            var changedProperties = updateEntityOperation.GetChangedProperties().Where(_ => !_.Property.Kind.HasFlag(PropertyKind.Relationship))
                .ToArray();
            Assert.AreEqual(properties.Length, changedProperties.Length);
            for (var i = 0; i < properties.Length; i++)
            {
                var property = properties[i];
                var match = changedProperties.FirstOrDefault(_ => _.Property.PropertyName == property);
                Assert.IsNotNull(match);
            }
        }
    }
}
