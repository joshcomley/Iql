using System.Threading.Tasks;
using Iql.Data;
using Iql.Data.Extensions;
using Iql.Entities.Permissions;
using Iql.Extensions;
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
//            var user = new ApplicationUser();
//            var client = new Client();
//            var permission = await rule.EvaluateEntityRuleAsync(user, client, Db);
//            Assert.AreEqual(IqlUserPermission.None, permission);
//            user.FullName = "abc";
//            permission = await rule.EvaluateEntityRuleAsync(user, client, Db);
//            Assert.AreEqual(IqlUserPermission.Read, permission);
//        }
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
