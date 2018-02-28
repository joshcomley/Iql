using Iql.Tests.Context;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tunnel.App.Data.Entities;

namespace Iql.Tests.Tests.Validation
{
    [TestClass]
    public class ValidationTests
    {
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