using Brandless.AspNetCore.OData.Extensions.Configuration;
using IqlSampleApp.Data.Entities;
using Microsoft.AspNet.OData.Builder;

namespace IqlSampleApp.Data.Configuration.Entities
{
    public class SiteAreaConfigurator : IODataEntitySetConfigurator
    {
        public void Configure(ODataModelBuilder builder)
        {
            builder.EntityType<SiteArea>()
                .HasRequired(
                    s => s.Site,
                    (area, site) => area.SiteId == site.Id,
                    siteArea => siteArea.Areas);
        }
    }
}