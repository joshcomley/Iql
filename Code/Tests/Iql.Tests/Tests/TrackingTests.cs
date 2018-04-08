using System;
using System.Linq;
using System.Threading.Tasks;
using Iql.OData;
using Iql.Queryable.Data.Crud.Operations;
using Iql.Queryable.Data.Http;
using Iql.Queryable.Data.Lists;
using Iql.Tests.Context;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tunnel.App.Data.Entities;

namespace Iql.Tests.Tests
{
    [TestClass]
    public class TrackingTests : TestsBase
    {
        [TestMethod]
        public async Task AssigningATargetFromSourceShouldSetSourceId()
        {
            PrepInMemoryDatabaseForLoadRelationshipPropertyTests();
            var person = await Db.People.WithKeyAsync(7);
            Assert.IsNull(person.Type);
            await Db.People.LoadRelationshipAsync(person, c => c.Type);
            Assert.IsNotNull(person.Type);
            Assert.AreEqual(2, person.Type.Id);
        }

        [TestMethod]
        public async Task LoadingOneToManyTargetRelationshipProperty()
        {
            PrepInMemoryDatabaseForLoadRelationshipPropertyTests();
            var person = await Db.People.WithKeyAsync(7);
            Assert.IsNull(person.Type);
            await Db.People.LoadRelationshipAsync(person, c => c.Type);
            Assert.IsNotNull(person.Type);
            Assert.AreEqual(2, person.Type.Id);
        }

        [TestMethod]
        public async Task LoadingOneToManySourceRelationshipProperty()
        {
            PrepInMemoryDatabaseForLoadRelationshipPropertyTests();
            var type = await Db.PersonTypes.WithKeyAsync(2);
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
            var person = await Db.People.WithKeyAsync(7);
            Assert.IsNull(person.Type);
            await Db.LoadRelationshipAsync(person, c => c.Type);
            Assert.IsNotNull(person.Type);
            Assert.AreEqual(2, person.Type.Id);
        }

        [TestMethod]
        public async Task LoadingOneToManySourceRelationshipPropertyFromDataContext()
        {
            PrepInMemoryDatabaseForLoadRelationshipPropertyTests();
            var type = await Db.PersonTypes.WithKeyAsync(2);
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
            var operations = Db.DataStore.Tracking.GetQueue().ToList();
            Assert.AreEqual(3, operations.Count);
            var order = new object[] {user, siteInspection, riskAssessment};
            for (var i = 0; i < operations.Count; i++)
            {
                var operation = operations[i];
                Assert.AreEqual(order[i], (operation.Operation as IEntityCrudOperationBase).Entity);
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