using System;
using System.Threading.Tasks;
using Iql.Tests.Context;
using Iql.Tests.Extensions;
using IqlSampleApp.Data.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Iql.Tests.Tests.SpecialTypes
{
    [TestClass]
    public class SpecialExpressionTests : TestsBase
    {
        protected override void PostTestCleanUp()
        {
            IqlNewGuidExpression.NewGuid = () => Guid.NewGuid();
        }

        [TestMethod]
        public async Task TestNewGuidOnHasGuid()
        {
            IqlNewGuidExpression.NewGuid = () => new Guid("22ba3c57-d261-42df-b41b-6a900eba9032");
            var project = new Project();
            project.Title = "My project";
            Db.Projects.Add(project);
            var result = await Db.SaveChangesAsync();
            Assert.IsTrue(result.Success);
            Assert.AreEqual("22ba3c57-d261-42df-b41b-6a900eba9032", project.Guid.ToString());
            IqlNewGuidExpression.NewGuid = () => new Guid("4ac450d3-7a61-4ca9-a816-8c0cfab0e896");
            project.Title = "Some change";
            result = await Db.SaveChangesAsync();
            Assert.IsTrue(result.Success);
            // Guid should not be changed this time
            Assert.AreEqual("22ba3c57-d261-42df-b41b-6a900eba9032", project.Guid.ToString());
        }

        [TestMethod]
        public async Task TestNewGuidOnHasGuidWithDefaultGenerator()
        {
            IqlNewGuidExpression.NewGuid = () => Guid.NewGuid();
            var project = new Project();
            project.Title = "My project";
            Db.Projects.Add(project);
            var result = await Db.SaveChangesAsync();
            Assert.IsTrue(result.Success);
            var guidString = project.Guid.ToString();
            Assert.AreNotEqual("00000000-0000-0000-0000-000000000000", guidString);
            project.Title = "Some change";
            result = await Db.SaveChangesAsync();
            Assert.IsTrue(result.Success);
            // Guid should not be changed this time
            Assert.AreEqual(guidString, project.Guid.ToString());
        }

        [TestMethod]
        public async Task PreExistingEmptyGuidShouldBeUpdated()
        {
            var dbProject = new Project
            {
                Id = 987,
                Title = "BadGuid"
            };
            AppDbContext.InMemoryDb.Projects.Add(dbProject);
            IqlNewGuidExpression.NewGuid = () => Guid.NewGuid();
            var project = await Db.Projects.GetWithKeyAsync(987);
            Assert.AreEqual("00000000-0000-0000-0000-000000000000", project.Guid.ToGuidString());
            project.Title = "BadGuidFixed";
            var result = await Db.SaveChangesAsync();
            Assert.IsTrue(result.Success);
            var guidString = project.Guid.ToGuidString();
            Assert.AreNotEqual("00000000-0000-0000-0000-000000000000", guidString);
            project.Title = "Some change";
            result = await Db.SaveChangesAsync();
            Assert.IsTrue(result.Success);
            // Guid should not be changed this time
            Assert.AreEqual(guidString, project.Guid.ToGuidString());
            Assert.AreEqual(dbProject.Guid.ToGuidString(), guidString);
        }
    }
}