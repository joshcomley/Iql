﻿using Iql.Data.Extensions;
using Iql.Entities;
#if !TypeScript
using Iql.Server.Serialization;
#endif
using Iql.Tests.Context;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;
using Iql.Entities.Dates;
using Tunnel.App.Data.Entities;

namespace Iql.Tests.Tests.EntityConfiguration
{
    [TestClass]
    public class EntityConfigurationTests : TestsBase
    {
        [TestMethod]
        public void MediaKeyShouldBeParseable()
        {
            var property = Db.EntityConfigurationContext.EntityType<ApplicationUser>().FindPropertyByExpression(l => l.FullName);
            var stringKey = "photo";
            var file = new File<ApplicationUser>(property);
            var mediaKey = new MediaKey<ApplicationUser>(file)
                .AddGroup(g =>
                    g.AddPropertyPath(l => l.Client.Type.Id)
                )
                .AddGroup(g =>
                    g
                        .AddPropertyPath(l => l.Id)
                        .AddString(stringKey))
                ;
            var user = new ApplicationUser();
            user.Id = "myuserid";
            user.Client = new Client();
            user.Client.Type = new ClientType();
            user.Client.Type.Id = 7;
            var evaluated = mediaKey.Evaluate(user);
            Assert.AreEqual(user.Client.Type.Id.ToString(), evaluated[0][0]);
            Assert.AreEqual(user.Id, evaluated[1][0]);
            Assert.AreEqual(stringKey, evaluated[1][1]);
            var evaluatedToString = mediaKey.EvaluateToString(user);
            Assert.AreEqual($"{user.Client.Type.Id.ToString()}/{user.Id}-{stringKey}", evaluatedToString);
            var relationshipPath = mediaKey.Groups[0].Parts[0].GetRelationshipPath();
            Assert.AreEqual($"{nameof(ApplicationUser.Client)}/{nameof(Client.Type)}", relationshipPath.PathToHere);
            mediaKey.Clear();
        }

        [TestMethod]
        public async Task MediaKeyShouldBeLazyLoaded()
        {
            var property = Db.EntityConfigurationContext.EntityType<ApplicationUser>().FindPropertyByExpression(l => l.FullName);
            var user = new ApplicationUser();
            user.Id = "myuserid";
            user.ClientId = 771;
            var client = new Client { Id = 771, TypeId = 441 };
            var clientType = new ClientType { Id = 441, Name = "4500dd19-e220-43d8-b178-80a0bdab8753" };
            AppDbContext.InMemoryDb.Users.Add(user);
            AppDbContext.InMemoryDb.Clients.Add(client);
            AppDbContext.InMemoryDb.ClientTypes.Add(clientType);
            var stringKey = "photo";
            var file = new File<ApplicationUser>(property);
            var mediaKey = new MediaKey<ApplicationUser>(file)
                .AddGroup(g =>
                    g.AddPropertyPath(l => l.Client.Type.Name)
                )
                .AddGroup(g =>
                    g
                        .AddPropertyPath(l => l.Id)
                        .AddString(stringKey))
                ;
            var dbUser = await Db.Users.GetWithKeyAsync(user.Id);
            var evaluated = await mediaKey.EvaluateAsync(dbUser, Db);
            Assert.AreEqual(clientType.Name, evaluated[0][0]);
            Assert.AreEqual(user.Id, evaluated[1][0]);
            Assert.AreEqual(stringKey, evaluated[1][1]);
            var evaluatedToString = await mediaKey.EvaluateToStringAsync(dbUser, Db);
            Assert.AreEqual($"{clientType.Name}/{user.Id}-{stringKey}", evaluatedToString);
            mediaKey.Clear();
        }

        [TestMethod]
        public async Task MediaKeyDoubleTest()
        {
            MediaKeyShouldBeParseable();
            await MediaKeyShouldBeLazyLoaded();
        }

        [TestMethod]
        public void CountPropertyShouldHaveCountKind()
        {
            var property = Db.EntityConfigurationContext.EntityType<Client>()
                .FindPropertyByExpression(p => p.SitesCount);
            Assert.IsTrue(property.Kind.HasFlag(PropertyKind.Count));
        }

        [TestMethod]
        public void CountPropertyShouldBeSet()
        {
            var clientsCreated = Db.EntityConfigurationContext.EntityType<ApplicationUser>().FindPropertyByExpression(p => p.ClientsCreated);
            var clientCreatedBy = Db.EntityConfigurationContext.EntityType<Client>().FindPropertyByExpression(p => p.CreatedByUser);
            var clientsCreatedCount = Db.EntityConfigurationContext.EntityType<ApplicationUser>().FindPropertyByExpression(p => p.ClientsCreatedCount);
            Assert.AreEqual(clientsCreatedCount, clientsCreated.Relationship.ThisEnd.CountProperty);
            Assert.AreEqual(clientsCreatedCount, clientCreatedBy.Relationship.ThisEnd.CountProperty);
        }

        [TestMethod]
        public void ResolveTypeFromTypeName()
        {
            var type = EntityConfigurationBuilder.FindEntityTypeFromName(nameof(ApplicationUser));
            Assert.AreEqual(typeof(ApplicationUser), type);
        }

        [TestMethod]
        public void GetDeepPropertyFromString()
        {
            var propertyPath = string.Join("/", new[]
            {
                    nameof(Person.Type),
                    nameof(PersonType.CreatedByUser),
                    nameof(ApplicationUser.Client),
                    nameof(Client.AverageSales)
                });
            var property = Db.EntityConfigurationContext.EntityType<Person>()
                .FindNestedProperty(
                    propertyPath);
            Assert.AreEqual(nameof(Client.AverageSales), property.Name);
        }

        [TestMethod]
        public void GetPrimarySearchProperties()
        {
            var searchProperties = Db.EntityConfigurationContext.EntityType<Person>()
                .ResolveSearchProperties();
            Assert.AreEqual(1, searchProperties.Length);
            Assert.IsNotNull(searchProperties.SingleOrDefault(p => p.Name == nameof(Person.Title)));
        }

        [TestMethod]
        public void GetSecondarySearchProperties()
        {
            var searchProperties = Db.EntityConfigurationContext.EntityType<Person>()
                .ResolveSearchProperties(PropertySearchKind.Secondary);
            Assert.AreEqual(4, searchProperties.Length);
            Assert.IsNotNull(searchProperties.SingleOrDefault(p => p.Name == nameof(Person.Key)));
            Assert.IsNotNull(searchProperties.SingleOrDefault(p => p.Name == nameof(Person.Title)));
            Assert.IsNotNull(searchProperties.SingleOrDefault(p => p.Name == nameof(Person.Description)));
            Assert.IsNotNull(searchProperties.SingleOrDefault(p => p.Name == nameof(Person.RevisionKey)));
        }

        [TestMethod]
        public void TestSetNameIsResolvedFromPropertyName()
        {
            Assert.AreEqual(nameof(AppDbContext.People), Db.EntityConfigurationContext.EntityType<Person>().SetName);
        }
    }
}