using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Iql.Data.Context;
using Iql.Data.Crud.Operations;
using Iql.Data.Http;
using Iql.Data.Lists;
using Iql.Entities.Exceptions;
using Iql.OData;
using Iql.Queryable.Extensions;
using Iql.Tests.Context;
using Iql.Tests.Data.Context;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IqlSampleApp.Data.Entities;

namespace Iql.Tests.Tests
{
    [TestClass]
    public class TrackingTests : TestsBase
    {
        [TestMethod]
        public async Task TestFetched()
        {
            AppDbContext.InMemoryDb.ClientTypes.Add(new ClientType
            {
                Id = 2,
                Name = "Test client type"
            });
            var fetchedCount = 0;
            var type = await Db.ClientTypes.Expand(_ => _.Clients).GetStateWithKeyAsync(2);
            type.Fetched.Subscribe(_ => { fetchedCount++; });
            var type2 = await Db.ClientTypes.Expand(_ => _.Clients).GetStateWithKeyAsync(2);
            Assert.AreEqual(type, type2);
            Assert.AreEqual(1, fetchedCount);
            await Db.RefreshEntityAsync(type.Entity);
            Assert.AreEqual(2, fetchedCount);
        }

        [TestMethod]
        public async Task TestHasNestedChanges()
        {
            AppDbContext.InMemoryDb.ClientTypes.Add(new ClientType
            {
                Id = 2,
                Name = "Test client type"
            });
            AppDbContext.InMemoryDb.Clients.Add(new Client
            {
                Id = 7,
                Name = "Test client",
                TypeId = 2,
                CreatedByUserId = "abc"
            });
            var type = await Db.ClientTypes.Expand(_ => _.Clients).GetStateWithKeyAsync(2);
            Assert.IsFalse(type.HasChanges);
            Assert.IsFalse(type.HasNestedChanges);
            Assert.IsFalse(type.HasAnyChanges);
            type.Entity.Name = "blah";
            Assert.IsTrue(type.HasChanges);
            Assert.IsFalse(type.HasNestedChanges);
            Assert.IsTrue(type.HasAnyChanges);
            Db.UndoChanges();
            Assert.IsFalse(type.HasChanges);
            Assert.IsFalse(type.HasNestedChanges);
            Assert.IsFalse(type.HasAnyChanges);
            type.Entity.Clients[0].Name = "blah";
            Assert.IsFalse(type.HasChanges);
            Assert.IsTrue(type.HasNestedChanges);
            Assert.IsTrue(type.HasAnyChanges);
            type.Entity.Name = "blah";
            Assert.IsTrue(type.HasChanges);
            Assert.IsTrue(type.HasNestedChanges);
            Assert.IsTrue(type.HasAnyChanges);
            Db.UndoChanges();
            Assert.IsFalse(type.HasChanges);
            Assert.IsFalse(type.HasNestedChanges);
            Assert.IsFalse(type.HasAnyChanges);
        }


        [TestMethod]
        public async Task TestHasNestedChangesWithUnwrappingSnapshots()
        {
            AppDbContext.InMemoryDb.ClientTypes.Add(new ClientType
            {
                Id = 2,
                Name = "Test client type"
            });
            AppDbContext.InMemoryDb.Clients.Add(new Client
            {
                Id = 7,
                Name = "Test client",
                TypeId = 2,
                CreatedByUserId = "abc"
            });
            var type = await Db.ClientTypes.Expand(_ => _.Clients).GetStateWithKeyAsync(2);
            Db.AddSnapshot();
            Assert.IsFalse(type.HasChanges);
            Assert.IsFalse(type.HasChangesSinceSnapshot);
            Assert.IsFalse(type.HasNestedChanges);
            Assert.IsFalse(type.HasNestedChangesSinceSnapshot);
            Assert.IsFalse(type.HasAnyChanges);
            Assert.IsFalse(type.HasAnyChangesSinceSnapshot);
            type.Entity.Name = "blah";
            Assert.IsTrue(type.HasChanges);
            Assert.IsTrue(type.HasChangesSinceSnapshot);
            Assert.IsFalse(type.HasNestedChanges);
            Assert.IsFalse(type.HasNestedChangesSinceSnapshot);
            Assert.IsTrue(type.HasAnyChanges);
            Assert.IsTrue(type.HasAnyChangesSinceSnapshot);
            Db.UndoChanges();
            Db.AddSnapshot();
            Assert.IsFalse(type.HasChanges);
            Assert.IsFalse(type.HasChangesSinceSnapshot);
            Assert.IsFalse(type.HasNestedChanges);
            Assert.IsFalse(type.HasNestedChangesSinceSnapshot);
            Assert.IsFalse(type.HasAnyChanges);
            Assert.IsFalse(type.HasAnyChangesSinceSnapshot);
            type.Entity.Clients[0].Name = "blah";
            Assert.IsFalse(type.HasChanges);
            Assert.IsFalse(type.HasChangesSinceSnapshot);
            Assert.IsTrue(type.HasNestedChanges);
            Assert.IsTrue(type.HasNestedChangesSinceSnapshot);
            Assert.IsTrue(type.HasAnyChanges);
            Assert.IsTrue(type.HasAnyChangesSinceSnapshot);
            type.Entity.Name = "blah";
            Assert.IsTrue(type.HasChanges);
            Assert.IsTrue(type.HasChangesSinceSnapshot);
            Assert.IsTrue(type.HasNestedChanges);
            Assert.IsTrue(type.HasNestedChangesSinceSnapshot);
            Assert.IsTrue(type.HasAnyChanges);
            Assert.IsTrue(type.HasAnyChangesSinceSnapshot);
            Db.RemoveLastSnapshot();
            Assert.IsTrue(type.HasChanges);
            Assert.IsTrue(type.HasChangesSinceSnapshot);
            Assert.IsTrue(type.HasNestedChanges);
            Assert.IsTrue(type.HasNestedChangesSinceSnapshot);
            Assert.IsTrue(type.HasAnyChanges);
            Assert.IsTrue(type.HasAnyChangesSinceSnapshot);
            Db.UndoChanges();
            Assert.IsFalse(type.HasChanges);
            Assert.IsFalse(type.HasChangesSinceSnapshot);
            Assert.IsFalse(type.HasNestedChanges);
            Assert.IsFalse(type.HasNestedChangesSinceSnapshot);
            Assert.IsFalse(type.HasAnyChanges);
            Assert.IsFalse(type.HasAnyChangesSinceSnapshot);
        }

        [TestMethod]
        public async Task TestChangeLocalPropertyEventIsFiredWhenEntityIsUpdated()
        {
            AppDbContext.InMemoryDb.Clients.Add(new Client
            {
                Id = 7,
                Name = "Test client",
                TypeId = 2
            });
            var clientState = await Db.Clients.GetStateWithKeyAsync(7);
            var nameState = clientState.GetPropertyState(nameof(Client.Name));
            var localValueChangedCount = 0;
            var entityPropertyLocalValueChangedCount = 0;
            Tuple<string, string> localValueChangedLog = null;
            Tuple<string, string> entityPropertyLocalValueChangedLog = null;
            nameState.LocalValueChanged.Subscribe(_ =>
            {
                localValueChangedCount++;
                localValueChangedLog = new Tuple<string, string>((string)_.NewValue, clientState.Entity.Name);
            });
            clientState.PropertyLocalValueChanged.Subscribe(_ =>
            {
                entityPropertyLocalValueChangedCount++;
                entityPropertyLocalValueChangedLog = new Tuple<string, string>((string)_.NewValue, clientState.Entity.Name);
            });
            nameState.LocalValue = "New value 1";
            Assert.AreEqual(1, localValueChangedCount);
            Assert.AreEqual(1, entityPropertyLocalValueChangedCount);
            Assert.AreEqual(localValueChangedLog.Item1, localValueChangedLog.Item2, "Property event has wrong value");
            Assert.AreEqual(entityPropertyLocalValueChangedLog.Item1, entityPropertyLocalValueChangedLog.Item2, "Entity event has wrong value");
            localValueChangedLog = null;
            entityPropertyLocalValueChangedLog = null;
            clientState.Entity.Name = "New value 2";
            Assert.AreEqual(2, localValueChangedCount);
            Assert.AreEqual(2, entityPropertyLocalValueChangedCount);
            Assert.AreEqual(localValueChangedLog.Item1, localValueChangedLog.Item2, "Entity property event has wrong value");
            Assert.AreEqual(entityPropertyLocalValueChangedLog.Item1, entityPropertyLocalValueChangedLog.Item2, "Entity event has wrong value");
        }

