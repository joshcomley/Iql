using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Iql.Queryable.Data.EntityConfiguration;
using Iql.Queryable.Expressions;
using Iql.Tests.Context;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tunnel.App.Data.Entities;

namespace Iql.Tests.Tests
{
    [TestClass]
    public class IqlExpressionTests : TestsBase
    {
        [TestMethod]
        public async Task FilterCollection()
        {
            AppDbContext.InMemoryDb.PeopleTypes.Add(new PersonType
            {
                Id = 1
            });
            AppDbContext.InMemoryDb.PeopleTypes.Add(new PersonType
            {
                Id = 2
            });
            AppDbContext.InMemoryDb.PeopleTypes.Add(new PersonType
            {
                Id = 3
            });
            AppDbContext.InMemoryDb.PeopleTypes.Add(new PersonType
            {
                Id = 4
            });
            AppDbContext.InMemoryDb.People.Add(new Person
            {
                Id = 1
            });
            AppDbContext.InMemoryDb.People.Add(new Person
            {
                Id = 2
            });
            AppDbContext.InMemoryDb.People.Add(new Person
            {
                Id = 3,
                Title = "Test"
            });
            AppDbContext.InMemoryDb.PeopleTypeMap.Add(new PersonTypeMap
            {
                PersonId = 1,
                TypeId = 1,
                Description = "Abc"
            });
            AppDbContext.InMemoryDb.PeopleTypeMap.Add(new PersonTypeMap
            {
                PersonId = 1,
                TypeId = 2,
                Description = "Abc"
            });
            AppDbContext.InMemoryDb.PeopleTypeMap.Add(new PersonTypeMap
            {
                PersonId = 2,
                TypeId = 1,
                Description = "Abc"
            });
            AppDbContext.InMemoryDb.PeopleTypeMap.Add(new PersonTypeMap
            {
                PersonId = 2,
                TypeId = 3,
                Description = "Kettle"
            });
            AppDbContext.InMemoryDb.PeopleTypeMap.Add(new PersonTypeMap
            {
                PersonId = 3,
                TypeId = 4,
                Description = "Def"
            });
            var query = Db.People.Where(p => p.Title == "Test" || p.Types.Any(t => t.TypeId == 4 || t.Description == "Kettle"));
            // OData: https://localhost:44364/odata/SampleData?$expand=ComplexPivots&$filter=ComplexPivots/any(_:(_/Title eq 'Flip flop' or _/SampleDataReferenceId eq 2))
            var results = await query.ToListAsync();
        }
        [TestMethod]
        public void Last24Hours()
        {
            AppDbContext.InMemoryDb.Clients.Add(new Client
            {
                Id = 1,
                CreatedDate = DateTimeOffset.Now.AddDays(-30)
            });
            AppDbContext.InMemoryDb.Clients.Add(new Client
            {
                Id = 2,
                CreatedDate = DateTimeOffset.Now.AddHours(-3)
            });
            AppDbContext.InMemoryDb.Clients.Add(new Client
            {
                Id = 3,
                CreatedDate = DateTimeOffset.Now.AddHours(-1)
            });
            AppDbContext.InMemoryDb.Clients.Add(new Client
            {
                Id = 4,
                CreatedDate = DateTimeOffset.Now.AddHours(3)
            });
            AppDbContext.InMemoryDb.Clients.Add(new Client
            {
                Id = 5,
                CreatedDate = DateTimeOffset.Now.AddDays(30)
            });
            var property = IqlExpression.GetPropertyExpression(nameof(Client.CreatedDate));
            var now = new IqlNowExpression();
            var eq = 
                new IqlAndExpression(
                    new IqlIsGreaterThanExpression(
                        property,
                        now),
                    new IqlIsLessThanExpression(
                        property,
                        now)
                    );
                ;

        }
        [TestMethod]
        public void TestGetDeepProperty()
        {
            var propertyExpected = Db.EntityConfigurationContext.EntityType<ApplicationUser>()
                .FindProperty(nameof(ApplicationUser.Client));
            var property = Db.EntityConfigurationContext.EntityType<Person>()
                .FindPropertyByExpression(p => p.Type.CreatedByUser.Client);
            Assert.AreEqual(propertyExpected, property);
            Assert.AreEqual(nameof(ApplicationUser.Client), property.Name);
            Assert.AreEqual(typeof(Client), property.TypeDefinition.Type);
            Assert.AreEqual(typeof(ApplicationUser), property.TypeDefinition.DeclaringType);
        }

        [TestMethod]
        public void GetPropertyPath()
        {
            var personConfiguration = Db.EntityConfigurationContext.EntityType<Person>();
            var userConfiguration = Db.EntityConfigurationContext.EntityType<ApplicationUser>();
            var personTypeConfiguration = Db.EntityConfigurationContext.EntityType<PersonType>();
            var clientConfiguration = Db.EntityConfigurationContext.EntityType<Client>();
            var path = IqlPropertyPath.FromLambda(u => u.Type.CreatedByUser.Client.AverageSales,
                personConfiguration);
            Assert.AreEqual(path.Property, clientConfiguration.FindProperty(nameof(Client.AverageSales)));
            var client = new Client();
            client.AverageSales = 12.3f;
            var person = new Person
            {
                Type = new PersonType
                {
                    CreatedByUser = new ApplicationUser
                    {
                        Client = client
                    }
                }
            };
            var averageSalesFound = path.Getter(person);
            Assert.AreEqual(client.AverageSales, averageSalesFound);
            var clientFound = path.Parent.Getter(person);
            Assert.AreEqual(client, clientFound);
            var pathPartsExpected = new List<IProperty>();
            pathPartsExpected.Add(personConfiguration.FindProperty(nameof(Person.Type)));
            pathPartsExpected.Add(personTypeConfiguration.FindProperty(nameof(PersonType.CreatedByUser)));
            pathPartsExpected.Add(userConfiguration.FindProperty(nameof(ApplicationUser.Client)));
            pathPartsExpected.Add(clientConfiguration.FindProperty(nameof(Client.AverageSales)));
            while (pathPartsExpected.Count > 0)
            {
                Assert.AreEqual(pathPartsExpected.Count, path.PropertyPath.Length);
                for (var i = 0; i < pathPartsExpected.Count; i++)
                {
                    var expectedProperty = pathPartsExpected[i];
                    var pathPath = path.PropertyPath[i];
                    Assert.AreEqual(expectedProperty, pathPath.Property);
                }

                path = path.Parent;
                pathPartsExpected.RemoveAt(pathPartsExpected.Count - 1);
            }
        }

        [TestMethod]
        public async Task ExpandShouldIncludeExpandedEntities()
        {
            var dates = new DateTimeOffset[] { DateTimeOffset.Now, DateTimeOffset.Now, };
            var root = new IqlRootReferenceExpression("root", "", typeof(RiskAssessment));
            var property = new IqlPropertyExpression(nameof(RiskAssessment.CreatedDate));
            property.Parent = root;
            var and = new IqlAndExpression(
                new IqlIsGreaterThanExpression(property, new IqlLiteralExpression(dates[0], IqlType.Date)),
                new IqlIsLessThanExpression(property, new IqlLiteralExpression(dates[1], IqlType.Date))
            );
            await Db.RiskAssessments.WhereEquals(and).ToListAsync();
        }
    }
}