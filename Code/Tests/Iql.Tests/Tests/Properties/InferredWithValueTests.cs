using System;
using System.Threading.Tasks;
using Iql.Data.Evaluation;
using Iql.Data.Extensions;
using Iql.Entities.Services;
using Iql.Tests.Context;
using Iql.Tests.Tests.Services;
using IqlSampleApp.Data.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Iql.Tests.Tests.Properties
{
    [TestClass]
    public class InferredWithValueTests : TestsBase
    {
        [TestMethod]
        public async Task InferRelationshipByKey()
        {
            Db.ServiceProvider.RegisterInstance<IqlCurrentUserService>(new TestCurrentUserResolver());
            var applicationUsersInMemory = Db.InMemoryDataStore.DataSet<ApplicationUser>();
            var applicationUserInMemory = new ApplicationUser
            {
                Id = TestCurrentUserResolver.TestCurrentUserId,
                FullName = "Mr Test"
            };
            applicationUsersInMemory.Add(applicationUserInMemory);
            var client = new Client();
            Db.Clients.Add(client);
            await new InferredValueEvaluationSession().TrySetInferredValuesAsync(Db, client);
            Assert.AreEqual(TestCurrentUserResolver.TestCurrentUserId, client.CreatedByUserId, "Relationship key not inferred");
            Assert.IsNotNull(client.CreatedByUser, "Relationship object not inferred");
            Assert.AreEqual(TestCurrentUserResolver.TestCurrentUserId, client.CreatedByUser.Id, "Relationship object incorrectly inferred");
            applicationUsersInMemory.Remove(applicationUserInMemory);
        }

        [TestMethod]
        public async Task InferRelationshipByKeyUnattached()
        {
            Db.ServiceProvider.RegisterInstance<IqlCurrentUserService>(new TestCurrentUserResolver());
            var applicationUsersInMemory = Db.InMemoryDataStore.DataSet<ApplicationUser>();
            var applicationUserInMemory = new ApplicationUser
            {
                Id = TestCurrentUserResolver.TestCurrentUserId,
                FullName = "Mr Test"
            };
            applicationUsersInMemory.Add(applicationUserInMemory);
            var client = new Client();
            await new InferredValueEvaluationSession().TrySetInferredValuesAsync(Db, client);
            Assert.AreEqual(TestCurrentUserResolver.TestCurrentUserId, client.CreatedByUserId, "Relationship key not inferred");
            Assert.IsNotNull(client.CreatedByUser, "Relationship object not inferred");
            Assert.AreEqual(TestCurrentUserResolver.TestCurrentUserId, client.CreatedByUser.Id, "Relationship object incorrectly inferred");
            applicationUsersInMemory.Remove(applicationUserInMemory);
        }

        [TestMethod]
        public async Task InferredWithOnAddUsingOldValuesShouldHaveNoEffectTest()
        {
            Db.ServiceProvider.Clear();
            var person = new Person();
            person.Title = "My person";
            person.Description = "Still my person";
            person.Skills = PersonSkills.Coder;
            person.InferredWhenKeyChanges = "Nothing";
            person.Key = "ABC";
            Db.People.Add(person);
            person.Key = "DEF";
            var result = await Db.SaveChangesAsync();
            Assert.IsTrue(result.Success);
            Assert.AreEqual("alphabet!", person.InferredWhenKeyChanges);
        }

        [TestMethod]
        public async Task InferredWithOnAddUsingOldValuesShouldNotBeSetIfValidationFailsTest()
        {
            Db.ServiceProvider.Clear();
            var person = new Person();
            person.Title = "My person";
            person.Description = "Still my person";
            person.Skills = PersonSkills.Coder;
            person.InferredWhenKeyChanges = "Nothing";
            person.Key = "ABC";
            Db.People.Add(person);
            person.Key = "DEF";
            person.Title = null;
            var result = await Db.SaveChangesAsync();
            Assert.IsFalse(result.Success);
            Assert.AreEqual("Nothing", person.InferredWhenKeyChanges);
        }

        [TestMethod]
        public async Task InferredWithOnUpdateUsingOldValuesTest()
        {
            Db.ServiceProvider.Clear();
            var person = new Person();
            person.Title = "My person";
            person.Description = "Still my person";
            person.Skills = PersonSkills.Coder;
            person.InferredWhenKeyChanges = "Nothing";
            Db.People.Add(person);
            var result = await Db.SaveChangesAsync();
            Assert.IsTrue(result.Success);

            person.Key = "123";
            result = await Db.SaveChangesAsync();
            Assert.IsTrue(result.Success);
            Assert.AreEqual("Nothing", person.InferredWhenKeyChanges);

            person.Key = "DEF";
            result = await Db.SaveChangesAsync();
            Assert.IsTrue(result.Success);
            Assert.AreEqual("Nothing", person.InferredWhenKeyChanges);

            person.Key = "ABC";
            result = await Db.SaveChangesAsync();
            Assert.IsTrue(result.Success);
            Assert.AreEqual("Nothing", person.InferredWhenKeyChanges);

            person.Key = "DEF";
            result = await Db.SaveChangesAsync();
            Assert.IsTrue(result.Success);
            Assert.AreEqual("alphabet!", person.InferredWhenKeyChanges);
        }

        [TestMethod]
        public async Task InferredWithOnUpdateUsingOldValuesShouldNotBeSetIfValidationFailsTest()
        {
            Db.ServiceProvider.Clear();
            var person = new Person();
            person.Title = "My person";
            person.Description = "Still my person";
            person.Skills = PersonSkills.Coder;
            person.InferredWhenKeyChanges = "Nothing";
            Db.People.Add(person);
            var result = await Db.SaveChangesAsync();
            Assert.IsTrue(result.Success);

            person.Key = "123";
            result = await Db.SaveChangesAsync();
            Assert.IsTrue(result.Success);
            Assert.AreEqual("Nothing", person.InferredWhenKeyChanges);

            person.Key = "DEF";
            result = await Db.SaveChangesAsync();
            Assert.IsTrue(result.Success);
            Assert.AreEqual("Nothing", person.InferredWhenKeyChanges);

            person.Key = "ABC";
            result = await Db.SaveChangesAsync();
            Assert.IsTrue(result.Success);
            Assert.AreEqual("Nothing", person.InferredWhenKeyChanges);

            person.Key = "DEF";
            person.Title = null;
            result = await Db.SaveChangesAsync();
            Assert.IsFalse(result.Success);
            Assert.AreEqual("Nothing", person.InferredWhenKeyChanges);
        }

        [TestMethod]
        public async Task InferredWithConversionToStringTest()
        {
            Db.ServiceProvider.Clear();
            var site = new Site();
            site.ClientId = 7;
            site.Address = "A\nB";
            site.PostCode = "DEF";
            Assert.IsFalse((await new InferredValueEvaluationSession().TrySetInferredValuesAsync(Db, site)).Success);
            Assert.AreEqual("7", site.Key);
            Assert.AreEqual("A\nB\nDEF", site.FullAddress);
        }

        [TestMethod]
        public async Task PopulateNewExistingInferredWithValueTest()
        {
            AppDbContext.InMemoryDb.People.Add(new Person
            {
                Id = 177,
                SiteId = 87,
                Title = "My person"
            });
            AppDbContext.InMemoryDb.Sites.Add(new Site
            {
                Id = 87,
                Name = "My site",
                ClientId = 107
            });
            AppDbContext.InMemoryDb.Clients.Add(new Client
            {
                Id = 107,
                Name = "My client"
            });
            var person = await Db.People.GetWithKeyAsync(177);
            Assert.IsFalse((await new InferredValueEvaluationSession().TrySetInferredValuesAsync(Db, person)).Success);
            Assert.AreEqual(null, person.CreatedByUserId);
            Assert.AreEqual(null, person.Location);
            Assert.AreEqual(107, person.ClientId);
            Assert.IsNotNull(person.Client);

            Assert.IsFalse((await new InferredValueEvaluationSession().TrySetInferredValuesAsync(Db, person)).Success);
            Assert.AreEqual(null, person.CreatedByUserId);
            Assert.AreEqual(null, person.Location);
            Assert.AreEqual(107, person.ClientId);
            Assert.IsNotNull(person.Client);

            Db.ServiceProvider.RegisterInstance(new TestCurrentUserResolver());
            Db.ServiceProvider.RegisterInstance(new TestCurrentLocationResolver());
            Assert.IsTrue((await new InferredValueEvaluationSession().TrySetInferredValuesAsync(Db, person)).Success);
            Assert.AreEqual(null, person.Location);
            Assert.AreEqual("testuserid", person.CreatedByUserId);
            Assert.AreEqual(107, person.ClientId);
            Assert.IsNotNull(person.Client);

            var currentLatitude = 51.5054597;
            TestCurrentLocationResolver.CurrentLatitude = currentLatitude;
            var currentLongitude = -0.0775452;
            TestCurrentLocationResolver.CurrentLongitude = currentLongitude;
            Db.ServiceProvider.Unregister<TestCurrentUserResolver>();

            Assert.IsFalse((await new InferredValueEvaluationSession().TrySetInferredValuesAsync(Db, person)).Success);
            Assert.AreEqual("testuserid", person.CreatedByUserId);
            Assert.IsNotNull(person.Location);
            Assert.AreEqual(TestCurrentLocationResolver.CurrentLongitude, person.Location.X);
            Assert.AreEqual(TestCurrentLocationResolver.CurrentLatitude, person.Location.Y);
            Assert.AreEqual(107, person.ClientId);
            Assert.IsNotNull(person.Client);
            var location = person.Location;

            TestCurrentLocationResolver.CurrentLatitude = 41.5054597;
            TestCurrentLocationResolver.CurrentLongitude = -1.0775452;

            Db.ServiceProvider.RegisterInstance(new TestCurrentUserResolver());
            Assert.IsTrue((await new InferredValueEvaluationSession().TrySetInferredValuesAsync(Db, person)).Success);
            Assert.AreEqual("testuserid", person.CreatedByUserId);
            Assert.AreEqual(location, person.Location);
            Assert.AreEqual(currentLongitude, person.Location.X);
            Assert.AreEqual(currentLatitude, person.Location.Y);
            Assert.AreEqual(107, person.ClientId);
            Assert.IsNotNull(person.Client);

            Db.ServiceProvider.Unregister<IqlCurrentUserService>();
            Assert.IsFalse((await new InferredValueEvaluationSession().TrySetInferredValuesAsync(Db, person)).Success);
            Assert.AreEqual("testuserid", person.CreatedByUserId);
            Assert.AreEqual(location, person.Location);
            Assert.AreEqual(currentLongitude, person.Location.X);
            Assert.AreEqual(currentLatitude, person.Location.Y);
            Assert.AreEqual(107, person.ClientId);
            Assert.IsNotNull(person.Client);

            Db.ServiceProvider.RegisterInstance<IqlCurrentUserService>(new TestCurrentUserResolver());
            person.Location = null;
            Assert.IsTrue((await new InferredValueEvaluationSession().TrySetInferredValuesAsync(Db, person)).Success);
            Assert.AreEqual("testuserid", person.CreatedByUserId);
            Assert.IsNotNull(person.Location);
            Assert.AreEqual(TestCurrentLocationResolver.CurrentLongitude, person.Location.X);
            Assert.AreEqual(TestCurrentLocationResolver.CurrentLatitude, person.Location.Y);
            Assert.AreEqual(107, person.ClientId);
            Assert.IsNotNull(person.Client);

            Db.ServiceProvider.Unregister<IqlCurrentUserService>();
            Assert.IsFalse((await new InferredValueEvaluationSession().TrySetInferredValuesAsync(Db, person)).Success);
            Assert.AreEqual("testuserid", person.CreatedByUserId);
            Assert.IsNotNull(person.Location);
            Assert.AreEqual(TestCurrentLocationResolver.CurrentLongitude, person.Location.X);
            Assert.AreEqual(TestCurrentLocationResolver.CurrentLatitude, person.Location.Y);
            Assert.AreEqual(107, person.ClientId);
            Assert.IsNotNull(person.Client);
            Db.ServiceProvider.Unregister<IqlCurrentUserService>();
            Db.ServiceProvider.Unregister<IqlCurrentLocationService>();
        }

        [TestMethod]
        public async Task PopulateNewEntityInferredWithValueTest()
        {
            var person = new Person();
            person.SiteId = 87;
            AppDbContext.InMemoryDb.Sites.Add(new Site
            {
                Id = 87,
                Name = "My site",
                ClientId = 107
            });
            AppDbContext.InMemoryDb.Clients.Add(new Client
            {
                Id = 107,
                Name = "My client"
            });

            Assert.IsFalse((await new InferredValueEvaluationSession().TrySetInferredValuesAsync(Db, person, true)).Success);
            Assert.IsTrue(person.CreatedDate.Year == 2019);
            Assert.IsTrue(person.CreatedDate.Month == 1);
            Assert.IsTrue(person.CreatedDate.Day == 2);
            Assert.AreEqual(PersonCategory.Conventional, person.Category);
            Assert.AreEqual(null, person.CreatedByUserId);
            Assert.AreEqual(null, person.Description);
            Assert.AreEqual(107, person.ClientId);
            Assert.IsNotNull(person.Client);

            person.Category = PersonCategory.AutoDescription;
            Assert.IsFalse((await new InferredValueEvaluationSession().TrySetInferredValuesAsync(Db, person)).Success);
            Assert.AreEqual("I'm \\ \"auto\"", person.Description);
            Assert.AreEqual(null, person.CreatedByUserId);
            Assert.AreEqual(107, person.ClientId);
            Assert.IsNotNull(person.Client);

            Db.ServiceProvider.RegisterInstance(new TestCurrentUserResolver());
            Assert.IsFalse((await new InferredValueEvaluationSession().TrySetInferredValuesAsync(Db, person)).Success);
            Assert.AreEqual("testuserid", person.CreatedByUserId);
            Assert.AreEqual(107, person.ClientId);
            Assert.IsNotNull(person.Client);

            Db.ServiceProvider.Unregister<TestCurrentUserResolver>();
            Assert.IsFalse((await new InferredValueEvaluationSession().TrySetInferredValuesAsync(Db, person)).Success);
            Assert.AreEqual("testuserid", person.CreatedByUserId);
            Assert.AreEqual(107, person.ClientId);
            Assert.IsNotNull(person.Client);

            Db.ServiceProvider.RegisterInstance(new TestCurrentUserResolver());
            Assert.IsFalse((await new InferredValueEvaluationSession().TrySetInferredValuesAsync(Db, person)).Success);
            Assert.AreEqual("testuserid", person.CreatedByUserId);
            Assert.AreEqual(107, person.ClientId);
            Assert.IsNotNull(person.Client);

            Db.ServiceProvider.Unregister<IqlCurrentUserService>();
            Assert.IsFalse((await new InferredValueEvaluationSession().TrySetInferredValuesAsync(Db, person)).Success);
            Assert.AreEqual("testuserid", person.CreatedByUserId);
            Assert.AreEqual(107, person.ClientId);
            Assert.IsNotNull(person.Client);

            Db.ServiceProvider.RegisterInstance<IqlCurrentUserService>(new TestCurrentUserResolver());
            Assert.IsFalse((await new InferredValueEvaluationSession().TrySetInferredValuesAsync(Db, person)).Success);
            Assert.AreEqual("testuserid", person.CreatedByUserId);
            Assert.AreEqual(107, person.ClientId);
            Assert.IsNotNull(person.Client);

            Db.ServiceProvider.Unregister<IqlCurrentUserService>();
            Assert.IsFalse((await new InferredValueEvaluationSession().TrySetInferredValuesAsync(Db, person)).Success);
            Assert.AreEqual("testuserid", person.CreatedByUserId);
            Assert.AreEqual(107, person.ClientId);
            Assert.IsNotNull(person.Client);
            Db.ServiceProvider.Unregister<IqlCurrentUserService>();
            Db.ServiceProvider.Unregister<IqlCurrentLocationService>();
        }
    }
}