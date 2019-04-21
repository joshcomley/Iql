using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Iql.Data.Rendering;
using Iql.Entities;
using IqlSampleApp.Data.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Iql.Tests.Tests.Properties
{
    [TestClass]
    public class PropertyDetailTests : TestsBase
    {
        [TestMethod]
        public void TestStandardReadOrdering()
        {
            var clientEntityConfiguration = Db.EntityConfigurationContext.EntityType<Client>();
            var allProperties = clientEntityConfiguration.GetDisplayConfiguration(DisplayConfigurationKind.Read).Properties;
            var index = 0;
            Assert.AreEqual(nameof(Client.Id), allProperties[index++].Name);
            Assert.AreEqual(nameof(Client.Name), allProperties[index++].Name);
            Assert.AreEqual(nameof(Client.Description), allProperties[index++].Name);
            Assert.AreEqual(nameof(Client.Type), allProperties[index++].Name);
            Assert.AreEqual(nameof(Client.CreatedByUser), allProperties[index++].Name);
            Assert.AreEqual(nameof(Client.Users), allProperties[index++].Name);
            Assert.AreEqual(nameof(Client.People), allProperties[index++].Name);
            Assert.AreEqual(nameof(Client.Sites), allProperties[index++].Name);
            Assert.AreEqual(nameof(Client.AverageSales), allProperties[index++].Name);
            Assert.AreEqual(nameof(Client.AverageIncome), allProperties[index++].Name);
            Assert.AreEqual(nameof(Client.Category), allProperties[index++].Name);
            Assert.AreEqual(nameof(Client.Discount), allProperties[index++].Name);
            Assert.AreEqual(nameof(Client.CreatedDate), allProperties[index++].Name);
        }

        [TestMethod]
        public async Task TestStandardOrdering()
        {
            var site = new Site();
            var currentUser = new ApplicationUser();
            var siteEntityConfiguration = Db.EntityConfigurationContext.EntityType<Site>();
            var detail = PropertyDetail.For(siteEntityConfiguration);
            var displayConfiguration = siteEntityConfiguration.GetDisplayConfiguration(
                DisplayConfigurationKind.Edit,
                DisplayConfigurationKeys.Default);
            var instance = await detail.GetSnapshotAsync(site, typeof(Site), currentUser, typeof(ApplicationUser), Db, displayConfiguration, SnapshotOrdering.Standard);
            Assert.AreEqual(21, instance.ChildProperties.Length);
            var expectedIndex = 0;
            var xx = GetAsserts<Site>(instance);
            // N.B. "Parent" should be in here twice, as it is specified twice in the configuration
            AssertProperty(expectedIndex++, instance, nameof(Site.Id), false);
            AssertProperty(expectedIndex++, instance, nameof(Site.FullAddress), false);
            AssertProperty(expectedIndex++, instance, nameof(Site.CreatedByUser), false);
            AssertProperty(expectedIndex++, instance, nameof(Site.CreatedDate), false);
            AssertProperty(expectedIndex++, instance, nameof(Site.Documents), false, false);
            AssertProperty(expectedIndex++, instance, nameof(Site.AdditionalSendReportsTo), false, false);
            AssertProperty(expectedIndex++, instance, nameof(Site.People), false, false);
            AssertProperty(expectedIndex++, instance, nameof(Site.Children), false, false);
            AssertProperty(expectedIndex++, instance, nameof(Site.SiteInspections), false, false);
            AssertProperty(expectedIndex++, instance, nameof(Site.Users), false, false);
            AssertProperty(expectedIndex++, instance, nameof(Site.Areas), true);
            AssertProperty(expectedIndex++, instance, nameof(Site.Client), true);
            AssertProperty(expectedIndex++, instance, nameof(Site.Name), true);
            AssertProperty(expectedIndex++, instance, nameof(Site.Parent), false);
            AssertProperty(expectedIndex++, instance, "Site Address", true);
            var siteAddress = instance.ChildProperties.FirstOrDefault(_ => _.PropertyName == "Site Address");
            AssertProperty(expectedIndex++, instance, nameof(Site.Parent), false);
            AssertProperty(expectedIndex++, instance, nameof(Site.Key), false);
            AssertProperty(expectedIndex++, instance, nameof(Site.Location), true);
            Assert.AreEqual(PropertyRenderingKind.Tree, instance.ChildProperties[expectedIndex].Kind);
            AssertProperty(expectedIndex++, instance, "Hierarchy", true);
            AssertProperty(expectedIndex++, instance, nameof(Site.Area), true);
            AssertProperty(expectedIndex++, instance, nameof(Site.Line), true);
        }

        [TestMethod]
        public async Task TestGetCustomDetailExact()
        {
            var site = new Site();
            var currentUser = new ApplicationUser();
            var siteEntityConfiguration = Db.EntityConfigurationContext.EntityType<Site>();
            var detail = PropertyDetail.For(siteEntityConfiguration);
            var instance = await detail.GetSnapshotAsync(site, typeof(Site), currentUser, typeof(ApplicationUser), Db, siteEntityConfiguration.GetDisplayConfiguration(
                DisplayConfigurationKind.Edit,
                DisplayConfigurationKeys.Default), SnapshotOrdering.Default, false);
            Assert.AreEqual(7, instance.ChildProperties.Length);
            var expectedIndex = 0;
            AssertProperty(expectedIndex++, instance, nameof(Site.Client), true);
            AssertProperty(expectedIndex++, instance, nameof(Site.Name), true);
            AssertProperty(expectedIndex++, instance, nameof(Site.Parent), false);
            AssertProperty(expectedIndex++, instance, "Site Address", true);
            var siteAddress = instance.ChildProperties.FirstOrDefault(_ => _.PropertyName == "Site Address");
            AssertProperty(0, siteAddress, nameof(Site.Address), true);
            AssertProperty(1, siteAddress, nameof(Site.PostCode), true);
            AssertProperty(expectedIndex++, instance, nameof(Site.Parent), false);
            AssertProperty(expectedIndex++, instance, nameof(Site.Key), false);
            AssertProperty(expectedIndex++, instance, nameof(Site.Location), true);
        }

        [TestMethod]
        public async Task TestGetCustomDetailExactOrdered()
        {
            var site = new Site();
            var currentUser = new ApplicationUser();
            var siteEntityConfiguration = Db.EntityConfigurationContext.EntityType<Site>();
            var detail = PropertyDetail.For(siteEntityConfiguration);
            var instance = await detail.GetSnapshotAsync(site, typeof(Site), currentUser, typeof(ApplicationUser), Db, siteEntityConfiguration.GetDisplayConfiguration(
                DisplayConfigurationKind.Edit,
                DisplayConfigurationKeys.Default), SnapshotOrdering.ReadOnlyFirst, false);
            Assert.AreEqual(7, instance.ChildProperties.Length);
            var expectedIndex = 0;
            AssertProperty(expectedIndex++, instance, nameof(Site.Client), true);
            AssertProperty(expectedIndex++, instance, nameof(Site.Name), true);
            AssertProperty(expectedIndex++, instance, nameof(Site.Parent), false);
            AssertProperty(expectedIndex++, instance, "Site Address", true);
            var siteAddress = instance.ChildProperties.FirstOrDefault(_ => _.PropertyName == "Site Address");
            AssertProperty(0, siteAddress, nameof(Site.Address), true);
            AssertProperty(1, siteAddress, nameof(Site.PostCode), true);
            AssertProperty(expectedIndex++, instance, nameof(Site.Parent), false);
            AssertProperty(expectedIndex++, instance, nameof(Site.Key), false);
            AssertProperty(expectedIndex++, instance, nameof(Site.Location), true);
        }

        [TestMethod]
        public async Task TestGetCustomDetail()
        {
            var site = new Site();
            var currentUser = new ApplicationUser();
            var siteEntityConfiguration = Db.EntityConfigurationContext.EntityType<Site>();
            var detail = PropertyDetail.For(siteEntityConfiguration);
            var displayConfiguration = siteEntityConfiguration.GetDisplayConfiguration(
                DisplayConfigurationKind.Edit,
                DisplayConfigurationKeys.Default);
            var instance = await detail.GetSnapshotAsync(site, typeof(Site), currentUser, typeof(ApplicationUser), Db, displayConfiguration);
            Assert.AreEqual(21, instance.ChildProperties.Length);
            var expectedIndex = 0;
            AssertProperty(expectedIndex++, instance, nameof(Site.Client), true);
            AssertProperty(expectedIndex++, instance, nameof(Site.Name), true);
            AssertProperty(expectedIndex++, instance, nameof(Site.Parent), false);
            AssertProperty(expectedIndex++, instance, "Site Address", true);
            var siteAddress = instance.ChildProperties.FirstOrDefault(_ => _.PropertyName == "Site Address");
            AssertProperty(0, siteAddress, nameof(Site.Address), true);
            AssertProperty(1, siteAddress, nameof(Site.PostCode), true);
            AssertProperty(expectedIndex++, instance, nameof(Site.Parent), false);
            AssertProperty(expectedIndex++, instance, nameof(Site.Key), false);
            AssertProperty(expectedIndex++, instance, nameof(Site.Location), true);
            AssertProperty(expectedIndex++, instance, nameof(Site.Id), false);
            AssertProperty(expectedIndex++, instance, nameof(Site.FullAddress), false);
            AssertProperty(expectedIndex++, instance, nameof(Site.CreatedByUser), false);
            AssertProperty(expectedIndex++, instance, nameof(Site.Documents), false, false);
            AssertProperty(expectedIndex++, instance, nameof(Site.AdditionalSendReportsTo), false, false);
            AssertProperty(expectedIndex++, instance, nameof(Site.People), false, false);
            AssertProperty(expectedIndex++, instance, nameof(Site.Children), false, false);
            AssertProperty(expectedIndex++, instance, nameof(Site.Areas), true);
            AssertProperty(expectedIndex++, instance, nameof(Site.SiteInspections), false, false);
            AssertProperty(expectedIndex++, instance, nameof(Site.Users), false, false);
            AssertProperty(expectedIndex++, instance, "Hierarchy", true);
            AssertProperty(expectedIndex++, instance, nameof(Site.Area), true);
            AssertProperty(expectedIndex++, instance, nameof(Site.Line), true);
            AssertProperty(expectedIndex++, instance, nameof(Site.CreatedDate), false);
            //AssertProperty(expectedIndex++, instance, nameof(Site.Guid), false, false);
            //AssertProperty(expectedIndex++, instance, nameof(Site.RevisionKey), false, false);
            //AssertProperty(expectedIndex++, instance, nameof(Site.PersistenceKey), false, false);
        }

        [TestMethod]
        public async Task TestGetCustomDetailOrdered()
        {
            var site = new Site();
            var currentUser = new ApplicationUser();
            var siteEntityConfiguration = Db.EntityConfigurationContext.EntityType<Site>();
            var detail = PropertyDetail.For(siteEntityConfiguration);
            var displayConfiguration = siteEntityConfiguration.GetDisplayConfiguration(
                DisplayConfigurationKind.Edit,
                DisplayConfigurationKeys.Default);
            Assert.IsFalse(displayConfiguration.AutoGenerated);
            var instance = await detail.GetSnapshotAsync(site, typeof(Site), currentUser, typeof(ApplicationUser), Db, displayConfiguration, SnapshotOrdering.ReadOnlyFirst);
            Assert.AreEqual(21, instance.ChildProperties.Length);
            var expectedIndex = 0;
            AssertProperty(expectedIndex++, instance, nameof(Site.Id), false);
            AssertProperty(expectedIndex++, instance, nameof(Site.FullAddress), false);
            AssertProperty(expectedIndex++, instance, nameof(Site.CreatedByUser), false);
            AssertProperty(expectedIndex++, instance, nameof(Site.Documents), false, false);
            AssertProperty(expectedIndex++, instance, nameof(Site.AdditionalSendReportsTo), false, false);
            AssertProperty(expectedIndex++, instance, nameof(Site.People), false, false);
            AssertProperty(expectedIndex++, instance, nameof(Site.Children), false, false);
            AssertProperty(expectedIndex++, instance, nameof(Site.SiteInspections), false, false);
            AssertProperty(expectedIndex++, instance, nameof(Site.Users), false, false);
            //AssertProperty(expectedIndex++, instance, nameof(Site.Guid), false, false);
            AssertProperty(expectedIndex++, instance, nameof(Site.CreatedDate), false);
            //AssertProperty(expectedIndex++, instance, nameof(Site.RevisionKey), false, false);
            //AssertProperty(expectedIndex++, instance, nameof(Site.PersistenceKey), false, false);
            AssertProperty(expectedIndex++, instance, nameof(Site.Client), true);
            AssertProperty(expectedIndex++, instance, nameof(Site.Name), true);
            AssertProperty(expectedIndex++, instance, nameof(Site.Parent), false);
            AssertProperty(expectedIndex++, instance, "Site Address", true);
            var siteAddress = instance.ChildProperties.FirstOrDefault(_ => _.PropertyName == "Site Address");
            AssertProperty(0, siteAddress, nameof(Site.Address), true);
            AssertProperty(1, siteAddress, nameof(Site.PostCode), true);
            AssertProperty(expectedIndex++, instance, nameof(Site.Parent), false);
            AssertProperty(expectedIndex++, instance, nameof(Site.Key), false);
            AssertProperty(expectedIndex++, instance, nameof(Site.Location), true);
            AssertProperty(expectedIndex++, instance, nameof(Site.Areas), true);
            AssertProperty(expectedIndex++, instance, "Hierarchy", true);
            AssertProperty(expectedIndex++, instance, nameof(Site.Area), true);
            AssertProperty(expectedIndex++, instance, nameof(Site.Line), true);
        }

        private static string GetAsserts<T>(EntityPropertySnapshot snapshot)
        {
            var str = "";
            for (var i = 0; i < snapshot.ChildProperties.Length; i++)
            {
                var child = snapshot.ChildProperties[i];
                var parameters = new List<string>();
                parameters.Add("expectedIndex++");
                parameters.Add("instance");
                parameters.Add($"nameof({typeof(T).Name}.{child.PropertyName})");
                parameters.Add(child.CanEdit.ToString().ToLower());
                if (child.CanShow == false)
                {
                    parameters.Add("false");
                }
                str +=
                    $"AssertProperty({string.Join(", ", parameters)});\n";
            }

            return str;
        }

        [TestMethod]
        public async Task TestGetDetailWithNoEditOrReadPermission()
        {
            var person = new Person();
            var currentUser = new ApplicationUser();
            currentUser.Email = "CannotSeeOrEdit";
            var personEntityConfiguration = Db.EntityConfigurationContext.EntityType<Person>();
            var detail = PropertyDetail.For(personEntityConfiguration);
            var displayConfiguration = personEntityConfiguration.FindDisplayConfiguration(DisplayConfigurationKind.Edit);
            var instance = await detail.GetSnapshotAsync(person, typeof(Person), currentUser, typeof(ApplicationUser), Db, displayConfiguration);
            // Currently just check no infinite loop is created
            Assert.IsNotNull(instance);
            Assert.AreEqual(21, instance.ChildProperties.Length);
            var xxx = GetAsserts<Person>(instance);
            var expectedIndex = 0;

            AssertProperty(expectedIndex++, instance, "Photo", true);
            AssertProperty(expectedIndex++, instance, nameof(Person.Id), false);
            AssertProperty(expectedIndex++, instance, nameof(Person.Title), true);
            AssertProperty(expectedIndex++, instance, nameof(Person.Key), true);
            AssertProperty(expectedIndex++, instance, nameof(Person.InferredWhenKeyChanges), false);
            AssertProperty(expectedIndex++, instance, nameof(Person.Description), true);
            AssertProperty(expectedIndex++, instance, nameof(Person.Client), true);
            AssertProperty(expectedIndex++, instance, nameof(Person.Site), true);
            AssertProperty(expectedIndex++, instance, nameof(Person.SiteArea), true);
            AssertProperty(expectedIndex++, instance, nameof(Person.Type), true);
            AssertProperty(expectedIndex++, instance, nameof(Person.Loading), true);
            AssertProperty(expectedIndex++, instance, nameof(Person.CreatedByUser), false);
            AssertProperty(expectedIndex++, instance, nameof(Person.Types), false, false);
            AssertProperty(expectedIndex++, instance, nameof(Person.Reports), true);
            AssertProperty(expectedIndex++, instance, nameof(Person.Location), true);
            AssertProperty(expectedIndex++, instance, nameof(Person.Birthday), true);
            AssertProperty(expectedIndex++, instance, nameof(Person.IsComplete), true);
            AssertProperty(expectedIndex++, instance, nameof(Person.HasPaid), true);
            AssertProperty(expectedIndex++, instance, nameof(Person.Skills), false, false);
            AssertProperty(expectedIndex++, instance, nameof(Person.Category), true);
            AssertProperty(expectedIndex++, instance, nameof(Person.CreatedDate), false);
        }


        [TestMethod]
        public async Task TestGetDetailWithReadOnlyPermission()
        {
            var person = new Person();
            var currentUser = new ApplicationUser();
            currentUser.Email = "CanSeeCannotEdit";
            var personEntityConfiguration = Db.EntityConfigurationContext.EntityType<Person>();
            var detail = PropertyDetail.For(personEntityConfiguration);
            var displayConfiguration = personEntityConfiguration.FindDisplayConfiguration(DisplayConfigurationKind.Edit);
            var instance = await detail.GetSnapshotAsync(person, typeof(Person), currentUser, typeof(ApplicationUser), Db, displayConfiguration);
            // Currently just check no infinite loop is created
            Assert.IsNotNull(instance);
            Assert.AreEqual(21, instance.ChildProperties.Length);
            var xxx = GetAsserts<Person>(instance);
            var expectedIndex = 0;

            AssertProperty(expectedIndex++, instance, "Photo", true);
            AssertProperty(expectedIndex++, instance, nameof(Person.Id), false);
            AssertProperty(expectedIndex++, instance, nameof(Person.Title), true);
            AssertProperty(expectedIndex++, instance, nameof(Person.Key), true);
            AssertProperty(expectedIndex++, instance, nameof(Person.InferredWhenKeyChanges), false);
            AssertProperty(expectedIndex++, instance, nameof(Person.Description), true);
            AssertProperty(expectedIndex++, instance, nameof(Person.Client), true);
            AssertProperty(expectedIndex++, instance, nameof(Person.Site), true);
            AssertProperty(expectedIndex++, instance, nameof(Person.SiteArea), true);
            AssertProperty(expectedIndex++, instance, nameof(Person.Type), true);
            AssertProperty(expectedIndex++, instance, nameof(Person.Loading), true);
            AssertProperty(expectedIndex++, instance, nameof(Person.CreatedByUser), false);
            AssertProperty(expectedIndex++, instance, nameof(Person.Types), false, false);
            AssertProperty(expectedIndex++, instance, nameof(Person.Reports), true);
            AssertProperty(expectedIndex++, instance, nameof(Person.Location), true);
            AssertProperty(expectedIndex++, instance, nameof(Person.Birthday), true);
            AssertProperty(expectedIndex++, instance, nameof(Person.IsComplete), true);
            AssertProperty(expectedIndex++, instance, nameof(Person.HasPaid), true);
            AssertProperty(expectedIndex++, instance, nameof(Person.Skills), false);
            AssertProperty(expectedIndex++, instance, nameof(Person.Category), true);
            AssertProperty(expectedIndex++, instance, nameof(Person.CreatedDate), false);
        }

        [TestMethod]
        public async Task TestGetDetail()
        {
            var person = new Person();
            var currentUser = new ApplicationUser();
            var personEntityConfiguration = Db.EntityConfigurationContext.EntityType<Person>();
            var detail = PropertyDetail.For(personEntityConfiguration);
            var displayConfiguration = personEntityConfiguration.FindDisplayConfiguration(DisplayConfigurationKind.Edit);
            var instance = await detail.GetSnapshotAsync(person, typeof(Person), currentUser, typeof(ApplicationUser), Db, displayConfiguration);
            // Currently just check no infinite loop is created
            Assert.IsNotNull(instance);
            Assert.AreEqual(21, instance.ChildProperties.Length);
            var xxx = GetAsserts<Person>(instance);
            var expectedIndex = 0;

            AssertProperty(expectedIndex++, instance, "Photo", true);
            AssertProperty(expectedIndex++, instance, nameof(Person.Id), false);
            AssertProperty(expectedIndex++, instance, nameof(Person.Title), true);
            AssertProperty(expectedIndex++, instance, nameof(Person.Key), true);
            AssertProperty(expectedIndex++, instance, nameof(Person.InferredWhenKeyChanges), false);
            AssertProperty(expectedIndex++, instance, nameof(Person.Description), true);
            AssertProperty(expectedIndex++, instance, nameof(Person.Client), true);
            AssertProperty(expectedIndex++, instance, nameof(Person.Site), true);
            AssertProperty(expectedIndex++, instance, nameof(Person.SiteArea), true);
            AssertProperty(expectedIndex++, instance, nameof(Person.Type), true);
            AssertProperty(expectedIndex++, instance, nameof(Person.Loading), true);
            AssertProperty(expectedIndex++, instance, nameof(Person.CreatedByUser), false);
            AssertProperty(expectedIndex++, instance, nameof(Person.Types), false, false);
            AssertProperty(expectedIndex++, instance, nameof(Person.Reports), true);
            AssertProperty(expectedIndex++, instance, nameof(Person.Location), true);
            AssertProperty(expectedIndex++, instance, nameof(Person.Birthday), true);
            AssertProperty(expectedIndex++, instance, nameof(Person.IsComplete), true);
            AssertProperty(expectedIndex++, instance, nameof(Person.HasPaid), true);
            AssertProperty(expectedIndex++, instance, nameof(Person.Skills), true);
            AssertProperty(expectedIndex++, instance, nameof(Person.Category), true);
            AssertProperty(expectedIndex++, instance, nameof(Person.CreatedDate), false);
        }

        static void AssertProperty(int expectedIndex, EntityPropertySnapshot snapshot, string name, bool canEdit, bool canShow = true)
        {
            var prop = snapshot.ChildProperties[expectedIndex];
            Assert.IsNotNull(prop);
            Assert.AreEqual(name, prop.PropertyName);
            Assert.AreEqual(canEdit, prop.CanEdit);
            Assert.AreEqual(canShow, prop.CanShow);
        }
    }
}