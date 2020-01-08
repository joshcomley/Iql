using System;
using System.Threading.Tasks;
using Iql.Data;
using Iql.Data.Evaluation;
using Iql.Data.Extensions;
using Iql.Entities.InferredValues;
using Iql.Server.Serialization.Serialization;
using IqlSampleApp.Data.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Iql.Tests.Server
{
    [TestClass]
    public class InferredValueTestsUserSetting : ServerTestsBase<UserSetting>
    {
        [TestMethod]
        public async Task InferValueChain()
        {
            var controller = ControllerContext<Site>();
            var site = new Site();
            var clone = (Site)site.Clone(Builder, typeof(Site));
            ServerTestCurrentUserService.RegisterUser("testuser", new ApplicationUser
            {
                UserName = "testuser"
            });
            var session = new InferredValueEvaluationSession();
            var inferredValuesResult = await session
                .TrySetInferredValuesCustomAsync(
                    controller.EntityConfiguration,
                    clone,
                    site,
                    true,
                    controller.ServerEvaluator,
                    ResolveServiceProviderProvider());
            Assert.AreEqual(site.InferredChainFromUserName, "testuser");
            Assert.AreEqual(site.InferredChainFromSelf, "testuser");
        }

        [TestMethod]
        public async Task InferValuesForUserSettings()
        {
            var controller = ControllerContext();
            var dbObject = new UserSetting
            {
                Key1 = "Abc",
                Key2 = "Def",
                Value = "Fish"
            };
            controller.ServerEvaluator.MarkAsUnsaved(dbObject);
            Assert.AreEqual(default(DateTimeOffset), dbObject.CreatedDate);
            var clone = (UserSetting)dbObject.Clone(Builder, typeof(UserSetting));
            var inferredValuesResult = await new InferredValueEvaluationSession()
                .TrySetInferredValuesCustomAsync(
                    controller.EntityConfiguration,
                    clone,
                    dbObject,
                    true,
                    controller.ServerEvaluator,
                    ResolveServiceProviderProvider());
            Assert.AreNotEqual(default(DateTimeOffset), dbObject.CreatedDate);
        }
    }

    [TestClass]
    public class InferredValueTests : ServerTestsBase<Person>
    {
        [TestMethod]
        public async Task InferValuesForPersonWithBirthdayNotSet()
        {
            var client = Client;
            var controller = ControllerContext();
            var dbObject = new Person();
            controller.ServerEvaluator.MarkAsUnsaved(dbObject);
            ServerTestCurrentUserService.RegisterUser("testuser", new ApplicationUser
            {
                UserName = "testuser",
                ClientId = client.Id
            });
            Assert.AreEqual(default(DateTimeOffset), dbObject.CreatedDate);
            Assert.AreEqual(null, dbObject.Birthday);
            Assert.AreNotEqual(PersonCategory.Conventional, dbObject.Category);
            Assert.AreNotEqual(PersonSkills.Coder, dbObject.Skills);
            var inferredValuesResult = await new InferredValueEvaluationSession().TrySetInferredValuesCustomAsync(
                controller.EntityConfiguration,
                null,
                dbObject,
                true,
                controller.ServerEvaluator,
                ResolveServiceProviderProvider());
            Assert.AreNotEqual(default(DateTimeOffset), dbObject.CreatedDate);
            Assert.AreEqual(null, dbObject.Birthday);
            Assert.AreEqual(client.Id, dbObject.InferredFromUserClientId);
            Assert.IsNotNull(dbObject.InferredFromUserClient);
            Assert.AreEqual(null, dbObject.Birthday);
            Assert.AreEqual(PersonSkills.Coder, dbObject.Skills);
            Assert.AreEqual(PersonCategory.Conventional, dbObject.Category);
        }

        [TestMethod]
        public async Task CreateInferredWithConditionUsingIsInitialized()
        {
            var controller = ControllerContext();
            controller.EntityConfiguration.ConfigureProperty(p => p.HasPaid, p =>
                p.IsConditionallyInferredWith(_ => true, _ => _.IsInitialize == true));
            var dbObjectNoInitialize = new Person();
            controller.ServerEvaluator.MarkAsUnsaved(dbObjectNoInitialize);
            dbObjectNoInitialize.Category = PersonCategory.AutoDescription;
            var result = await new InferredValueEvaluationSession().TrySetInferredValuesCustomAsync(
                controller.EntityConfiguration,
                null,
                dbObjectNoInitialize,
                false,
                controller.ServerEvaluator,
                ResolveServiceProviderProvider());
            Assert.AreEqual(dbObjectNoInitialize.HasPaid, null);
            result = await new InferredValueEvaluationSession().TrySetInferredValuesCustomAsync(
               controller.EntityConfiguration,
               null,
               dbObjectNoInitialize,
               false,
               controller.ServerEvaluator,
               ResolveServiceProviderProvider());
            Assert.AreEqual(dbObjectNoInitialize.HasPaid, null);
            result = await new InferredValueEvaluationSession().TrySetInferredValuesCustomAsync(
                controller.EntityConfiguration,
                null,
                dbObjectNoInitialize,
                true,
                controller.ServerEvaluator,
                ResolveServiceProviderProvider());
            Assert.AreEqual(dbObjectNoInitialize.HasPaid, true);
            var json = controller.EntityConfiguration.Builder.ToJson();
        }

        [TestMethod]
        public async Task InferValuesForPersonWithBirthdaySet()
        {
            var controller = ControllerContext();
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
                    _ => (_.PreviousEntityState == null || _.PreviousEntityState.Category != PersonCategory.AutoDescription) &&
                         _.CurrentEntityState.Category == PersonCategory.AutoDescription
                );
                p.IsConditionallyInferredWith(
                    _ => null,
                    _ => (_.PreviousEntityState != null && _.PreviousEntityState.Category == PersonCategory.AutoDescription) &&
                         _.CurrentEntityState.Category == PersonCategory.Conventional
                );
            });
            var dbObjectInitialize = new Person();
            controller.ServerEvaluator.MarkAsUnsaved(dbObjectInitialize);
            dbObjectInitialize.Category = PersonCategory.AutoDescription;
            Assert.AreEqual(default(DateTimeOffset), dbObjectInitialize.CreatedDate);
            Assert.AreEqual(null, dbObjectInitialize.Birthday);
            Assert.AreNotEqual(PersonCategory.Conventional, dbObjectInitialize.Category);
            Assert.AreNotEqual(PersonSkills.Coder, dbObjectInitialize.Skills);
            var inferredValuesResult = await new InferredValueEvaluationSession().TrySetInferredValuesCustomAsync(
                controller.EntityConfiguration,
                null,
                dbObjectInitialize,
                true,
                controller.ServerEvaluator,
                ResolveServiceProviderProvider());
            Assert.AreEqual(PersonCategory.Conventional, dbObjectInitialize.Category);
            Assert.AreNotEqual(default(DateTimeOffset), dbObjectInitialize.CreatedDate);
            Assert.IsNull(dbObjectInitialize.Birthday);
            Assert.AreEqual(PersonSkills.Coder, dbObjectInitialize.Skills);
            Assert.AreEqual(PersonCategory.Conventional, dbObjectInitialize.Category);

            var dbObjectNoInitialize = new Person();
            controller.ServerEvaluator.MarkAsUnsaved(dbObjectNoInitialize);
            dbObjectNoInitialize.Category = PersonCategory.AutoDescription;
            Assert.AreEqual(default(DateTimeOffset), dbObjectNoInitialize.CreatedDate);
            Assert.AreEqual(null, dbObjectNoInitialize.Birthday);
            Assert.AreNotEqual(PersonCategory.Conventional, dbObjectNoInitialize.Category);
            Assert.AreNotEqual(PersonSkills.Coder, dbObjectNoInitialize.Skills);
            inferredValuesResult = await new InferredValueEvaluationSession().TrySetInferredValuesCustomAsync(
                controller.EntityConfiguration,
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