        [TestMethod]
        public async Task TestGetDataContextForUntrackedEntity()
        {
            AppDbContext.InMemoryDb.Clients.Add(new Client
            {
                Id = 7,
                Name = "Test client",
                TypeId = 2
            });
            var client = await Db.Clients.NoTracking().GetWithKeyAsync(7);
            var db = DataContext.FindDataContextForEntity(client);
            Assert.AreEqual(db, Db);
        }

        [TestMethod]
        public async Task TestEntityStateChangedEvents()
        {
            AppDbContext.InMemoryDb.Clients.Add(new Client
            {
                Id = 7,
                Name = "Test client",
                TypeId = 2
            });
            var client = await Db.Clients.GetWithKeyAsync(7);
            var state = Db.GetEntityState(client);
            Assert.IsFalse(state.HasChanges);
            var hasChangedChangedCount = 0;
            state.HasChangesChanged.Subscribe(_ =>
            {
                hasChangedChangedCount++;
            });
            Assert.AreEqual(0, hasChangedChangedCount);
            client.Name = "Blah";
            Assert.IsTrue(state.HasChanges);
            Assert.AreEqual(1, hasChangedChangedCount);
            client.Name = "Test client";
            Assert.IsFalse(state.HasChanges);
            Assert.AreEqual(2, hasChangedChangedCount);
        }

        [TestMethod]
        public async Task UpdatingNonNullableRemoteRelationshipFromSetToUnsetShouldMarkEntityForDelete()
        {
            AppDbContext.InMemoryDb.ClientTypes.Add(new ClientType
            {
                Id = 2,
                Name = "Test client type"
            });
            AppDbContext.InMemoryDb.Clients.Add(new Client
            {
                Id = 7,
                Name = "Test client",
                TypeId = 2,
                CreatedByUserId = "abc"
            });
            var client = await Db.Clients.Expand(_ => _.Type).GetWithKeyAsync(7);
            Assert.IsTrue(client.Type.Clients.Contains(client));
            var clientType = client.Type;
            var clientState = Db.GetEntityState(client);
            Assert.IsFalse(clientState.MarkedForAnyDeletion);
            client.TypeId = 0;
            Assert.AreEqual("abc", client.CreatedByUserId);
            var client2 = await Db.Clients.Expand(_ => _.Type).GetWithKeyAsync(7);
            Assert.IsFalse(clientType.Clients.Contains(client));
            Assert.AreEqual(client, client2);
            Assert.IsNull(client.Type);
            Assert.AreEqual(0, client.TypeId);
            Assert.IsTrue(clientState.MarkedForCascadeDeletion);
            Assert.AreEqual("abc", client.CreatedByUserId);
        }

        [TestMethod]
        public async Task UpdatingNullableRemoteRelationshipFromSetToUnsetShouldNotMarkEntityForDelete()
        {
            var dbPerson = new Person
            {
                Id = 1,
                Title = "Test",
                ClientId = 7
            };
            AppDbContext.InMemoryDb.People.Add(dbPerson);
            AppDbContext.InMemoryDb.Clients.Add(new Client
            {
                Id = 7,
                Name = "Test client"
            });
            var person = await Db.People.Expand(_ => _.Client).GetWithKeyAsync(1);
            Assert.IsTrue(person.Client.People.Contains(person));
            var client = person.Client;
            var personState = Db.GetEntityState(person);
            Assert.IsFalse(personState.MarkedForAnyDeletion);
            dbPerson.ClientId = null;
            var person2 = await Db.People.Expand(_ => _.Client).GetWithKeyAsync(1);
            Assert.IsFalse(client.People.Contains(person));
            Assert.AreEqual(person, person2);
            Assert.IsNull(person2.Client);
            Assert.IsNull(person2.ClientId);
            Assert.IsFalse(personState.MarkedForAnyDeletion);
        }

        [TestMethod]
        public async Task TestPropertyChangeEventOnEntityState()
        {
            AppDbContext.InMemoryDb.Clients.Add(new Client
            {
                Id = 7,
                Name = "Test client"
            });
            var client = await Db.Clients.GetWithKeyAsync(7);
            var state = Db.GetEntityState(client);
            var changeCount = 0;
            state.PropertyLocalValueChanged.Subscribe(e => { changeCount++; });
            client.AverageIncome = 7;
            Assert.AreEqual(1, changeCount);
            client.AverageIncome = 7;
            Assert.AreEqual(1, changeCount);
            client.AverageIncome = 8;
            Assert.AreEqual(2, changeCount);
            client.AverageIncome = 8;
            Assert.AreEqual(2, changeCount);
            client.Name = "New";
            Assert.AreEqual(3, changeCount);
            client.AverageIncome = 7;
            Assert.AreEqual(4, changeCount);
        }

        [TestMethod]
        public async Task RetrievingLatestDataFromRemoteDatabaseDoesNotOverrideLocalChanges()
        {
            var unchanged = "Unchanged";
            var item = new Person
            {
                Id = 1,
                Title = unchanged
            };
            AppDbContext.InMemoryDb.People.Add(item);
            AppDbContext.InMemoryDb.PeopleTypes.Add(new PersonType
            {
                Id = 7
            });
            var person = await Db.People.GetWithKeyAsync(1);
            Assert.AreEqual(unchanged, person.Title);
            var changed = "Changed";
            person.Title = changed;
            Assert.AreEqual(changed, person.Title);
            await Db.People.GetWithKeyAsync(1);
            Assert.AreEqual(changed, person.Title);
        }

        [TestMethod]
        public async Task AddingAnUntrackedEntityToANonNewTrackedRelatedListShouldSetTheRelationshipKey()
        {
            AppDbContext.InMemoryDb.People.Add(new Person
            {
                Id = 1
            });
            AppDbContext.InMemoryDb.PeopleTypes.Add(new PersonType
            {
                Id = 7
            });
            var person = await Db.People.GetWithKeyAsync(1);
            var personType = await Db.PersonTypes.GetWithKeyAsync(7);
            var personTypeMap = new PersonTypeMap
            {
                TypeId = personType.Id,
                //Type = personType,
                Person = person
            };
            personTypeMap.Person = person;
            person.Types.Add(personTypeMap);
            Assert.AreEqual(person.Id, personTypeMap.PersonId);
            Assert.AreEqual(person, personTypeMap.Person);
        }

        [TestMethod]
        public async Task
            AddingAnEntityToARelatedListWithTheRelationshipReferenceAlreadySetShouldStillPropogateKeyValues()
        {
            AppDbContext.InMemoryDb.PeopleTypes.Add(new PersonType
            {
                Id = 2
            });
            AppDbContext.InMemoryDb.People.Add(new Person
            {
                Id = 7
            });

            var person = await Db.People.GetWithKeyAsync(7);
            var map = new PersonTypeMap
            {
                Person = person,
                TypeId = 2
            };
            person.Types.Add(map);
            Assert.AreEqual(2, map.TypeId);
            Assert.AreEqual(7, map.PersonId);
        }

        [TestMethod]
        public async Task PopulatingAnEntityReferenceFromAServerRequestShouldNotResultInAPropertyChangedState()
        {
            AppDbContext.InMemoryDb.ClientTypes.Add(new ClientType
            {
                Id = 1
            });
            AppDbContext.InMemoryDb.Clients.Add(new Client
            {
                Id = 1,
                TypeId = 1
            });
            var client = await Db.Clients.GetWithKeyAsync(1);
            var clientType = await Db.ClientTypes.ToListAsync();
            var propertyState = Db.TemporalDataTracker.TrackingSet<Client>()
                .FindMatchingEntityState(client)
                .GetPropertyState(nameof(Client.Type));
            Assert.IsFalse(propertyState.HasChanges);
        }

        [TestMethod]
        public async Task ChangingRelationshipViaEntityReferenceShouldUpdatePropertyStateToChanged()
        {
            AppDbContext.InMemoryDb.ClientTypes.Add(new ClientType
            {
                Id = 1
            });
            AppDbContext.InMemoryDb.ClientTypes.Add(new ClientType
            {
                Id = 2
            });
            AppDbContext.InMemoryDb.Clients.Add(new Client
            {
                Id = 1,
                TypeId = 1
            });
            var client = await Db.Clients.GetWithKeyAsync(1);
            var clientType1 = await Db.ClientTypes.GetWithKeyAsync(1);
            var clientType2 = await Db.ClientTypes.GetWithKeyAsync(2);
            client.Type = clientType2;
            var referencePropertyState = Db.TemporalDataTracker.TrackingSet<Client>()
                .FindMatchingEntityState(client)
                .GetPropertyState(nameof(Client.Type));
            var referenceKeyPropertyState = Db.TemporalDataTracker.TrackingSet<Client>()
                .FindMatchingEntityState(client)
                .GetPropertyState(nameof(Client.TypeId));
            var changes = Db.GetChanges();
            Assert.IsTrue(referencePropertyState.HasChanges);
        }

