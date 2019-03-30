using System.Threading.Tasks;
using Iql.Data;
using Iql.Data.Extensions;
using Iql.Entities.Permissions;
using Iql.Extensions;
using Iql.Tests.Context;
using IqlSampleApp.Data.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
#if TypeScript
using Iql.Parsing;
using Iql.Parsing.Expressions;
#endif

namespace Iql.Tests.Tests
{
    [TestClass]
    public class PermissionsTests : TestsBase
    {
        [TestMethod]
        public async Task TestSimplePermissionRule()
        {
            var clientConfiguration = Db.EntityConfigurationContext.EntityType<Client>();
            var rule =
                clientConfiguration.Permissions.DefineEntityUserPermissionRule<Client, ApplicationUser>(
                    _ => IqlUserPermission.Read
#if TypeScript
            , null, new EvaluateContext(_ => Evaluator.Eval(_))
#endif
                );
            var user = new ApplicationUser();
            var client = new Client();
            var permission = await rule.EvaluateEntityRuleAsync(user, client, Db);
            Assert.AreEqual(IqlUserPermission.Read, permission);
        }

        [TestMethod]
        public async Task TestComplexPermissionRule()
        {
            var clientConfiguration = Db.EntityConfigurationContext.EntityType<Client>();
            var rule =
                clientConfiguration.Permissions.DefineEntityUserPermissionRule<Client, ApplicationUser>(
                    context => context.User.FullName == "abc" ? IqlUserPermission.Read : IqlUserPermission.None
#if TypeScript
            , null, new EvaluateContext(_ => Evaluator.Eval(_))
#endif
                );
            var user = new ApplicationUser();
            var client = new Client();
            var permission = await rule.EvaluateEntityRuleAsync(user, client, Db);
            Assert.AreEqual(IqlUserPermission.None, permission);
            user.FullName = "abc";
            permission = await rule.EvaluateEntityRuleAsync(user, client, Db);
            Assert.AreEqual(IqlUserPermission.Read, permission);
        }

        [TestMethod]
        public async Task TestConvolutedPermissionRule()
        {
            var cloudUserClient = new Client
            {
                Id = 9232,
                Description = ""
            };
            AppDbContext.InMemoryDb.Clients.Add(cloudUserClient);
            var clientConfiguration = Db.EntityConfigurationContext.EntityType<Client>();
            var rule =
                clientConfiguration.Permissions.DefineEntityUserPermissionRule<Client, ApplicationUser>(
                    context => context.User.Client.Description.Contains("abc") && context.IsNew && context.Entity.AverageSales > 100 ? IqlUserPermission.Read : IqlUserPermission.ReadAndEdit
#if TypeScript
            , null, new EvaluateContext(_ => Evaluator.Eval(_))
#endif
                );
            var user = new ApplicationUser
            {
                ClientId = 9232
            };
            var client = new Client
            {
                AverageSales = 10,
                Name = "New client",
                TypeId = 7
            };
            var permission = await rule.EvaluateEntityRuleAsync(user, client, Db);
            Assert.AreEqual(IqlUserPermission.ReadAndEdit, permission);
            cloudUserClient.Description = "one two abc three";
            client.AverageSales = 200;
            permission = await rule.EvaluateEntityRuleAsync(user, client, Db);
            Assert.AreEqual(IqlUserPermission.Read, permission);
            client.AverageSales = 99;
            permission = await rule.EvaluateEntityRuleAsync(user, client, Db);
            Assert.AreEqual(IqlUserPermission.ReadAndEdit, permission);
            client.AverageSales = 101;
            permission = await rule.EvaluateEntityRuleAsync(user, client, Db);
            Assert.AreEqual(IqlUserPermission.Read, permission);
            cloudUserClient.Description = "one two three";
            permission = await rule.EvaluateEntityRuleAsync(user, client, Db);
            Assert.AreEqual(IqlUserPermission.ReadAndEdit, permission);
            cloudUserClient.Description = "one two abc three";
            permission = await rule.EvaluateEntityRuleAsync(user, client, Db);
            Assert.AreEqual(IqlUserPermission.Read, permission);
            Db.Add(client);
            var result = await Db.SaveChangesAsync();
            Assert.AreEqual(true, result.Success);
            permission = await rule.EvaluateEntityRuleAsync(user, client, Db);
            Assert.AreEqual(IqlUserPermission.ReadAndEdit, permission);
            //permission = await rule.EvaluateEntityRuleAsync(user, client, Db);
            //Assert.AreEqual(IqlUserPermission.Read, permission);
        }
        //        [TestMethod]
        //        public async Task TestComplexPermissionRule()
        //        {
        //            var clientConfiguration = Db.EntityConfigurationContext.EntityType<Client>();
        //            var rule =
        //                clientConfiguration.Permissions.DefineEntityUserPermissionRule<Client, ApplicationUser>(
        //                    context => context.User.FullName == "abc" ? IqlUserPermission.Read : IqlUserPermission.None
        //#if TypeScript
        //            , null, new EvaluateContext(_ => Evaluator.Eval(_))
        //#endif
        //                );
        //            var applicationUser = new ApplicationUser();
        //            var result = rule.Run(applicationUser, new Client());
        //            Assert.AreEqual(IqlUserPermission.None, result);
        //        }
    }
}
