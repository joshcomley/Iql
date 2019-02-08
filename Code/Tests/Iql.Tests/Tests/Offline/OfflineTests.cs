using System.Linq;
using System.Threading.Tasks;
using Iql.Conversion.State;
using Iql.Data.Context;
using Iql.Data.Crud.Operations;
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
        public async Task SaveChangeWhenOfflineAndResyncWhenOnline()
        {
            Db.OfflineDataStore.Clear();
            Db.IsOffline = false;
            var clients = await Db.Clients.Expand(_ => _.Type).ToListAsync();
            Assert.AreEqual(3, clients.Count);

            var changes = Db.GetChanges();
            Assert.AreEqual(0, changes.Length);

            var offlineChanges = Db.GetOfflineChanges();
            Assert.AreEqual(0, offlineChanges.Length);

            Db.IsOffline = true;
            var client = clients.First();
            client.Name = "Changed";

            changes = Db.GetChanges();
            Assert.AreEqual(1, changes.Length);
            AssertChangedProperties(changes, client, nameof(Client.Name));

            offlineChanges = Db.GetOfflineChanges();
            Assert.AreEqual(0, offlineChanges.Length);

            var result = await Db.SaveChangesAsync();
            Assert.AreEqual(true, result.Success);

            changes = Db.GetChanges();
            Assert.AreEqual(0, changes.Length);

            offlineChanges = Db.GetOfflineChanges();
            Assert.AreEqual(1, offlineChanges.Length);
            AssertChangedProperties(offlineChanges, client, nameof(Client.Name));

            client.TypeId = 2;

            offlineChanges = Db.GetOfflineChanges();
            Assert.AreEqual(1, offlineChanges.Length);
            AssertChangedProperties(offlineChanges, client, nameof(Client.Name));

            changes = Db.GetChanges();
            Assert.AreEqual(1, changes.Length);
            AssertChangedProperties(changes, client, nameof(Client.TypeId));

            result = await Db.SaveChangesAsync();
            Assert.AreEqual(true, result.Success);

            changes = Db.GetChanges();
            Assert.AreEqual(0, changes.Length);

            offlineChanges = Db.GetOfflineChanges();
            Assert.AreEqual(1, offlineChanges.Length);
            AssertChangedProperties(offlineChanges, client, nameof(Client.TypeId), nameof(Client.Name));
            //Db.IsOffline = true;
            //var clientsOffline2 = await Db.Clients.Expand(_ => _.Type).ToListAsync();
            //Assert.AreEqual(3, clientsOffline2.Count);
        }

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