        [TestMethod]
        public async Task AbandonOneChangeViaAbandonAllChanges()
        {
            AppDbContext.InMemoryDb.ClientTypes.Add(new ClientType
            {
                Id = 1
            });
            AppDbContext.InMemoryDb.ClientTypes.Add(new ClientType
            {
                Id = 2
            });
            AppDbContext.InMemoryDb.Clients.Add(new Client
            {
                Id = 1,
                TypeId = 1,
            });
            var client = await Db.Clients.GetWithKeyAsync(1);
            var clientTypes = await Db.ClientTypes.ToListAsync();
            clientTypes[1].Clients.Add(client);
            var changes = Db.GetChanges();
            Assert.AreEqual(1, changes.Count);
            Db.AbandonChanges();
            changes = Db.GetChanges();
            Assert.AreEqual(0, changes.Count);
        }

        [TestMethod]
        public async Task AbandonOneChangeViaAbandonChangesForEntity()
        {
            AppDbContext.InMemoryDb.ClientTypes.Add(new ClientType
            {
                Id = 1
            });
            AppDbContext.InMemoryDb.ClientTypes.Add(new ClientType
            {
                Id = 2
            });
            AppDbContext.InMemoryDb.Clients.Add(new Client
            {
                Id = 1,
                TypeId = 1,
            });
            var client = await Db.Clients.GetWithKeyAsync(1);
            var clientTypes = await Db.ClientTypes.ToListAsync();
            clientTypes[1].Clients.Add(client);
            var changes = Db.GetChanges();
            Assert.AreEqual(1, changes.Count);
            Db.AbandonChangesForEntity(client);
            changes = Db.GetChanges();
            Assert.AreEqual(0, changes.Count);
        }

        [TestMethod]
        public async Task AbandonMultipleChangesViaAbandonAllChanges()
        {
            AppDbContext.InMemoryDb.ClientTypes.Add(new ClientType
            {
                Id = 1
            });
            AppDbContext.InMemoryDb.ClientTypes.Add(new ClientType
            {
                Id = 2
            });
            AppDbContext.InMemoryDb.Clients.Add(new Client
            {
                Id = 1,
                TypeId = 1,
            });
            AppDbContext.InMemoryDb.Clients.Add(new Client
            {
                Id = 2,
                TypeId = 2,
            });
            AppDbContext.InMemoryDb.Sites.Add(new Site
            {
                Id = 1
            });
            var site = await Db.Sites.GetWithKeyAsync(1);
            site.CreatedByUser = new ApplicationUser();
            site.Name = "Some new completely changed name";
            var client1 = await Db.Clients.GetWithKeyAsync(1);
            var client2 = await Db.Clients.GetWithKeyAsync(2);
            var clientTypes = await Db.ClientTypes.ToListAsync();
            clientTypes[1].Clients.Add(client1);
            Assert.AreEqual(2, clientTypes[1].Clients.Count);
            clientTypes[0].Clients.Add(client2);
            Assert.AreEqual(1, clientTypes[0].Clients.Count);
            Assert.AreEqual(1, clientTypes[1].Clients.Count);
            var changes = Db.GetChanges();
            Assert.AreEqual(4, changes.Count);
            Db.AbandonChanges();
            changes = Db.GetChanges();
            Assert.AreEqual(0, changes.Count);
        }

        [TestMethod]
        public async Task
            AssigningAnEntityAToARelatedListOnEntityBThenRefreshingEntityAShouldKeepTheItemInTheRelatedListOnEntityA()
        {
            AppDbContext.InMemoryDb.ClientTypes.Add(new ClientType
            {
                Id = 1
            });
            AppDbContext.InMemoryDb.ClientTypes.Add(new ClientType
            {
                Id = 2
            });
            var clientRemoteName = "Client Remote";
            var clientRemote = new Client
            {
                Id = 1,
                TypeId = 1,
                Name = clientRemoteName
            };
            AppDbContext.InMemoryDb.Clients.Add(clientRemote);
            var client = await Db.Clients.GetWithKeyAsync(1);
            var relationship = Db.EntityConfigurationContext.EntityType<Client>()
                .FindPropertyByExpression(c => c.Type)
                .Relationship.ThisEnd;
            var key = relationship.GetCompositeKey(client, true);
            Assert.AreEqual(1, key.Keys[0].Value);
            var clientType2 = await Db.ClientTypes.GetWithKeyAsync(2);
            clientType2.Clients.Add(client);
            Assert.AreEqual(2, client.TypeId);
            key = relationship.GetCompositeKey(client, true);
            Assert.AreEqual(2, key.Keys[0].Value);
            Assert.AreEqual(1, clientType2.Clients.Count);
            Assert.AreEqual(clientRemoteName, clientRemote.Name);
            var clientRemoteNewName = "A new name";
            clientRemote.Name = clientRemoteNewName;
            var refreshedClient = await Db.Clients.GetWithKeyAsync(1);
            Assert.AreEqual(client, refreshedClient);
            Assert.AreEqual(2, refreshedClient.TypeId);
            Assert.AreEqual(1, clientType2.Clients.Count);
            Assert.AreEqual(clientRemoteNewName, refreshedClient.Name);
            Assert.AreEqual(client, refreshedClient);
        }

        [TestMethod]
        public async Task
            ChangingAValueLocallyAndThenRefreshingTheEntityShouldKeepTheChangedValueButUpdateTheRemoteValue()
        {
            AppDbContext.InMemoryDb.ClientTypes.Add(new ClientType
            {
                Id = 1
            });
            AppDbContext.InMemoryDb.ClientTypes.Add(new ClientType
            {
                Id = 2
            });
            var clientRemoteName = "Client Remote";
            var clientRemote = new Client
            {
                Id = 1,
                TypeId = 1,
                Name = clientRemoteName
            };
            AppDbContext.InMemoryDb.Clients.Add(clientRemote);
            var client = await Db.Clients.GetWithKeyAsync(1);
            client.Name = "My name";
            var state = Db.GetEntityState(client);
            var propertyState = state.GetPropertyState(nameof(Client.Name));
            Assert.AreEqual("Client Remote", propertyState.RemoteValue);
            Assert.AreEqual("My name", propertyState.LocalValue);
            clientRemote.Name = "New Remote Name";
            Assert.AreEqual("Client Remote", propertyState.RemoteValue);
            Assert.AreEqual("My name", propertyState.LocalValue);
            var hasChanged = false;
            propertyState.RemoteValueChanged.Subscribe(_ => { hasChanged = true; });
            await Db.Clients.GetWithKeyAsync(1);
            Assert.AreEqual("New Remote Name", propertyState.RemoteValue);
            Assert.AreEqual("My name", propertyState.LocalValue);
            Assert.IsTrue(propertyState.HasChanges);
            Assert.IsTrue(hasChanged);
        }

        [TestMethod]
        public async Task AssigningAnEntityAToARelatedListWithASelfReferentialConstraintOnEntityBThenRefreshingEntityAShouldKeepTheItemInTheRelatedListOnEntityA()
        {
            AppDbContext.InMemoryDb.Sites.Add(new Site
            {
                Id = 1
            });
            // Now try with a self referential constraint
            var site = await Db.Sites.GetWithKeyAsync(1);
            Assert.AreEqual(null, site.ParentId);
            Assert.AreEqual(0, site.Children.Count);
            site.Children.Add(site);
            Assert.AreEqual(1, site.ParentId);
            Assert.AreEqual(1, site.Children.Count);
            var refreshedSite = await Db.Sites.GetWithKeyAsync(1);
            Assert.AreEqual(1, site.ParentId);
            Assert.AreEqual(1, site.Children.Count);
            Assert.AreEqual(site, refreshedSite);
        }

        [TestMethod]
        public async Task AssigningAnEntityToARelatedListShouldUpdateTheEntitysRelationshipCompositeKeyCache()
        {
            AppDbContext.InMemoryDb.ClientTypes.Add(new ClientType
            {
                Id = 1
            });
            AppDbContext.InMemoryDb.ClientTypes.Add(new ClientType
            {
                Id = 2
            });
            AppDbContext.InMemoryDb.Clients.Add(new Client
            {
                Id = 1,
                TypeId = 1
            });
            var client = await Db.Clients.GetWithKeyAsync(1);
            var relationship = Db.EntityConfigurationContext.EntityType<Client>()
                .FindPropertyByExpression(c => c.Type)
                .Relationship.ThisEnd;
            var key = relationship.GetCompositeKey(client, true);
            Assert.AreEqual(1, key.Keys[0].Value);
            var clientType2 = await Db.ClientTypes.GetWithKeyAsync(2);
            clientType2.Clients.Add(client);
            key = relationship.GetCompositeKey(client, true);
            Assert.AreEqual(2, key.Keys[0].Value);
        }

