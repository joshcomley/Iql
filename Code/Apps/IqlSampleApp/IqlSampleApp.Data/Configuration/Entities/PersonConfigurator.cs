using System;
using System.Linq.Expressions;
using Brandless.AspNetCore.OData.Extensions.Configuration;
using Iql;
using Iql.Entities;
using Iql.Entities.InferredValues;
using Iql.Entities.Permissions;
using Iql.Server;
using IqlSampleApp.Data.Entities;
using Microsoft.OData.ModelBuilder;
using Microsoft.AspNetCore.OData.NetTopology;

namespace IqlSampleApp.Data.Configuration.Entities
{
    public class PersonIqlConfigurator : IIqlEntitySetConfigurator
    {
        public void Configure(IEntityConfigurationBuilder builder)
        {
            //model.ModelConfiguration();
            builder.PermissionManager.DefineUserPermissionRule<ApplicationUser>(
                "SkillsPermission",
                context =>
                    context.User.Email == "CannotSeeOrEdit"
                        ? IqlUserPermission.None
                        : (
                            context.User.Email == "CanSeeCannotEdit"
                                ? IqlUserPermission.Read
                                : IqlUserPermission.ReadAndUpdate
                        )
            );
            builder.PermissionManager.DefineUserPermissionRule<ApplicationUser>(
                "SkillsPermissionOnlyReadAttemptedOverride",
                context =>
                    context.User.Email == "OnlyRead"
                        ? IqlUserPermission.Full
                        : IqlUserPermission.Unset
            );
            builder.PermissionManager.DefineUserPermissionRule<ApplicationUser>(
                "PersonsPermission",
                context =>
                    context.User.Email == "CannotSeeOrEditAnyPersons" ? IqlUserPermission.None : IqlUserPermission.Full
            );
            builder.PermissionManager.DefineUserPermissionRule<ApplicationUser>(
                "PersonsPermissionOnlyRead",
                context =>
                    context.User.Email == "OnlyRead" ? IqlUserPermission.Read : IqlUserPermission.Full
            );
            var model = builder.EntityType<Person>();
            model.Permissions.UseRule("PersonsPermission");
            model.Permissions.UseRule("PersonsPermissionOnlyRead");
            model.HasFile(s => s.PhotoUrl,
                new Guid("2ac84af2-2090-4bfd-a27e-ea68ad0faccb"),
                f =>
                {
                    f.SetStateProperty(_ => _.PhotoState);
                    f.SetVersionProperty(_ => _.PhotoRevisionKey);
                    //f.SetHint(KnownHints.Image);
                    //f.SetHint(KnownHints.Video);
                    f.FriendlyName = "Photo";
                });
            model.PreviewPropertyName = model.FindPropertyByExpression(_ => _.PhotoUrl).File.Name;
            model.ConfigureProperty(_ => _.Client, p =>
            {
                p.IsInferredWith(_ => _.CurrentEntityState.Site.Client);
            });

            model.ConfigureProperty(p => p.Skills, p =>
            {
                p.Permissions.UseRule("SkillsPermission");
                p.Permissions.UseRule("SkillsPermissionOnlyReadAttemptedOverride");
                p.IsInferredWith(_ => PersonSkills.Coder,
                    true,
                    InferredValueKind.IfNullOrEmpty,
                    true);
            });
            model.ConfigureProperty(p => p.Birthday, p =>
            {
                p.IsConditionallyInferredWith(
                    _ => new IqlNowExpression(),
                    _ => (_.PreviousEntityState == null || _.PreviousEntityState.Category != PersonCategory.Conventional) &&
                         _.CurrentEntityState.Category == PersonCategory.AutoDescription
                );
            });
            var reports = model.FindCollectionRelationship(_ => _.Reports);
            reports.EditKind = IqlPropertyEditKind.Edit;
            model.ConfigureProperty(_ => _.Description,
                p => { p.IsConditionallyInferredWith(_ => "I'm \\ \"auto\"", _ => _.CurrentEntityState.Category == PersonCategory.AutoDescription); });
            model.ConfigureProperty(_ => _.Location, p =>
            {
                p.IsInferredWith(_ => new IqlCurrentLocationExpression(), false, InferredValueKind.IfNull, true);
            });
            model.ConfigureProperty(_ => _.IsComplete, p => { p.ForceDecision = true; });
            model.ConfigureProperty(_ => _.Category, p =>
            {
                p.IsInferredWith(_ => PersonCategory.Conventional, true, InferredValueKind.InitializeOnly, true);
            });
            model.ConfigureProperty(_ => _.InferredFromUserClientId, p =>
            {
                p.IsInferredWith(_ => IqlCurrentUser.Get<ApplicationUser>().Client.Id, true, InferredValueKind.InitializeOnly);
            });
            model.ConfigureProperty(_ => _.InferredWhenKeyChanges, p =>
            {
                p.IsInferredWith(_ => (_.PreviousEntityState == null || _.PreviousEntityState.Key == "ABC") && _.CurrentEntityState.Key == "DEF" ? "alphabet!" : _.CurrentEntityState.InferredWhenKeyChanges, false, InferredValueKind.Always, false, nameof(Person.Key));
            });
            model.DefineRelationshipFilterRule(
                _ => _.Site,
                context => context.Owner.ClientId == 0 ? (Expression<Func<Site, bool>>)(site => true) : site => site.ClientId == context.Owner.ClientId);
            //model
            //    .FindRelationship(_ => _.SiteArea)
            //    .CreateWithRelationshipValue(_ => _.Site, ctx => _ => _.Site.Parent);
            model
                .FindRelationship(_ => _.SiteArea)
                .CreateWithRelationshipValue(_ => _.Site, ctx => _ => ctx.Owner.Site);

            model.DefineRelationshipFilterRule(
                _ => _.Loading,
                context => loading => loading.Name == "some constant");

            model.DefineDisplayFormatter(p => p.Title);
            model.DefineDisplayFormatter(p => p.Title + " (" + p.Id + ")", "Report");
            model.DefineEntityValidation(
                s => string.IsNullOrWhiteSpace(s.Title) && string.IsNullOrWhiteSpace(s.Description),
                "Please enter either a title or a description",
                "NoTitleOrDescription");
            model.DefineEntityValidation(s => s.Title == "Josh" && s.Description != "Josh",
                "If the name is 'Josh' please match it in the description",
                "JoshCheck");
            model.DefinePropertyValidation(s => s.Title, s => string.IsNullOrWhiteSpace(s.Title),
                "Please enter a person title",
                "EmptyTitle");
            model.DefinePropertyValidation(s => s.Description, s => string.IsNullOrWhiteSpace(s.Description),
                "Please enter a person description",
                "EmptyDescription");
            model.DefinePropertyValidation(s => s.Title, s => !string.IsNullOrWhiteSpace(s.Title) && s.Title.Trim().Length > 50,
                "Please enter less than fifty characters",
                "TitleMaxLength");
            model.DefinePropertyValidation(s => s.Title, s => !string.IsNullOrWhiteSpace(s.Title) && s.Title.Trim().Length < 3,
                "Please enter at least three characters for the person's title",
                "TitleMinLength");
        }
    }

