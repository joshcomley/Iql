using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Iql.Queryable;
using Iql.Tests.Context;
using IqlSampleApp.Data.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Iql.Tests.Tests.QueryableTests
{
    [TestClass]
    public class QueryableTests : InMemoryTests
    {
        [TestMethod]
        public async Task TestGetAllPages()
        {
            for (var i = 1; i <= 300; i++)
            {
                AppDbContext.InMemoryDb.PeopleTypes.Add(new PersonType { Id = i });
            }
            Db.InMemoryDataStore.DefaultPageSize = 10;
            var progressNotifier = new ProgressNotifier();
            var progressReports = new List<double>();
            progressNotifier.OnProgress.Subscribe(_ =>
            {
                progressReports.Add(_.Progress);
            });
            var peopleTypes = await Db.PersonTypes.Take(30).AllPagesToListAsync(progressNotifier);
            Assert.IsTrue(peopleTypes.Success);
            Assert.AreEqual(11, progressReports.Count);
            var expectedProgressSoFar = 0d;
            foreach (var progressReport in progressReports)
            {
                Assert.IsTrue(Math.Abs(expectedProgressSoFar - progressReport) < 0.00001);
                expectedProgressSoFar += 0.1;
            }
            Assert.AreEqual(300, peopleTypes.Count);
        }

        [TestMethod]
        public async Task TestGetAllPagesWithOnlyOnePage()
        {
            for (var i = 1; i <= 3; i++)
            {
                AppDbContext.InMemoryDb.PeopleTypes.Add(new PersonType { Id = i });
            }
            Db.InMemoryDataStore.DefaultPageSize = 10;
            var progressNotifier = new ProgressNotifier();
            var progressReports = new List<double>();
            progressNotifier.OnProgress.Subscribe(_ =>
            {
                progressReports.Add(_.Progress);
            });
            var peopleTypes = await Db.PersonTypes.Take(30).AllPagesToListAsync(progressNotifier);
            Assert.IsTrue(peopleTypes.Success);
            Assert.AreEqual(2, progressReports.Count);
            var expectedProgressSoFar = 0d;
            foreach (var progressReport in progressReports)
            {
                Assert.IsTrue(Math.Abs(expectedProgressSoFar - progressReport) < 0.00001);
                expectedProgressSoFar += 1;
            }
            Assert.AreEqual(3, peopleTypes.Count);
        }
    }
}