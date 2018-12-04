using System;
using System.Linq;
using System.Threading.Tasks;
using Iql.Data.Crud.Operations.Results;
using Iql.Entities;
using Iql.Extensions;
using Iql.Tests.Context;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IqlSampleApp.Data.Entities;

namespace Iql.Tests.Tests.Validation
{
    [TestClass]
    public class ValidationTests : TestsBase
    {
        [TestMethod]
        public async Task EmptyInferredWithShouldFailValidationIfValidateClientSideIsTrue()
        {
            Db.EntityConfigurationContext.ValidateInferredWithClientSide = true;
            var site = new Site();
            Db.Sites.Add(site);
            var result = await Db.SaveChangesAsync();
            Assert.IsFalse(result.Success);
        }

        [TestMethod]
        public async Task EmptyInferredWithShouldSucceedValidationIfValidateClientSideIsFalse()
        {
            Db.EntityConfigurationContext.ValidateInferredWithClientSide = false;
            var site = new Site();
            Db.Sites.Add(site);
            var result = await Db.SaveChangesAsync();
            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public async Task AddingAnEntityWithANonValidatedButNonNullablePropertySetToNullShouldFailAutoValidation()
        {
            var inspection = EntityHelper.NewSiteInspection();
            inspection.SiteId = 0;
            var db = new AppDbContext();
            db.SiteInspections.Add(inspection);
            var saveChangesResult = await db.SaveChangesAsync();
            AssertPropertyValidationFailures(
                saveChangesResult,
                new ExpectedPropertyValidationFailure(nameof(SiteInspection.Site),
                    EntityConfigurationBase.DefaultRequiredAutoValidationFailureKey,
                    EntityConfigurationBase.DefaultRequiredAutoValidationFailureMessage));
        }

        [TestMethod]
        public async Task EmptyStringShouldFailNonNullableTest()
        {
            var reportCategory = new ReportCategory();
            reportCategory.Name = "";
            var db = new AppDbContext();
            db.ReportCategories.Add(reportCategory);
            var saveChangesResult = await db.SaveChangesAsync();
            AssertSinglePropertyValidationFailure(
                saveChangesResult,
                nameof(ReportCategory.Name),
                EntityConfigurationBase.DefaultRequiredAutoValidationFailureKey,
                EntityConfigurationBase.DefaultRequiredAutoValidationFailureMessage);
        }

        [TestMethod]
        public async Task EmptyEnumShouldFailNonNullableTest()
        {
            var personInspection = EntityHelper.NewPersonInspection();
            personInspection.InspectionStatus = 0;
            var db = new AppDbContext();
            db.PersonInspections.Add(personInspection);
            var saveChangesResult = await db.SaveChangesAsync();
            AssertSinglePropertyValidationFailure(
                saveChangesResult,
                nameof(PersonInspection.InspectionStatus),
                EntityConfigurationBase.DefaultRequiredAutoValidationFailureKey,
                EntityConfigurationBase.DefaultRequiredAutoValidationFailureMessage);
        }
        
        [TestMethod]
        public async Task EmptyReferenceFieldShouldFailNonNullableTest()
        {
            // Mark key as generated remotely so our entity will be
            // attempted to be inserted upon save
            Db.EntityConfigurationContext.EntityType<PersonTypeMap>()
                .Key.SetEditKind(PropertyEditKind.Hidden);
            var personTypeMap = EntityHelper.NewPersonTypeMap();
            personTypeMap.PersonId = 0;
            personTypeMap.TypeId = 0;
            var db = new AppDbContext();
            db.PersonTypesMap.Add(personTypeMap);
            var changes = db.DataStore.GetChanges();
            //Assert.AreEqual(1, changes.Length);
            var saveChangesResult = await db.SaveChangesAsync();
            Assert.IsFalse(saveChangesResult.Success);
            changes = db.DataStore.GetChanges();
            Assert.AreEqual(1, changes.Length);
            AssertPropertyValidationFailures(
                saveChangesResult,
                new ExpectedPropertyValidationFailure(nameof(PersonTypeMap.Person),
                    EntityConfigurationBase.DefaultRequiredAutoValidationFailureKey,
                    EntityConfigurationBase.DefaultRequiredAutoValidationFailureMessage),
                new ExpectedPropertyValidationFailure(nameof(PersonTypeMap.Type),
                    EntityConfigurationBase.DefaultRequiredAutoValidationFailureKey,
                    EntityConfigurationBase.DefaultRequiredAutoValidationFailureMessage));
            Db.EntityConfigurationContext.EntityType<PersonTypeMap>()
                .Key.SetEditKind(PropertyEditKind.Edit);
        }

#if TypeScript
        [TestMethod]
        public async Task NullReferenceFieldShouldFailNonNullableTest()
        {
            // Mark key as generated remotely so our entity will be
            // attempted to be inserted upon save
            Db.EntityConfigurationContext.EntityType<PersonTypeMap>()
                .Key.SetEditKind(PropertyEditKind.Hidden);
            var personTypeMap = EntityHelper.NewPersonTypeMap();
            personTypeMap.SetPropertyValueByName(nameof(PersonTypeMap.PersonId), null);
            personTypeMap.SetPropertyValueByName(nameof(PersonTypeMap.TypeId), null);
            var db = new AppDbContext();
            db.PersonTypesMap.Add(personTypeMap);
            var saveChangesResult = await db.SaveChangesAsync();
            AssertPropertyValidationFailures(
                saveChangesResult,
                new ExpectedPropertyValidationFailure(nameof(PersonTypeMap.Person),
                    EntityConfigurationBase.DefaultRequiredAutoValidationFailureKey,
                    EntityConfigurationBase.DefaultRequiredAutoValidationFailureMessage),
                new ExpectedPropertyValidationFailure(nameof(PersonTypeMap.Type),
                    EntityConfigurationBase.DefaultRequiredAutoValidationFailureKey,
                    EntityConfigurationBase.DefaultRequiredAutoValidationFailureMessage));
            Db.EntityConfigurationContext.EntityType<PersonTypeMap>()
                .Key.SetEditKind(PropertyEditKind.Edit);
        }
#endif

        [TestMethod]
        public async Task EmptyDateShouldFailNonNullableTest()
        {
            var personInspection = EntityHelper.NewPersonInspection();
            personInspection.StartTime = new DateTimeOffset();
            personInspection.InspectionStatus = PersonInspectionStatus.PassWithObservations;
            var db = new AppDbContext();
            db.PersonInspections.Add(personInspection);
            var saveChangesResult = await db.SaveChangesAsync();
            AssertSinglePropertyValidationFailure(
                saveChangesResult,
                nameof(PersonInspection.StartTime),
                EntityConfigurationBase.DefaultRequiredAutoValidationFailureKey,
                EntityConfigurationBase.DefaultRequiredAutoValidationFailureMessage);
        }

        class ExpectedPropertyValidationFailure
        {
            public string PropertyName { get; set; }
            public string ExpectedKey { get; set; }
            public string ExpectedMessage { get; set; }

            public ExpectedPropertyValidationFailure(string propertyName, string expectedKey, string expectedMessage)
            {
                PropertyName = propertyName;
                ExpectedKey = expectedKey;
                ExpectedMessage = expectedMessage;
            }
        }

        private static void AssertSinglePropertyValidationFailure(SaveChangesResult saveChangesResult,
            string propertyName,
            string expectedKey,
            string expectedMessage)
        {
            AssertPropertyValidationFailures(saveChangesResult, new ExpectedPropertyValidationFailure(
                propertyName,
                expectedKey,
                expectedMessage
            ));
        }

        private static void AssertPropertyValidationFailures(SaveChangesResult saveChangesResult, params ExpectedPropertyValidationFailure[] expectedFailures)
        {
            Assert.IsFalse(saveChangesResult.Success);
            Assert.AreEqual(1, saveChangesResult.Results.Count);
            var result = saveChangesResult.Results[0];
            Assert.AreEqual(1, result.EntityValidationResults.Count);
            var entityValidationResult = result.EntityValidationResults.First().Value;
            Assert.AreEqual(expectedFailures.Length, entityValidationResult.PropertyValidationResults.Count());
            foreach (var expectedFailure in expectedFailures)
            {
                var propertyValidationResult = entityValidationResult.PropertyValidationResults.First(
                    pf => pf.Property.Name == expectedFailure.PropertyName);
                Assert.IsNotNull(propertyValidationResult);
                Assert.AreEqual(1, propertyValidationResult.ValidationFailures.Count);
                var validationFailure = propertyValidationResult.ValidationFailures[0];
                Assert.AreEqual(validationFailure.Key, expectedFailure.ExpectedKey);
                Assert.AreEqual(validationFailure.Message, expectedFailure.ExpectedMessage);
            }
        }

        [TestMethod]
        public async Task SavingANewButInvalidEntityShouldFailBeforePosting()
        {
            var person = new Person();
            person.Title = "a";
            var db = new AppDbContext();
            db.People.Add(person);
            var result = await db.SaveChangesAsync();
            Assert.AreEqual(false, result.Success);
            Assert.AreEqual(1, result.Results.Count);
            var entityValidationResults = result.Results[0].EntityValidationResults;
            Assert.AreEqual(1, entityValidationResults.Count);
            Assert.AreEqual(2, entityValidationResults[person].PropertyValidationResults.Count());
            var title = entityValidationResults[person].PropertyValidationResults
                .First(p => p.Property.Name == nameof(Person.Title));
            Assert.IsNotNull(title);
            Assert.AreEqual(1, title.ValidationFailures.Count);
            Assert.AreEqual("TitleMinLength", title.ValidationFailures[0].Key);
            var description = entityValidationResults[person].PropertyValidationResults
                .First(p => p.Property.Name == nameof(Person.Description));
            Assert.IsNotNull(description);
            Assert.AreEqual(1, description.ValidationFailures.Count);
            Assert.AreEqual("EmptyDescription", description.ValidationFailures[0].Key);
            //Assert.AreEqual(entityValidationResult.ValidationFailures.Count, 1);
            //Assert.AreEqual("NoTitleOrDescription", entityValidationResult.ValidationFailures[0].Key);
        }

        [TestMethod]
        public void TestValidateProperty()
        {
            var person = new Person();
            person.Title = "a";
            var entityConfig = new AppDbContext().EntityConfigurationContext.EntityType<Person>();

            var propertyValidationResult = entityConfig.ValidateEntityPropertyByExpression(person, p => p.Title);
            Assert.IsNotNull(propertyValidationResult);
            Assert.AreEqual(propertyValidationResult.ValidationFailures.Count, 1);
            Assert.AreEqual("TitleMinLength", propertyValidationResult.ValidationFailures[0].Key);

            person.Title = "This title is more than fifty characters long and so should fail the validation";

            propertyValidationResult = entityConfig.ValidateEntityPropertyByExpression(person, p => p.Title);
            Assert.IsNotNull(propertyValidationResult);
            Assert.AreEqual(propertyValidationResult.ValidationFailures.Count, 1);
            Assert.AreEqual("TitleMaxLength", propertyValidationResult.ValidationFailures[0].Key);
        }

        [TestMethod]
        public void TestValidateEntity()
        {
            var person = new Person();
            var entityConfig = new AppDbContext().EntityConfigurationContext.EntityType<Person>();

            var entityValidationResult = entityConfig.ValidateEntity(person);
            Assert.IsNotNull(entityValidationResult);
            Assert.AreEqual(entityValidationResult.ValidationFailures.Count, 1);
            Assert.AreEqual("NoTitleOrDescription", entityValidationResult.ValidationFailures[0].Key);
        }
    }
}