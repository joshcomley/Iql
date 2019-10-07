using System;
using System.Threading.Tasks;
using Iql.Entities;
using Iql.Tests.Context;
using Iql.Tests.Tests.OData;
using IqlSampleApp.ApiContext.Base;
using IqlSampleApp.Sets;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IqlSampleApp.Data.Entities;
using Iql.JavaScript.JavaScriptExpressionToIql;

namespace Iql.Tests.Tests.DataContextTests
{
    [TestClass]
    public class DataContextSnapshotTests : TestsBase
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
            var snapshot = Db.RecordSnapshot();
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
            var snapshot = Db.RecordSnapshot();
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
            var snapshot = Db.RecordSnapshot();
            Assert.AreEqual(snapshot, Db.CurrentSnapshot);
            entity1.Name = "ghi";
            Db.RestoreToPreviousSnapshot();
            Assert.AreEqual("def", entity1.Name);
            Db.RestoreToPreviousSnapshot();
            Assert.AreEqual("abc", entity1.Name);
            Db.RestoreToNextSnapshot();
            Assert.AreEqual("def", entity1.Name);
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
            var snapshot1 = Db.RecordSnapshot();

            var entity2 = new Client
            {
                Name = "abc"
            };
            Db.Clients.Add(entity2);
            entity2.Name = "def";
            var snapshot2 = Db.RecordSnapshot();

            Assert.IsTrue(Db.IsTracked(entity2));

            Db.RestoreToPreviousSnapshot();

            Assert.IsFalse(Db.IsTracked(entity2));

            Db.RestoreToNextSnapshot();

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
            var snapshot1 = Db.RecordSnapshot();
            var state = Db.GetEntityState(entity1);

            Db.DeleteEntity(entity1);

            additions = Db.GetAdditions();
            Assert.AreEqual(0, additions.Length);

            var snapshot2 = Db.RecordSnapshot();

            Assert.IsTrue(state.MarkedForAnyDeletion);

            Db.RestoreToPreviousSnapshot();

            additions = Db.GetAdditions();
            Assert.AreEqual(1, additions.Length);

            Assert.IsFalse(state.MarkedForAnyDeletion);

            Db.RestoreToNextSnapshot();

            additions = Db.GetAdditions();
            Assert.AreEqual(0, additions.Length);

            Assert.IsTrue(state.MarkedForAnyDeletion);
        }
    }
}