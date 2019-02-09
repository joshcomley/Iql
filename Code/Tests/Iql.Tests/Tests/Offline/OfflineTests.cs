using System.Linq;
using System.Threading.Tasks;
using Iql.Conversion.State;
using Iql.Data.Context;
using Iql.Data.Crud.Operations;
using Iql.Data.DataStores;
using Iql.Entities;
using Iql.Queryable;
using Iql.Tests.Context;
using IqlSampleApp.Data.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace Iql.Tests.Tests.Offline
{
    [TestClass]
    public class OfflineTests
    {
        private static OfflineAppDbContext _db = new OfflineAppDbContext();

        public static OfflineAppDbContext Db => _db;

        [TestMethod]
        public async Task SerializeState()
        {
            Db.IsOffline = false;
            var clients = await Db.Clients.Expand(_ => _.Type).ToListAsync();
            var client = clients.First();
            client.Name = "Changed";
            client.TypeId = 2;
            client.Type.Name = "A new name";
            var entityState = Db.GetEntityState(client);
            var json = Db.Tracking.SerializeToJson();
            var state = JsonConvert.DeserializeObject(json, typeof(TrackingState));
        }

        [TestMethod]
        public async Task GetDataWhenOnlineAndRefetchWhenOffline()
        {
            Db.OfflineDataStore.Clear();
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
        public async Task NonTrackedResultsShouldNotPropagateToOffline()
        {
            Db.OfflineDataStore.Clear();

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
            Assert.AreEqual(true, result.Success);

            // Should still be no temporal changes
            changes = Db.GetChanges();
            Assert.AreEqual(0, changes.Length);

            // As we are not tracking, there should be no changes propagated to offline
            offlineChanges = Db.GetOfflineChanges();
            Assert.AreEqual(0, offlineChanges.Length);
        }

        [TestMethod]
        public async Task SaveChangeWhenOfflineAndResyncWhenOnline()
        {
            Db.OfflineDataStore.Clear();
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
            var offlineClient = offlineDataSet.Single(_ => _.Id == 1);
            var onlineClient = onlineDataSet.Single(_ => _.Id == 1);
            Assert.AreEqual(OfflineAppDbContext.Client1Name, offlineClient.Name);
            Assert.AreEqual(OfflineAppDbContext.Client1Name, onlineClient.Name);
            // Should be no changes
            var changes = Db.GetChanges();
            Assert.AreEqual(0, changes.Length);

            var offlineChanges = Db.GetOfflineChanges();
            Assert.AreEqual(0, offlineChanges.Length);

            // Go offline
            Db.IsOffline = true;
            var client = clients.First();
            Assert.AreEqual(1, client.Id);

            // Change the "Name" property
            var clientChangedName = "Changed";
            client.Name = clientChangedName;

            // Should be one change queued in the data context
            changes = Db.GetChanges();
            Assert.AreEqual(1, changes.Length);
            AssertChangedProperties(changes, client, nameof(Client.Name));

            // Should be no changes in the offline context
            offlineChanges = Db.GetOfflineChanges();
            Assert.AreEqual(0, offlineChanges.Length);

            // Save changes should persist to offline data store and tracker
            var result = await Db.SaveChangesAsync();
            Assert.AreEqual(true, result.Success);

            // Should be no more temporal changes
            changes = Db.GetChanges();
            Assert.AreEqual(0, changes.Length);

            // Making a fresh request should result in an entity returned with the name change propagated, via the offline data store
            var clients2 = await Db.Clients.Expand(_ => _.Type).ToListAsync();
            var client2 = clients2.First();
            Assert.AreEqual(1, client2.Id);
            Assert.AreEqual(clientChangedName, client2.Name);
            Assert.AreEqual(client2, client);

            // The same should happen without tracking enabled
            var clientsUntracked = await Db.Clients.NoTracking().Expand(_ => _.Type).ToListAsync();
            var clientUntracked = clientsUntracked.First();
            Assert.AreEqual(1, clientUntracked.Id);
            Assert.AreEqual(clientChangedName, clientUntracked.Name);

            // The online client's "Name" should remain the same
            Assert.AreEqual(OfflineAppDbContext.Client1Name, onlineClient.Name);
            
            // "Name" change should be propagated to the offline tracker
            offlineChanges = Db.GetOfflineChanges();
            Assert.AreEqual(1, offlineChanges.Length);
            AssertChangedProperties(offlineChanges, client, nameof(Client.Name));
            Assert.AreEqual(clientChangedName, offlineClient.Name);

            // Make another change
            client.TypeId = 2;

            // Latest change should still be temporal
            offlineChanges = Db.GetOfflineChanges();
            Assert.AreEqual(1, offlineChanges.Length);
            AssertChangedProperties(offlineChanges, client, nameof(Client.Name));

            // "TypeId" change should be in the temporal changes
            changes = Db.GetChanges();
            Assert.AreEqual(1, changes.Length);
            AssertChangedProperties(changes, client, nameof(Client.TypeId));

            // Still offline, "TypeId" change should persist to offline
            result = await Db.SaveChangesAsync();
            Assert.AreEqual(true, result.Success);

            // Changes should be propagated to offline
            changes = Db.GetChanges();
            Assert.AreEqual(0, changes.Length);

            // Offline should now contain both "TypeId" and "Name" change
            offlineChanges = Db.GetOfflineChanges();
            Assert.AreEqual(1, offlineChanges.Length);
            AssertChangedProperties(offlineChanges, client, nameof(Client.TypeId), nameof(Client.Name));

            // The online client's "Name" and "TypeId" should remain the same
            Assert.AreEqual(OfflineAppDbContext.Client1Name, onlineClient.Name);
            Assert.AreEqual(OfflineAppDbContext.Client1TypeId, onlineClient.TypeId);

            result = await Db.SaveChangesAsync();
            Assert.AreEqual(true, result.Success);

            // There should be no new temporal changes
            changes = Db.GetChanges();
            Assert.AreEqual(0, changes.Length);

            // There should be no new offline changes
            offlineChanges = Db.GetOfflineChanges();
            Assert.AreEqual(1, offlineChanges.Length);
            AssertChangedProperties(offlineChanges, client, nameof(Client.TypeId), nameof(Client.Name));

            // The online client's "Name" and "TypeId" should remain the same
            Assert.AreEqual(OfflineAppDbContext.Client1Name, onlineClient.Name);
            Assert.AreEqual(OfflineAppDbContext.Client1TypeId, onlineClient.TypeId);

            //Db.IsOffline = false;
            //result = await Db.SaveChangesAsync();
            //Assert.
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
