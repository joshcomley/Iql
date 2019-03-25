using System;
using System.Threading.Tasks;
using Iql.Data;
using Iql.Data.Extensions;
using Iql.Entities.InferredValues;
using IqlSampleApp.Data.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Iql.Tests.Server
{
    [TestClass]
    public class InferredValueTests : ServerTestsBase
    {
        [TestMethod]
        public async Task InferValuesForUserSettings()
        {
            var controller = ControllerContext<UserSetting>(true);
            var dbObject = new UserSetting
            {
                Key1 = "Abc",
                Key2 = "Def",
                Value = "Fish"
            };
            Assert.AreEqual(default(DateTimeOffset), dbObject.CreatedDate);
            var clone = (UserSetting)dbObject.Clone(Builder, typeof(UserSetting));
            var inferredValuesResult = await controller.EntityConfiguration.TrySetInferredValuesAsync(
                clone,
                dbObject,
                true,
                controller.ServerEvaluator,
                ResolveServiceProviderProvider());
            Assert.AreNotEqual(default(DateTimeOffset), dbObject.CreatedDate);
        }

        [TestMethod]
        public async Task InferValuesForPersonWithBirthdayNotSet()
        {
            var controller = ControllerContext<Person>(true);
            var dbObject = new Person();
            Assert.AreEqual(default(DateTimeOffset), dbObject.CreatedDate);
            Assert.AreEqual(null, dbObject.Birthday);
            Assert.AreNotEqual(PersonCategory.Conventional, dbObject.Category);
            Assert.AreNotEqual(PersonSkills.Coder, dbObject.Skills);
            var inferredValuesResult = await controller.EntityConfiguration.TrySetInferredValuesAsync(
                null,
                dbObject,
                true,
                controller.ServerEvaluator,
                ResolveServiceProviderProvider());
            Assert.AreNotEqual(default(DateTimeOffset), dbObject.CreatedDate);
            Assert.AreEqual(null, dbObject.Birthday);
            Assert.AreEqual(PersonSkills.Coder, dbObject.Skills);
            Assert.AreEqual(PersonCategory.Conventional, dbObject.Category);
        }

        [TestMethod]
        public async Task InferValuesForPersonWithBirthdaySet()
        {
            var controller = ControllerContext<Person>(true);
            controller.EntityConfiguration.ConfigureProperty(p => p.Skills, p =>
            {
                p.IsInferredWith(_ => PersonSkills.Coder,
                    true,
                    InferredValueKind.IfNullOrEmpty,
                    true);
            });
            controller.EntityConfiguration.ConfigureProperty(p => p.Birthday, p =>
            {
                p.IsConditionallyInferredWith(
                    _ => new IqlNowExpression(),
                    _ => (_.OldEntityState == null || _.OldEntityState.Category != PersonCategory.AutoDescription) &&
                         _.CurrentEntityState.Category == PersonCategory.AutoDescription
                );
                p.IsConditionallyInferredWith(
                    _ => null,
                    _ => (_.OldEntityState != null && _.OldEntityState.Category == PersonCategory.AutoDescription) &&
                         _.CurrentEntityState.Category == PersonCategory.Conventional
                );
            });
            var dbObjectInitialize = new Person();
            dbObjectInitialize.Category = PersonCategory.AutoDescription;
            Assert.AreEqual(default(DateTimeOffset), dbObjectInitialize.CreatedDate);
            Assert.AreEqual(null, dbObjectInitialize.Birthday);
            Assert.AreNotEqual(PersonCategory.Conventional, dbObjectInitialize.Category);
            Assert.AreNotEqual(PersonSkills.Coder, dbObjectInitialize.Skills);
            var inferredValuesResult = await controller.EntityConfiguration.TrySetInferredValuesAsync(
                null,
                dbObjectInitialize,
                true,
                controller.ServerEvaluator,
                ResolveServiceProviderProvider());
            Assert.AreNotEqual(default(DateTimeOffset), dbObjectInitialize.CreatedDate);
            Assert.IsNotNull(dbObjectInitialize.Birthday);
            Assert.AreEqual(PersonSkills.Coder, dbObjectInitialize.Skills);
            Assert.AreEqual(PersonCategory.Conventional, dbObjectInitialize.Category);

            var dbObjectNoInitialize = new Person();
            dbObjectNoInitialize.Category = PersonCategory.AutoDescription;
            Assert.AreEqual(default(DateTimeOffset), dbObjectNoInitialize.CreatedDate);
            Assert.AreEqual(null, dbObjectNoInitialize.Birthday);
            Assert.AreNotEqual(PersonCategory.Conventional, dbObjectNoInitialize.Category);
            Assert.AreNotEqual(PersonSkills.Coder, dbObjectNoInitialize.Skills);
            inferredValuesResult = await controller.EntityConfiguration.TrySetInferredValuesAsync(
                null,
                dbObjectNoInitialize,
                false,
                controller.ServerEvaluator,
                ResolveServiceProviderProvider());
            Assert.AreNotEqual(default(DateTimeOffset), dbObjectNoInitialize.CreatedDate);
            Assert.IsNotNull(dbObjectNoInitialize.Birthday);
            Assert.AreEqual(PersonSkills.Coder, dbObjectNoInitialize.Skills);
        }
    }
}
