using System;
using Tunnel.App.Data.Entities;
using Tunnel.App.Web.Controllers.Api.Entities;
using Microsoft.AspNet.OData.Builder;
using Brandless.AspNetCore.OData.Extensions;
using Brandless.AspNetCore.OData.Extensions.Configuration;
using Brandless.AspNetCore.OData.Extensions.Extensions;
using Iql.Entities;
using Iql.Server;
using Microsoft.OData.Edm;

namespace Tunnel.App.Web.OData.Configuration.Entities
{
    public class PersonIqlConfigurator : IIqlEntitySetConfigurator
    {
        public void Configure(IEntityConfigurationBuilder builder)
        {
            //model.ModelConfiguration();
            var model = builder.EntityType<Person>();
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
            builder.EntityType<Person>()
                .HasOptional(
                    s => s.Client,
                    (person, client) => person.ClientId == client.Id,
                    client => client.People);
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