    public class PersonConfigurator : IODataEntitySetConfigurator
    {
        public void Configure(ODataModelBuilder builder)
        {
            // builder.UseNetTopology();
            builder.EntityType<Person>()
                .HasOptional(
                    s => s.Client,
                    (person, client) => person.ClientId == client.Id,
                    client => client.People);
            builder.EntityType<Person>()
                .HasOptional(
                    s => s.InferredFromUserClient,
                    (person, client) => person.InferredFromUserClientId == client.Id,
                    client => client.InferredPeople);
            builder.EntityType<Person>()
                .HasOptional(
                    s => s.Site,
                    (person, site) => person.SiteId == site.Id,
                    site => site.People);
            builder.EntityType<Person>()
                .HasOptional(
                    s => s.SiteArea,
                    (person, siteArea) => person.SiteAreaId == siteArea.Id,
                    siteArea => siteArea.People);
            builder.EntityType<Person>()
                .HasOptional(s => s.Type,
                    (person, type) => person.TypeId == type.Id,
                    type => type.People);
            builder.EntityType<Person>()
                .HasOptional(
                    s => s.Loading,
                    (person, loading) => person.LoadingId == loading.Id,
                    loading => loading.People);
            //ef.Entity<Person>()
            //    .HasOne(s => s.Client)
            //    .WithMany(s => s.People);

            //builder.EntityType<Person>()
            //    .RemoveAllProperties()
            //    //.AddProperty(p => p.Location)
            //    .AddProperty(p => p.Id)
            //    //.AddProperty(p => p.CreatedByUser)
            //    .AddProperty(p => p.CreatedByUserId)
            //    .AddProperty(p => p.ClientId)
            //    .AddProperty(p => p.Title)
            //    .AddProperty(p => p.Description)
            //    //.AddProperty(p => p.TypeId)
            //    ;
            // Tested
            //builder
            //    .EntityType<Person>()
            //    .Action(nameof(PeopleController.IncrementVersion))
            //    .Returns<string>();
        }
    }
}
