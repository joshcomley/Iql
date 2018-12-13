using Brandless.AspNetCore.OData.Extensions.Configuration;
using Iql.Entities;
using Iql.Server;
using IqlSampleApp.Data.Entities;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNetCore.OData.NetTopology;

namespace IqlSampleApp.Data.Configuration.Entities
{
    public class SiteIqlConfigurator : IIqlEntitySetConfigurator
    {
        public void Configure(IEntityConfigurationBuilder builder)
        {
            builder.EntityType<Site>()
                .ConfigureProperty(_ => _.Key,
                    _ =>
                    {
                        _.IsInferredWith(site => site.ClientId);
                    })
                .ConfigureProperty(_ => _.FullAddress,
                    _ =>
                    {
                        _.IsInferredWith(site => site.Address + "\n" + site.PostCode);
                    })
                .DefinePropertyValidation(_ => _.FullAddress, _ => _.FullAddress == null || _.FullAddress == "");
        }
    }

    public class SiteConfigurator : IODataEntitySetConfigurator
    {
        public void Configure(ODataModelBuilder builder)
        {
            builder.MapSpatial<Site>(_ => _.EdmLocation, _ => _.Location);
            builder.MapSpatial<Site>(_ => _.EdmArea, _ => _.Area);
            builder.MapSpatial<Site>(_ => _.EdmLine, _ => _.Line);
            builder.EntityType<Site>()
                .HasOptional(s => s.Parent, (left, right) => left.ParentId == right.Id, site => site.Children);
            builder.EntityType<Site>()
                .HasOptional(s => s.Client, (left, right) => left.ClientId == right.Id, client => client.Sites);
        }
    }
}