using Brandless.AspNetCore.OData.Extensions.Configuration;
using Iql.Entities;
using Iql.Entities.Permissions;
using Iql.Server;
using IqlSampleApp.Data.Contracts;
using IqlSampleApp.Data.Controllers.Api.Entities;
using IqlSampleApp.Data.Entities;
using Microsoft.AspNet.OData.Builder;

namespace IqlSampleApp.Data.Configuration.Entities
{
    public class ClientIqlConfigurator : IIqlEntitySetConfigurator
    {
        public void Configure(IEntityConfigurationBuilder builder)
        {
            EntityConfiguration<Client> entityConfiguration = builder.EntityType<Client>();
            entityConfiguration.FindMethod(nameof(ClientsController.IncrementVersion), true, method =>
                {
                    method.NameSpace = "Abc";
                    method.Permissions.UseRule("FlipFlop");
                });
            entityConfiguration.Permissions.UseRule("BlipBlop");
            entityConfiguration.Permissions.UseRule("FlipFlop");
            entityConfiguration.Permissions.RemoveRule("FlipFlop");
            entityConfiguration.Permissions.UseRule("BooBoo");
            entityConfiguration
                .Builder
                .Permissions
                .DefineUserPermissionRule<ApplicationUser>("ClientReadAndEdit1",
                    context =>
                    context.User.ClientId == null ? IqlUserPermission.ReadAndEdit : IqlUserPermission.Read
                    );
            entityConfiguration.ConfigureProperty(_ => _.AverageIncome, property =>
            {
                property.Permissions.UseRule("PropertyRule1").UseRule("PropertyRule2");
                    property.EntityConfiguration.Builder.Permissions.DefineUserPermissionRule<ApplicationUser>("ClientReadAndEdit2",
                        context =>
                        context.User.ClientId == null ? IqlUserPermission.ReadAndEdit : IqlUserPermission.Read
                        );
                });
        }
    }

    public class ClientODataConfigurator : IODataEntitySetConfigurator
    {
        public void Configure(ODataModelBuilder builder)
        {
            builder.EntityType<Client>()
                .HasRequired(
                    client => client.Type,
                    (client, type) => client.TypeId == type.Id,
                    clientType => clientType.Clients);
            // Tested
            builder
                .EntityType<Client>()
                .Collection
                .Function(nameof(ClientsController.All))
                .ReturnsCollectionFromEntitySet<Client>(nameof(IIqlSampleAppService.Clients))
                ;
        }
    }
}