        [TestMethod]
        public async Task SettingARelationshipToAnUnchangedValueShouldHaveNoEffect()
        {
            AppDbContext.InMemoryDb.People.Add(new Person
            {
                Id = 1,
                CreatedByUserId = "a"
            });
            AppDbContext.InMemoryDb.Users.Add(new ApplicationUser
            {
                Id = "a"
            });
            var person = await Db.People.WithKey(1).Expand(p => p.CreatedByUser).SingleAsync();
            Assert.AreEqual("a", person.CreatedByUserId);
            person.CreatedByUserId = "a";
            Assert.AreEqual("a", person.CreatedByUserId);
        }

        [TestMethod]
        public async Task SettingARelationshipKeyToAnUntrackedEntityKeyShouldLeaveTheKeyIntact()
        {
            AppDbContext.InMemoryDb.People.Add(new Person
            {
                Id = 1,
                CreatedByUserId = "a"
            });
            AppDbContext.InMemoryDb.Users.Add(new ApplicationUser
            {
                Id = "a"
            });
            var person = await Db.People.WithKey(1).Expand(p => p.CreatedByUser).SingleAsync();
            Assert.AreEqual("a", person.CreatedByUserId);
            person.CreatedByUserId = "b";
            Assert.AreEqual("b", person.CreatedByUserId);
        }

        [TestMethod]
        public async Task ShouldBeAbleToAddDifferentEntitiesWithSameKeyIfEntityIsNew()
        {
            AppDbContext.InMemoryDb.PeopleTypes.Add(new PersonType
            {
                Id = 1
            });
            var type = await Db.PersonTypes.GetWithKeyAsync(1);
            var person1 = new Person();
            var person2 = new Person();
            type.People.Add(person1);
            type.People.Add(person2);
            Assert.AreEqual(2, type.People.Count);
            Assert.AreEqual(type.People[0], person1);
            Assert.AreEqual(type.People[1], person2);
        }

        [TestMethod]
        public async Task RemovingAPivotEntityAndAddingAnEquivalentWithChangedPropertiesShouldThrowDuplicateKeyException()
        {
            AppDbContext.InMemoryDb.PeopleTypes.Add(new PersonType
            {
                Id = 1
            });
            AppDbContext.InMemoryDb.People.Add(new Person
            {
                Id = 1
            });
            AppDbContext.InMemoryDb.PeopleTypeMap.Add(new PersonTypeMap
            {
                PersonId = 1,
                TypeId = 1,
                Description = "Abc"
            });
            var person = await Db.People.WithKey(1).Expand(p => p.Types).SingleAsync();
            Assert.AreEqual(1, person.Types.Count);
            var typeMap = person.Types[0];
            person.Types.Remove(typeMap);
            Assert.AreEqual(0, person.Types.Count);
            var newEquivalentTypeMap = new PersonTypeMap
            {
                PersonId = 1,
                TypeId = 1,
                Description = "Def"
            };
            ShouldThrowException<DuplicateKeyException>(() =>
            {
                var addedMap = person.Types.Add(newEquivalentTypeMap);
            });
            //var newEquivalentTypeMapState = Db.DataStore.Tracking.TrackingSet<PersonTypeMap>()
            //    .FindMatchingEntityState(newEquivalentTypeMap);
            //Assert.AreEqual(addedMap, newEquivalentTypeMap);

            //var changes = Db.DataStore.GetChanges();
            //Assert.AreEqual(1, changes.Length, "Expecting only a single change");
            //Assert.AreEqual(changes[0].Type, QueuedOperationType.Update, "Queued operation is not an update operation");
            //var update = changes[0].Operation as IUpdateEntityOperation;
            //var changedProperties = update.EntityState.GetChangedProperties();
            //Assert.AreEqual(1, changedProperties.Length, "Only the 'Description' property should have changed");
            //var changedProperty = changedProperties[0];
            //Assert.AreEqual(nameof(PersonTypeMap.Description), changedProperty.Property.Name);
            //Assert.AreEqual("Abc", changedProperty.OldValue);
            //Assert.AreEqual("Def", changedProperty.NewValue);
        }

        //[TestMethod]
        //public async Task RepeatedRemovingAndAddingOfPivotShouldStillResultInNoChanges()
        //{
        //    AppDbContext.InMemoryDb.PeopleTypes.Add(new PersonType
        //    {
        //        Id = 1
        //    });
        //    AppDbContext.InMemoryDb.People.Add(new Person
        //    {
        //        Id = 1
        //    });
        //    PersonTypeMap newTypeMap()
        //    {
        //        return new PersonTypeMap
        //        {
        //            PersonId = 1,
        //            TypeId = 1
        //        };
        //    }
        //    AppDbContext.InMemoryDb.PeopleTypeMap.Add(newTypeMap());

        //    var person = await Db.People.WithKey(1).Expand(p => p.Types).SingleAsync();
        //    var typeMap = person.Types[0];
        //    person.Types.Remove(typeMap);

        //    var unusedMaps = new List<PersonTypeMap>();
        //    for (var i = 0; i < 10; i++)
        //    {
        //        var personTypeMap = newTypeMap();
        //        unusedMaps.Add(personTypeMap);
        //        person.Types.Add(personTypeMap);
        //        person.Types.Remove(personTypeMap);
        //    }

        //    var personTypeMapFinal = newTypeMap();
        //    person.Types.Add(personTypeMapFinal);

        //    var changes = Db.DataStore.Tracking.GetChanges().ToArray();
        //    Assert.AreEqual(0, changes.Length, "There should be no changes");

        //    unusedMaps[3].PersonId = 4;
        //    unusedMaps[3].Description = "Abc123";
        //    changes = Db.DataStore.Tracking.GetChanges().ToArray();
        //    Assert.AreEqual(1, changes.Length);
        //    var addEntityOperation = changes[0].Operation as IEntityCrudOperationBase;
        //    Assert.IsNotNull(addEntityOperation);
        //    Assert.AreEqual(OperationType.Add, addEntityOperation.Type);
        //    Assert.AreEqual(unusedMaps[3], addEntityOperation.Entity);
        //}

        [TestMethod]
        public async Task RemovingAPivotEntityAndAddingAnEquivalentAndSavingShouldKeepOriginalTracked()
        {
            AppDbContext.InMemoryDb.PeopleTypes.Add(new PersonType
            {
                Id = 1
            });
            AppDbContext.InMemoryDb.People.Add(new Person
            {
                Id = 1
            });
            AppDbContext.InMemoryDb.PeopleTypeMap.Add(new PersonTypeMap
            {
                PersonId = 1,
                TypeId = 1
            });
            var person = await Db.People.WithKey(1).Expand(p => p.Types).SingleAsync();
            Assert.AreEqual(1, person.Types.Count);
            var typeMap = person.Types[0];
            var typeMapState = Db.TemporalDataTracker.TrackingSet<PersonTypeMap>()
                .FindMatchingEntityState(typeMap);
            person.Types.Remove(typeMap);
            Assert.AreEqual(0, person.Types.Count);
            var newEquivalentTypeMap = new PersonTypeMap
            {
                PersonId = 1,
                TypeId = 1
            };
            typeMap.Description = "A new description for the old type map";
            ShouldThrowException<DuplicateKeyException>(() =>
            {
                var addedMap = person.Types.Add(newEquivalentTypeMap);
            });
            //Assert.AreEqual(addedMap, newEquivalentTypeMap);

            //var changes = Db.DataStore.GetChanges();
            //Assert.AreEqual(0, changes.Length, "Expecting no changes");

            //Assert.IsTrue(Db.DataStore.Tracking.IsTracked(typeMap, typeof(PersonTypeMap)), "Entity should still be tracked");
            //await Db.SaveChangesAsync();
            //Assert.IsTrue(Db.DataStore.Tracking.IsTracked(typeMap, typeof(PersonTypeMap)), "Entity should still be tracked");
            //typeMap.PersonId = 2;
            //changes = Db.DataStore.GetChanges();
            //Assert.AreEqual(1, changes.Length, "Expecting a single add change");
            //var addOperation = changes[0].Operation as IEntityCrudOperationBase;
            //Assert.IsNotNull(addOperation);
            //Assert.AreEqual(OperationType.Add, addOperation.Type);
            //Assert.AreEqual(typeMap, addOperation.Entity);
        }

