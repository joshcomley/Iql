using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Brandless.AspNetCore.OData.Extensions.Configuration;
using Brandless.Data.Entities;
using Iql.Entities;
using Iql.Entities.Permissions;
using Iql.Entities.SpecialTypes;
using Iql.Server;
using IqlSampleApp.Data.Contracts;
using IqlSampleApp.Data.Controllers.Api.Entities;
using IqlSampleApp.Data.Entities;
using Microsoft.AspNet.OData.Builder;

namespace IqlSampleApp.Data.Configuration.Entities
{
    public class UserIqlConfigurator : IIqlEntitySetConfigurator
    {
        public void Configure(IEntityConfigurationBuilder builder)
        {
            builder.PermissionManager.DefineUserPermissionRule<ApplicationUser>("SuperUser",
                context => context.User.UserType == UserType.Super
                    ? IqlUserPermission.ReadAndUpdate
                    : IqlUserPermission.None);
            builder.PermissionManager.DefineUserPermissionRule<ApplicationUser>("PrecedenceBase",
                context =>
                    context.User.UserName == "PrecedenceTest"
                        ? context.User.Email == "PrecedenceBase"
                            ? IqlUserPermission.Full
                            : IqlUserPermission.None
                        : IqlUserPermission.Unset);
            builder.PermissionManager.DefineUserPermissionRule<ApplicationUser>("PrecedenceShouldOverride",
                context =>
                    context.User.UserName == "PrecedenceTest"
                        ? context.User.FullName == "PrecedenceShouldOverride"
                            ? IqlUserPermission.Full
                            : IqlUserPermission.Unset
                        : IqlUserPermission.Unset,
                IqlUserPermissionRulePrecedenceDirection.Up);
            builder.PermissionManager.DefineUserPermissionRule<ApplicationUser>("PrecedenceShouldNotOverride",
                context =>
                    context.User.UserName == "PrecedenceTest"
                        ? context.User.FullName == "PrecedenceShouldNotOverride"
                            ? IqlUserPermission.Full
                            : IqlUserPermission.Unset
                        : IqlUserPermission.Unset);
            var users = builder.EntityType<ApplicationUser>();
            users.Permissions.UseRule("PrecedenceBase").UseRule("PrecedenceShouldOverride").UseRule("PrecedenceShouldNotOverride");
            users.ConfigureProperty(_ => _.IsLockedOut, property => { property.Permissions.UseRule("SuperUser"); });
            builder.UsersDefinition = UsersDefinition.Define(users,
                _ => _.Id,
                _ => _.FullName
            );
            users
                .FindRelationship(_ => _.Client)
                .CreateWithRelationshipValue(_ => _.CreatedByUser, ctx => _ => ctx.Owner.CreatedByUser)
                .CreateWithPropertyValue(_ => _.AverageIncome, _ => 12)
                ;
        }
    }

    public class UserConfigurator : IODataEntitySetConfigurator
    {
        public void Configure(ODataModelBuilder builder)
        {
            var user = builder.EntityType<ApplicationUser>();
            user.Ignore(p => p.NormalizedEmail);
            user.Ignore(p => p.NormalizedUserName);
            user.Ignore(p => p.ConcurrencyStamp);
            user.Ignore(p => p.SecurityStamp);
            user.Ignore(p => p.PasswordHash);
            user.Ignore(p => p.AccessFailedCount);
            user.Property(p => p.IsLockedOut);
            user.Property(p => p.IsEnabled);
            user
                .HasOptional(
                    c => c.Client,
                    (applicationUser, client) => applicationUser.ClientId == client.Id,
                    c => c.Users);
            //user
            //    .HasKey(p => p.Id);
            ApplyCreatedByUser(builder, p => p.ClientsCreated);
            ApplyCreatedByUser(builder, p => p.PeopleCreated);
            ApplyCreatedByUser(builder, p => p.ProjectCreated);
            ApplyCreatedByUser(builder, p => p.PersonTypesCreated);
            ApplyCreatedByUser(builder, p => p.DocumentCategoriesCreated);
            ApplyCreatedByUser(builder, p => p.SiteDocumentsCreated);
            ApplyCreatedByUser(builder, p => p.FaultActionsTakenCreated);
            ApplyCreatedByUser(builder, p => p.FaultCategoriesCreated);
            ApplyCreatedByUser(builder, p => p.FaultTypesCreated);
            ApplyCreatedByUser(builder, p => p.FaultReportsCreated);
            ApplyCreatedByUser(builder, p => p.FaultRecommendationsCreated);
            ApplyCreatedByUser(builder, p => p.FaultDefaultRecommendationsCreated);
            ApplyCreatedByUser(builder, p => p.ReportReceiverEmailAddressesCreated);
            ApplyCreatedByUser(builder, p => p.RiskAssessmentsCreated);
            ApplyCreatedByUser(builder, p => p.RiskAssessmentAnswersCreated);
            ApplyCreatedByUser(builder, p => p.RiskAssessmentSolutionsCreated);
            ApplyCreatedByUser(builder, p => p.RiskAssessmentQuestionsCreated);
            ApplyCreatedByUser(builder, p => p.PersonInspectionsCreated);
            ApplyCreatedByUser(builder, p => p.PersonLoadingsCreated);
            ApplyCreatedByUser(builder, p => p.SitesCreated);
            ApplyCreatedByUser(builder, p => p.SiteAreasCreated);
            ApplyCreatedByUser(builder, p => p.SiteInspectionsCreated);
            ApplyCreatedByUser(builder, p => p.UserSettingsCreated);

            // Tested
            var forClient = builder
                .EntityType<ApplicationUser>()
                .Collection
                .Function(nameof(UsersController.ForClient))
                .ReturnsCollectionFromEntitySet<ApplicationUser>(nameof(IIqlSampleAppService.Users));
            forClient.Parameter<int>("id");
            forClient.Parameter<int>("type").Optional();
            // Tested
            builder.EntityType<ApplicationUser>()
                .Function(nameof(UsersController.GeneratePasswordResetLink))
                .Returns<string>();
            // Tested
            builder.EntityType<ApplicationUser>()
                .Action(nameof(UsersController.AccountConfirm))
                .Returns<string>();
            //// Tested
            //builder.EntityType<ApplicationUser>()
            //    .Action(nameof(UsersController.SendAccountConfirmationEmail))
            //    .Returns<string>();
            // Tested
            builder.EntityType<ApplicationUser>()
                .Action(nameof(UsersController.SendPasswordResetEmail))
                .Returns<string>();
            // Tested
            builder.EntityType<ApplicationUser>()
                .Action(nameof(UsersController.ReinstateUser))
                .Returns<string>();
            // Tested
            builder
                .EntityType<ApplicationUser>()
                .Collection
                .Function(nameof(UsersController.Me))
                //.Returns<string>()
                .ReturnsFromEntitySet<ApplicationUser>(nameof(IIqlSampleAppService.Users));
        }

        private static void ApplyCreatedByUser<T>(ODataModelBuilder builder,
            Expression<Func<ApplicationUser, IEnumerable<T>>> partnerExpression)
            where T : class, ICreatedBy<ApplicationUser>
        {
            builder.EntityType<T>()
                .Ignore(p => p.CreatedByUserId)
                ;
            builder.EntityType<T>()
                .HasOptional(
                    c => c.CreatedByUser,
                    (entity, user) => entity.CreatedByUserId == user.Id,
                    partnerExpression)
                ;
        }
    }
}