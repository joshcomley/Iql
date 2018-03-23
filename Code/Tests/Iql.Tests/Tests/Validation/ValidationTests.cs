using System;
using System.Linq;
using System.Threading.Tasks;
using Iql.Tests.Context;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tunnel.App.Data.Entities;

namespace Iql.Tests.Tests.Validation
{
    [TestClass]
    public class ValidationTests
    {
        [TestMethod]
        public async Task SavingANewButInvalidEntityShouldFailBeforePosting()
        {
            var person = new Person();
            person.Title = "a";
            var db = new AppDbContext();
            db.People.Add(person);
            var result = await db.SaveChanges();
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