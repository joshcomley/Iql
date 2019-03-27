using System.Threading.Tasks;
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
        public async Task TestPermissionRule()
        {
            var clientConfiguration = Db.EntityConfigurationContext.EntityType<Client>();
            var rule =
                clientConfiguration.Permissions.DefineEntityUserPermissionRule<Client, ApplicationUser>(
                    context => IqlUserPermission.Read
#if TypeScript
            , null, new EvaluateContext(_ => Evaluator.Eval(_))
#endif
                    );
            var lambdaExpression = rule.Expression;
            var lambda = lambdaExpression.Compile();
            var result = lambda.DynamicInvoke(new IqlEntityUserPermissionContext<Client, ApplicationUser>(true, null, new ApplicationUser(), new Client()));
            Assert.AreEqual(IqlUserPermission.Read, result);
        }
    }
}
