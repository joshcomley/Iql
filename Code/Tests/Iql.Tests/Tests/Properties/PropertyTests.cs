using Microsoft.VisualStudio.TestTools.UnitTesting;
using IqlSampleApp.Data.Entities;

namespace Iql.Tests.Tests.Properties
{
    [TestClass]
    public class PropertyTests : TestsBase
    {
        [TestMethod]
        public void TestPropertyResolveFriendlyName()
        {
            var config = Db.EntityConfigurationContext.EntityType<Client>();
            var property = config.FindPropertyByExpression(c => c.CreatedDate);
            Assert.AreEqual(nameof(Client.CreatedDate), property.Name);
            Assert.AreEqual("Created Date", property.FriendlyName);
        }

        [TestMethod]
        public void TestPropertyNullability()
        {
            Assert.AreEqual(false,
                Db.EntityConfigurationContext.EntityType<Client>().FindPropertyByExpression(p => p.CreatedDate)
                    .TypeDefinition.Nullable);
        }

        [TestMethod]
        public void TestPropertyIqlTypeString()
        {
            Assert.AreEqual(IqlType.String,
                Db.EntityConfigurationContext.EntityType<Client>().FindPropertyByExpression(p => p.Name)
                    .TypeDefinition.Kind);
        }

        [TestMethod]
        public void TestPropertyIqlTypeDate()
        {
            Assert.AreEqual(IqlType.Date,
                Db.EntityConfigurationContext.EntityType<Client>().FindPropertyByExpression(p => p.CreatedDate)
                    .TypeDefinition.Kind);
        }

        [TestMethod]
        public void TestPropertyIqlTypeInteger()
        {
            Assert.AreEqual(IqlType.Integer,
                Db.EntityConfigurationContext.EntityType<Client>().FindPropertyByExpression(p => p.Id)
                    .TypeDefinition.Kind);
        }

        [TestMethod]
        public void TestPropertyIqlTypeDecimalFromDecimal()
        {
            Assert.AreEqual(IqlType.Decimal,
                Db.EntityConfigurationContext.EntityType<Client>().FindPropertyByExpression(p => p.Discount)
                    .TypeDefinition.Kind);
        }

        [TestMethod]
        public void TestPropertyIqlTypeDecimalFromFloat()
        {
            Assert.AreEqual(IqlType.Decimal,
                Db.EntityConfigurationContext.EntityType<Client>().FindPropertyByExpression(p => p.AverageSales)
                    .TypeDefinition.Kind);
        }

        [TestMethod]
        public void TestPropertyIqlTypeDecimalFromDouble()
        {
            Assert.AreEqual(IqlType.Decimal,
                Db.EntityConfigurationContext.EntityType<Client>().FindPropertyByExpression(p => p.AverageIncome)
                    .TypeDefinition.Kind);
        }

        [TestMethod]
        public void TestPropertyIqlTypeBoolean()
        {
            Assert.AreEqual(IqlType.Boolean,
                Db.EntityConfigurationContext.EntityType<ApplicationUser>().FindPropertyByExpression(p => p.EmailConfirmed)
                    .TypeDefinition.Kind);
        }
    }
}