using System.Threading.Tasks;
using Iql.Data.Security;
using Iql.Entities;
using Iql.Entities.Permissions;
#if TypeScript
using Iql.Parsing;
using Iql.Parsing.Expressions;
#endif
using IqlSampleApp.Data.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Iql.Tests.Tests
{
    [TestClass]
    public class SecurityServiceTests : TestsBase
    {
        [TestMethod]
        public async Task TestDefineAndUseOnProperty()
        {
            var clientConfiguration = Db.EntityConfigurationContext.EntityType<Client>();
            await RunTestAsync(
                clientConfiguration
                    .FindPropertyByExpression(c => c.Description));
        }

        [TestMethod]
        public async Task TestDefineAndUseOnEntity()
        {
            var clientConfiguration = Db.EntityConfigurationContext.EntityType<Client>();
            await RunTestAsync(clientConfiguration);
        }

        private async Task RunTestAsync(IUserPermission container)
        {
            var rule = container
                .Permissions.DefineAndUseUserPermissionRule<ApplicationUser>(
                    c =>
                        c.User.FullName == nameof(TestDefineAndUseOnProperty)
                            ? IqlUserPermission.Delete
                            : IqlUserPermission.Unset
#if TypeScript
                , null
                , null
                , new EvaluateContext(_ => Evaluator.Eval(_))
#endif
                );
            var user = new ApplicationUser();
            var client = new Client();
            var securityService = new SecurityService<ApplicationUser>(user);
            var result = await securityService.GetPermissionsAsync(
                Db,
                client,
                container
            );
            Assert.AreEqual(IqlUserPermission.Unset, result.Permissions);
            user.FullName = nameof(TestDefineAndUseOnProperty);
            result = await securityService.GetPermissionsAsync(
                Db,
                client,
                container
            );
            Assert.AreEqual(IqlUserPermission.Delete, result.Permissions);
            container.Permissions.RemoveRule(rule.Key);
        }
    }
}