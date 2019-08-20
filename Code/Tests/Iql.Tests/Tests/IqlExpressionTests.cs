using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Iql.Conversion;
using Iql.Data;
using Iql.Data.Evaluation;
using Iql.Entities;
using Iql.Entities.Extensions;
using Iql.Tests.Context;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IqlSampleApp.Data.Entities;

namespace Iql.Tests.Tests
{
    [TestClass]
    public class IqlExpressionTests : TestsBase
    {
        [TestMethod]
        public async Task EvaluateComplexExpressionSynchronouslyTest()
        {
            var person = new Person();
            person.Description = "Person Description";
            person.Client = new Client();
            person.Client.CreatedByUser = new ApplicationUser();
            person.Client.CreatedByUser.ClientId = 7;
            var result = await new EvaluationSession().EvaluateExpressionAsync(_ => _.Description + " - " + _.Client.CreatedByUser.ClientId, person, Db.EntityConfigurationContext, Db.EntityConfigurationContext);
            Assert.AreEqual("Person Description - 7", result.Result);
        }

        [TestMethod]
        public async Task EvaluateComplexExpressionWithDeadEndSynchronouslyTest()
        {
            var person = new Person();
            person.Description = "Person Description";
            person.Client = new Client();
            var result = await new EvaluationSession().EvaluateExpressionAsync(_ => _.Description + " - " + _.Client.CreatedByUser.ClientId, person, Db.EntityConfigurationContext, Db.EntityConfigurationContext);
            Assert.AreEqual("Person Description - ", result.Result);
        }

        [TestMethod]
        public async Task EvaluateComplexExpressionAsynchronouslyTest()
        {
            var person = new Person();
            person.Description = "Asynchronous Person Description";
            person.ClientId = 8;
            AppDbContext.InMemoryDb.Clients.Add(new Client
            {
                Id = 8,
                CreatedByUserId = "myuser",
                Name = "My Client"
            });
            AppDbContext.InMemoryDb.Clients.Add(new Client
            {
                Id = 9,
                CreatedByUserId = "myuser",
                Name = "My Other Client"
            });
            AppDbContext.InMemoryDb.Users.Add(new ApplicationUser
            {
                Id = "myuser",
                ClientId = 9
            });
            Expression<Func<Person, object>> expression =
                _ => _.Description + ": " + (_.CreatedByUserId == null ? "No User" : "Has User") + " - " + (_.Client.CreatedByUser.ClientId == null ? "No Client" : _.Client.CreatedByUser.Client.Name);
            //var iql = IqlConverter.Instance.ConvertLambdaExpressionToIqlByType(expression, typeof(Person)).Expression;
            //var xml = IqlXmlSerializer.SerializeToXml(iql);
            var result = await new EvaluationSession().EvaluateExpressionWithDbAsync(expression, person, Db);
            Assert.AreEqual("Asynchronous Person Description: No User - My Other Client", result.Result);
        }

        [TestMethod]
        public async Task EvaluateComplexExpressionWithDeadEndAsynchronouslyTest()
        {
            var person = new Person();
            person.Description = "Asynchronous Person Description";
            person.ClientId = 8;
            AppDbContext.InMemoryDb.Clients.Add(new Client
            {
                Id = 8
            });
            var result = await new EvaluationSession().EvaluateExpressionWithDbAsync(_ => _.Description + " - " + _.Client.CreatedByUser.ClientId, person, Db);
            Assert.AreEqual("Asynchronous Person Description - ", result.Result);
        }

        [TestMethod]
        public void TopLevelPropertyExpressionsTest()
        {
            Expression<Func<Person, string>> expression = _ => _.Description + " - " + _.Client.CreatedByUser.ClientId;
            var iql = IqlConverter.Instance.ConvertLambdaExpressionToIqlByType(expression, Db.EntityConfigurationContext, typeof(Person)).Expression;
            var propertyExpressions = iql.TopLevelPropertyExpressions();
            Assert.AreEqual(2, propertyExpressions.Length);
            Assert.AreEqual(nameof(Person.Description), (propertyExpressions[0].Expression as IqlPropertyExpression).PropertyName);
            Assert.AreEqual(nameof(ApplicationUser.ClientId), (propertyExpressions[1].Expression as IqlPropertyExpression).PropertyName);
        }

