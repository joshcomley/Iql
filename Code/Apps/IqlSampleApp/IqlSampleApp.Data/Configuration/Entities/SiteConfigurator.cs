using Brandless.AspNetCore.OData.Extensions.Configuration;
using IqlSampleApp.Data.Entities;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNetCore.OData.NetTopology;

namespace IqlSampleApp.Data.Configuration.Entities
{
    public class SiteConfigurator : IODataEntitySetConfigurator
    {
        public void Configure(ODataModelBuilder builder)
        {
            builder.MapSpatial<Site>(_ => _.EdmLocation, _ => _.Location);
            builder.EntityType<Site>()
                .HasOptional(s => s.Parent, (left, right) => left.ParentId == right.Id, site => site.Children);
            builder.EntityType<Site>()
                .HasOptional(s => s.Client, (left, right) => left.ClientId == right.Id, client => client.Sites);
        }
    }
}