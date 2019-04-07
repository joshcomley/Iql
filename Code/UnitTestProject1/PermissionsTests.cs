using System;
using System.Linq;
using System.Threading.Tasks;
using Iql.Data.Evaluation;
using Iql.Entities.Permissions;
using IqlSampleApp.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IqlSampleApp.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Iql.Tests.Server
{
    [TestClass]
    public class PermissionsTests : ServerTestsBase
    {
        [TestMethod]
        public async Task TestSimplePermissionRule()
        {
            var controller = ControllerContext<Client>();
            var clientConfiguration = controller.EntityConfiguration;
            var rule =
                clientConfiguration.Builder.Permissions.DefineEntityUserPermissionRule<Client, ApplicationUser>(
                    _ => IqlUserPermission.Read,
                    nameof(TestSimplePermissionRule)
                );
            var user = new ApplicationUser();
            var client = new Client();
            controller.ServerEvaluator.MarkAsUnsaved(client);
            var permission = await new PermissionsEvaluationSession().EvaluateEntityPermissionsRuleCustomAsync(
                rule,
                user,
                client,
                ResolveServiceProviderProvider(),
                controller.ServerEvaluator,
                clientConfiguration.Builder);
            Assert.AreEqual(IqlUserPermission.Read, permission);
        }

        [TestMethod]
        public async Task TestComplexPermissionRule()
        {
            var controller = ControllerContext<Client>();
            var clientConfiguration = controller.EntityConfiguration;
            var rule =
                clientConfiguration.Builder.Permissions.DefineEntityUserPermissionRule<Client, ApplicationUser>(
                    context => context.User.FullName == "abc" ? IqlUserPermission.Read : IqlUserPermission.None,
                    nameof(TestComplexPermissionRule)
#if TypeScript
            , null, new EvaluateContext(_ => Evaluator.Eval(_))
#endif
                );
            var user = new ApplicationUser();
            var client = new Client();
            controller.ServerEvaluator.MarkAsUnsaved(client);
            var permission = await new PermissionsEvaluationSession().EvaluateEntityPermissionsRuleCustomAsync(
                rule,
                user,
                client,
                ResolveServiceProviderProvider(),
                controller.ServerEvaluator,
                clientConfiguration.Builder);
            Assert.AreEqual(IqlUserPermission.None, permission);
            user.FullName = "abc";
            permission = await new PermissionsEvaluationSession().EvaluateEntityPermissionsRuleCustomAsync(
                rule,
                user,
                client,
                ResolveServiceProviderProvider(),
                controller.ServerEvaluator,
                clientConfiguration.Builder);
            Assert.AreEqual(IqlUserPermission.Read, permission);
        }

        [TestMethod]
        public async Task TestConvolutedPermissionRuleOnNewEntity()
        {
            var controller = ControllerContext<Client>();
            var clientConfiguration = controller.EntityConfiguration;
            var db = controller.CrudBase.NewUnsecuredDb();
            var cloudDb = controller.CrudBase.NewUnsecuredDb();
            var cloudClientType = await GetTestClientTypeAsync(cloudDb);

            var cloudUserClient = await EnsureClientByName(cloudDb, nameof(TestConvolutedPermissionRuleOnNewEntity), c =>
            {
                c.AverageSales = 1;
                c.TypeId = cloudClientType.Id;
                c.Name = nameof(TestConvolutedPermissionRuleOnNewEntity);
                c.Description = "";
            });

            await db.SaveChangesAsync();

            var rule =
                clientConfiguration.Builder.Permissions.DefineEntityUserPermissionRule<Client, ApplicationUser>(
                    context => context.User.Client.Description.Contains("abc") && context.IsEntityNew && context.Entity.AverageSales > 100 ? IqlUserPermission.Read : IqlUserPermission.ReadAndEdit,
                    nameof(TestConvolutedPermissionRuleOnNewEntity)
#if TypeScript
            , null, new EvaluateContext(_ => Evaluator.Eval(_))
#endif
                );
            var user = new ApplicationUser
            {
                ClientId = cloudUserClient.Id
            };
            var client = new Client
            {
                AverageSales = 10,
                Name = "New client",
                TypeId = cloudClientType.Id
            };
            controller.ServerEvaluator.MarkAsUnsaved(user);
            controller.ServerEvaluator.MarkAsUnsaved(client);
            var permission = await new PermissionsEvaluationSession().EvaluateEntityPermissionsRuleCustomAsync(
                rule,
                user,
                client,
                ResolveServiceProviderProvider(),
                controller.ServerEvaluator,
                clientConfiguration.Builder);
            Assert.AreEqual(IqlUserPermission.ReadAndEdit, permission);
            cloudUserClient.Description = "one two abc three";
            client.AverageSales = 200;
            await cloudDb.SaveChangesAsync();
            permission = await new PermissionsEvaluationSession().EvaluateEntityPermissionsRuleCustomAsync(
                rule,
                user,
                client,
                ResolveServiceProviderProvider(),
                controller.ServerEvaluator,
                clientConfiguration.Builder);
            Assert.AreEqual(IqlUserPermission.Read, permission);
            client.AverageSales = 99;
            await cloudDb.SaveChangesAsync();
            permission = await new PermissionsEvaluationSession().EvaluateEntityPermissionsRuleCustomAsync(
                rule,
                user,
                client,
                ResolveServiceProviderProvider(),
                controller.ServerEvaluator,
                clientConfiguration.Builder);
            Assert.AreEqual(IqlUserPermission.ReadAndEdit, permission);
            client.AverageSales = 101;
            await cloudDb.SaveChangesAsync();
            permission = await new PermissionsEvaluationSession().EvaluateEntityPermissionsRuleCustomAsync(
                rule,
                user,
                client,
                ResolveServiceProviderProvider(),
                controller.ServerEvaluator,
                clientConfiguration.Builder);
            Assert.AreEqual(IqlUserPermission.Read, permission);
            cloudUserClient.Description = "one two three";
            await cloudDb.SaveChangesAsync();
            permission = await new PermissionsEvaluationSession().EvaluateEntityPermissionsRuleCustomAsync(
                rule,
                user,
                client,
                ResolveServiceProviderProvider(),
                controller.ServerEvaluator,
                clientConfiguration.Builder);
            Assert.AreEqual(IqlUserPermission.ReadAndEdit, permission);
            cloudUserClient.Description = "one two abc three";
            await cloudDb.SaveChangesAsync();
            permission = await new PermissionsEvaluationSession().EvaluateEntityPermissionsRuleCustomAsync(
                rule,
                user,
                client,
                ResolveServiceProviderProvider(),
                controller.ServerEvaluator,
                clientConfiguration.Builder);
            Assert.AreEqual(IqlUserPermission.Read, permission);
            //db.Add(client);
            //var result = await db.SaveChangesAsync();
            //Assert.IsTrue(result > 0);
            controller.ServerEvaluator.MarkAsSaved(user, client);
            permission = await new PermissionsEvaluationSession().EvaluateEntityPermissionsRuleCustomAsync(
                rule,
                user,
                client,
                ResolveServiceProviderProvider(),
                controller.ServerEvaluator,
                clientConfiguration.Builder);
            Assert.AreEqual(IqlUserPermission.ReadAndEdit, permission);
        }

        private async Task<Client> EnsureClientByName(
            ApplicationDbContext db,
            string name,
            Action<Client> configure = null
        )
        {
            var cloudUserClient = db.Clients
                .SingleOrDefault(c => c.Name == name);
            if (cloudUserClient == null)
            {
                cloudUserClient = new Client();
                db.Clients.Add(cloudUserClient);
            }
            cloudUserClient.Name = name;
            cloudUserClient.TypeId = (await GetTestClientTypeAsync(db)).Id;
            configure?.Invoke(cloudUserClient);
            await db.SaveChangesAsync();
            return cloudUserClient;
        }
        private async Task<ApplicationUser> EnsureUserByName(
            ApplicationDbContext db,
            string name,
            Action<ApplicationUser> configure = null
        )
        {
            var entity = db.Users
                .SingleOrDefault(c => c.FullName == name);
            if (entity == null)
            {
                entity = new ApplicationUser();
                db.Users.Add(entity);
            }
            entity.FullName = name;
            configure?.Invoke(entity);
            await db.SaveChangesAsync();
            return entity;
        }

        [TestMethod]
        public async Task TestPermissionRuleOnChildCollectionOnExistingEntity()
        {
            var controller = ControllerContext<Client>();
            var clientConfiguration = controller.EntityConfiguration;
            var db = controller.CrudBase.NewUnsecuredDb();
            var cloudDb = controller.CrudBase.NewUnsecuredDb();
            var cloudClient = await EnsureClientByName(cloudDb, nameof(TestPermissionRuleOnChildCollectionOnExistingEntity));
            var cloudClient2 = await EnsureClientByName(cloudDb, $"{nameof(TestPermissionRuleOnChildCollectionOnExistingEntity)}2");
            var cloudUser = await EnsureUserByName(cloudDb, nameof(TestPermissionRuleOnChildCollectionOnExistingEntity),
                applicationUser => { applicationUser.UserType = UserType.Candidate; });
            //var result = Db.Clients.Where(_ => _.CreatedByUserId == "abc").Any();
            // Only allow read and edit if the user has created this client
            var rule =
                clientConfiguration.Builder.Permissions.DefineEntityUserPermissionRule<Client, ApplicationUser>(
                    context =>
                        context.QueryAny<Client>(_ => _.CreatedByUserId == context.User.Id) ||
                        context.QueryAny<Client>(_ => _.Description.Contains(context.User.Id)) ||
                        context.User.UserType == UserType.Super
                            ? IqlUserPermission.ReadAndEdit
                            : IqlUserPermission.None,
                    nameof(TestPermissionRuleOnChildCollectionOnExistingEntity)
#if TypeScript
            , null, new EvaluateContext(_ => Evaluator.Eval(_))
#endif
                );
            var user = await db.Users.SingleOrDefaultAsync(_=>_.FullName == nameof(TestPermissionRuleOnChildCollectionOnExistingEntity));
            var client = await db.Clients.SingleOrDefaultAsync(_=>_.Name == cloudClient.Name);

            var permission = await new PermissionsEvaluationSession().EvaluateEntityPermissionsRuleCustomAsync(
                rule,
                user,
                client,
                ResolveServiceProviderProvider(),
                controller.ServerEvaluator,
                clientConfiguration.Builder);
            Assert.AreEqual(IqlUserPermission.None, permission);

            for (var i = 0; i < 11; i++)
            {
                permission = await new PermissionsEvaluationSession().EvaluateEntityPermissionsRuleCustomAsync(
                    rule,
                    user,
                    client,
                    ResolveServiceProviderProvider(),
                    controller.ServerEvaluator,
                    clientConfiguration.Builder);
                Assert.AreEqual(IqlUserPermission.None, permission);
            }

            cloudClient.CreatedByUserId = cloudUser.Id;
            await cloudDb.SaveChangesAsync();
            permission = await new PermissionsEvaluationSession().EvaluateEntityPermissionsRuleCustomAsync(
                rule,
                user,
                client,
                ResolveServiceProviderProvider(),
                controller.ServerEvaluator,
                clientConfiguration.Builder);
            Assert.AreEqual(IqlUserPermission.ReadAndEdit, permission);

            cloudClient.CreatedByUserId = null;
            await cloudDb.SaveChangesAsync();
            permission = await new PermissionsEvaluationSession().EvaluateEntityPermissionsRuleCustomAsync(
                rule,
                user,
                client,
                ResolveServiceProviderProvider(),
                controller.ServerEvaluator,
                clientConfiguration.Builder);
            Assert.AreEqual(IqlUserPermission.None, permission);

            cloudClient2.Description = $"Made by {user.Id}.";
            await cloudDb.SaveChangesAsync();
            permission = await new PermissionsEvaluationSession().EvaluateEntityPermissionsRuleCustomAsync(
                rule,
                user,
                client,
                ResolveServiceProviderProvider(),
                controller.ServerEvaluator,
                clientConfiguration.Builder);
            Assert.AreEqual(IqlUserPermission.ReadAndEdit, permission);

            cloudClient2.Description = $"Made by NotUs.";
            await cloudDb.SaveChangesAsync();
            permission = await new PermissionsEvaluationSession().EvaluateEntityPermissionsRuleCustomAsync(
                rule,
                user,
                client,
                ResolveServiceProviderProvider(),
                controller.ServerEvaluator,
                clientConfiguration.Builder);
            Assert.AreEqual(IqlUserPermission.None, permission);

            cloudUser.UserType = UserType.Super;
            await cloudDb.SaveChangesAsync();
            permission = await new PermissionsEvaluationSession().EvaluateEntityPermissionsRuleCustomAsync(
                rule,
                user,
                client,
                ResolveServiceProviderProvider(),
                controller.ServerEvaluator,
                clientConfiguration.Builder);
            Assert.AreEqual(IqlUserPermission.ReadAndEdit, permission);
        }



        [TestMethod]
        public async Task TestPermissionRuleOnChildCollectionOnExistingEntity2()
        {
            var controller = ControllerContext<Client>();
            var clientConfiguration = controller.EntityConfiguration;
            var db = controller.CrudBase.NewUnsecuredDb();
            var cloudDb = controller.CrudBase.NewUnsecuredDb();
            var cloudClient = await EnsureClientByName(cloudDb, nameof(TestPermissionRuleOnChildCollectionOnExistingEntity2));
            var cloudUser = await EnsureUserByName(cloudDb, nameof(TestPermissionRuleOnChildCollectionOnExistingEntity2),
                applicationUser => { applicationUser.UserType = UserType.Candidate; });
            //var result = Db.Clients.Where(_ => _.CreatedByUserId == "abc").Any();
            // Only allow read and edit if the user has created this client
            var rule =
                clientConfiguration.Builder.Permissions.DefineEntityUserPermissionRule<Client, ApplicationUser>(
                    context =>
                        context.User.UserType == UserType.Super
                            ? IqlUserPermission.ReadAndEdit
                            : IqlUserPermission.None,
                    nameof(TestPermissionRuleOnChildCollectionOnExistingEntity2)
#if TypeScript
            , null, new EvaluateContext(_ => Evaluator.Eval(_))
#endif
                );
            var user = await db.Users.SingleOrDefaultAsync(_ => _.FullName == nameof(TestPermissionRuleOnChildCollectionOnExistingEntity2));
            var client = await db.Clients.SingleOrDefaultAsync(_ => _.Name == cloudClient.Name);

            var permission = await new PermissionsEvaluationSession().EvaluateEntityPermissionsRuleCustomAsync(
                rule,
                user,
                client,
                ResolveServiceProviderProvider(),
                controller.ServerEvaluator,
                clientConfiguration.Builder);
            Assert.AreEqual(IqlUserPermission.None, permission);

            cloudUser.UserType = UserType.Super;
            await cloudDb.SaveChangesAsync();
            permission = await new PermissionsEvaluationSession().EvaluateEntityPermissionsRuleCustomAsync(
                rule,
                user,
                client,
                ResolveServiceProviderProvider(),
                controller.ServerEvaluator,
                clientConfiguration.Builder);
            Assert.AreEqual(IqlUserPermission.ReadAndEdit, permission);
        }

        private static async Task<ClientType> GetTestClientTypeAsync(ApplicationDbContext db)
        {
            var cloudClientType = db.ClientTypes
                .SingleOrDefault(c => c.Name == "TestClientType" && c.Id == 7829);
            var needsSave = cloudClientType == null;
            if (needsSave)
            {
                cloudClientType = new ClientType();
                cloudClientType.Name = "TestClientType";
                cloudClientType.Id = 7829;
                db.ClientTypes.Add(cloudClientType);
            }
            if (needsSave)
            {
                await db.SaveChangesAsync();
            }
            return cloudClientType;
        }
    }
}