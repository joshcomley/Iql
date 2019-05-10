using System;
using System.Linq;
using System.Threading.Tasks;
using Iql.Data.Context;
using Iql.Data.Crud.Operations;
using Iql.Data.DataStores;
using Iql.Data.Serialization;
using Iql.Data.Tracking;
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
            _oldPersistenceKeyGenerator = PersistenceKeyGenerator.New;
            Db.Reset();
            Db.IsOffline = false;
        }

        [TestCleanup]
        public void TestCleanUp()
        {
            PersistenceKeyGenerator.New = _oldPersistenceKeyGenerator;
        }

        private static OfflineAppDbContext _db = new OfflineAppDbContext();
        private Func<Guid> _oldPersistenceKeyGenerator;
        public static OfflineAppDbContext Db => _db;

        [TestMethod]
        public async Task RestoreEmptyOrBadStateShouldNotThrowError()
        {
            await Db.RestoreOfflineStateAsync();
            Assert.IsTrue(true);
        }

        [TestMethod]
            public async Task TestGetAllPages()
            {
            Db.IsOffline = false;
            var offlinePeopleTypes = Db.OfflinableDataStore.DataSet<PersonType>();
            for (var i = 1; i <= 300; i++)
            {
                offlinePeopleTypes.Add(new PersonType { Id = i });
            }
            Db.InMemoryDataStore.DefaultPageSize = 10;
            var peopleTypes = await Db.PersonTypes.Take(125).ToListAsync();
            Assert.IsTrue(peopleTypes.Success);
            Assert.AreEqual(125, peopleTypes.Count);

            Db.IsOffline = true;
            var peopleTypesOffline = await Db.PersonTypes.Take(125).ToListAsync();
            Assert.IsTrue(peopleTypesOffline.Success);
            Assert.AreEqual(125, peopleTypesOffline.Count);

            Db.IsOffline = false;
            peopleTypes = await Db.PersonTypes.Take(60).ToListAsync();
            Assert.IsTrue(peopleTypes.Success);
            Assert.AreEqual(60, peopleTypes.Count);

            Db.IsOffline = true;
            peopleTypesOffline = await Db.PersonTypes.Take(125).ToListAsync();
            Assert.IsTrue(peopleTypesOffline.Success);
            Assert.AreEqual(125, peopleTypesOffline.Count);
        }

        [TestMethod]
        public async Task OfflineCacheBetweenTwoDataContextsShouldBeSynchronised()
        {
            Db.IsOffline = false;
            var offlinePeopleTypes = Db.OfflinableDataStore.DataSet<PersonType>();
            for (var i = 1; i <= 300; i++)
            {
                offlinePeopleTypes.Add(new PersonType { Id = i });
            }
            var tempDb = new OfflineAppDbContext();
            var tempOfflinePeopleTypes = tempDb.OfflinableDataStore.DataSet<PersonType>();
            Assert.AreEqual(offlinePeopleTypes.Count, tempOfflinePeopleTypes.Count);
            tempDb.IsOffline = false;
            var personTypes = await tempDb.PersonTypes.ToListAsync();
            Assert.AreEqual(300, personTypes.Count);
            Db.IsOffline = true;
            var personTypesOffline = await Db.PersonTypes.ToListAsync();
            Assert.AreEqual(300, personTypesOffline.Count);
        }

        [TestMethod]
        public async Task OfflineChangesBetweenTwoDataContextsShouldBeSynchronised()
        {
            Db.IsOffline = false;
            var offlinePeopleTypes = Db.OfflinableDataStore.DataSet<PersonType>();
            for (var i = 1; i <= 300; i++)
            {
                offlinePeopleTypes.Add(new PersonType { Id = i });
            }

            var personType1 = await Db.PersonTypes.GetWithKeyAsync(1);
            personType1.Title = "Changed";
            Db.IsOffline = true;
            var result = await Db.SaveChangesAsync();
            Assert.IsTrue(result.Success);
            Assert.IsTrue(Db.HasOfflineChanges());
            var tempDb = new OfflineAppDbContext();
            Assert.AreEqual(tempDb.OfflineDataTracker, Db.OfflineDataTracker);
            Assert.IsTrue(tempDb.HasOfflineChanges());
        }

        [TestMethod]
        public async Task OfflineChangesToDataTrackerShouldTriggerEvent()
        {
            var eventCount = 0;
            Db.OfflineStateChanged.Subscribe(_ =>
            {
                eventCount++;
                switch (eventCount)
                {
                    case 1:
                        Assert.IsTrue(_.HasChanges);
                        break;
                    case 2:
                        Assert.IsFalse(_.HasChanges);
                        break;
                }
            });
            Db.IsOffline = false;
            var offlinePeopleTypes = Db.OfflinableDataStore.DataSet<PersonType>();
            for (var i = 1; i <= 300; i++)
            {
                offlinePeopleTypes.Add(new PersonType { Id = i });
            }

            var personType1 = await Db.PersonTypes.GetWithKeyAsync(1);
            personType1.Title = "Changed";
            Db.IsOffline = true;
            var result = await Db.SaveChangesAsync();
            Assert.AreEqual(1, eventCount);
            result = await Db.SaveChangesAsync();
            Assert.AreEqual(1, eventCount);
            Assert.IsTrue(result.Success);
            Assert.IsTrue(Db.HasOfflineChanges());
            var tempDb = new OfflineAppDbContext();
            Assert.AreEqual(tempDb.OfflineDataTracker, Db.OfflineDataTracker);
            Assert.IsTrue(tempDb.HasOfflineChanges());
            Db.IsOffline = false;
            Assert.AreEqual(1, eventCount);
            result = await Db.SaveOfflineChangesAsync();
            Assert.IsTrue(result.Success);
            Assert.AreEqual(2, eventCount);
            Assert.IsFalse(Db.HasOfflineChanges());
            Assert.IsFalse(tempDb.HasOfflineChanges());
        }

        [TestMethod]
        public async Task OfflineChangesToDataTrackerShouldTriggerEventOnSynchronisedDataContext()
        {
            var eventCount = 0;
            var tempDb = new OfflineAppDbContext();
            tempDb.OfflineStateChanged.Subscribe(_ =>
            {
                eventCount++;
                switch (eventCount)
                {
                    case 1:
                        Assert.IsTrue(_.HasChanges);
                        break;
                    case 2:
                        Assert.IsFalse(_.HasChanges);
                        break;
                }
            });
            Db.IsOffline = false;
            var offlinePeopleTypes = Db.OfflinableDataStore.DataSet<PersonType>();
            for (var i = 1; i <= 300; i++)
            {
                offlinePeopleTypes.Add(new PersonType { Id = i });
            }

            var personType1 = await Db.PersonTypes.GetWithKeyAsync(1);
            personType1.Title = "Changed";
            Db.IsOffline = true;
            var result = await Db.SaveChangesAsync();
            Assert.AreEqual(1, eventCount);
            result = await Db.SaveChangesAsync();
            Assert.AreEqual(1, eventCount);
            Assert.IsTrue(result.Success);
            Assert.IsTrue(Db.HasOfflineChanges());
            Assert.AreEqual(tempDb.OfflineDataTracker, Db.OfflineDataTracker);
            Assert.IsTrue(tempDb.HasOfflineChanges());
            Db.IsOffline = false;
            Assert.AreEqual(1, eventCount);
            result = await Db.SaveOfflineChangesAsync();
            Assert.IsTrue(result.Success);
            Assert.AreEqual(2, eventCount);
            Assert.IsFalse(Db.HasOfflineChanges());
            Assert.IsFalse(tempDb.HasOfflineChanges());
        }

        [TestMethod]
        public async Task OfflineStoreShouldBeUpdatedWithEachPersistedStateChange()
        {
            await Db.OfflineInMemoryDataStore.ResetAsync();
            await Db.OfflinableDataStore.ResetAsync();
            PersistenceKeyGenerator.New = () => new Guid("dec281fe-96fd-4117-8e4e-2eb575d3b5a2");
            IqlNewGuidExpression.NewGuid = () => new Guid("ff3948f6-2e86-47da-9588-3128b07090a1");
            var state = Db.OfflineInMemoryDataStore.SerializeStateToJson();
            Assert.AreEqual("[]", state);
            var client = new Client();
            client.Name = "Newly added client";
            client.TypeId = 1;
            Db.Clients.Add(client);
            var result = await Db.SaveChangesAsync();
            Assert.IsTrue(result.Success);
            state = Db.OfflineInMemoryDataStore.SerializeStateToJson().NormalizeJson();
            Assert.AreEqual(@"[{""Type"":""Client"",""Entities"":[{""Id"":1,""TypeId"":1,""AverageSales"":0,""AverageIncome"":0,""Category"":0,""Discount"":0,""Name"":""Newly added client"",""Guid"":""ff3948f6-2e86-47da-9588-3128b07090a1"",""CreatedDate"":""2019-01-02T03:04:05.0+00:00"",""PersistenceKey"":""dec281fe-96fd-4117-8e4e-2eb575d3b5a2""}]}]",
                state);
            client.Name = "Newly added client2";
            result = await Db.SaveChangesAsync();
            Assert.IsTrue(result.Success);
            state = Db.OfflineInMemoryDataStore.SerializeStateToJson().NormalizeJson();
            Assert.AreEqual(@"[{""Type"":""Client"",""Entities"":[{""Id"":1,""TypeId"":1,""AverageSales"":0,""AverageIncome"":0,""Category"":0,""Discount"":0,""Name"":""Newly added client2"",""Guid"":""ff3948f6-2e86-47da-9588-3128b07090a1"",""CreatedDate"":""2019-01-02T03:04:05.0+00:00"",""PersistenceKey"":""dec281fe-96fd-4117-8e4e-2eb575d3b5a2""}]}]",
                state);
            IqlNewGuidExpression.NewGuid = () => Guid.NewGuid();
            PersistenceKeyGenerator.New = () => Guid.NewGuid();
        }

        [TestMethod]
        public async Task RestoreOfDataStoreState()
        {
            Db.IsOffline = true;
            await Db.OfflineInMemoryDataStore.ResetAsync();
            var currentStateJson = Db.OfflineInMemoryDataStore.SerializeStateToJson().NormalizeJson();
            Assert.AreEqual("[]", currentStateJson);
            var stateJson =
                @"[{""Type"":""Client"",""Entities"":[{""Id"":1,""TypeId"":1,""Name"":""Coca-Cola"",""AverageSales"":0,""AverageIncome"":12,""Category"":0,""Discount"":0,""Guid"":""00000000-0000-0000-0000-000000000000"",""CreatedDate"":""2019-01-02T03:04:05.0+00:00"",""PersistenceKey"":""00000000-0000-0000-0000-000000000000""},{""Id"":2,""TypeId"":1,""Name"":""Pepsi"",""AverageSales"":0,""AverageIncome"":33,""Category"":0,""Discount"":0,""Guid"":""00000000-0000-0000-0000-000000000000"",""CreatedDate"":""2019-01-02T03:04:05.0+00:00"",""PersistenceKey"":""00000000-0000-0000-0000-000000000000""},{""Id"":3,""TypeId"":2,""Name"":""Microsoft"",""AverageSales"":0,""AverageIncome"":97,""Category"":0,""Discount"":0,""Guid"":""00000000-0000-0000-0000-000000000000"",""CreatedDate"":""2019-01-02T03:04:05.0+00:00"",""PersistenceKey"":""00000000-0000-0000-0000-000000000000""}]},{""Type"":""ClientType"",""Entities"":[{""Id"":1,""Name"":""Beverages""},{""Id"":2,""Name"":""Software""}]},{""Type"":""Site"",""Entities"":[{""Id"":0,""Location"":{""type"":""Point"",""coordinates"":[13.2846516,52.5069704]},""Name"":""Berlin"",""Left"":0,""Right"":0,""Guid"":""00000000-0000-0000-0000-000000000000"",""CreatedDate"":""2019-01-02T03:04:05.0+00:00"",""PersistenceKey"":""00000000-0000-0000-0000-000000000000""}]}]";
            await Db.OfflineInMemoryDataStore.RestoreStateFromJsonAsync(stateJson);
            var clients = await Db.Clients.ToListAsync();
            Assert.AreEqual(3, clients.Count);
        }

        [TestMethod]
        public async Task RestoreOfEmptyDataStoreState()
        {
            Db.IsOffline = true;
            var stateJson =
                @"[]";
            await Db.OfflineInMemoryDataStore.RestoreStateFromJsonAsync(stateJson);
            var clients = await Db.Clients.ToListAsync();
            Assert.AreEqual(0, clients.Count);
        }

        [TestMethod]
        public async Task AddingAnEntityRemotelyShouldAlsoAddItToOfflineDataStore2()
        {
            var clientSet = Db.OfflineInMemoryDataStore.DataSet<Client>();
            Assert.AreEqual(0, clientSet.Count);
            var client = new Client();
            client.Name = "Newly added client";
            client.TypeId = 1;
            Db.Clients.Add(client);
            var result = await Db.SaveChangesAsync();
            Assert.IsTrue(result.Success);
            Assert.AreEqual(1, clientSet.Count);
        }

        [TestMethod]
        public async Task UpdatingAnEntityRemotelyShouldAlsoUpdateItInTheOfflineDataStore2()
        {
            var clientSet = Db.OfflineInMemoryDataStore.DataSet<Client>();
            Assert.AreEqual(0, clientSet.Count);
            var clients = await Db.Clients.ToListAsync();
            var offlineClient = clientSet.Single(_ => _.Id == 1);
            Assert.IsNotNull(offlineClient);
            Assert.AreEqual(3, clients.Count);
            var client = clients.First(_ => _.Id == 1);
            client.Name = "Abc123";
            Assert.AreEqual(1, Db.GetChanges().Count);
            var result = await Db.SaveChangesAsync();
            Assert.IsTrue(result.Success);
            Assert.AreEqual(0, Db.GetChanges().Count);
            Assert.AreEqual("Abc123", offlineClient.Name);
        }

        [TestMethod]
        public async Task DeletingAnEntityRemotelyShouldAlsoDeleteItFromTheOfflineDataStore2()
        {
            var clientSet = Db.OfflineInMemoryDataStore.DataSet<Client>();
            Assert.AreEqual(0, clientSet.Count);
            var client = new Client();
            client.Name = "Newly added client";
            client.TypeId = 1;
            Db.Clients.Add(client);
            var result = await Db.SaveChangesAsync();
            Assert.IsTrue(result.Success);
            Assert.AreEqual(1, clientSet.Count);
            Db.DeleteEntity(client);
            result = await Db.SaveChangesAsync();
            Assert.IsTrue(result.Success);
            Assert.AreEqual(0, clientSet.Count);
        }

        [TestMethod]
        public async Task RestoringStateAndFetchingPreChangedEntitiesShouldLeaveChangesIntact()
        {
            Db.IsOffline = false;
            var changes = Db.GetChanges();
            Assert.AreEqual(0, changes.Count);
            Db.TemporalDataTracker.RestoreFromJson(@"{""Sets"":[{""Type"":""Client"",""EntityStates"":[{""CurrentKey"":{""Keys"":[{""Name"":""Id"",""Value"":1}]},""IsNew"":false,""MarkedForDeletion"":false,""MarkedForCascadeDeletion"":false,""PropertyStates"":[{""RemoteValue"":1,""LocalValue"":2,""Property"":""TypeId""},{""RemoteValue"":""Coca-Cola"",""LocalValue"":""Changed"",""Property"":""Name""}]}]},{""Type"":""ClientType"",""EntityStates"":[{""CurrentKey"":{""Keys"":[{""Name"":""Id"",""Value"":2}]},""IsNew"":false,""MarkedForDeletion"":false,""MarkedForCascadeDeletion"":false,""PropertyStates"":[{""RemoteValue"":""Software"",""LocalValue"":""A new name"",""Property"":""Name""}]}]}]}");
            changes = Db.GetChanges();
            Assert.AreEqual(2, Db.GetChanges().Count);
            var clients = await Db.Clients.ToListAsync();
            Assert.AreEqual(2, Db.GetChanges().Count);
            var client = clients.Single(_ => _.Id == 1);
            Assert.AreEqual("Changed", client.Name);
            Assert.AreEqual(2, Db.GetChanges().Count);
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
                Assert.AreEqual(2, changes.Count);
                var clientChange = changes.AllChanges.FirstOrDefault(_ => _.Operation.EntityType == typeof(Client));
                var clientTypeChange = changes.AllChanges.FirstOrDefault(_ => _.Operation.EntityType == typeof(ClientType));
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
            var json = Db.DataStore.SerializeStateToJson();
            var normalizedJson = json.NormalizeJsonNoNulls();
#if !TypeScript
            Assert.AreEqual(@"[{""Type"":""ApplicationUser"",""Entities"":[{""Id"":""user1"",""IsLockedOut"":false,""ClientId"":1,""Permissions"":""0"",""UserType"":""0"",""FullName"":null,""UserName"":""User 1"",""EmailConfirmed"":false,""PhoneNumberConfirmed"":false,""TwoFactorEnabled"":false,""LockoutEnabled"":false},{""Id"":""user2"",""IsLockedOut"":false,""ClientId"":2,""Permissions"":""0"",""UserType"":""0"",""FullName"":null,""UserName"":""User 2"",""EmailConfirmed"":false,""PhoneNumberConfirmed"":false,""TwoFactorEnabled"":false,""LockoutEnabled"":false},{""Id"":""user3"",""IsLockedOut"":false,""ClientId"":3,""Permissions"":""0"",""UserType"":""0"",""FullName"":null,""UserName"":""User 3"",""EmailConfirmed"":false,""PhoneNumberConfirmed"":false,""TwoFactorEnabled"":false,""LockoutEnabled"":false}]},{""Type"":""Client"",""Entities"":[{""Id"":1,""TypeId"":1,""CreatedByUserId"":""user1"",""AverageSales"":0,""AverageIncome"":12,""Category"":0,""Discount"":0,""Name"":""Coca-Cola"",""Guid"":""00000000-0000-0000-0000-000000000000"",""CreatedDate"":""0001-01-01T00:00:00.0+00:00"",""PersistenceKey"":""00000000-0000-0000-0000-000000000000""},{""Id"":2,""TypeId"":1,""CreatedByUserId"":""user2"",""AverageSales"":0,""AverageIncome"":33,""Category"":0,""Discount"":0,""Name"":""Pepsi"",""Guid"":""00000000-0000-0000-0000-000000000000"",""CreatedDate"":""0001-01-01T00:00:00.0+00:00"",""PersistenceKey"":""00000000-0000-0000-0000-000000000000""},{""Id"":3,""TypeId"":2,""CreatedByUserId"":""user3"",""AverageSales"":0,""AverageIncome"":97,""Category"":0,""Discount"":0,""Name"":""Microsoft"",""Guid"":""00000000-0000-0000-0000-000000000000"",""CreatedDate"":""0001-01-01T00:00:00.0+00:00"",""PersistenceKey"":""00000000-0000-0000-0000-000000000000""}]},{""Type"":""ClientType"",""Entities"":[{""Id"":1,""Name"":""Beverages""},{""Id"":2,""Name"":""Software""}]},{""Type"":""PersonType"",""Entities"":[]},{""Type"":""Site"",""Entities"":[{""Id"":0,""Location"":{""type"":""Point"",""coordinates"":[13.2846516,52.5069704]},""Name"":""Berlin"",""Level"":0,""Left"":0,""Right"":0,""Guid"":""00000000-0000-0000-0000-000000000000"",""CreatedDate"":""0001-01-01T00:00:00.0+00:00"",""PersistenceKey"":""00000000-0000-0000-0000-000000000000""}]}]",
                normalizedJson);
#else
            Assert.AreEqual(@"[{""Type"":""ApplicationUser"",""Entities"":[{""Id"":""user1"",""IsLockedOut"":false,""ClientId"":1,""Permissions"":""0"",""UserType"":""0"",""UserName"":""User 1"",""EmailConfirmed"":false,""PhoneNumberConfirmed"":false,""TwoFactorEnabled"":false,""LockoutEnabled"":false},{""Id"":""user2"",""IsLockedOut"":false,""ClientId"":2,""Permissions"":""0"",""UserType"":""0"",""UserName"":""User 2"",""EmailConfirmed"":false,""PhoneNumberConfirmed"":false,""TwoFactorEnabled"":false,""LockoutEnabled"":false},{""Id"":""user3"",""IsLockedOut"":false,""ClientId"":3,""Permissions"":""0"",""UserType"":""0"",""UserName"":""User 3"",""EmailConfirmed"":false,""PhoneNumberConfirmed"":false,""TwoFactorEnabled"":false,""LockoutEnabled"":false}]},{""Type"":""Client"",""Entities"":[{""Id"":1,""TypeId"":1,""CreatedByUserId"":""user1"",""AverageSales"":0,""AverageIncome"":12,""Category"":0,""Discount"":0,""Name"":""Coca-Cola"",""Guid"":""00000000-0000-0000-0000-000000000000"",""CreatedDate"":""0001-01-01T00:00:00.0+00:00"",""PersistenceKey"":""00000000-0000-0000-0000-000000000000""},{""Id"":2,""TypeId"":1,""CreatedByUserId"":""user2"",""AverageSales"":0,""AverageIncome"":33,""Category"":0,""Discount"":0,""Name"":""Pepsi"",""Guid"":""00000000-0000-0000-0000-000000000000"",""CreatedDate"":""0001-01-01T00:00:00.0+00:00"",""PersistenceKey"":""00000000-0000-0000-0000-000000000000""},{""Id"":3,""TypeId"":2,""CreatedByUserId"":""user3"",""AverageSales"":0,""AverageIncome"":97,""Category"":0,""Discount"":0,""Name"":""Microsoft"",""Guid"":""00000000-0000-0000-0000-000000000000"",""CreatedDate"":""0001-01-01T00:00:00.0+00:00"",""PersistenceKey"":""00000000-0000-0000-0000-000000000000""}]},{""Type"":""ClientType"",""Entities"":[{""Id"":1,""Name"":""Beverages""},{""Id"":2,""Name"":""Software""}]},{""Type"":""PersonType"",""Entities"":[]},{""Type"":""Site"",""Entities"":[{""Id"":0,""Location"":{""type"":""Point"",""coordinates"":[13.2846516,52.5069704]},""Name"":""Berlin"",""Level"":0,""Left"":0,""Right"":0,""Guid"":""00000000-0000-0000-0000-000000000000"",""CreatedDate"":""0001-01-01T00:00:00.0+00:00"",""PersistenceKey"":""00000000-0000-0000-0000-000000000000""}]}]",
                normalizedJson);
#endif
            var sets = JsonDataSerializer.DeserializeEntitySets(Db.EntityConfigurationContext, json);
            Assert.AreEqual(5, sets.Count);
            var clients = sets.Set<Client>();
            Assert.AreEqual(3, clients.Count);
            Assert.AreEqual("Coca-Cola", clients[0].Name);
            var sites = sets.Set<Site>();
            var site = sites.First();
            Assert.AreEqual(13.2846516, site.Location.X);
            Assert.AreEqual(52.5069704, site.Location.Y);
            // DONE: Check Geographic properties serialize correctly
            // TODO: Create "Restore" method on DataStore
            // TODO: OfflineDataStore2 should persist to file the state upon every change commit
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
        public async Task GetDataWhenOnlineAndRefetchWhenOfflineWithOfflineDisabledInQueryableShouldYieldNoResults()
        {
            Db.IsOffline = true;
            var clientsOffline1 = await Db.Clients.ToListAsync();
            Assert.AreEqual(0, clientsOffline1.Count);

            Db.IsOffline = false;
            var clients = await Db.Clients.Expand(_ => _.Type).ToListAsync();
            Assert.AreEqual(3, clients.Count);

            Db.IsOffline = true;
            var dbClients = Db.Clients;
            var clientsOffline2 = await dbClients.NoOffline().Expand(_ => _.Type).ToListAsync();
            Assert.IsFalse(clientsOffline2.Success);
            Assert.AreEqual(0, clientsOffline2.Count);
        }

        [TestMethod]
        public async Task GetExpandedDataWhenOnlineAndRefetchWhenOffline()
        {
            Db.IsOffline = true;
            var clientsOffline1 = await Db.Clients.ToListAsync();
            Assert.AreEqual(0, clientsOffline1.Count);

            Db.IsOffline = false;
            var clients = await Db.Clients.Expand(_ => _.Type).ToListAsync();
            Assert.AreEqual(3, clients.Count);
            var client = clients.First();
            Assert.IsNotNull(client.Type);
            Assert.AreEqual(client.TypeId, client.Type.Id);

            Db.IsOffline = true;
            var clientsOffline2 = await Db.Clients.Expand(_ => _.Type).ToListAsync();
            Assert.AreEqual(3, clientsOffline2.Count);
        }

        [TestMethod]
        public async Task GetDataWithNestedFilterWhenOnlineAndRefetchWhenOfflineButWithoutSupportingData()
        {
            Db.IsOffline = true;
            var clientsOffline1 = await Db.Clients.Where(_ => _.CreatedByUser.Client.Type.Name.Contains("Beverages")).ToListAsync();
            Assert.AreEqual(0, clientsOffline1.Count);

            Db.IsOffline = false;
            var clients = await Db.Clients.Where(_ => _.CreatedByUser.Client.Type.Name.Contains("Beverages")).ToListAsync();
            Assert.AreEqual(2, clients.Count);

            Db.IsOffline = true;
            var clientsOffline2 = await Db.Clients.Where(_ => _.CreatedByUser.Client.Type.Name.Contains("Beverages")).ToListAsync();
            Assert.AreEqual(0, clientsOffline2.Count);
        }

        [TestMethod]
        public async Task GetDataWithNestedFilterWhenOnlineAndRefetchWhenOfflineButWithSupportingData()
        {
            Db.IsOffline = true;
            var clientsOffline1 = await Db.Clients.Where(_ => _.CreatedByUser.Client.Type.Name.Contains("Beverages")).ToListAsync();
            Assert.AreEqual(0, clientsOffline1.Count);

            Db.IsOffline = false;
            var clients = await Db.Clients.Where(_ => _.CreatedByUser.Client.Type.Name.Contains("Beverages")).ToListAsync();
            var users = await Db.Users.ToListAsync();
            var clientTypes = await Db.ClientTypes.ToListAsync();
            Assert.AreEqual(2, clients.Count);

            Db.IsOffline = true;
            var clientsOffline2 = await Db.Clients.Where(_ => _.CreatedByUser.Client.Type.Name.Contains("Beverages")).ToListAsync();
            Assert.AreEqual(2, clientsOffline2.Count);
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

            Assert.AreEqual(1, Db.GetChanges().Count);
            Assert.AreEqual(0, Db.GetOfflineChanges().Count);

            var result = await Db.SaveChangesAsync();
            Assert.IsTrue(result.Success);
            Assert.IsTrue(RemoteClientExists());
            Assert.AreEqual(0, Db.GetChanges().Count);
            Assert.AreEqual(1, Db.GetOfflineChanges().Count);

            Db.IsOffline = false;

            var offlineResyncResult = await Db.SaveOfflineChangesAsync();
            Assert.IsTrue(offlineResyncResult.Success);
            Assert.AreEqual(0, Db.GetChanges().Count);
            Assert.AreEqual(0, Db.GetOfflineChanges().Count);
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
            Assert.AreEqual(0, changes.Count);

            var offlineChanges = Db.GetOfflineChanges();
            Assert.AreEqual(0, offlineChanges.Count);

            // Go offline
            Db.IsOffline = true;
            var client = clients.First();

            // Change the "Name" property
            client.Name = "Changed";

            // Should be one change queued in the data context
            changes = Db.GetChanges();
            Assert.AreEqual(0, changes.Count);

            // Should be no changes in the offline context
            offlineChanges = Db.GetOfflineChanges();
            Assert.AreEqual(0, offlineChanges.Count);

            // Save changes should persist to offline data store and tracker
            var result = await Db.SaveChangesAsync();
            Assert.IsTrue(result.Success);

            // Should still be no temporal changes
            changes = Db.GetChanges();
            Assert.AreEqual(0, changes.Count);

            // As we are not tracking, there should be no changes propagated to offline
            offlineChanges = Db.GetOfflineChanges();
            Assert.AreEqual(0, offlineChanges.Count);
        }

        [TestMethod]
        public async Task AddingAnEntityWhenOnline()
        {
            var offlineDataSet = Db.OfflineInMemoryDataStore.DataSet<Client>();
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
        public async Task ResyncingAnEntityAddedWhenOffline()
        {
            var offlineDataSet = Db.OfflineInMemoryDataStore.DataSet<Client>();
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
            await Db.SaveOfflineStateAsync();
            await Db.ClearOfflineStateAsync();
            StaticPersistState.UseDummyState = true;
            await Db.RestoreOfflineStateAsync();
            StaticPersistState.UseDummyState = false;
            Db.IsOffline = false;
            result = await Db.SaveOfflineChangesAsync();
            Assert.IsTrue(result.Success);
            var client = onlineDataSet.SingleOrDefault(_ => _.Name == "New Client 123");
            Assert.IsNotNull(client);
        }

        [TestMethod]
        public async Task AddingAnEntityWhenOffline()
        {
            var offlineDataSet = Db.OfflineInMemoryDataStore.DataSet<Client>();
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
            Assert.AreEqual(1, offlineChanges.Count);
            Assert.IsTrue(offlineChanges.AllChanges[0].Kind == QueuedOperationKind.Add);

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
            var offlineDataSet = Db.OfflineInMemoryDataStore.DataSet<Client>();
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
        public async Task AddingARelatedEntityWhenOfflineShouldResyncNewEntityFirst()
        {
            var onlineDataSet = (Db.DataStore as IOfflineDataStore).DataSet<ClientType>();
            var offlineClientsDataSet = Db.OfflineInMemoryDataStore.DataSet<Client>();
            var offlineClientTypesDataSet = Db.OfflineInMemoryDataStore.DataSet<ClientType>();
            var client = await Db.Clients.FirstOrDefaultAsync();
            var offlineClient = offlineClientsDataSet.Single(_ => _.Id == 1);
            Assert.AreEqual(1, offlineClient.TypeId);
            // Go offline
            Db.IsOffline = true;

            var clientType = new ClientType
            {
                Name = "New related entity"
            };

            Db.ClientTypes.Add(clientType);
            client.Type = clientType;
            var changes = Db.GetChanges();
            Assert.AreEqual(changes.AllChanges[0].Kind, QueuedOperationKind.Add, "Add operation should come first");
            Assert.AreEqual(changes.AllChanges[1].Kind, QueuedOperationKind.Update, "Update operation should come second");
            var result = await Db.SaveChangesAsync();
            Assert.IsTrue(result.Success);
            Assert.AreEqual(-1, clientType.Id);
            Assert.AreEqual(-1, client.TypeId);
            Assert.AreEqual(-1, offlineClient.TypeId);
            Db.IsOffline = false;
            changes = Db.GetOfflineChanges();
            Assert.AreEqual(changes.AllChanges[0].Kind, QueuedOperationKind.Add, "Add operation should come first");
            Assert.AreEqual(changes.AllChanges[1].Kind, QueuedOperationKind.Update, "Update operation should come second");
            result = await Db.SaveOfflineChangesAsync();
            Assert.IsTrue(result.Success);
            Assert.AreEqual(3, clientType.Id);
            Assert.AreEqual(3, client.TypeId);
            Assert.AreEqual(3, offlineClient.TypeId);
            Assert.IsFalse(Db.HasOfflineChanges());
            Assert.IsNotNull(offlineClientTypesDataSet.Single(_ => _.Id == 3));
        }


        [TestMethod]
        public async Task SaveChangeWhenOfflineAndResyncWhenOnline()
        {
            var offlineDataSet = Db.OfflineInMemoryDataStore.DataSet<Client>();
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
            Assert.AreEqual(0, changes.Count);

            var offlineChanges = Db.GetOfflineChanges();
            Assert.AreEqual(0, offlineChanges.Count);

            // Go offline
            Db.IsOffline = true;
            var firstClient = clients.First();
            Assert.AreEqual(1, firstClient.Id);

            // Change the "Name" property
            var firstClientNewName = "Changed";
            firstClient.Name = firstClientNewName;

            // Should be one change queued in the data context
            changes = Db.GetChanges();
            Assert.AreEqual(1, changes.Count);
            AssertChangedProperties(changes.AllChanges, firstClient, nameof(Client.Name));

            // Should be no changes in the offline context
            offlineChanges = Db.GetOfflineChanges();
            Assert.AreEqual(0, offlineChanges.Count);

            // Save changes should persist to offline data store and tracker
            var result = await Db.SaveChangesAsync();
            Assert.IsTrue(result.Success);

            // Should be no more temporal changes
            changes = Db.GetChanges();
            Assert.AreEqual(0, changes.Count);

            // Changes should have moved to offline tracker
            offlineChanges = Db.GetOfflineChanges();
            Assert.AreEqual(1, offlineChanges.Count);

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
            Assert.AreEqual(1, offlineChanges.Count);
            AssertChangedProperties(offlineChanges.AllChanges, firstClient, nameof(Client.Name), nameof(Client.Guid), nameof(Client.PersistenceKey));
            Assert.AreEqual(firstClientNewName, offlineFirstClient.Name);

            // Make another change
            var firstClientNewTypeId = 2;
            firstClient.TypeId = firstClientNewTypeId;

            // Latest change should still be temporal
            offlineChanges = Db.GetOfflineChanges();
            Assert.AreEqual(1, offlineChanges.Count);
            AssertChangedProperties(offlineChanges.AllChanges, firstClient, nameof(Client.Name), nameof(Client.Guid), nameof(Client.PersistenceKey));

            // "TypeId" change should be in the temporal changes
            changes = Db.GetChanges();
            Assert.AreEqual(1, changes.Count);
            AssertChangedProperties(changes.AllChanges, firstClient, nameof(Client.TypeId));

            // Still offline, "TypeId" change should persist to offline
            result = await Db.SaveChangesAsync();
            Assert.IsTrue(result.Success);

            // Changes should be propagated to offline
            changes = Db.GetChanges();
            Assert.AreEqual(0, changes.Count);

            // Offline should now contain both "TypeId" and "Name" change
            offlineChanges = Db.GetOfflineChanges();
            Assert.AreEqual(1, offlineChanges.Count);
            AssertChangedProperties(offlineChanges.AllChanges, firstClient, nameof(Client.TypeId), nameof(Client.Name), nameof(Client.Guid), nameof(Client.PersistenceKey));

            // The online client's "Name" and "TypeId" should remain the same
            Assert.AreEqual(OfflineAppDbContext.Client1Name, onlineFirstClient.Name);
            Assert.AreEqual(OfflineAppDbContext.Client1TypeId, onlineFirstClient.TypeId);

            result = await Db.SaveChangesAsync();
            Assert.IsTrue(result.Success);

            // There should be no new temporal changes
            changes = Db.GetChanges();
            Assert.AreEqual(0, changes.Count);

            // There should be no new offline changes
            offlineChanges = Db.GetOfflineChanges();
            Assert.AreEqual(1, offlineChanges.Count);
            AssertChangedProperties(offlineChanges.AllChanges, firstClient, nameof(Client.TypeId), nameof(Client.Name), nameof(Client.Guid), nameof(Client.PersistenceKey));

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
            Assert.AreEqual(1, changes.Count);
            AssertChangedProperties(changes.AllChanges, secondClient, nameof(Client.Name));

            // There should be no new offline changes
            offlineChanges = Db.GetOfflineChanges();
            Assert.AreEqual(1, offlineChanges.Count);
            AssertChangedProperties(offlineChanges.AllChanges, firstClient, nameof(Client.TypeId), nameof(Client.Name), nameof(Client.Guid), nameof(Client.PersistenceKey));

            result = await Db.SaveChangesAsync();
            Assert.IsTrue(result.Success);

            // We should still be in offline mode, because we haven't resynced the offline changes
            // As such, there now should be no temporal changes but we should have a new offline change
            changes = Db.GetChanges();
            Assert.AreEqual(0, changes.Count);

            // There should be one new offline change
            offlineChanges = Db.GetOfflineChanges();
            Assert.AreEqual(2, offlineChanges.Count);
            AssertChangedProperties(offlineChanges.AllChanges, firstClient, nameof(Client.TypeId), nameof(Client.Name), nameof(Client.Guid), nameof(Client.PersistenceKey));
            AssertChangedProperties(offlineChanges.AllChanges, secondClient, nameof(Client.Name), nameof(Client.Guid), nameof(Client.PersistenceKey));

            Assert.AreEqual(OfflineAppDbContext.Client2Name, onlineSecondClient.Name);

            Assert.AreEqual(firstClientNewNameInternal, onlineFirstClient.Name);

            result = await Db.SaveOfflineChangesAsync();
            Assert.IsTrue(result.Success);

            Assert.AreEqual(firstClientNewName, onlineFirstClient.Name);
            Assert.AreEqual(firstClientNewTypeId, onlineFirstClient.TypeId);
            Assert.AreEqual(secondClientNewName, onlineSecondClient.Name);

            changes = Db.GetChanges();
            Assert.AreEqual(0, changes.Count);

            // There should be one new offline change
            offlineChanges = Db.GetOfflineChanges();
            Assert.AreEqual(0, offlineChanges.Count);
        }

        /* TODO:
         * Adding an entity
         * Deleting an entity
         */

        private void AssertChangedProperties(IQueuedOperation[] changes, object entity, params string[] properties)
        {
            var findEntityState = DataContext.FindEntityState(entity);
            var key = findEntityState.LocalKey;
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