        [TestMethod]
        public async Task SimpleQueryableToIql()
        {
            var query = Db.Clients
                .Where(c => c.Name.StartsWith("abc"))
                .Where(c => c.AverageIncome > 10)
                .WhereEquals(new IqlIsGreaterThanOrEqualToExpression(
                    IqlExpression.GetPropertyExpression(nameof(Client.AverageSales)),
                    new IqlLiteralExpression(7)))
                .OrderByDescending(c => c.AverageIncome)
                .ExpandSingle(c => c.CreatedByUser, queryable => queryable.Where(u => u.FullName == "Hopper").Expand(u => u.ClientsCreated));
            var iql = await query.ToIqlAsync();
            //var xml = IqlSerializer.SerializeToXml(iql);
            //File.WriteAllText(@"D:\Code\iql-query.xml", xml);
        }

        [TestMethod]
        public async Task FilterCollectionNative()
        {
            var query = TestPrep.PrepFilterCollectionTest();
            var results = await query.ToListAsync();
            Assert.AreEqual(2, results.Count);
            Assert.AreEqual(2, results[0].Id);
            Assert.AreEqual(3, results[1].Id);
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
        }

        [TestMethod]
        public void TestGetDeepProperty()
        {
            var propertyExpected = Db.EntityConfigurationContext.EntityType<ApplicationUser>()
                .FindProperty(nameof(ApplicationUser.Client));
            var property = Db.EntityConfigurationContext.EntityType<Person>()
                .FindNestedPropertyByExpression(p => p.Type.CreatedByUser.Client);
            Assert.AreEqual(propertyExpected, property);
            Assert.AreEqual(nameof(ApplicationUser.Client), property.Name);
            Assert.AreEqual(typeof(Client), property.TypeDefinition.Type);
            Assert.AreEqual(typeof(ApplicationUser), property.TypeDefinition.DeclaringType);
        }

        [TestMethod]
        public void RebasePropertyPath()
        {
            var personConfiguration = Db.EntityConfigurationContext.EntityType<Person>();
            var pathBase = IqlPropertyPath.FromLambda<Person>(u => u.Type.CreatedByUser,
                Db.EntityConfigurationContext);
            Assert.AreEqual($"{nameof(Person.Type)}/{nameof(PersonType.CreatedByUser)}", pathBase.PathToHere);
            var path = IqlPropertyPath.FromLambda<Person>(u => u.Type.CreatedByUser.Client.AverageSales,
                Db.EntityConfigurationContext);
            Assert.AreEqual($"{nameof(Person.Type)}/{nameof(PersonType.CreatedByUser)}/{nameof(ApplicationUser.Client)}/{nameof(Client.AverageSales)}", path.PathToHere);
            var subPath = path.RebaseFrom(pathBase);
            Assert.AreEqual($"{nameof(ApplicationUser.Client)}/{nameof(Client.AverageSales)}", subPath.PathToHere);
        }

        [TestMethod]
        public void GetPropertyPath()
        {
            var personConfiguration = Db.EntityConfigurationContext.EntityType<Person>();
            var userConfiguration = Db.EntityConfigurationContext.EntityType<ApplicationUser>();
            var personTypeConfiguration = Db.EntityConfigurationContext.EntityType<PersonType>();
            var clientConfiguration = Db.EntityConfigurationContext.EntityType<Client>();
            var path = IqlPropertyPath.FromLambda<Person>(u => u.Type.CreatedByUser.Client.AverageSales,
                Db.EntityConfigurationContext);
            Assert.AreEqual(path.Property.EntityProperty(), clientConfiguration.FindProperty(nameof(Client.AverageSales)));
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
            var averageSalesFound = path.GetValue(person);
            Assert.AreEqual(client.AverageSales, averageSalesFound);
            var clientFound = path.Parent.GetValue(person);
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
                    Assert.AreEqual(expectedProperty, pathPath.Property.EntityProperty());
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

        [TestMethod]
        public void FlattenIqlExpressionsTest()
        {
            var dates = new DateTimeOffset[] { DateTimeOffset.Now, DateTimeOffset.Now, };
            var root = new IqlRootReferenceExpression("root", "", typeof(RiskAssessment));
            var property = new IqlPropertyExpression(nameof(RiskAssessment.CreatedDate));
            property.Parent = root;
            var literal = new IqlLiteralExpression(dates[0], IqlType.Date);
            var left = new IqlIsGreaterThanExpression(property, literal);
            var right = new IqlIsLessThanExpression(property, literal);
            var and = new IqlAndExpression(
                left,
                right
            );
            var flattened = and.Flatten().Select(_ => _.Expression).ToArray();
            Assert.AreEqual(6, flattened.Length);
            Assert.IsTrue(flattened.Contains(root));
            Assert.IsTrue(flattened.Contains(property));
            Assert.IsTrue(flattened.Contains(literal));
            Assert.IsTrue(flattened.Contains(left));
            Assert.IsTrue(flattened.Contains(right));
            Assert.IsTrue(flattened.Contains(and));
        }
    }
}