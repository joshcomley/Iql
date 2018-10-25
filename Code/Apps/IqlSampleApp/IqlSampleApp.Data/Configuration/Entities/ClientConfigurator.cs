using System;
using Tunnel.App.Data.Entities;
using Tunnel.App.Data.Models.Contracts;
using Tunnel.App.Web.Controllers.Api.Entities;
using Microsoft.AspNet.OData.Builder;
using Brandless.AspNetCore.OData.Extensions.Configuration;
using Microsoft.AspNet.OData.Builder;
using Microsoft.OData.Edm;

namespace Tunnel.App.Web.OData.Configuration.Entities
{
    public class ClientConfigurator : IODataEntitySetConfigurator
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
                .ReturnsCollectionFromEntitySet<Client>(nameof(ITunnelService.Clients))
                ;
        }
    }
}
