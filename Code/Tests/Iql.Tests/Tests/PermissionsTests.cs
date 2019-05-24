using System.Threading.Tasks;
using Iql.Data;
using Iql.Data.Evaluation;
#if !TypeScript
using Iql.DotNet.Serialization;
#endif
using Iql.Entities.Permissions;
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
                clientConfiguration.Builder.PermissionManager.DefineEntityUserPermissionRule<Client, ApplicationUser>(nameof(TestSimplePermissionRule), _ => IqlUserPermission.Read, null
#if TypeScript
            , new EvaluateContext(_ => Evaluator.Eval(_))
#endif
                );
            var user = new ApplicationUser();
            var client = new Client();
            var permission = await new PermissionsEvaluationSession().EvaluateEntityPermissionsRuleAsync(rule, user, client, Db);
            Assert.AreEqual(IqlUserPermission.Read, permission);
        }

        [TestMethod]
        public async Task TestInheritedPermissionRule()
        {
            var user = new ApplicationUser();
            var descriptionProperty = Db.EntityConfigurationContext.EntityType<Person>().FindProperty(nameof(Person.Description));
            var permission = await new PermissionsEvaluationSession().GetDbUserPermissionAsync(
                Db,
                descriptionProperty,
                user);
            Assert.AreEqual(IqlUserPermission.Full, permission);
            user.Email = "CannotSeeOrEditAnyPersons";
            permission = await new PermissionsEvaluationSession().GetDbUserPermissionAsync(
               Db,
               descriptionProperty,
               user);
            Assert.AreEqual(IqlUserPermission.None, permission);
            var skillsProperty = Db.EntityConfigurationContext.EntityType<Person>().FindProperty(nameof(Person.Skills));
            permission = await new PermissionsEvaluationSession().GetDbUserPermissionAsync(
                Db,
                skillsProperty,
                user);
            Assert.AreEqual(IqlUserPermission.None, permission);
            user.Email = "OnlyRead";
            permission = await new PermissionsEvaluationSession().GetDbUserPermissionAsync(
                Db,
                skillsProperty,
                user);
            Assert.AreEqual(IqlUserPermission.Read, permission);
        }

        [TestMethod]
        public async Task TestComplexPermissionRule()
        {
            var clientConfiguration = Db.EntityConfigurationContext.EntityType<Client>();
            var rule =
                clientConfiguration.Builder.PermissionManager.DefineEntityUserPermissionRule<Client, ApplicationUser>(
                    nameof(TestComplexPermissionRule), 
                    context => context.User.FullName == "abc" ? IqlUserPermission.Read : IqlUserPermission.None,
                    null
#if TypeScript
            , new EvaluateContext(_ => Evaluator.Eval(_))
#endif
                );
            var user = new ApplicationUser();
            var client = new Client();
            var permission = await new PermissionsEvaluationSession().EvaluateEntityPermissionsRuleAsync(
                rule,
                user,
                client,
                Db);
            Assert.AreEqual(IqlUserPermission.None, permission);
            user.FullName = "abc";
            permission = await new PermissionsEvaluationSession().EvaluateEntityPermissionsRuleAsync(
                rule,
                user,
                client,
                Db);
            Assert.AreEqual(IqlUserPermission.Read, permission);
        }


        [TestMethod]
        public async Task TestComplexPermissionRuleWithFetchOnlyCacheMode()
        {
            var cloudUserClient = new Client
            {
                Id = 9233,
                Description = "The ABCd"
            };
            AppDbContext.InMemoryDb.Clients.Add(cloudUserClient);
            var cloudUser = new ApplicationUser
            {
                Id = nameof(TestComplexPermissionRuleWithFetchOnlyCacheMode),
                ClientId = 9233,
                EmailConfirmed = true
            };
            AppDbContext.InMemoryDb.Users.Add(cloudUser);
            var clientConfiguration = Db.EntityConfigurationContext.EntityType<Client>();
            var rule =
                clientConfiguration.Builder.PermissionManager.DefineEntityUserPermissionRule<Client, ApplicationUser>(nameof(TestComplexPermissionRuleWithFetchOnlyCacheMode), context => context.User.Client.Description.Contains("abc") && context.User.EmailConfirmed == true ? IqlUserPermission.Read : IqlUserPermission.ReadAndUpdate,
                    null
#if TypeScript
            , new EvaluateContext(_ => Evaluator.Eval(_))
#endif
                );
            var client = new Client
            {
                AverageSales = 10,
                Name = "New client",
                TypeId = 7
            };
            var user = await Db.Users.GetWithKeyAsync(nameof(TestComplexPermissionRuleWithFetchOnlyCacheMode));
            var permissionsEvaluationSession = new PermissionsEvaluationSession(false, EvaluationCacheMode.FetchesOnly);
            var permission = await permissionsEvaluationSession.EvaluateEntityPermissionsRuleAsync(rule, user, client, Db);
            Assert.AreEqual(IqlUserPermission.Read, permission);

            cloudUser.EmailConfirmed = false;

            permission = await permissionsEvaluationSession.EvaluateEntityPermissionsRuleAsync(rule, user, client, Db);
            Assert.AreEqual(IqlUserPermission.Read, permission);
        }

        [TestMethod]
        public async Task TestPropertyPermissionRule()
        {
            var cloudUserClient = new Client
            {
                Id = 9233,
                Description = "The ABCd"
            };
            AppDbContext.InMemoryDb.Clients.Add(cloudUserClient);
            var cloudUser = new ApplicationUser
            {
                Id = nameof(TestPropertyPermissionRule),
                ClientId = 9233,
                EmailConfirmed = true,
                UserType = UserType.Client
            };
            AppDbContext.InMemoryDb.Users.Add(cloudUser);
            var userConfiguration = Db.EntityConfigurationContext.EntityType<ApplicationUser>();
            var property = userConfiguration.FindProperty(nameof(ApplicationUser.IsLockedOut));
            var user = await Db.Users.GetWithKeyAsync(nameof(TestPropertyPermissionRule));
            var permissionsEvaluationSession = new PermissionsEvaluationSession(false, EvaluationCacheMode.None);
            permissionsEvaluationSession.ResultsCachingKind = PermissionsResultsCachingKind.None;
            var permission = await permissionsEvaluationSession.GetUserPermissionAsync(
                Db.EntityConfigurationContext.PermissionManager,
                property,
                Db,
                user,
                typeof(ApplicationUser),
                null,
                null,
                Db,
                Db.EntityConfigurationContext);
            Assert.AreEqual(IqlUserPermission.None, permission);

            user.UserType = UserType.Super;

            permission = await permissionsEvaluationSession.GetUserPermissionAsync(
               Db.EntityConfigurationContext.PermissionManager,
               property,
               Db,
               user,
               typeof(ApplicationUser),
               null,
               null,
               Db,
               Db.EntityConfigurationContext);
            Assert.AreEqual(IqlUserPermission.ReadAndUpdate, permission);
        }

        [TestMethod]
        public async Task TestComplexPermissionRuleWithNoneCacheMode()
        {
            var cloudUserClient = new Client
            {
                Id = 9233,
                Description = "The ABCd"
            };
            AppDbContext.InMemoryDb.Clients.Add(cloudUserClient);
            var cloudUser = new ApplicationUser
            {
                Id = nameof(TestComplexPermissionRuleWithNoneCacheMode),
                ClientId = 9233,
                EmailConfirmed = true
            };
            AppDbContext.InMemoryDb.Users.Add(cloudUser);
            var clientConfiguration = Db.EntityConfigurationContext.EntityType<Client>();
            var rule =
                clientConfiguration.Builder.PermissionManager.DefineEntityUserPermissionRule<Client, ApplicationUser>(nameof(TestComplexPermissionRuleWithNoneCacheMode), context => context.User.Client.Description.Contains("abc") && context.User.EmailConfirmed == true ? IqlUserPermission.Read : IqlUserPermission.ReadAndUpdate,
                    null
#if TypeScript
            , new EvaluateContext(_ => Evaluator.Eval(_))
#endif
                );
            var client = new Client
            {
                AverageSales = 10,
                Name = "New client",
                TypeId = 7
            };
            var user = await Db.Users.GetWithKeyAsync(nameof(TestComplexPermissionRuleWithNoneCacheMode));
            var permissionsEvaluationSession = new PermissionsEvaluationSession(false, EvaluationCacheMode.None);
            var permission = await permissionsEvaluationSession.EvaluateEntityPermissionsRuleAsync(rule, user, client, Db);
            Assert.AreEqual(IqlUserPermission.Read, permission);

            cloudUser.EmailConfirmed = false;

            permission = await permissionsEvaluationSession.EvaluateEntityPermissionsRuleAsync(rule, user, client, Db);
            Assert.AreEqual(IqlUserPermission.Read, permission);

            permission = await permissionsEvaluationSession.EvaluateEntityPermissionsRuleAsync(rule, user, client, Db);
            Assert.AreEqual(IqlUserPermission.Read, permission);

            user = await Db.Users.GetWithKeyAsync(nameof(TestComplexPermissionRuleWithNoneCacheMode));
            permissionsEvaluationSession.ClearUsersResultsCache(Db.EntityConfigurationContext, typeof(ApplicationUser), user);
            permission = await permissionsEvaluationSession.EvaluateEntityPermissionsRuleAsync(rule, user, client, Db);
            Assert.AreEqual(IqlUserPermission.ReadAndUpdate, permission);
        }

        [TestMethod]
        public async Task TestConvolutedPermissionRuleOnNewEntity()
        {
            var cloudUserClient = new Client
            {
                Id = 9232,
                Description = ""
            };
            AppDbContext.InMemoryDb.Clients.Add(cloudUserClient);
            var clientConfiguration = Db.EntityConfigurationContext.EntityType<Client>();
            var rule =
                clientConfiguration.Builder.PermissionManager.DefineEntityUserPermissionRule<Client, ApplicationUser>(nameof(TestConvolutedPermissionRuleOnNewEntity), context => context.User.Client.Description.Contains("abc") && context.IsEntityNew && context.Entity.AverageSales > 100 ? IqlUserPermission.Read : IqlUserPermission.ReadAndUpdate,
                    null
#if TypeScript
            , new EvaluateContext(_ => Evaluator.Eval(_))
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
            var permission = await new PermissionsEvaluationSession().EvaluateEntityPermissionsRuleAsync(rule, user, client, Db);
            Assert.AreEqual(IqlUserPermission.ReadAndUpdate, permission);
            cloudUserClient.Description = "one two abc three";
            client.AverageSales = 200;
            permission = await new PermissionsEvaluationSession().EvaluateEntityPermissionsRuleAsync(rule, user, client, Db);
            Assert.AreEqual(IqlUserPermission.Read, permission);
            client.AverageSales = 99;
            permission = await new PermissionsEvaluationSession().EvaluateEntityPermissionsRuleAsync(rule, user, client, Db);
            Assert.AreEqual(IqlUserPermission.ReadAndUpdate, permission);
            client.AverageSales = 101;
            permission = await new PermissionsEvaluationSession().EvaluateEntityPermissionsRuleAsync(rule, user, client, Db);
            Assert.AreEqual(IqlUserPermission.Read, permission);
            cloudUserClient.Description = "one two three";
            permission = await new PermissionsEvaluationSession().EvaluateEntityPermissionsRuleAsync(rule, user, client, Db);
            Assert.AreEqual(IqlUserPermission.ReadAndUpdate, permission);
            cloudUserClient.Description = "one two abc three";
            permission = await new PermissionsEvaluationSession().EvaluateEntityPermissionsRuleAsync(rule, user, client, Db);
            Assert.AreEqual(IqlUserPermission.Read, permission);
            Db.Add(client);
            var result = await Db.SaveChangesAsync();
            Assert.AreEqual(true, result.Success);
            permission = await new PermissionsEvaluationSession().EvaluateEntityPermissionsRuleAsync(rule, user, client, Db);
            Assert.AreEqual(IqlUserPermission.ReadAndUpdate, permission);
        }

        [TestMethod]
        public async Task TestConvolutedPermissionRuleOnExistingEntity()
        {
            var cloudUserClient = new Client
            {
                Id = 9233,
                Description = ""
            };
            AppDbContext.InMemoryDb.Clients.Add(cloudUserClient);
            var cloudUser = new ApplicationUser
            {
                Id = nameof(TestConvolutedPermissionRuleOnExistingEntity),
                ClientId = 9233
            };
            AppDbContext.InMemoryDb.Users.Add(cloudUser);
            var clientConfiguration = Db.EntityConfigurationContext.EntityType<Client>();
            var rule =
                clientConfiguration.Builder.PermissionManager.DefineEntityUserPermissionRule<Client, ApplicationUser>(nameof(TestConvolutedPermissionRuleOnExistingEntity), context => context.User.Client.Description.Contains("abc") && context.IsEntityNew && context.Entity.AverageSales > 100 ? IqlUserPermission.Read : IqlUserPermission.ReadAndUpdate,
                    null
#if TypeScript
            , new EvaluateContext(_ => Evaluator.Eval(_))
#endif
                );
            var client = new Client
            {
                AverageSales = 10,
                Name = "New client",
                TypeId = 7
            };
            var user = await Db.Users.GetWithKeyAsync(nameof(TestConvolutedPermissionRuleOnExistingEntity));
            var permission = await new PermissionsEvaluationSession().EvaluateEntityPermissionsRuleAsync(rule, user, client, Db);
            Assert.AreEqual(IqlUserPermission.ReadAndUpdate, permission);
            cloudUserClient.Description = "one two abc three";
            client.AverageSales = 200;
            permission = await new PermissionsEvaluationSession().EvaluateEntityPermissionsRuleAsync(rule, user, client, Db);
            Assert.AreEqual(IqlUserPermission.Read, permission);
            client.AverageSales = 99;
            permission = await new PermissionsEvaluationSession().EvaluateEntityPermissionsRuleAsync(rule, user, client, Db);
            Assert.AreEqual(IqlUserPermission.ReadAndUpdate, permission);
            client.AverageSales = 101;
            permission = await new PermissionsEvaluationSession().EvaluateEntityPermissionsRuleAsync(rule, user, client, Db);
            Assert.AreEqual(IqlUserPermission.Read, permission);
            cloudUserClient.Description = "one two three";
            permission = await new PermissionsEvaluationSession().EvaluateEntityPermissionsRuleAsync(rule, user, client, Db);
            Assert.AreEqual(IqlUserPermission.ReadAndUpdate, permission);
            cloudUserClient.Description = "one two abc three";
            permission = await new PermissionsEvaluationSession().EvaluateEntityPermissionsRuleAsync(rule, user, client, Db);
            Assert.AreEqual(IqlUserPermission.Read, permission);
            Db.Add(client);
            var result = await Db.SaveChangesAsync();
            Assert.AreEqual(true, result.Success);
            permission = await new PermissionsEvaluationSession().EvaluateEntityPermissionsRuleAsync(rule, user, client, Db);
            Assert.AreEqual(IqlUserPermission.ReadAndUpdate, permission);
        }

        [TestMethod]
        public async Task TestPermissionPrecedence()
        {
            var cloudLoggedInUser1 = new ApplicationUser
            {
                Id = "8877",
                FullName = "User 1",
                UserName = "PrecedenceTest"
            };
            var cloudUser2 = new ApplicationUser
            {
                Id = "8876",
                FullName = "User 2"
            };
            AppDbContext.InMemoryDb.Users.Add(cloudLoggedInUser1);
            AppDbContext.InMemoryDb.Users.Add(cloudUser2);
            var loggedInUser = await Db.Users.GetWithKeyAsync("8877");
            var userConfig = Db.EntityConfigurationContext.EntityType<ApplicationUser>();
            var permissionsEvaluationSession = new PermissionsEvaluationSession(true, EvaluationCacheMode.FetchesOnly);
            var permission = await permissionsEvaluationSession.GetDbUserPermissionAsync(Db, userConfig, loggedInUser);
            Assert.AreEqual(IqlUserPermission.None, permission);
            cloudLoggedInUser1.FullName = "PrecedenceShouldNotOverride";
            permission = await permissionsEvaluationSession.GetDbUserPermissionAsync(Db, userConfig, loggedInUser);
            Assert.AreEqual(IqlUserPermission.None, permission);
            loggedInUser.FullName = "PrecedenceShouldOverride";
            cloudLoggedInUser1.FullName = "PrecedenceShouldOverride";
            permission = await permissionsEvaluationSession.GetDbUserPermissionAsync(Db, userConfig, loggedInUser);
            Assert.AreEqual(IqlUserPermission.Full, permission);
        }

        [TestMethod]
        public async Task TestPermissionRuleOnChildCollectionOnExistingEntity()
        {
            var cloudClient = new Client
            {
                Id = 9234,
                Description = "",
                CreatedByUserId = "NotUs"
            };
            var cloudClient2 = new Client
            {
                Id = 9235,
                Description = "",
                CreatedByUserId = "NotUs"
            };
            AppDbContext.InMemoryDb.Clients.Add(cloudClient);
            AppDbContext.InMemoryDb.Clients.Add(cloudClient2);
            var cloudUser = new ApplicationUser
            {
                Id = nameof(TestPermissionRuleOnChildCollectionOnExistingEntity),
                ClientId = 9234,
                UserType = UserType.Candidate
            };
            AppDbContext.InMemoryDb.Users.Add(cloudUser);
            var test1 = await Db.Clients.AnyAsync(_ => _.Id == 8888);
            Assert.AreEqual(false, test1);
            var test2 = await Db.Clients.AnyAsync(_ => _.Id == 9234);
            Assert.AreEqual(true, test2);
            //var result = Db.Clients.Where(_ => _.CreatedByUserId == "abc").Any();
            var clientConfiguration = Db.EntityConfigurationContext.EntityType<Client>();
            // Only allow read and edit if the user has created this client
            var rule =
                clientConfiguration.Builder.PermissionManager.DefineEntityUserPermissionRule<Client, ApplicationUser>(nameof(TestPermissionRuleOnChildCollectionOnExistingEntity), context =>
                        context.QueryAny<Client>(_ => _.CreatedByUserId == context.User.Id) ||
                        context.QueryAny<Client>(_ => _.Description.Contains(context.User.Id)) ||
                        context.User.UserType == UserType.Super
                            ? IqlUserPermission.ReadAndUpdate
                            : IqlUserPermission.None,
                    null
#if TypeScript
            , new EvaluateContext(_ => Evaluator.Eval(_))
#endif
                );
#if !TypeScript
            var xml = rule.IqlExpression.SerializeToXml();
#endif
            var user = await Db.Users.GetWithKeyAsync(nameof(TestPermissionRuleOnChildCollectionOnExistingEntity));
            var client = await Db.Clients.GetWithKeyAsync(9234);

            var permission = await new PermissionsEvaluationSession().EvaluateEntityPermissionsRuleAsync(rule, user, client, Db);
            Assert.AreEqual(IqlUserPermission.None, permission);

            for (var i = 0; i < 11; i++)
            {
                permission = await new PermissionsEvaluationSession().EvaluateEntityPermissionsRuleAsync(rule, user, client, Db);
                Assert.AreEqual(IqlUserPermission.None, permission);
            }

            cloudClient.CreatedByUserId = cloudUser.Id;
            permission = await new PermissionsEvaluationSession().EvaluateEntityPermissionsRuleAsync(rule, user, client, Db);
            Assert.AreEqual(IqlUserPermission.ReadAndUpdate, permission);

            cloudClient.CreatedByUserId = "NotUs";
            permission = await new PermissionsEvaluationSession().EvaluateEntityPermissionsRuleAsync(rule, user, client, Db);
            Assert.AreEqual(IqlUserPermission.None, permission);

            cloudClient2.Description = $"Made by {nameof(TestPermissionRuleOnChildCollectionOnExistingEntity)}.";
            permission = await new PermissionsEvaluationSession().EvaluateEntityPermissionsRuleAsync(rule, user, client, Db);
            Assert.AreEqual(IqlUserPermission.ReadAndUpdate, permission);

            cloudClient2.Description = $"Made by NotUs.";
            permission = await new PermissionsEvaluationSession().EvaluateEntityPermissionsRuleAsync(rule, user, client, Db);
            Assert.AreEqual(IqlUserPermission.None, permission);

            cloudUser.UserType = UserType.Super;
            permission = await new PermissionsEvaluationSession(true).EvaluateEntityPermissionsRuleAsync(rule, user, client, Db);
            Assert.AreEqual(IqlUserPermission.ReadAndUpdate, permission);
        }

        [TestMethod]
        public async Task TestPermissionRuleOnChildCollectionOnExistingEntity2()
        {
            var cloudClient = new Client
            {
                Id = 9234,
                Description = "",
                CreatedByUserId = "NotUs"
            };
            var cloudClient2 = new Client
            {
                Id = 9235,
                Description = "",
                CreatedByUserId = "NotUs"
            };
            AppDbContext.InMemoryDb.Clients.Add(cloudClient);
            AppDbContext.InMemoryDb.Clients.Add(cloudClient2);
            var cloudUser = new ApplicationUser
            {
                Id = nameof(TestPermissionRuleOnChildCollectionOnExistingEntity2),
                ClientId = 9234,
                UserType = UserType.Candidate
            };
            AppDbContext.InMemoryDb.Users.Add(cloudUser);
            var test1 = await Db.Clients.AnyAsync(_ => _.Id == 8888);
            Assert.AreEqual(false, test1);
            var test2 = await Db.Clients.AnyAsync(_ => _.Id == 9234);
            Assert.AreEqual(true, test2);
            //var result = Db.Clients.Where(_ => _.CreatedByUserId == "abc").Any();
            var clientConfiguration = Db.EntityConfigurationContext.EntityType<Client>();
            // Only allow read and edit if the user has created this client
            var rule =
                clientConfiguration.Builder.PermissionManager.DefineEntityUserPermissionRule<Client, ApplicationUser>(nameof(TestPermissionRuleOnChildCollectionOnExistingEntity2), context =>
                        context.User.UserType == UserType.Super
                            ? IqlUserPermission.ReadAndUpdate
                            : IqlUserPermission.None,
                    null
#if TypeScript
            , new EvaluateContext(_ => Evaluator.Eval(_))
#endif
                );
            var user = await Db.Users.GetWithKeyAsync(nameof(TestPermissionRuleOnChildCollectionOnExistingEntity2));
            var client = await Db.Clients.GetWithKeyAsync(9234);

            var permission = await new PermissionsEvaluationSession().EvaluateEntityPermissionsRuleAsync(rule, user, client, Db);
            Assert.AreEqual(IqlUserPermission.None, permission);

            cloudUser.UserType = UserType.Super;
            permission = await new PermissionsEvaluationSession(true).EvaluateEntityPermissionsRuleAsync(rule, user, client, Db);
            Assert.AreEqual(IqlUserPermission.ReadAndUpdate, permission);
        }
    }
}