        [TestMethod]
        public async Task DetermineIfNonTrackedEntitiesAreTracked()
        {
            AppDbContext.InMemoryDb.PeopleTypes.Add(new PersonType
            {
                Id = 1
            });
            AppDbContext.InMemoryDb.People.Add(new Person
            {
                Id = 1
            });
            AppDbContext.InMemoryDb.PeopleTypeMap.Add(new PersonTypeMap
            {
                PersonId = 1,
                TypeId = 1
            });
            var person = await Db.People.NoTracking().WithKey(1).Expand(p => p.Types).SingleAsync();
            Assert.IsFalse(Db.IsTracked(person));
            var entityState = DataContext.FindEntityState(person);
            Assert.IsNotNull(entityState);
        }

        [TestMethod]
        public async Task GetSingleState()
        {
            AppDbContext.InMemoryDb.PeopleTypes.Add(new PersonType
            {
                Id = 1
            });
            AppDbContext.InMemoryDb.People.Add(new Person
            {
                Id = 1
            });
            AppDbContext.InMemoryDb.PeopleTypeMap.Add(new PersonTypeMap
            {
                PersonId = 1,
                TypeId = 1
            });
            var person = await Db.People.NoTracking().WithKey(1).Expand(p => p.Types).SingleStateAsync();
            Assert.IsFalse(person.IsTracked);
            var personTracked = await Db.People.WithKey(1).Expand(p => p.Types).SingleStateAsync();
            Assert.IsTrue(personTracked.IsTracked);
        }

        [TestMethod]
        public async Task GetEntityStatesNonTracked()
        {
            AppDbContext.InMemoryDb.PeopleTypes.Add(new PersonType
            {
                Id = 1
            });
            AppDbContext.InMemoryDb.People.Add(new Person
            {
                Id = 1
            });
            AppDbContext.InMemoryDb.PeopleTypeMap.Add(new PersonTypeMap
            {
                PersonId = 1,
                TypeId = 1
            });
            var persons = await Db.People.NoTracking().WithKey(1).Expand(p => p.Types).ToListAsync();
            var states = persons.States();
            Assert.AreEqual(1, states.Length);
            Assert.IsFalse(states[0].IsTracked);
        }

        [TestMethod]
        public async Task GetEntityStatesTracked()
        {
            AppDbContext.InMemoryDb.PeopleTypes.Add(new PersonType
            {
                Id = 1
            });
            AppDbContext.InMemoryDb.People.Add(new Person
            {
                Id = 1
            });
            AppDbContext.InMemoryDb.PeopleTypeMap.Add(new PersonTypeMap
            {
                PersonId = 1,
                TypeId = 1
            });
            var persons = await Db.People.WithKey(1).Expand(p => p.Types).ToListAsync();
            var states = persons.States();
            Assert.AreEqual(1, states.Length);
            Assert.IsTrue(states[0].IsTracked);
        }

