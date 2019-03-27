using System.Threading.Tasks;
using Iql.Data;
using Iql.Data.Extensions;
using Iql.Entities.Permissions;
using IqlSampleApp.Data.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
#if TypeScript
using Iql.Parsing;
using Iql.Parsing.Expressions;
#endif

namespace Iql.Tests.Tests
{
    [TestClass]
    public class PermissionsTests:TestsBase
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
            var context = new IqlEntityUserPermissionContext<Client, ApplicationUser>(false, null, user, client);
            var result = await rule.IqlExpression.EvaluateIqlAsync(context, Db,
                typeof(IqlEntityUserPermissionContext<Client, ApplicationUser>));
            var permission = (IqlUserPermission) result.Result;
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
//            var applicationUser = new ApplicationUser();
//            var result = rule.Run(applicationUser, new Client());
//            Assert.AreEqual(IqlUserPermission.None, result);
//        }
    }
}
