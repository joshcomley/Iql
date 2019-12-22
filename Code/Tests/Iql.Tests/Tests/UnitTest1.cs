using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Iql.Data.Crud.Operations;
using Iql.Data.Crud.Operations.Queued;
using Iql.Data.Events;
using Iql.Data.Exceptions;
using Iql.Entities;
using Iql.Entities.Events;
using Iql.Events;
using Iql.Parsing;
using Iql.Queryable;
using Iql.Queryable.Operations;
using Iql.Tests.Context;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IqlSampleApp.Data.Entities;

namespace Iql.Tests.Tests
{
    [TestClass]
    public class UnitTest1 : TestsBase
    {
        [TestMethod]
        public async Task TestCreateReportCategory()
        {
            var reportCategory = new ReportCategory();
            reportCategory.Name = "Abc";
            Db.ReportCategories.Add(reportCategory);
            var result = await Db.SaveChangesAsync();
            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public async Task TestAbandonNewEntity()
        {
            var reportType = new ReportType();
            Db.ReportTypes.Add(reportType);
            Db.AbandonChangesForEntity(reportType);
            var reportCategory = new ReportCategory();
            reportCategory.Name = "Abc";
            Db.ReportCategories.Add(reportCategory);
            var result = await Db.SaveChangesAsync();
            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public async Task TestAbandonComplexTypeChange()
        {
            AppDbContext.InMemoryDb.Sites.Add(new Site
            {
                Id = 1235,
                Location = new IqlPointExpression(10, 20)
            });
            var site = await Db.Sites.GetWithKeyAsync(1235);
            var state = Db.TemporalDataTracker.TrackingSetByType(typeof(Site))
                .FindMatchingEntityState(site);

            for (var i = 0; i < 10; i++)
            {
                var changedProperties = state.GetChangedProperties();
                Assert.AreEqual(0, changedProperties.Length);
                site.Location.Y = 30;
                Assert.AreEqual(site.Location.X, 10);
                Assert.AreEqual(site.Location.Y, 30);
                changedProperties = state.GetChangedProperties();
                Assert.AreEqual(1, changedProperties.Length);
                Db.AbandonChanges();
                changedProperties = state.GetChangedProperties();
                Assert.AreEqual(0, changedProperties.Length);
                Assert.AreEqual(site.Location.X, 10);
                Assert.AreEqual(site.Location.Y, 20);
            }
        }


        [TestMethod]
        public async Task TestSaveComplexTypeChange()
        {
            AppDbContext.InMemoryDb.Sites.Add(new Site
            {
                Id = 1235,
                Location = new IqlPointExpression(10, 20)
            });
            var site = await Db.Sites.GetWithKeyAsync(1235);
            var state = Db.TemporalDataTracker.TrackingSetByType(typeof(Site))
                .FindMatchingEntityState(site);

            for (var i = 0; i < 10; i++)
            {
                var changedProperties = state.GetChangedProperties();
                Assert.AreEqual(0, changedProperties.Length);
                site.Location.Y++;
                changedProperties = state.GetChangedProperties();
                Assert.AreEqual(1, changedProperties.Length);
                await Db.SaveChangesAsync();
                changedProperties = state.GetChangedProperties();
                Assert.AreEqual(0, changedProperties.Length);
            }

            for (var i = 0; i < 10; i++)
            {
                site.Location.Y++;
                await Db.SaveChangesAsync();
            }
        }

        [TestMethod]
        public async Task TestPerformance()
        {
            AppDbContext.InMemoryDb.Sites.Add(new Site
            {
                Id = 1235,
                Location = new IqlPointExpression(10, 20)
            });
            var site = await Db.Sites.GetWithKeyAsync(1235);

            for (var i = 0; i < 50; i++)
            {
                site.Location.Y++;
                await Db.SaveChangesAsync();
            }
        }

        [TestMethod]
        public void TestDetectedPropertyChanges()
        {
            var clientTypes = TestsBlock.AddClientTypes();
            var client = clientTypes.ClientType1.Clients[0];
            var state = Db.TemporalDataTracker.TrackingSetByType(typeof(Client))
                .FindMatchingEntityState(client);
            var entityConfiguration = Db.EntityConfigurationContext.GetEntityByType(typeof(Client));
            var nameProperty = entityConfiguration
                .FindProperty(nameof(Client.Name));
            var descriptionProperty = entityConfiguration
                .FindProperty(nameof(Client.Description));
            var changedProperties = state.GetChangedProperties();
            Assert.AreEqual(0, changedProperties.Length);

            // Change name once
            client.Name = "Me";
            state = Db.TemporalDataTracker.TrackingSetByType(typeof(Client))
                .FindMatchingEntityState(client);
            Assert.AreEqual(1, state.GetChangedProperties().Length);
            var change = state.GetChangedProperties()[0];
            var nameState = change;
            Assert.AreEqual("Client 1", change.RemoteValue);
            Assert.AreEqual("Me", change.LocalValue);
            Assert.AreEqual(nameProperty, change.Property);
            Assert.AreEqual(change.EntityState, state);

            // Change name a second time, the old value should still be the *original* value
            client.Name = "Me2";
            Assert.AreEqual(1, state.GetChangedProperties().Length);
            change = state.GetChangedProperties().Single(_ => _.Property.Name == nameof(Client.Name));
            Assert.AreEqual("Client 1", change.RemoteValue);
            Assert.AreEqual("Me2", change.LocalValue);
            Assert.AreEqual(nameProperty, change.Property);
            Assert.AreEqual(client, state.Entity);
            Assert.AreEqual(change.EntityState, state);

            // Change description to the same value as it currently is
            // We should see no new property changes
            client.Description = null;
            Assert.AreEqual(1, state.GetChangedProperties().Length);
            change = state.GetChangedProperties().Single(_ => _.Property.Name == nameof(Client.Name));
            Assert.AreEqual("Client 1", change.RemoteValue);
            Assert.AreEqual("Me2", change.LocalValue);
            Assert.AreEqual(nameProperty, change.Property);
            Assert.AreEqual(client, state.Entity);
            Assert.AreEqual(change.EntityState, state);

            // Change description
            client.Description = "A new description";
            Assert.AreEqual(2, state.GetChangedProperties().Length);
            change = state.GetChangedProperties().Single(_ => _.Property.Name == nameof(Client.Description));
            Assert.AreEqual(null, change.RemoteValue);
            Assert.AreEqual("A new description", change.LocalValue);
            Assert.AreEqual(descriptionProperty, change.Property);
            Assert.AreEqual(client, state.Entity);
            Assert.AreEqual(change.EntityState, state);

            // Change description again
            client.Description = "Another new description";
            Assert.AreEqual(2, state.GetChangedProperties().Length);
            change = state.GetChangedProperties().Single(_ => _.Property.Name == nameof(Client.Description));
            Assert.AreEqual(null, change.RemoteValue);
            Assert.AreEqual("Another new description", change.LocalValue);
            Assert.AreEqual(descriptionProperty, change.Property);
            Assert.AreEqual(client, state.Entity);
            Assert.AreEqual(change.EntityState, state);

            // Change name back to the original value should remove the change record
            client.Name = "Client 1";
            Assert.AreEqual(1, state.GetChangedProperties().Length);
            change = state.GetChangedProperties().Single(_ => _.Property.Name == nameof(Client.Description));
            Assert.AreEqual(null, change.RemoteValue);
            Assert.AreEqual("Another new description", change.LocalValue);
            Assert.AreEqual(descriptionProperty, change.Property);
            Assert.AreEqual(client, state.Entity);
            Assert.AreEqual(change.EntityState, state);

            // Change name to something different again should give us the same property state object
            client.Name = "Client 1 - changed";
            Assert.AreEqual(2, state.GetChangedProperties().Length);
            change = state.GetChangedProperties().Single(p => p.Property.Name == nameof(Client.Name));
            Assert.AreEqual("Client 1", change.RemoteValue);
            Assert.AreEqual("Client 1 - changed", change.LocalValue);
            Assert.AreEqual(nameProperty, change.Property);
            Assert.AreEqual(client, state.Entity);
            Assert.AreEqual(change.EntityState, state);
            Assert.AreEqual(nameState, change);
        }

        [TestMethod]
        public async Task AddingAnEntityPersistsToDb()
        {
            Assert.AreEqual(0, AppDbContext.InMemoryDb.ClientTypes.Count);
            var clientType = new ClientType()
            {
                //Id = 1
            };
            Db.ClientTypes.Add(clientType);
            var entityState = Db.GetEntityState(clientType
#if TypeScript
                , typeof(ClientType)
#endif
                );
            Assert.IsTrue(entityState.IsNew);
            Assert.AreEqual(1, Db.GetChanges().Count);
            await Db.SaveChangesAsync();
            Assert.IsFalse(entityState.IsNew);
            Assert.AreEqual(0, Db.GetChanges().Count);
            Assert.AreEqual(1, AppDbContext.InMemoryDb.ClientTypes.Count);
        }

        [TestMethod]
        public async Task TestWithKey()
        {
            var id = 1;
            AppDbContext.InMemoryDb.ClientTypes.Add(new ClientType { Id = id });
            AppDbContext.InMemoryDb.ClientTypes.Add(new ClientType { Id = id + 1 });
            var clientType = await Db.ClientTypes.GetWithKeyAsync(id);
            Assert.IsNotNull(clientType);
            Assert.AreEqual(clientType.Id, id);
        }

        [TestMethod]
        public async Task TestWhere()
        {
            Db.ClientTypes.Add(new ClientType { Name = "First" });
            Db.ClientTypes.Add(new ClientType { Name = "Second" });
            var result = await Db.SaveChangesAsync();
            var clientType = await Db.ClientTypes.Where(ct => ct.Name == "Second").SingleAsync();
            Assert.IsNotNull(clientType);
            Assert.AreEqual(clientType.Name, "Second");
        }

        [TestMethod]
        public async Task TestWhereDifferentCase()
        {
            Db.ClientTypes.Add(new ClientType { Name = "First" });
            Db.ClientTypes.Add(new ClientType { Name = "Second" });
            await Db.SaveChangesAsync();
            var clientType = await Db.ClientTypes.Where(ct => ct.Name == "SECOND").SingleAsync();
            Assert.IsNotNull(clientType);
            Assert.AreEqual(clientType.Name, "Second");
        }

        [TestMethod]
        public async Task ChangingAnEntity()
        {
            var entity = new ClientType { };
            Db.ClientTypes.Add(entity);
            await Db.SaveChangesAsync();
            Db.EvaluateContext = new EvaluateContext();
            Db.EvaluateContext.Evaluate = s => entity;
            var clientType = await Db.ClientTypes.FirstAsync(ct => ct.Id == entity.Id);
            Assert.AreEqual(entity.Id, clientType.Id);
            Assert.AreEqual(entity, clientType);
            var changes = Db.GetUpdates().ToList();
            Assert.AreEqual(0, changes.Count);
            clientType.Name = "Something else";
            changes = Db.GetUpdates().ToList();
            Assert.AreEqual(1, changes.Count);
            var change = changes[0];
            Assert.AreEqual(QueuedOperationKind.Update, change.Kind);
            var updateOperation = change as QueuedUpdateEntityOperation<ClientType>;
            Assert.IsNotNull(updateOperation);
            Assert.AreEqual(1, updateOperation.Operation.EntityState.GetChangedProperties().Length);
            var property = updateOperation.Operation.EntityState.GetChangedProperties()[0];
            Assert.AreEqual(nameof(ClientType.Name), property.Property.Name);
            //Assert.AreEqual(0, property.ChildChangedProperties.Length);
            //Assert.AreEqual(0, property.EnumerableChangedProperties.Length);
        }

        [TestMethod]
        public async Task ChangingAnEntityWithTheSameValueShouldResultInNoUpdates()
        {
            var entity = new ClientType { Name = "Something else" };
            Db.ClientTypes.Add(entity);
            await Db.SaveChangesAsync();
            Db.EvaluateContext = new EvaluateContext();
            Db.EvaluateContext.Evaluate = s => entity;
            var clientType = await Db.ClientTypes.FirstAsync(ct => ct.Id == entity.Id);
            Assert.AreEqual(entity.Id, clientType.Id);
            var changes = Db.GetUpdates().ToList();
            Assert.AreEqual(0, changes.Count);
            clientType.Name = "Something else";
            changes = Db.GetUpdates().ToList();
            Assert.AreEqual(0, changes.Count);
        }

        [TestMethod]
        public async Task FetchingEntitiesWithANewDataContextShouldReturnDifferentObjectsThanWereInserted()
        {
            var clientTypes = TestsBlock.AddClientTypes();
            await Db.SaveChangesAsync();
            TestsBlock.Db = new AppDbContext();
            var entity1 = await Db.ClientTypes.GetWithKeyAsync(2);
            var entity2 = await Db.ClientTypes.GetWithKeyAsync(3);
            Assert.AreNotEqual(entity1, clientTypes.ClientType1);
            Assert.AreNotEqual(entity2, clientTypes.ClientType2);
        }

        [TestMethod]
        public async Task FetchingEntitiesWithTheSameDataContextShouldReturnTheSameObjectsThatWereInserted()
        {
            var clientTypes = TestsBlock.AddClientTypes();
            await Db.SaveChangesAsync();
            var entity1 = await Db.ClientTypes.GetWithKeyAsync(clientTypes.ClientType1.Id);
            var entity2 = await Db.ClientTypes.GetWithKeyAsync(clientTypes.ClientType2.Id);
            Assert.AreEqual(entity1, clientTypes.ClientType1);
            Assert.AreEqual(entity2, clientTypes.ClientType2);
        }

        [TestMethod]
        public async Task AttemptingToAddAnEntityThatAlreadyExistsShouldNotThrowAnException()
        {
            var clientTypes = TestsBlock.AddClientTypes();
            await Db.SaveChangesAsync();
            var addEntityResult = Db.ClientTypes.Add(clientTypes.ClientType2);
            await Db.SaveChangesAsync();
            Assert.AreEqual(addEntityResult.Entity, clientTypes.ClientType2);
        }

        //[TestMethod]
        //public async Task AddingAnEntityWithAnAttachedEntityWithWithTheSameKeyAsAnExistingTrackedEntityShouldThrowException()
        //{
        //    TestsBlock.AddClientTypes();
        //    await Db.SaveChanges();
        //    var clients = AppDbContext.InMemoryDb.Clients.ToList();
        //    var exceptionThrown = false;
        //    try
        //    {
        //        Db.Clients.Add(new Client
        //        {
        //            Name = "Client 1 b",
        //            Type = new ClientType
        //            {
        //                Name = "This should cause an error"
        //            }
        //        });
        //    }
        //    catch (EntityAlreadyTrackedException)
        //    {
        //        exceptionThrown = true;
        //    }
        //    Assert.IsTrue(exceptionThrown);
        //}

        [TestMethod]
        public async Task AddingAnEntityWithAnAttachedEntityWithANewKeyShouldNotThrowException()
        {
            TestsBlock.AddClientTypes();
            await Db.SaveChangesAsync();
            var exceptionThrown = false;
            try
            {
                Db.Clients.Add(new Client
                {
                    Name = "Client 1 b",
                    Type = new ClientType
                    {
                        Name = "This should not cause an error"
                    }
                });
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
            TestsBlock.AddClientTypes();
            await Db.SaveChangesAsync();
            var exceptionThrown = false;
            try
            {
                Db.Clients.Add(new Client { Name = "Client 1 b", Type = new ClientType { Name = "This should not cause an error" } });
            }
            catch (EntityAlreadyTrackedException)
            {
                exceptionThrown = true;
            }
            Assert.IsFalse(exceptionThrown);
        }

        [TestMethod]
        public void PropertyChangeEventShouldFireWhenAPropertyIsChanged()
        {
            var clientTypes = TestsBlock.AddClientTypes();
            IPropertyChangeEvent propertyChangeEvent = null;
            clientTypes.ClientType1.PropertyChanged.Subscribe(pc =>
            {
                propertyChangeEvent = pc;
            });
            clientTypes.ClientType1.Name = "Me";
            Assert.IsNotNull(propertyChangeEvent);
            Assert.AreEqual(nameof(ClientType.Name), propertyChangeEvent.PropertyName);
            Assert.AreEqual(clientTypes.ClientType1, propertyChangeEvent.Entity);
        }

        [TestMethod]
        public async Task TestReassigningRelationshipEvents()
        {
            var clientTypes = TestsBlock.AddClientTypes();
            await Db.SaveChangesAsync();
            var eventFiredCount = 0;
            var assignEventFiredCount = 0;
            var removeEventFiredCount = 0;
            clientTypes.ClientType1.Clients.RelatedListChange.Subscribe(e =>
            {
                eventFiredCount++;
                switch (e.Kind)
                {
                    case RelatedListChangeKind.Added:
                        assignEventFiredCount++;
                        break;
                    case RelatedListChangeKind.Removed:
                        removeEventFiredCount++;
                        break;
                }
            });
            Assert.AreEqual(0, eventFiredCount);
            Assert.AreEqual(0, assignEventFiredCount);
            Assert.AreEqual(0, removeEventFiredCount);

            var compositeKey = Db.EntityConfigurationContext.EntityType<Client>()
                .GetCompositeKey(clientTypes.ClientType2.Clients[0]);
            clientTypes.ClientType1.Clients.AssignRelationshipByKey(compositeKey);
            Assert.AreEqual(3, eventFiredCount);
            Assert.AreEqual(1, assignEventFiredCount);
            Assert.AreEqual(0, removeEventFiredCount);

            clientTypes.ClientType1.Clients.Add(new Client());
            Assert.AreEqual(5, eventFiredCount);
            Assert.AreEqual(2, assignEventFiredCount);
            Assert.AreEqual(0, removeEventFiredCount);

            //clientTypes.ClientType1.Clients.RemoveRelationshipByKey(new CompositeKey());
            //Assert.AreEqual(4, eventFiredCount);
            //Assert.AreEqual(3, assignEventFiredCount);
            //Assert.AreEqual(1, removeEventFiredCount);

            //clientTypes.ClientType1.Clients.RemoveRelationshipByKey(new CompositeKey());
            //Assert.AreEqual(5, eventFiredCount);
            //Assert.AreEqual(3, assignEventFiredCount);
            //Assert.AreEqual(2, removeEventFiredCount);

            //clientTypes.ClientType1.Clients.RemoveRelationship(new Client());
            //Assert.AreEqual(6, eventFiredCount);
            //Assert.AreEqual(3, assignEventFiredCount);
            //Assert.AreEqual(3, removeEventFiredCount);
        }

        [TestMethod]
        public async Task TestAssignNewEntityToRelatedListSetsRelationshipKeyValues()
        {
            var clientTypes = TestsBlock.AddClientTypes();
            var clientType = clientTypes.ClientType1;
            var newClient = new Client { Name = "My new client" };
            clientType.Clients.Add(newClient);
            Assert.AreEqual(clientType.Id, newClient.TypeId);
            //var changes = Db.GetChanges();
            //Assert.AreEqual(1, changes.Count());
            //var change = changes.Single();
            //Assert.AreEqual(QueuedOperationType.Add, change.Type);
        }

        [TestMethod]
        public async Task TestAssignToCollectionByReferenceAndReassignByReference()
        {
            await TestReassign(ReassignType.AssignByReference, ReassignType.AssignByReference);
        }

        [TestMethod]
        public async Task TestAssignToCollectionByReferenceAndReassignByKey()
        {
            await TestReassign(ReassignType.AssignByReference, ReassignType.Key);
        }

        [TestMethod]
        public async Task TestAssignToCollectionByReferenceAndReassignByRelationship()
        {
            await TestReassign(ReassignType.AssignByReference, ReassignType.Relationship);
        }

        [TestMethod]
        public async Task TestAssignToCollectionByReferenceAndReassignByKeyMethod()
        {
            await TestReassign(ReassignType.AssignByReference, ReassignType.AssignByKeyMethod);
        }


        [TestMethod]
        public async Task TestAssignToCollectionByKeyAndReassignByReference()
        {
            await TestReassign(ReassignType.Key, ReassignType.AssignByReference);
        }

        [TestMethod]
        public async Task TestAssignToCollectionByKeyAndReassignByKey()
        {
            await TestReassign(ReassignType.Key, ReassignType.Key);
        }

        [TestMethod]
        public async Task TestAssignToCollectionByKeyAndReassignByRelationship()
        {
            await TestReassign(ReassignType.Key, ReassignType.Relationship);
        }

        [TestMethod]
        public async Task TestAssignToCollectionByRelationshipAndReassignByReference()
        {
            await TestReassign(ReassignType.Relationship, ReassignType.AssignByReference);
        }

        [TestMethod]
        public async Task TestAssignToCollectionByRelationshipAndReassignByKey()
        {
            await TestReassign(ReassignType.Relationship, ReassignType.Key);
        }

        [TestMethod]
        public async Task TestAssignToCollectionByRelationshipAndReassignByRelationship()
        {
            await TestReassign(ReassignType.Relationship, ReassignType.Relationship);
        }

        [TestMethod]
        public async Task TestAssignToCollectionByKeyMethodAndReassignByReference()
        {
            await TestReassign(ReassignType.AssignByKeyMethod, ReassignType.AssignByReference);
        }

        [TestMethod]
        public async Task TestAssignToCollectionByKeyMethodAndReassignByKeyMethod()
        {
            await TestReassign(ReassignType.AssignByKeyMethod, ReassignType.AssignByKeyMethod);
        }

        [TestMethod]
        public async Task TestAssignToCollectionByKeyMethodAndReassignByKey()
        {
            await TestReassign(ReassignType.AssignByKeyMethod, ReassignType.Key);
        }

        [TestMethod]
        public async Task TestAssignToCollectionByKeyMethodAndReassignByRelationship()
        {
            await TestReassign(ReassignType.AssignByKeyMethod, ReassignType.Relationship);
        }
        //[TestMethod]
        //public async Task AttemptingToSetNullValueToNonNullablePropertyShouldProduceNullNotAllowedException()
        //{
        //    var clientTypes = TestsBlock.AddClientTypes();
        //    var clientType1 = clientTypes.ClientType1;
        //    var clientType1Client = clientType1.Clients[0];
        //    ExpectException<NullNotAllowedException>(() =>
        //    {
        //        clientType1.Clients.RemoveRelationship(clientType1Client);
        //    });
        //    //operations = Db.DataStore.Queue.ToList();
        //    //Assert.AreEqual(1, operations.Count);
        //}

        private void ExpectException<TException>(Action action)
            where TException : Exception
        {
            var exceptionCount = 0;
            try
            {
                action();
            }
            catch (Exception e)
            {
                var ex = e.InnerException ?? e;
                exceptionCount++;
                Assert.AreEqual(typeof(TException).Name, ex.GetType().Name);
            }
            Assert.AreEqual(1, exceptionCount);
        }


        [TestMethod]
        public async Task TestRemovingAnAssociationSetsRelevantConstraintsToNull()
        {
            var site = new Site
            {
                Name = "My site"
            };
            var client = new Client
            {
                Name = "My client"
            };
            client.Sites.Add(site);
            Db.Clients.Add(client);
            await Db.SaveChangesAsync();
            client.Sites.Remove(site);
            Assert.AreEqual(null, site.ClientId);
        }

        [TestMethod]
        public async Task TestAssigningAnAssociationWithANewEntityCreatesRelevantCreateAction()
        {
            await AssigningAnAssociationWithANewEntity(1);
        }

        private static async Task AssigningAnAssociationWithANewEntity(int assignCount)
        {
            var clientTypes = TestsBlock.AddClientTypes();
            var clientType1 = clientTypes.ClientType1;
            var clientType1Clients = clientType1.Clients;
            await Db.SaveChangesAsync();
            Assert.AreEqual(clientType1Clients, clientType1.Clients);
            var clientType1NewClient = new Client();
            clientType1NewClient.Name = "abc";
            Assert.AreEqual(0, Db.GetChanges().Count);
            Assert.AreEqual(1, clientType1.Clients.Count);

            void AssertCheck()
            {
                Assert.AreEqual(2, clientType1.Clients.Count);
                Assert.AreEqual(clientType1.Id, clientType1NewClient.TypeId);
                Assert.AreEqual(clientType1, clientType1NewClient.Type);
                var queuedOperations = Db.GetChanges();
                Assert.AreEqual(1, queuedOperations.Count);
                var addOperation = queuedOperations.AllChanges.First() as QueuedAddEntityOperation<Client>;
                Assert.IsNotNull(addOperation);
                Assert.AreEqual(clientType1NewClient, addOperation.Operation.EntityState.Entity);
            }

            for (var i = 0; i < assignCount; i++)
            {
                clientType1.Clients.Add(clientType1NewClient);
                AssertCheck();
            }

            // Now remove this association and ensure that the add operation is no longer
            // in the queue
            clientType1.Clients.Remove(clientType1NewClient);
            Assert.AreEqual(1, clientType1.Clients.Count);
            var operations = Db.GetChanges();
            Assert.AreEqual(0, operations.Count);
        }

        [TestMethod]
        public async Task TestDoubleAssignmentHasNoSideEffects()
        {
            await AssigningAnAssociationWithANewEntity(5);
        }

        public enum ReassignType
        {
            Key,
            Relationship,
            AssignByReference,
            AssignByKeyMethod
        }

        public async Task TestReassign(ReassignType type1, ReassignType type2)
        {
            var clientTypes = TestsBlock.AddClientTypes();
            var result = await Db.SaveChangesAsync();
            var clientType1 = clientTypes.ClientType1;
            var clientType2 = clientTypes.ClientType2;
            var originalClientType1Client = clientType1.Clients[0];
            var originalClientType2Client = clientType2.Clients[0];
            var existingClient = clientType2.Clients[0];
            var existingClientKey = Db.EntityConfigurationContext.GetEntityByType(typeof(Client))
                .GetCompositeKey(existingClient);
            switch (type1)
            {
                case ReassignType.AssignByReference:
                    clientType1.Clients.Add(existingClient);
                    break;
                case ReassignType.AssignByKeyMethod:
                    clientType1.Clients.AssignRelationshipByKey(existingClientKey);
                    break;
                case ReassignType.Key:
                    existingClient.TypeId = clientType1.Id;
                    break;
                case ReassignType.Relationship:
                    existingClient.Type = clientType1;
                    break;
            }
            Assert.AreEqual(2, clientType1.Clients.Count);
            Assert.AreEqual(0, clientType2.Clients.Count);
            Assert.AreEqual(clientType1.Id, existingClient.TypeId);

            //var previousOwnerChangedList = clientTypes.ClientType2.Clients.GetChanges();
            //Assert.AreEqual(1, previousOwnerChangedList.Count);
            //var previousOwnerChangedRecord = previousOwnerChangedList[0];
            //Assert.AreEqual(RelatedListChangeKind.Remove, previousOwnerChangedRecord.Kind);

            //var newOwnerChangedList = clientTypes.ClientType1.Clients.GetChanges();
            //Assert.AreEqual(1, newOwnerChangedList.Count);
            //var newOwnerChangedRecord = newOwnerChangedList[0];
            //Assert.AreEqual(RelatedListChangeKind.Assign, newOwnerChangedRecord.Kind);

            // We should have only one database update to change the TypeId on the Client object
            var changes = Db.GetUpdates().ToList();
            Assert.AreEqual(1, changes.Count);
            var change = changes.First();
            var changeOperation = change.Operation as UpdateEntityOperation<Client>;
            Assert.IsNotNull(changeOperation);
            Assert.AreEqual(QueuedOperationKind.Update, change.Kind);
            Assert.AreEqual(existingClient, changeOperation.EntityState.Entity);
            Assert.AreEqual(existingClient, changeOperation.EntityState.Entity);
            var propertyChanges = changeOperation.EntityState.GetChangedProperties();
            Assert.AreEqual(1, propertyChanges.Length);

            var keyPropertyChange = propertyChanges.SingleOrDefault(p => p.Property.Name == nameof(Client.TypeId));
            Assert.IsNotNull(keyPropertyChange);
            Assert.AreEqual(clientType2.Id, keyPropertyChange.RemoteValue);
            Assert.AreEqual(clientType1.Id, keyPropertyChange.LocalValue);
            //var referencePropertyChange = propertyChanges.SingleOrDefault(p => p.Property.Name == nameof(Client.Type));
            //Assert.IsNotNull(referencePropertyChange);
            //Assert.AreEqual(clientType2, referencePropertyChange.RemoteValue);
            //Assert.AreEqual(clientType1, referencePropertyChange.LocalValue);

            //var relationshipPropertyChange = propertyChanges.SingleOrDefault(p => p.Property.Name == nameof(Client.Type));
            //Assert.IsNotNull(relationshipPropertyChange);
            //Assert.AreEqual(clientType2, relationshipPropertyChange.OldValue);
            //Assert.AreEqual(clientType1, relationshipPropertyChange.NewValue);

            // Revert the change
            switch (type1)
            {
                case ReassignType.AssignByReference:
                    clientType2.Clients.Add(existingClient);
                    break;
                case ReassignType.AssignByKeyMethod:
                    clientType2.Clients.AssignRelationshipByKey(existingClientKey);
                    break;
                case ReassignType.Key:
                    existingClient.TypeId = clientType2.Id;
                    break;
                case ReassignType.Relationship:
                    existingClient.Type = clientType2;
                    break;
            }
            Assert.AreEqual(1, clientType1.Clients.Count);
            Assert.AreEqual(1, clientType2.Clients.Count);
            Assert.AreEqual(originalClientType1Client, clientType1.Clients[0]);
            Assert.AreEqual(originalClientType2Client, clientType2.Clients[0]);
            //Assert.AreEqual(0, previousOwnerChangedList.Count);
            //Assert.AreEqual(0, newOwnerChangedList.Count);

            // We should no longer have any changes as we have reverted our update
            changes = Db.GetUpdates().ToList();
            Assert.AreEqual(0, changes.Count);
        }

        [TestMethod]
        public async Task PropertyKindValues()
        {
#if !TypeScript
            var kind = IqlPropertyKind.Primitive;
            Assert.AreEqual(nameof(IqlPropertyKind.Primitive), kind.ToString());
            kind = IqlPropertyKind.Relationship;
            Assert.AreEqual(nameof(IqlPropertyKind.Relationship), kind.ToString());
#endif
            Assert.IsTrue(!IqlPropertyKind.Primitive.HasFlag(IqlPropertyKind.Relationship));
            Assert.IsTrue(!IqlPropertyKind.Relationship.HasFlag(IqlPropertyKind.Primitive));
            //kind = IqlPropertyKind.RelationshipKey;
            //Assert.AreEqual(string.Join(", ", new[] { nameof(IqlPropertyKind.Primitive), nameof(IqlPropertyKind.RelationshipKey) }), kind.ToString());
            //kind = IqlPropertyKind.Key;
            //Assert.AreEqual(string.Join(", ", new[] { nameof(IqlPropertyKind.Primitive), nameof(IqlPropertyKind.Key) }), kind.ToString());
            //kind = IqlPropertyKind.Count;
            //Assert.AreEqual(string.Join(", ", new[] { nameof(IqlPropertyKind.Primitive), nameof(IqlPropertyKind.Count) }), kind.ToString());
        }

        [TestMethod]
        public async Task PrimaryKeyWithNoRelationshipShouldNotHaveRelationshipKeyFlagSet()
        {
            var property = Db.EntityConfigurationContext.EntityType<Person>()
                .FindPropertyByExpression(p => p.Id);
            Assert.IsFalse(property.Kind.HasFlag(IqlPropertyKind.RelationshipKey));
            Assert.IsNull(property.Relationship);
        }

        //[TestMethod]
        public async Task
            PrintOutAllPropertyKinds()
        {
            var sb = new StringBuilder();
            foreach (var config in Db.EntityConfigurationContext.AllEntityTypes())
            {
                sb.AppendLine(config.Type.Name);
                for (var i = 0; i < config.Properties.Count; i++)
                {
                    var property = config.Properties[i];
                    sb.AppendLine($"- {property.Name}: {property.Kind.ToString()}");
                }

                sb.AppendLine();
            }
        }

        [TestMethod]
        public async Task
            TestAddingEntity()
        {
            // Create two new client types
            var clientType1 = new ClientType
            {
                Name = "Something else",
            };
            clientType1.Clients.AddRange(new[]
            {
                new Client {Name = "Client 1"}
            });
            Db.ClientTypes.Add(clientType1);

            await Db.SaveChangesAsync();
            Assert.AreEqual(1, clientType1.Clients.Count);

            var clientType2 = new ClientType
            {
                Name = "Another",
            };
            Db.ClientTypes.Add(clientType2);

            await Db.SaveChangesAsync();
            Assert.AreEqual(1, clientType1.Clients.Count);
        }

        [TestMethod]
        public async Task SimpleAddEntityWithNestedEntity()
        {
            var clientType1 = new ClientType
            {
                Name = "Something else",
            };
            clientType1.Clients.AddRange(new[]
            {
                new Client {Name = "Client 1"}
            });
            Db.ClientTypes.Add(clientType1);
            var clientType2 = new ClientType
            {
                Name = "Another",
            };
            clientType2.Clients.AddRange(new[]
            {
                new Client {Name = "Client 2"}
            });
            Db.ClientTypes.Add(clientType2);
            var clients = AppDbContext.InMemoryDb.Clients;
            var clientTypes = AppDbContext.InMemoryDb.ClientTypes;
            await Db.SaveChangesAsync();
            Assert.AreEqual(1, clientType1.Clients.Count);
        }
        [TestMethod]
        public async Task
            DeletingAnEntityThatIsInAChildCollectionOfAnotherEntityShouldRemoveTheEntityFromTheChildCollection()
        {
            var clientTypes = TestsBlock.AddClientTypes();
            Assert.AreEqual(1, clientTypes.ClientType1.Clients.Count);
            await Db.SaveChangesAsync();
            var queuedOperations = Db.GetChanges();
            var clients = await Db.Clients.ToListAsync();
            queuedOperations = Db.GetChanges();
            var dbClients = AppDbContext.InMemoryDb.Clients.ToList();
            Assert.AreEqual(dbClients.Count, clients.Count);
            Assert.AreEqual(1, clientTypes.ClientType1.Clients.Count);

            var clientToDelete = clientTypes.ClientType1.Clients[0];
            Db.Clients.Delete(clientToDelete);
            Assert.AreEqual(0, clientTypes.ClientType1.Clients.Count);
            queuedOperations = Db.GetChanges();
            Assert.AreEqual(1, queuedOperations.Count);
            var deleteOperation = queuedOperations.AllChanges.First() as QueuedDeleteEntityOperation<Client>;
            Assert.IsNotNull(deleteOperation);
            Assert.AreEqual(clientToDelete, deleteOperation.Operation.EntityState.Entity);

            await Db.SaveChangesAsync();
            Assert.AreEqual(0, clientTypes.ClientType1.Clients.Count);
        }

        [TestMethod]
        public async Task
            PuttingADeletedEntityBackShouldRemoveTheDeleteEntityOperation()
        {
            var clients = TestsBlock.AddClientTypes();
            await Db.SaveChangesAsync();
            var clientToDelete = clients.ClientType1.Clients[0];

            Assert.AreEqual(1, clients.ClientType1.Clients.Count);

            var entityState = Db.TemporalDataTracker.TrackingSetByType(typeof(Client))
                .FindMatchingEntityState(clientToDelete);

            Assert.AreEqual(false, entityState.MarkedForDeletion, "Entity is incorrectly marked for deletion (1).");
            Assert.AreEqual(false, entityState.MarkedForCascadeDeletion, "Entity is incorrectly marked for cascade deletion (1).");

            Db.Clients.Delete(clientToDelete);

            Assert.AreEqual(true, entityState.MarkedForDeletion, "Entity should be marked for deletion.");
            Assert.AreEqual(false, entityState.MarkedForCascadeDeletion, "Entity is incorrectly marked for cascade deletion.");

            Assert.AreEqual(0, clients.ClientType1.Clients.Count);

            var deleteOperation = Db.GetChanges().AllChanges.First() as QueuedDeleteEntityOperation<Client>;

            Assert.IsNotNull(deleteOperation);
            Assert.AreEqual(clientToDelete, deleteOperation.Operation.EntityState.Entity);

            // Reinstate the deleted entity
            clients.ClientType2.Clients.Add(deleteOperation.Operation.EntityState.Entity);

            Assert.AreEqual(false, entityState.MarkedForDeletion, "Entity is incorrectly marked for deletion (2).");
            Assert.AreEqual(false, entityState.MarkedForCascadeDeletion, "Entity is incorrectly marked for cascade deletion (2).");
        }

        [TestMethod]
        public async Task
            AddingAnEntityWithAOneToOneRelationshipShouldPersistConstraintKeysAndRelationshipProperties()
        {
            var siteInspection1 = EntityHelper.NewSiteInspection();
            var siteInspection2 = EntityHelper.NewSiteInspection();
            Db.SiteInspections.Add(siteInspection1);
            Db.SiteInspections.Add(siteInspection2);
            var saveChangesResult = await Db.SaveChangesAsync();
            EntityHelper.AssertSuccess(saveChangesResult);
            var riskAssessment = new RiskAssessment
            {
                //Id = 42,
                SiteInspectionId = siteInspection1.Id
            };

            Db.RiskAssessments.Add(riskAssessment);
            //Assert.AreEqual(siteInspection1.RiskAssessment, riskAssessment);
            Assert.AreEqual(riskAssessment.SiteInspectionId, siteInspection1.Id);
            Assert.AreEqual(riskAssessment.SiteInspection, siteInspection1);
        }

        [TestMethod]
        public async Task
            AssigningAOneToOneRelationshipShouldPersistConstraintKeysAndRelationshipProperties()
        {
            var siteInspection1 = EntityHelper.NewSiteInspection();
            var siteInspection2 = EntityHelper.NewSiteInspection();
            Db.SiteInspections.Add(siteInspection1);
            Db.SiteInspections.Add(siteInspection2);
            await Db.SaveChangesAsync();
            var riskAssessment = new RiskAssessment
            {
            };
            Db.RiskAssessments.Add(riskAssessment);
            riskAssessment.SiteInspectionId = siteInspection1.Id;
            //Assert.AreEqual(riskAssessment, siteInspection1.RiskAssessment);
            Assert.AreEqual(siteInspection1.Id, riskAssessment.SiteInspectionId);
            Assert.AreEqual(siteInspection1, riskAssessment.SiteInspection);
        }


        [TestMethod]
        public async Task
            RemovingAOneToOneRelationshipShouldPersistConstraintKeysAndRelationshipProperties()
        {
            var siteInspection1 = EntityHelper.NewSiteInspection();
            var siteInspection2 = EntityHelper.NewSiteInspection();
            Db.SiteInspections.Add(siteInspection1);
            Db.SiteInspections.Add(siteInspection2);
            await Db.SaveChangesAsync();
            var riskAssessment = new RiskAssessment
            {
                SiteInspectionId = siteInspection1.Id
            };
            Db.RiskAssessments.Add(riskAssessment);
            //Assert.AreEqual(siteInspection1.RiskAssessment, riskAssessment);
            Assert.AreEqual(riskAssessment.SiteInspectionId, siteInspection1.Id);
            Assert.AreEqual(riskAssessment.SiteInspection, siteInspection1);
            Db.RiskAssessments.Delete(riskAssessment);
            //Assert.AreEqual(siteInspection1.RiskAssessment, null);
        }

        //[TestMethod]
        //public async Task AddingANewEntityWithChildCollectionShouldResultInASingleAddEntityOperation()
        //{
        //    var entity = new ClientType
        //    {
        //        Id = 2,
        //        Name = "Something else",
        //        Clients = new RelatedList<Client>(new[]
        //        {
        //            new Client {Name = "Client 1"}
        //        })
        //    };
        //    Db.ClientTypes.Add(entity);
        //    var changes = Db.GetChanges().ToList();
        //    Assert.AreEqual(0, changes.Count);
        //    var operations = Db.DataStore.Queue;
        //    Assert.AreEqual(1, operations.Count);
        //    var operation = operations[0] as QueuedAddEntityOperation<ClientType>;
        //    Assert.IsNotNull(operation);
        //    Assert.AreEqual(entity, operation.Operation.Entity);
        //    await Db.SaveChanges();
        //    Assert.AreEqual(0, Db.DataStore.Queue.Count);
        //    changes = Db.GetChanges().ToList();
        //    Assert.AreEqual(0, changes.Count);
        //}

        //[TestMethod]
        //public async Task AssigningChildToMulitpleParentsOnTheSameRelationshipShouldResultInDuplicateParentException()
        //{
        //    // Create two new client types
        //    var clientType1 = new ClientType
        //    {
        //        Id = 2,
        //        Name = "Something else",
        //        Clients = new RelatedList<Client>(new[]
        //        {
        //            new Client {Id = 1, Name = "Client 1"}
        //        })
        //    };
        //    Db.ClientTypes.Add(clientType1);
        //    Assert.AreEqual(clientType1.Clients[0].Type, clientType1);
        //    Assert.AreEqual(clientType1.Clients[0].Type.Id, clientType1.Clients[0].TypeId);
        //    var clientType2 = new ClientType
        //    {
        //        Id = 3,
        //        Name = "Another",
        //        Clients = new RelatedList<Client>(new[]
        //        {
        //            new Client {Id = 2, Name = "Client 2"}
        //        })
        //    };
        //    Db.ClientTypes.Add(clientType2);
        //    var changes1 = Db.GetChanges();
        //    var changes2 = Db.GetChanges();
        //    await Db.SaveChanges();
        //    Db = new AppDbContext();
        //    var entity1 = await Db.ClientTypes.Expand(c => c.Clients).WithKey(2);
        //    var entity2 = await Db.ClientTypes.Expand(c => c.Clients).WithKey(3);
        //    // Assign two existing clients to the first client type
        //    var client = new Client { Id = 3, Name = "Client 1 b" };
        //    entity1.Clients.Add(client);
        //    entity1.Clients.Add(new Client { Id = 4, Name = "Client 1 c" });
        //    // Assign a client from the first client type to the second
        //    entity2.Clients.Add(client);
        //    /* Here's what should happen:
        //     * entity2.Clients[0] has two inferred TypeIds and as such an error should be thrown
        //     */
        //    List<IQueuedOperation> changes = null;
        //    var errorThrown = false;
        //    try
        //    {
        //        // Just getting the changes alone should trigger the error
        //        changes = Db.GetChanges().ToList();
        //    }
        //    catch (DuplicateParentException e)
        //    {
        //        errorThrown = true;
        //    }
        //    Assert.IsTrue(errorThrown, "No error thrown for having a child entity belonging to multiple one-to-many collections.");
        //    errorThrown = false;
        //    try
        //    {
        //        // Attempting to save changes should also trigger the error
        //        await Db.SaveChanges();
        //    }
        //    catch (DuplicateParentException e)
        //    {
        //        errorThrown = true;
        //    }
        //    Assert.IsTrue(errorThrown, "No error thrown for having a child entity belonging to multiple one-to-many collections.");


        //    //entity2.Clients.RemoveAt(0);
        //    //changes = Db.GetChanges().ToList();
        //    /* Here's what should happen:
        //     * entity2 should be updated internally only
        //     * entity2.Clients[0].TypeId should be set to 2
        //     * The TypeId for entity1's newest clients should be set to entity1.Id
        //     * An insert operation for client ID 3 and 4 each should be created
        //     * In total, three operations:
        //     * - One update of entity2.Clients[0]
        //     * - Two adds of the new clients
        //     */

        //}

        //[TestMethod]
        //public async Task ChangingAChildObjectsParentIdForOneToManyShouldSanitiseTheParentObjectsCollection1()
        //{
        //    var clientTypes = TestsBlock.AddClientTypes();
        //    await Db.SaveChanges();
        //    var type1Client = clientTypes.ClientType1.Clients[0];
        //    type1Client.TypeId = clientTypes.ClientType2.Id;
        //    // Trigger sanitisation
        //    var changes = Db.GetChanges().ToList();
        //    Assert.AreEqual(type1Client.Type, clientTypes.ClientType2);
        //    Assert.IsTrue(clientTypes.ClientType2.Clients.Count == 2);
        //    Assert.IsTrue(clientTypes.ClientType2.Clients.Contains(type1Client));
        //    Assert.IsTrue(clientTypes.ClientType1.Clients.Count == 0);
        //}

        //[TestMethod]
        //public async Task ChangingAChildObjectsParentIdForOneToManyShouldSanitiseTheParentObjectsCollection2()
        //{
        //    var clientTypes = TestsBlock.AddClientTypes();
        //    await Db.SaveChanges();
        //    var type1Client = clientTypes.ClientType1.Clients[0];
        //    type1Client.TypeId = clientTypes.ClientType2.Id;
        //    var type4Client = clientTypes.ClientType4.Clients[0];
        //    type4Client.TypeId = clientTypes.ClientType2.Id;
        //    // Trigger sanitisation
        //    var changes = Db.GetChanges().ToList();
        //    Assert.AreEqual(type1Client.Type, clientTypes.ClientType2);
        //    Assert.IsTrue(clientTypes.ClientType2.Clients.Count == 3);
        //    Assert.IsTrue(clientTypes.ClientType2.Clients.Contains(type1Client));
        //    Assert.IsTrue(clientTypes.ClientType1.Clients.Count == 0);
        //    Assert.IsTrue(clientTypes.ClientType4.Clients.Count == 0);
        //    Assert.IsTrue(clientTypes.ClientType5.Clients.Count == 2);
        //}

        //[TestMethod]
        //public async Task ChangingAChildObjectsParentIdForOneToManyShouldSanitiseTheParentObjectsCollection3()
        //{
        //    var clientType1 = new ClientType
        //    {
        //        Id = 2,
        //        Name = "Something else",
        //        Clients = new RelatedList<Client>(new[]
        //        {
        //            new Client {Id = 1, Name = "Client 1"}
        //        })
        //    };
        //    Db.ClientTypes.Add(clientType1);
        //    var clientType2 = new ClientType
        //    {
        //        Id = 3,
        //        Name = "Another",
        //        Clients = new RelatedList<Client>()
        //    };
        //    Db.ClientTypes.Add(clientType2);
        //    await Db.SaveChanges();
        //    var newClient = new Client { Id = 2, Name = "Client 2", TypeId = 3 };
        //    Db.Clients.Add(newClient);
        //    //await Db.SaveChanges();
        //    Assert.AreEqual(1, clientType2.Clients.Count);
        //    Assert.AreEqual(newClient, clientType2.Clients[0]);
        //}

        //[TestMethod]
        //public async Task ChangingAChildObjectsParentIdForOneToManyShouldSanitiseTheParentObjectsCollection4()
        //{
        //    var siteInspection = new SiteInspection()
        //    {
        //        Id = 21,
        //        RiskAssessmentId = 41
        //    };
        //    var riskAssessment = new RiskAssessment()
        //    {
        //        Id = 41,
        //        SiteInspectionId = 21
        //    };
        //    Db.SiteInspections.Add(siteInspection);
        //    Db.RiskAssessments.Add(riskAssessment);
        //    Assert.AreEqual(riskAssessment, siteInspection.RiskAssessment);
        //    Assert.AreEqual(siteInspection, riskAssessment.SiteInspection);
        //}

        //[TestMethod]
        //[ExpectedException(typeof(InconsistentRelationshipAssignmentException))]
        //public async Task TwoInconsistentChangesToChildCollectionsShouldThrowInconsistentChangeException()
        //{
        //    var clientTypes = TestsBlock.AddClientTypes();
        //    await Db.SaveChanges();
        //    var type1Client = clientTypes.ClientType1.Clients[0];
        //    type1Client.TypeId = clientTypes.ClientType2.Id;
        //    clientTypes.ClientType3.Clients.Add(type1Client);
        //    // Trigger sanitisation
        //    var changes = Db.GetChanges().ToList();
        //}

        //[TestMethod]
        //public async Task AddingAnEntityToAChildCollectionShouldPersistRelationship()
        //{
        //    var clientTypes = TestsBlock.AddClientTypes();
        //    await Db.SaveChanges();
        //    var type1Client = clientTypes.ClientType1.Clients[0];
        //    clientTypes.ClientType3.Clients.Add(type1Client);
        //    // Trigger sanitisation
        //    var changes = Db.GetChanges().ToList();
        //    Assert.AreEqual(0, clientTypes.ClientType1.Clients.Count);
        //}

        //[TestMethod]
        //[ExpectedException(typeof(InconsistentRelationshipAssignmentException))]
        //public async Task ChangingBothARelationshipAndARelationshipKeyShouldThrowAnErrorIfNotConsistent()
        //{
        //    var clientTypes = TestsBlock.AddClientTypes();
        //    await Db.SaveChanges();
        //    var item1Client = clientTypes.ClientType1.Clients[0];
        //    item1Client.TypeId = clientTypes.ClientType2.Id;
        //    item1Client.Type = new ClientType
        //    {
        //        Id = 5
        //    };
        //    // Trigger sanitisation and exception
        //    Db.GetChanges();
        //}
    }
}