        [TestMethod]
        public async Task RemovingAPivotEntity()
        {
            AppDbContext.InMemoryDb.PeopleTypes.Add(new PersonType
            {
                Id = 1
            });
            AppDbContext.InMemoryDb.People.Add(new Person
            {
                Id = 1
            });
            AppDbContext.InMemoryDb.PeopleTypeMap.Add(new PersonTypeMap
            {
                PersonId = 1,
                TypeId = 1
            });
            var person = await Db.People.WithKey(1).Expand(p => p.Types).SingleAsync();
            Assert.AreEqual(1, person.Types.Count);
            var personTypeMap = person.Types[0];
            Db.Delete(personTypeMap);
            Assert.AreEqual(0, person.Types.Count);
            var result = await Db.SaveChangesAsync();
            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public async Task ChangingAPivotEntityRelationshipIdShouldChangeTheRelationshipProperty()
        {
            AppDbContext.InMemoryDb.PeopleTypes.Add(new PersonType
            {
                Id = 1
            });
            AppDbContext.InMemoryDb.People.Add(new Person
            {
                Id = 1
            });
            AppDbContext.InMemoryDb.People.Add(new Person
            {
                Id = 2
            });
            AppDbContext.InMemoryDb.PeopleTypeMap.Add(new PersonTypeMap
            {
                PersonId = 1,
                TypeId = 1
            });
            var person = await Db.People.WithKey(1).ExpandCollection(p => p.Types, types => types.Expand(t => t.Type)).SingleAsync();
            var people = await Db.People.ToListAsync();
            var person1 = people.Single(p => p.Id == 1);
            var person2 = people.Single(p => p.Id == 2);
            Assert.AreEqual(1, person.Types.Count);
            var mapping = person.Types[0];
            Assert.AreEqual(person1, mapping.Person);
            mapping.PersonId = 2;
            Assert.AreEqual(person2, mapping.Person);
            mapping.PersonId = 3;
            Assert.AreEqual(null, mapping.Person);
        }

        [TestMethod]
        public async Task RemovingAPivotEntityAndAddingAnUnchangedEquivalentShouldThrowDuplicateKeyException()
        {
            AppDbContext.InMemoryDb.PeopleTypes.Add(new PersonType
            {
                Id = 1
            });
            AppDbContext.InMemoryDb.People.Add(new Person
            {
                Id = 1
            });
            AppDbContext.InMemoryDb.PeopleTypeMap.Add(new PersonTypeMap
            {
                PersonId = 1,
                TypeId = 1
            });
            var person = await Db.People.WithKey(1).Expand(p => p.Types).SingleAsync();
            Assert.AreEqual(1, person.Types.Count);
            var typeMap = person.Types[0];
            person.Types.Remove(typeMap);
            var changes = Db.GetChanges();
            Assert.AreEqual(1, changes.Count, "Expecting a delete operation");
            Assert.AreEqual(QueuedOperationKind.Delete, changes.AllChanges[0].Kind, "Expecting a delete operation");

            Assert.AreEqual(0, person.Types.Count);
            var newEquivalentTypeMap = new PersonTypeMap
            {
                PersonId = 1,
                TypeId = 1
            };
            ShouldThrowException<DuplicateKeyException>(() =>
            {
                var addedMap = person.Types.Add(newEquivalentTypeMap);
            });
            //Assert.AreEqual(addedMap, newEquivalentTypeMap);

            //changes = Db.DataStore.GetChanges();
            //Assert.AreEqual(0, changes.Length, "Expecting no changes");
        }

        [TestMethod]
        public async Task PersistingAPivotDeleteShouldLeaveNoMoreActionsQueued()
        {
            AppDbContext.InMemoryDb.PeopleTypes.Add(new PersonType
            {
                Id = 1
            });
            AppDbContext.InMemoryDb.People.Add(new Person
            {
                Id = 1
            });
            AppDbContext.InMemoryDb.PeopleTypeMap.Add(new PersonTypeMap
            {
                PersonId = 1,
                TypeId = 1
            });
            var person = await Db.People.WithKey(1).Expand(p => p.Types).SingleAsync();
            Assert.AreEqual(1, person.Types.Count);
            var typeMap = person.Types[0];
            person.Types.Remove(typeMap);
            var changes = Db.GetChanges();
            Assert.AreEqual(1, changes.Count, "Expecting a delete operation");
            Assert.AreEqual(QueuedOperationKind.Delete, changes.AllChanges[0].Kind, "Expecting a delete operation");
            var result = await Db.SaveChangesAsync();
            changes = Db.GetChanges();
            Assert.AreEqual(0, changes.Count);
        }

        //[TestMethod]
        //public async Task RemovingAPivotEntityAndAddingAnUnchangedEquivalentThenChangingTheEquivalentKeyShouldThrowDuplicateKeyException()
        //{
        //    AppDbContext.InMemoryDb.PeopleTypes.Add(new PersonType
        //    {
        //        Id = 1
        //    });
        //    AppDbContext.InMemoryDb.People.Add(new Person
        //    {
        //        Id = 1
        //    });
        //    AppDbContext.InMemoryDb.PeopleTypeMap.Add(new PersonTypeMap
        //    {
        //        PersonId = 1,
        //        TypeId = 1
        //    });
        //    var person = await Db.People.WithKey(1).Expand(p => p.Types).SingleAsync();
        //    Assert.AreEqual(1, person.Types.Count);
        //    var typeMap = person.Types[0];
        //    person.Types.Remove(typeMap);
        //    var changes = Db.DataStore.GetChanges();
        //    Assert.AreEqual(1, changes.Length, "Expecting a delete operation");
        //    Assert.AreEqual(QueuedOperationType.Delete, changes[0].Type, "Expecting a delete operation");

        //    Assert.AreEqual(0, person.Types.Count);
        //    var newEquivalentTypeMap = new PersonTypeMap
        //    {
        //        PersonId = 1,
        //        TypeId = 1
        //    };
        //    var addedMap = person.Types.Add(newEquivalentTypeMap);
        //    Assert.AreEqual(addedMap, newEquivalentTypeMap);

        //    changes = Db.DataStore.GetChanges();
        //    Assert.AreEqual(0, changes.Length, "Expecting no changes");

        //    newEquivalentTypeMap.PersonId = 4;
        //    changes = Db.DataStore.GetChanges();
        //    Assert.AreEqual(1, changes.Length);
        //    var updateOperation = changes[0].Operation as IUpdateEntityOperation;
        //    Assert.IsNotNull(updateOperation);
        //    Assert.AreEqual(OperationType.Update, updateOperation.Type);
        //    var changedProperties = updateOperation.EntityState.GetChangedProperties();
        //    Assert.AreEqual(2, changedProperties.Length);
        //    var personIdPropertyState = changedProperties.FirstOrDefault(p => p.Property.Name == nameof(PersonTypeMap.PersonId));
        //    Assert.IsNotNull(personIdPropertyState);
        //    Assert.AreEqual(1, personIdPropertyState.OldValue);
        //    Assert.AreEqual(4, personIdPropertyState.NewValue);
        //    var personPropertyState = changedProperties.FirstOrDefault(p => p.Property.Name == nameof(PersonTypeMap.PersonId));
        //    Assert.IsNotNull(personPropertyState);
        //    Assert.AreEqual(1, personPropertyState.OldValue);
        //    Assert.AreEqual(4, personPropertyState.NewValue);
        //}

        [TestMethod]
        public async Task RemovingAPivotEntityAndAddingAnChangedEquivalentThenChangingTheEquivalentKeyToMatchTheRemovedPivotShouldThrowDuplicateKeyException()
        {
            var newEquivalentTypeMap = await SetupPivotTest();
            ShouldThrowException<DuplicateKeyException>(() =>
            {
                newEquivalentTypeMap.TypeId = 1;
            });
            //changes = Db.DataStore.GetChanges();
            //Assert.AreEqual(2, changes.Length);
            //Assert.AreEqual(QueuedOperationType.Delete, changes[0].Type);
            //Assert.AreEqual(QueuedOperationType.Add, changes[1].Type);
            //var result = await Db.SaveChangesAsync();
            //Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public async Task RemovingAPivotEntityAndAddingAnChangedEquivalentThenCommittingTheRemovalThenChangingTheEquivalentKeyToMatchTheRemovedPivotShouldNotThrowDuplicateKeyException()
        {
            var newEquivalentTypeMap = await SetupPivotTest();
            var result = await Db.SaveChangesAsync();
            newEquivalentTypeMap.TypeId = 1;
            //var result = await Db.SaveChangesAsync();
            Assert.IsTrue(result.Success);
        }

        private static async Task<PersonTypeMap> SetupPivotTest()
        {
            AppDbContext.InMemoryDb.PeopleTypes.Add(new PersonType
            {
                Id = 1
            });
            AppDbContext.InMemoryDb.People.Add(new Person
            {
                Id = 1
            });
            AppDbContext.InMemoryDb.PeopleTypeMap.Add(new PersonTypeMap
            {
                PersonId = 1,
                TypeId = 1
            });
            var person = await Db.People.WithKey(1).Expand(p => p.Types).SingleAsync();
            Assert.AreEqual(1, person.Types.Count);
            var typeMap = person.Types[0];
            person.Types.Remove(typeMap);
            var changes = Db.GetChanges();
            Assert.AreEqual(1, changes.Count, "Expecting a delete operation");
            Assert.AreEqual(QueuedOperationKind.Delete, changes.AllChanges[0].Kind, "Expecting a delete operation");

            Assert.AreEqual(0, person.Types.Count);
            var newEquivalentTypeMap = new PersonTypeMap
            {
                PersonId = 1,
                TypeId = 4
            };
            var addedMap = person.Types.Add(newEquivalentTypeMap);
            Assert.AreEqual(addedMap, newEquivalentTypeMap);

            changes = Db.GetChanges();
            Assert.AreEqual(2, changes.Count);
            Assert.AreEqual(2, changes.Count);
            Assert.AreEqual(QueuedOperationKind.Delete, changes.AllChanges[0].Kind);
            Assert.AreEqual(QueuedOperationKind.Add, changes.AllChanges[1].Kind);
            return newEquivalentTypeMap;
        }

        [TestMethod]
        public async Task RemovingAnEntityWhoseKeysAreRelationshipKeysShouldReturnOriginalKeyOnResolveKey()
        {
            AppDbContext.InMemoryDb.PeopleTypes.Add(new PersonType
            {
                Id = 1
            });
            AppDbContext.InMemoryDb.People.Add(new Person
            {
                Id = 1
            });
            AppDbContext.InMemoryDb.PeopleTypeMap.Add(new PersonTypeMap
            {
                PersonId = 1,
                TypeId = 1
            });
            var person = await Db.People.WithKey(1).Expand(p => p.Types).SingleAsync();
            Assert.AreEqual(1, person.Types.Count);
            var typeMap = person.Types[0];
            person.Types.Remove(typeMap);
            Assert.AreEqual(0, person.Types.Count);
            Assert.AreEqual(0, typeMap.PersonId);
            var entityState = Db.TemporalDataTracker.TrackingSet<PersonTypeMap>()
                .FindMatchingEntityState(typeMap);
            var currentKey = entityState.LocalKey;
            //var originalKey = entityState.RemoteKey;
            //Assert.AreEqual(1, originalKey.GetValue(nameof(PersonTypeMap.PersonId)));
            //Assert.AreEqual(1, originalKey.GetValue(nameof(PersonTypeMap.TypeId)));
            //Assert.AreEqual(0, currentKey.GetValue(nameof(PersonTypeMap.PersonId)));
            //Assert.AreEqual(1, currentKey.GetValue(nameof(PersonTypeMap.TypeId)));
        }


        [TestMethod]
        public async Task ShouldNotBeAbleToAddDifferentEntitiesWithSameKey()
        {
            AppDbContext.InMemoryDb.People.Add(new Person
            {
                Id = 1,
                TypeId = 1
            });
            AppDbContext.InMemoryDb.PeopleTypes.Add(new PersonType
            {
                Id = 1
            });
            var map1 = new PersonTypeMap
            {
                PersonId = 1,
                TypeId = 1
            };
            var map2 = new PersonTypeMap
            {
                PersonId = 1,
                TypeId = 1
            };
            var person = await Db.People.GetWithKeyAsync(1);
            person.Types.Add(map1);
            person.Types.Add(map2);
            Assert.AreEqual(1, person.Types.Count);
        }

        [TestMethod]
        public async Task ShouldNotBeAbleToAddDifferentEntitiesWithSameKeyToRelatedListWhenRelationshipKeyFormsKey()
        {
            AppDbContext.InMemoryDb.People.Add(new Person
            {
                Id = 1,
                TypeId = 1
            });
            AppDbContext.InMemoryDb.PeopleTypes.Add(new PersonType
            {
                Id = 1
            });
            var map1 = new PersonTypeMap
            {
                PersonId = 0,
                TypeId = 1
            };
            var map2 = new PersonTypeMap
            {
                PersonId = 0,
                TypeId = 1
            };
            var person = await Db.People.GetWithKeyAsync(1);
            person.Types.Add(map1);
            person.Types.Add(map2);
            Assert.AreEqual(1, person.Types.Count);
            Assert.AreEqual(map1, person.Types[0]);
        }

        [TestMethod]
        public async Task NoTracking()
        {
            AppDbContext.InMemoryDb.Clients.Add(new Client
            {
                Id = 1,
                Name = "First client"
            });
            AppDbContext.InMemoryDb.Clients.Add(new Client
            {
                Id = 2,
                Name = "Second client"
            });
            var db1 = new AppDbContext();

            var clientsNoTracking1 = await db1.Clients.SetTracking(false).ToListAsync();
            var clientsNoTracking2 = await db1.Clients.SetTracking(false).ToListAsync();
            var clientsWithTracking1 = await db1.Clients.ToListAsync();
            var clientsWithTracking2 = await db1.Clients.ToListAsync();

            Assert.AreEqual(2, clientsNoTracking1.Count);
            Assert.AreEqual(2, clientsNoTracking2.Count);
            Assert.AreEqual(2, clientsWithTracking1.Count);
            Assert.AreEqual(2, clientsWithTracking2.Count);

            Assert.AreNotEqual(clientsNoTracking1[0], clientsNoTracking2[0]);
            Assert.AreNotEqual(clientsNoTracking1[1], clientsNoTracking2[1]);
            Assert.AreEqual(clientsWithTracking1[0], clientsWithTracking2[0]);
            Assert.AreEqual(clientsWithTracking1[1], clientsWithTracking2[1]);
        }

        [TestMethod]
        public async Task NoTrackingWithExpands()
        {
            AppDbContext.InMemoryDb.People.Add(new Person
            {
                Id = 1,
                Title = "First person"
            });
            AppDbContext.InMemoryDb.People.Add(new Person
            {
                Id = 2,
                Title = "Second person"
            });
            AppDbContext.InMemoryDb.PeopleTypes.Add(new PersonType
            {
                Id = 1,
                Title = "First person type"
            });
            AppDbContext.InMemoryDb.PeopleTypeMap.Add(new PersonTypeMap
            {
                PersonId = 1,
                TypeId = 1
            });
            AppDbContext.InMemoryDb.PeopleTypeMap.Add(new PersonTypeMap
            {
                PersonId = 2,
                TypeId = 1
            });
            var db1 = new AppDbContext();

            var peopleNoTracking1 = await db1.People.Expand(p => p.Types)
                .SetTracking(false)
                .ToListAsync();
            var peopleNoTracking2 = await db1.People.Expand(p => p.Types)
                .SetTracking(false)
                .ToListAsync();

            Assert.AreEqual(2, peopleNoTracking1.Count);
            Assert.AreEqual(2, peopleNoTracking2.Count);

            Assert.AreEqual(1, peopleNoTracking1[0].Types.Count);
            Assert.AreEqual(1, peopleNoTracking1[1].Types.Count);
            Assert.AreEqual(1, peopleNoTracking2[0].Types.Count);
            Assert.AreEqual(1, peopleNoTracking2[1].Types.Count);

            Assert.AreNotEqual(peopleNoTracking1[0], peopleNoTracking2[0]);
            Assert.AreNotEqual(peopleNoTracking1[1], peopleNoTracking2[1]);

            Assert.AreNotEqual(peopleNoTracking1[0].Types[0], peopleNoTracking2[0].Types[0]);
            Assert.AreNotEqual(peopleNoTracking1[1].Types[0], peopleNoTracking2[1].Types[0]);
        }

        [TestMethod]
        public async Task AssigningATargetFromSourceShouldSetSourceId()
        {
            PrepInMemoryDatabaseForLoadRelationshipPropertyTests();
            var person = await Db.People.GetWithKeyAsync(7);
            Assert.IsNull(person.Type);
            await Db.People.LoadRelationshipAsync(person, c => c.Type);
            Assert.IsNotNull(person.Type);
            Assert.AreEqual(2, person.Type.Id);
        }

        [TestMethod]
        public async Task LoadingOneToManyTargetRelationshipProperty()
        {
            PrepInMemoryDatabaseForLoadRelationshipPropertyTests();
            var person = await Db.People.GetWithKeyAsync(7);
            Assert.IsNull(person.Type);
            await Db.People.LoadRelationshipAsync(person, c => c.Type);
            Assert.IsNotNull(person.Type);
            Assert.AreEqual(2, person.Type.Id);
        }

        public async Task<Client> PrepLoadRelationshipsAsync()
        {
            AppDbContext.InMemoryDb.Clients.Add(new Client
            {
                Id = 1,
                TypeId = 7,
                CreatedByUserId = "a"
            });
            var dbApplicationUser = new ApplicationUser
            {
                Id = "a",
                FullName = "Dynamically loaded user"
            };
            AppDbContext.InMemoryDb.Users.Add(dbApplicationUser);
            var dbClientType = new ClientType
            {
                Id = 7,
                Name = "Dynamically loaded client type"
            };
            AppDbContext.InMemoryDb.ClientTypes.Add(dbClientType);
            AppDbContext.InMemoryDb.Sites.Add(new Site
            {
                Id = 11,
                ClientId = 2,
                Name = "Site 1"
            });
            AppDbContext.InMemoryDb.Sites.Add(new Site
            {
                Id = 12,
                ClientId = 1,
                Name = "Site 2"
            });
            AppDbContext.InMemoryDb.Sites.Add(new Site
            {
                Id = 13,
                ClientId = 1,
                Name = "Site 3"
            });
            var client = await Db.Clients.GetWithKeyAsync(1);
            Assert.AreEqual(1, client.Id);
            Assert.IsNull(client.Type);
            Assert.IsNull(client.CreatedByUser);
            Assert.AreEqual(0, client.Sites.Count);
            Assert.AreEqual(7, client.TypeId);
            Assert.AreEqual("a", client.CreatedByUserId);
            return client;
        }

        [TestMethod]
        public async Task LoadAllRelationships()
        {
            var client = await PrepLoadRelationshipsAsync();
            var result = await Db.LoadAllRelationshipsAsync(client);
            Assert.AreEqual(7, result.Count);
            var clientConfig = Db.EntityConfigurationContext.EntityType<Client>();
            Assert.IsTrue(result.ContainsKey(clientConfig.FindPropertyByExpression(c => c.Users)));
            Assert.IsTrue(result.ContainsKey(clientConfig.FindPropertyByExpression(c => c.Sites)));
            Assert.IsTrue(result.ContainsKey(clientConfig.FindPropertyByExpression(c => c.Categories)));
            Assert.IsTrue(result.ContainsKey(clientConfig.FindPropertyByExpression(c => c.InferredPeople)));
            Assert.IsTrue(result.ContainsKey(clientConfig.FindPropertyByExpression(c => c.CreatedByUser)));
            Assert.IsTrue(result.ContainsKey(clientConfig.FindPropertyByExpression(c => c.Type)));
            Assert.IsTrue(result.ContainsKey(clientConfig.FindPropertyByExpression(c => c.People)));
            Assert.IsNotNull(client.Type);
            Assert.IsNotNull(client.CreatedByUser);
            Assert.AreEqual(7, client.Type.Id);
            Assert.AreEqual("a", client.CreatedByUser.Id);
            Assert.AreEqual("Dynamically loaded client type", client.Type.Name);
            Assert.AreEqual("Dynamically loaded user", client.CreatedByUser.FullName);
            Assert.AreEqual(2, client.Sites.Count);
            Assert.IsTrue(client.Sites.Any(s => s.Id == 12));
            Assert.IsTrue(client.Sites.Any(s => s.Id == 13));
        }

        [TestMethod]
        public async Task LoadCollectionRelationships()
        {
            var client = await PrepLoadRelationshipsAsync();
            var result = await Db.LoadAllRelationshipsAsync(client, LoadRelationshipMode.Collections);
            Assert.AreEqual(5, result.Count);
            var clientConfig = Db.EntityConfigurationContext.EntityType<Client>();
            Assert.IsTrue(result.ContainsKey(clientConfig.FindPropertyByExpression(c => c.Users)));
            Assert.IsTrue(result.ContainsKey(clientConfig.FindPropertyByExpression(c => c.InferredPeople)));
            Assert.IsTrue(result.ContainsKey(clientConfig.FindPropertyByExpression(c => c.Categories)));
            Assert.IsTrue(result.ContainsKey(clientConfig.FindPropertyByExpression(c => c.Sites)));
            Assert.IsTrue(result.ContainsKey(clientConfig.FindPropertyByExpression(c => c.People)));
            Assert.IsNull(client.Type);
            Assert.IsNull(client.CreatedByUser);
            Assert.AreEqual(2, client.Sites.Count);
            Assert.IsTrue(client.Sites.Any(s => s.Id == 12));
            Assert.IsTrue(client.Sites.Any(s => s.Id == 13));
        }

        [TestMethod]
        public async Task LoadReferenceRelationships()
        {
            var client = await PrepLoadRelationshipsAsync();
            var result = await Db.LoadAllRelationshipsAsync(client, LoadRelationshipMode.References);
            Assert.AreEqual(2, result.Count);
            var clientConfig = Db.EntityConfigurationContext.EntityType<Client>();
            Assert.IsTrue(result.ContainsKey(clientConfig.FindPropertyByExpression(c => c.CreatedByUser)));
            Assert.IsTrue(result.ContainsKey(clientConfig.FindPropertyByExpression(c => c.Type)));
            Assert.IsNotNull(client.Type);
            Assert.IsNotNull(client.CreatedByUser);
            Assert.AreEqual(7, client.Type.Id);
            Assert.AreEqual("a", client.CreatedByUser.Id);
            Assert.AreEqual("Dynamically loaded client type", client.Type.Name);
            Assert.AreEqual("Dynamically loaded user", client.CreatedByUser.FullName);
            Assert.AreEqual(0, client.Sites.Count);
        }

        [TestMethod]
        public async Task LoadingOneToManyTargetRelationshipPropertyWithExpand()
        {
            PrepInMemoryDatabaseForLoadRelationshipPropertyTests();
            var person = await Db.People.GetWithKeyAsync(7);
            Assert.IsNull(person.Type);
            await Db.People.LoadRelationshipAsync(person, c => c.Type, q => q.ExpandRelationship(nameof(PersonType.PeopleMap)));
            Assert.IsNotNull(person.Type);
            Assert.AreEqual(2, person.Type.Id);
            Assert.AreEqual(1, person.Types.Count);
        }

        [TestMethod]
        public async Task LoadingOneToManySourceRelationshipProperty()
        {
            PrepInMemoryDatabaseForLoadRelationshipPropertyTests();
            var type = await Db.PersonTypes.GetWithKeyAsync(2);
            Assert.AreEqual(0, type.People.Count);
            await Db.PersonTypes.LoadRelationshipAsync(type, c => c.People);
            Assert.AreEqual(3, type.People.Count);
            var personWithId7 = type.People.SingleOrDefault(c => c.Id == 7);
            Assert.IsNotNull(personWithId7);
            Assert.IsNotNull(type.People.SingleOrDefault(c => c.Id == 9));
            Assert.IsNotNull(type.People.SingleOrDefault(c => c.Id == 8));
            Assert.AreEqual(personWithId7.Type, type);
        }

        [TestMethod]
        public async Task LoadingOneToManyTargetRelationshipPropertyFromDataContext()
        {
            PrepInMemoryDatabaseForLoadRelationshipPropertyTests();
            var person = await Db.People.GetWithKeyAsync(7);
            Assert.IsNull(person.Type);
            await Db.LoadRelationshipAsync(person, c => c.Type);
            Assert.IsNotNull(person.Type);
            Assert.AreEqual(2, person.Type.Id);
        }

        [TestMethod]
        public async Task LoadingOneToManySourceRelationshipPropertyFromDataContext()
        {
            PrepInMemoryDatabaseForLoadRelationshipPropertyTests();
            var type = await Db.PersonTypes.GetWithKeyAsync(2);
            Assert.AreEqual(0, type.People.Count);
            await Db.LoadRelationshipAsync(type, c => c.People);
            Assert.AreEqual(3, type.People.Count);
            var personWithId7 = type.People.SingleOrDefault(c => c.Id == 7);
            Assert.IsNotNull(personWithId7);
            Assert.IsNotNull(type.People.SingleOrDefault(c => c.Id == 9));
            Assert.IsNotNull(type.People.SingleOrDefault(c => c.Id == 8));
            Assert.AreEqual(personWithId7.Type, type);
        }

        private static void PrepInMemoryDatabaseForLoadRelationshipPropertyTests()
        {
            AppDbContext.InMemoryDb.PeopleTypeMap.Add(new PersonTypeMap
            {
                PersonId = 7,
                TypeId = 2
            });
            AppDbContext.InMemoryDb.People.Add(new Person
            {
                Id = 7,
                TypeId = 2
            });
            AppDbContext.InMemoryDb.People.Add(new Person
            {
                Id = 8,
                TypeId = 2
            });
            AppDbContext.InMemoryDb.People.Add(new Person
            {
                Id = 9,
                TypeId = 2
            });
            AppDbContext.InMemoryDb.People.Add(new Person
            {
                Id = 9,
                TypeId = 3
            });
            AppDbContext.InMemoryDb.PeopleTypes.Add(new PersonType
            {
                Id = 2
            });
        }

        [TestMethod]
        public async Task InsertsWithNewDependenciesShouldBeInsertedInTheCorrectDependencyOrder()
        {
            var riskAssessment = new RiskAssessment();
            Db.RiskAssessments.Add(riskAssessment);
            var siteInspection = new SiteInspection();
            riskAssessment.SiteInspection = siteInspection;
            var user = new ApplicationUser();
            riskAssessment.SiteInspection.CreatedByUser = user;
            var operations = Db.TemporalDataTracker.GetChanges();
            Assert.AreEqual(3, operations.Count);
            var order = new object[] { user, siteInspection, riskAssessment };
            for (var i = 0; i < operations.Count; i++)
            {
                var operation = operations.AllChanges[i];
                Assert.AreEqual(order[i], (operation.Operation as IEntityCrudOperationBase).EntityState.Entity);
            }
        }

        [TestMethod]
        public async Task TestLocallyCreatedEntityBecomesEntityStateEntity()
        {
            var client = new Client();
            client.TypeId = 7;
            client.Name = "Locally created client";
            var clientOData = $@"  ""TypeId"": 1,
  ""Id"": 44,
  ""CreatedByUserId"": ""e7bbb8a0-c242-44f1-9e53-35b6aec9ebf3"",
  ""Name"": ""{client.Name}"",
  ""Description"": ""Description of: {client.Name}"",
  ""Guid"": ""3075f684-af2c-4d97-84f2-4fe90864216b"",
  ""CreatedDate"": ""2018-02-24T13:32:53.6865454Z"",
  ""Version"": 0,
  ""PersistenceKey"": ""baa8d299-57db-4839-8029-1c7ae30a24c1""
";
            await RequestLog.LogSessionAsync(async log =>
            {
                var db = new AppDbContext(new ODataDataStore());
                db.Clients.Add(client);
                await log.InterceptAsync((method, uri, request) =>
                    {
                        if (uri == "http://localhost:28000/odata/Clients(0)?$count=true")
                        {
                            return null;
                        }
                        return HttpResult.FromString($@"{{
  ""@odata.context"": ""http://josh-pc:58000/odata/$metadata#Clients/$entity"",
  {clientOData}
}}");
                    },
                    async () =>
                    {
                        await db.SaveChangesAsync();
                    });
                DbList<Client> clients = null;
                await log.InterceptAsync((method, uri, request) =>
                    {
                        return HttpResult.FromString($@"{{
  ""@odata.context"": ""http://localhost:28000/odata/$metadata#Clients"",
  ""@odata.count"": 1,
  ""value"": [
    {{
      {clientOData}
    }}
  ]
}}");
                    },
                    async () =>
                    {
                        clients = await db.Clients.ToListAsync();
                    });
                Assert.AreEqual(1, clients.Count);
                Assert.AreSame(client, clients[0]);
            });
        }

