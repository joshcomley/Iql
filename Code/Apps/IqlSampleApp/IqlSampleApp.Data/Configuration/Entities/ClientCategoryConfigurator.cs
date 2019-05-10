using Brandless.AspNetCore.OData.Extensions.Configuration;
using IqlSampleApp.Data.Entities;
using Microsoft.AspNet.OData.Builder;

namespace IqlSampleApp.Data.Configuration.Entities
{
    public class ClientODataCategoryConfigurator : IODataEntitySetConfigurator
    {
        public void Configure(ODataModelBuilder builder)
        {
            builder.EntityType<ClientCategoryPivot>()
                .HasRequired(_ => _.Client, (pivot, client) => pivot.ClientId == client.Id, _ => _.Categories);
            builder.EntityType<ClientCategoryPivot>()
                .HasRequired(_ => _.Category, (pivot, category) => pivot.CategoryId == category.Id, _ => _.Clients);
            builder.EntityType<ClientCategoryPivot>()
                .HasKey(_ => new
                {
                    _.ClientId,
                    _.CategoryId
                    });
        }
    }
}