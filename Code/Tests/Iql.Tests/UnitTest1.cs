using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Iql.DotNet;
using Iql.Queryable;
using Iql.Queryable.Data.Crud.Operations;
using Iql.Queryable.Data.Crud.Operations.Queued;
using Iql.Queryable.Data.Tracking;
using Iql.Tests.Context;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Iql.Tests
{
    [TestClass]
    public class UnitTest1
    {
        private static AppDbContext Db = new AppDbContext();

        [ClassInitialize]
        public static async Task SetUp(TestContext textContext)
        {
            IqlQueryableAdapter.ExpressionConverter = () => new ExpressionToIqlConverter();
        }

        [ClassCleanup]
        public static void CleanUp()
        {

        }

        [TestCleanup]
        public void TestCleanUp()
        {
            AppDbContext.InMemoryDb.ClientTypes.Clear();
            Db = new AppDbContext();
        }

        [TestMethod]
        public async Task AddingAnEntityPersistsToDb()
        {
            Assert.AreEqual(0, AppDbContext.InMemoryDb.ClientTypes.Count);
            Db.ClientTypes.Add(new ClientType() { Id = 1 });
            Assert.AreEqual(1, Db.DataStore.Queue.Count);
            await Db.SaveChanges();
            Assert.AreEqual(0, Db.DataStore.Queue.Count);
            Assert.AreEqual(1, AppDbContext.InMemoryDb.ClientTypes.Count);
        }

        [TestMethod]
        public async Task TestWithKey()
        {
            var id = 1;
            Db.ClientTypes.Add(new ClientType { Id = id });
            Db.ClientTypes.Add(new ClientType { Id = id + 1 });
            await Db.SaveChanges();
            var clientType = await Db.ClientTypes.WithKey(id);
            Assert.IsNotNull(clientType);
            Assert.AreEqual(clientType.Id, id);
        }

        [TestMethod]
        public async Task TestWhere()
        {
            var id = 1;
            Db.ClientTypes.Add(new ClientType { Id = id, Name = "First" });
            var id2 = id + 1;
            Db.ClientTypes.Add(new ClientType { Id = id2, Name = "Second" });
            await Db.SaveChanges();
            var clientType = await Db.ClientTypes.Where(ct => ct.Name == "Second").Single();
            Assert.IsNotNull(clientType);
            Assert.AreEqual(clientType.Id, id2);
        }

        [TestMethod]
        public async Task TestWhereDifferentCase()
        {
            var id = 1;
            Db.ClientTypes.Add(new ClientType { Id = id, Name = "First" });
            var id2 = id + 1;
            Db.ClientTypes.Add(new ClientType { Id = id2, Name = "Second" });
            await Db.SaveChanges();
            var clientType = await Db.ClientTypes.Where(ct => ct.Name == "SECOND").Single();
            Assert.IsNotNull(clientType);
            Assert.AreEqual(clientType.Id, id2);
        }

        [TestMethod]
        public async Task ChangingAnEntity()
        {
            var entity = new ClientType { Id = 2 };
            Db.ClientTypes.Add(entity);
            await Db.SaveChanges();
            var clientType = await Db.ClientTypes.First(ct => ct.Id == entity.Id);
            Assert.AreEqual(entity.Id, clientType.Id);
            var changes = Db.DataStore.GetChanges().ToList();
            Assert.AreEqual(0, changes.Count);
            clientType.Name = "Something else";
            changes = Db.DataStore.GetChanges().ToList();
            Assert.AreEqual(1, changes.Count);
            var change = changes[0];
            Assert.AreEqual(QueuedOperationType.Update, change.Type);
            var updateOperation = change as QueuedUpdateEntityOperation<ClientType>;
            Assert.IsNotNull(updateOperation);
            Assert.AreEqual(1, updateOperation.Operation.EntityState.ChangedProperties.Count);
            var property = updateOperation.Operation.EntityState.ChangedProperties[0];
            Assert.AreEqual(nameof(ClientType.Name), property.Property.Name);
            Assert.AreEqual(0, property.ChildChangedProperties.Count);
            Assert.AreEqual(0, property.EnumerableChangedProperties.Count);
        }

        [TestMethod]
        public async Task ChangingAnEntityWithTheSameValueShouldResultInNoUpdates()
        {
            var entity = new ClientType { Id = 2, Name = "Something else" };
            Db.ClientTypes.Add(entity);
            await Db.SaveChanges();
            var clientType = await Db.ClientTypes.First(ct => ct.Id == entity.Id);
            Assert.AreEqual(entity.Id, clientType.Id);
            var changes = Db.DataStore.GetChanges().ToList();
            Assert.AreEqual(0, changes.Count);
            clientType.Name = "Something else";
            changes = Db.DataStore.GetChanges().ToList();
            Assert.AreEqual(0, changes.Count);
        }

        [TestMethod]
        public async Task AddingANewEntityWithChildCollectionShouldResultInASingleAddEntityOperation()
        {
            var entity = new ClientType
            {
                Id = 2,
                Name = "Something else",
                Clients = new List<Client>(new[]
                {
                    new Client {Name = "Client 1"}
                })
            };
            Db.ClientTypes.Add(entity);
            var changes = Db.DataStore.GetChanges().ToList();
            Assert.AreEqual(0, changes.Count);
            var operations = Db.DataStore.Queue;
            Assert.AreEqual(1, operations.Count);
            var operation = operations[0] as QueuedAddEntityOperation<ClientType>;
            Assert.IsNotNull(operation);
            Assert.AreEqual(entity, operation.Operation.Entity);
            await Db.SaveChanges();
            Assert.AreEqual(0, Db.DataStore.Queue.Count);
            changes = Db.DataStore.GetChanges().ToList();
            Assert.AreEqual(0, changes.Count);
        }

        [TestMethod]
        public async Task AssigningChildToMulitpleParentsOnTheSameRelationshipShouldResultInDuplicateParentException()
        {
            // Create two new client types
            var clientType1 = new ClientType
            {
                Id = 2,
                Name = "Something else",
                Clients = new List<Client>(new[]
                {
                    new Client {Id = 1, Name = "Client 1"}
                })
            };
            Db.ClientTypes.Add(clientType1);
            Assert.AreEqual(clientType1.Clients[0].Type, clientType1);
            Assert.AreEqual(clientType1.Clients[0].Type.Id, clientType1.Clients[0].TypeId);
            var clientType2 = new ClientType
            {
                Id = 3,
                Name = "Another",
                Clients = new List<Client>(new[]
                {
                    new Client {Id = 2, Name = "Client 2"}
                })
            };
            Db.ClientTypes.Add(clientType2);
            var changes1 = Db.DataStore.GetChanges();
            var changes2 = Db.DataStore.GetChanges();
            await Db.SaveChanges();
            Db = new AppDbContext();
            var entity1 = await Db.ClientTypes.Expand(c => c.Clients).WithKey(2);
            var entity1a = await Db.ClientTypes.Expand(c => c.Clients).WithKey(2);
            var entity2 = await Db.ClientTypes.Expand(c => c.Clients).WithKey(3);
            var entity2a = await Db.ClientTypes.Expand(c => c.Clients).WithKey(3);
            // Assign two existing clients to the first client type
            entity1.Clients.Add(new Client { Id = 3, Name = "Client 1 b" });
            entity1.Clients.Add(new Client { Id = 4, Name = "Client 1 c" });
            // Assign a client from the first client type to the second
            entity1.Clients.Add(entity2.Clients[0]);
            /* Here's what should happen:
             * entity2.Clients[0] has two inferred TypeIds and as such an error should be thrown
             */
            List<IQueuedOperation> changes = null;
            var errorThrown = false;
            try
            {
                // Just getting the changes alone should trigger the error
                changes = Db.DataStore.GetChanges().ToList();
            }
            catch (DuplicateParentException e)
            {
                errorThrown = true;
            }
            Assert.IsTrue(errorThrown, "No error thrown for having a child entity belonging to multiple one-to-many collections.");
            errorThrown = false;
            try
            {
                // Attempting to save changes should also trigger the error
                await Db.SaveChanges();
            }
            catch (DuplicateParentException e)
            {
                errorThrown = true;
            }
            Assert.IsTrue(errorThrown, "No error thrown for having a child entity belonging to multiple one-to-many collections.");


            //entity2.Clients.RemoveAt(0);
            //changes = Db.DataStore.GetChanges().ToList();
            /* Here's what should happen:
             * entity2 should be updated internally only
             * entity2.Clients[0].TypeId should be set to 2
             * The TypeId for entity1's newest clients should be set to entity1.Id
             * An insert operation for client ID 3 and 4 each should be created
             * In total, three operations:
             * - One update of entity2.Clients[0]
             * - Two adds of the new clients
             */

        }

        [TestMethod]
        public async Task FetchingEntitiesWithANewDataContextShouldReturnDifferentObjectsThatWereInserted()
        {
            var clientTypes = AddClientTypes();
            await Db.SaveChanges();
            Db = new AppDbContext();
            var entity1 = await Db.ClientTypes.WithKey(2);
            var entity2 = await Db.ClientTypes.WithKey(3);
            Assert.AreNotEqual(entity1, clientTypes.ClientType1);
            Assert.AreNotEqual(entity2, clientTypes.ClientType2);
        }

        [TestMethod]
        public async Task FetchingEntitiesWithTheSameDataContextShouldReturnTheSameObjectsThatWereInserted()
        {
            var clientTypes = AddClientTypes();
            await Db.SaveChanges();
            var entity1 = await Db.ClientTypes.WithKey(2);
            var entity2 = await Db.ClientTypes.WithKey(3);
            Assert.AreEqual(entity1, clientTypes.ClientType1);
            Assert.AreEqual(entity2, clientTypes.ClientType2);
        }

        class ClientTypes
        {
            public ClientType ClientType1 { get; }
            public ClientType ClientType2 { get; }
            public ClientType ClientType3 { get; }
            public ClientType ClientType4 { get; }
            public ClientType ClientType5 { get; }

            public ClientTypes(ClientType clientType1, ClientType clientType2, ClientType clientType3, ClientType clientType4,
                ClientType clientType5)
            {
                ClientType1 = clientType1;
                ClientType2 = clientType2;
                ClientType3 = clientType3;
                ClientType4 = clientType4;
                ClientType5 = clientType5;
            }
        }
        private static ClientTypes AddClientTypes()
        {
            // Create two new client types
            var clientType1 = new ClientType
            {
                Id = 2,
                Name = "Something else",
                Clients = new List<Client>(new[]
                {
                    new Client {Id = 1, Name = "Client 1"}
                })
            };
            Db.ClientTypes.Add(clientType1);
            var clientType2 = new ClientType
            {
                Id = 3,
                Name = "Another",
                Clients = new List<Client>(new[]
                {
                    new Client {Id = 2, Name = "Client 2"}
                })
            };
            Db.ClientTypes.Add(clientType2);
            var clientType3 = new ClientType
            {
                Id = 41,
                Name = "Another",
                Clients = new List<Client>()
            };
            Db.ClientTypes.Add(clientType3);
            var clientType4 = new ClientType
            {
                Id = 42,
                Name = "A fourth",
                Clients = new List<Client>(new[]
                {
                    new Client {Id = 21, Name = "Client 21"}
                })
            };
            Db.ClientTypes.Add(clientType4);
            var clientType5 = new ClientType
            {
                Id = 43,
                Name = "A fifth",
                Clients = new List<Client>(new[]
                {
                    new Client {Id = 22, Name = "Client 22"},
                    new Client {Id = 23, Name = "Client 23"}
                })
            };
            Db.ClientTypes.Add(clientType5);
            return new ClientTypes(clientType1, clientType2, clientType3, clientType4, clientType5);
        }

        [TestMethod]
        public async Task AttemptingToAddAnEntityThatAlreadyExistsShouldNotThrowAnException()
        {
            var clientTypes = AddClientTypes();
            await Db.SaveChanges();
            var addEntityResult = Db.ClientTypes.Add(clientTypes.ClientType2);
            await Db.SaveChanges();
            Assert.IsNull(addEntityResult);
        }

        [TestMethod]
        public async Task AddingAnEntityWithAnAttachedEntityWithWithTheSameKeyAsAnExistingTrackedEntityShouldThrowException()
        {
            AddClientTypes();
            await Db.SaveChanges();
            var exceptionThrown = false;
            try
            {
                Db.Clients.Add(new Client { Id = 3, Name = "Client 1 b", Type = new ClientType { Id = 2, Name = "This should cause an error" } });
            }
            catch (EntityAlreadyTrackedException)
            {
                exceptionThrown = true;
            }
            Assert.IsTrue(exceptionThrown);
        }

        [TestMethod]
        public async Task AddingAnEntityWithAnAttachedEntityWithANewKeyShouldNotThrowException()
        {
            AddClientTypes();
            await Db.SaveChanges();
            var exceptionThrown = false;
            try
            {
                Db.Clients.Add(new Client { Id = 3, Name = "Client 1 b", Type = new ClientType { Id = 4, Name = "This should not cause an error" } });
            }
            catch (EntityAlreadyTrackedException)
            {
                exceptionThrown = true;
            }
            Assert.IsFalse(exceptionThrown);
        }

        [TestMethod]
        public async Task AddingAnEntityWithAnAttachedEntityWithWithNoKeyKeyShouldNotThrowException()
        {
            AddClientTypes();
            await Db.SaveChanges();
            var exceptionThrown = false;
            try
            {
                Db.Clients.Add(new Client { Id = 3, Name = "Client 1 b", Type = new ClientType { Name = "This should not cause an error" } });
            }
            catch (EntityAlreadyTrackedException)
            {
                exceptionThrown = true;
            }
            Assert.IsFalse(exceptionThrown);
        }

        [TestMethod]
        public async Task ChangingAChildObjectsParentIdForOneToManyShouldSanitiseTheParentObjectsCollection1()
        {
            var clientTypes = AddClientTypes();
            await Db.SaveChanges();
            var type1Client = clientTypes.ClientType1.Clients[0];
            type1Client.TypeId = clientTypes.ClientType2.Id;
            // Trigger sanitisation
            var changes = Db.DataStore.GetChanges().ToList();
            Assert.AreEqual(type1Client.Type, clientTypes.ClientType2);
            Assert.IsTrue(clientTypes.ClientType2.Clients.Count == 2);
            Assert.IsTrue(clientTypes.ClientType2.Clients.Contains(type1Client));
            Assert.IsTrue(clientTypes.ClientType1.Clients.Count == 0);
        }

        [TestMethod]
        public async Task ChangingAChildObjectsParentIdForOneToManyShouldSanitiseTheParentObjectsCollection2()
        {
            var clientTypes = AddClientTypes();
            await Db.SaveChanges();
            var type1Client = clientTypes.ClientType1.Clients[0];
            type1Client.TypeId = clientTypes.ClientType2.Id;
            var type4Client = clientTypes.ClientType4.Clients[0];
            type4Client.TypeId = clientTypes.ClientType2.Id;
            // Trigger sanitisation
            var changes = Db.DataStore.GetChanges().ToList();
            Assert.AreEqual(type1Client.Type, clientTypes.ClientType2);
            Assert.IsTrue(clientTypes.ClientType2.Clients.Count == 3);
            Assert.IsTrue(clientTypes.ClientType2.Clients.Contains(type1Client));
            Assert.IsTrue(clientTypes.ClientType1.Clients.Count == 0);
            Assert.IsTrue(clientTypes.ClientType4.Clients.Count == 0);
            Assert.IsTrue(clientTypes.ClientType5.Clients.Count == 2);
        }

        [TestMethod]
        public async Task ChangingAChildObjectsParentIdForOneToManyShouldSanitiseTheParentObjectsCollection3()
        {
            var clientType1 = new ClientType
            {
                Id = 2,
                Name = "Something else",
                Clients = new List<Client>(new[]
                {
                    new Client {Id = 1, Name = "Client 1"}
                })
            };
            Db.ClientTypes.Add(clientType1);
            var clientType2 = new ClientType
            {
                Id = 3,
                Name = "Another",
                Clients = new List<Client>()
            };
            Db.ClientTypes.Add(clientType2);
            await Db.SaveChanges();
            var newClient = new Client { Id = 2, Name = "Client 2", TypeId = 3 };
            Db.Clients.Add(newClient);
            //await Db.SaveChanges();
            Assert.AreEqual(1, clientType2.Clients.Count);
            Assert.AreEqual(newClient, clientType2.Clients[0]);
        }

        [TestMethod]
        public async Task ChangingAChildObjectsParentIdForOneToManyShouldSanitiseTheParentObjectsCollection4()
        {
            var siteInspection = new SiteInspection()
            {
                Id = 21,
                RiskAssessmentId = 41
            };
            var riskAssessment = new RiskAssessment()
            {
                Id = 41,
                SiteInspectionId = 21
            };
            Db.SiteInspections.Add(siteInspection);
            Db.RiskAssessments.Add(riskAssessment);
            Assert.AreEqual(riskAssessment, siteInspection.RiskAssessment);
            Assert.AreEqual(siteInspection, riskAssessment.SiteInspection);
        }

        [TestMethod]
        [ExpectedException(typeof(InconsistentRelationshipAssignmentException))]
        public async Task TwoInconsistentChangesToChildCollectionsShouldThrowInconsistentChangeException()
        {
            var clientTypes = AddClientTypes();
            await Db.SaveChanges();
            var type1Client = clientTypes.ClientType1.Clients[0];
            type1Client.TypeId = clientTypes.ClientType2.Id;
            clientTypes.ClientType3.Clients.Add(type1Client);
            // Trigger sanitisation
            var changes = Db.DataStore.GetChanges().ToList();
        }

        [TestMethod]
        [ExpectedException(typeof(InconsistentRelationshipAssignmentException))]
        public async Task AddingAnEntityToAChildCollectionShouldPersistRelationship()
        {
            var clientTypes = AddClientTypes();
            await Db.SaveChanges();
            var type1Client = clientTypes.ClientType1.Clients[0];
            clientTypes.ClientType3.Clients.Add(type1Client);
            // Trigger sanitisation
            var changes = Db.DataStore.GetChanges().ToList();
        }

        [TestMethod]
        [ExpectedException(typeof(InconsistentRelationshipAssignmentException))]
        public async Task ChangingBothARelationshipAndARelationshipKeyShouldThrowAnErrorIfNotConsistent()
        {
            var clientTypes = AddClientTypes();
            await Db.SaveChanges();
            var item1Client = clientTypes.ClientType1.Clients[0];
            item1Client.TypeId = clientTypes.ClientType2.Id;
            item1Client.Type = new ClientType
            {
                Id = 5
            };
            // Trigger sanitisation and exception
            Db.DataStore.GetChanges();
        }
    }
}