        [TestMethod]
        public async Task TestGetHazceptionNoExpands()
        {
            var db = new HazceptionDataContext();
            var examCandidateResults =
                await db
                    .ExamCandidateResults
                    .ToListAsync();
        }

        [TestMethod]
        public async Task TestGetHazceptionOneExpand()
        {
            var db = new HazceptionDataContext();
            var examCandidateResults =
                await db
                    .ExamCandidateResults
                    .Expand(e => e.Client)
                    .ToListAsync();
        }

        [TestMethod]
        public async Task MultipleDataContextsShouldReturnDifferentEntitiesForTheSameRequest()
        {
            var db1 = new HazceptionDataContext();
            var db2 = new HazceptionDataContext();
            var examCandidateResults1 =
                await db1
                    .ExamCandidateResults
                    .ToListAsync();
            var examCandidateResults2 =
                await db2
                    .ExamCandidateResults
                    .ToListAsync();
            Assert.AreEqual(examCandidateResults1.Count, examCandidateResults2.Count);
            for (var i = 0; i < examCandidateResults2.Count; i++)
            {
                var examCandidateResult1 = examCandidateResults1[i];
                var examCandidateResult2 = examCandidateResults2[i];
                Assert.AreNotSame(examCandidateResult1, examCandidateResult2);
            }
        }

        [TestMethod]
        public async Task TestGetHazceptionAllExpands()
        {
            var db = new HazceptionDataContext();
            try
            {
                var examCandidateResults =
                    await db
                        .ExamCandidateResults
                        //.Take(50)
                        .Expand(e => e.Client)
                        .Expand(e => e.Candidate)
                        .Expand(e => e.CreatedByUser)
                        .Expand(e => e.ExamCandidate)
                        .Expand(e => e.Video)
                        .Expand(e => e.Exam)
                        .Expand(e => e.Results)
                        //.ExpandAll()
                        .ToListAsync();
            }
            catch (Exception e)
            {

            }
        }

        [TestMethod]
        public void TestTrackingSpeed()
        {
            var date = DateTime.Now;
            for (var i = 0; i < 100; i++)
            {
                var clientType = new ClientType
                {
                    Name = $"Client type {i + 1}"
                };
                clientType.Clients.AddRange(new[]
                {
                    new Client {Name = $"Client {i + 1}"}
                });
                Db.ClientTypes.Add(clientType);
            }
            var time = DateTime.Now - date;
            var seconds = time.TotalSeconds;
        }
    }
}