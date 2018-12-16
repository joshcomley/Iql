using System.Linq;
using Brandless.AspNetCore.OData.Extensions.Configuration;
using Iql.Entities;
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
