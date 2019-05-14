using Brandless.AspNetCore.OData.Extensions.Configuration;
using Iql.Entities;
using Iql.Server;
using IqlSampleApp.Data.Entities;
using Microsoft.AspNet.OData.Builder;

namespace IqlSampleApp.Data.Configuration.Entities
{
    public class SiteAreaIqlConfigurator : IIqlEntitySetConfigurator
    {
        public void Configure(IEntityConfigurationBuilder builder)
        {
            builder.EntityType<SiteArea>()
                .DefineDisplayFormatter(siteArea =>
                    (siteArea.Site == null ? "no site" : siteArea.Site.Name) +
                    (siteArea.CreatedByUser == null ? "" : " - " + siteArea.CreatedByUser.FullName));
        }
    }

    public class SiteAreaODataConfigurator : IODataEntitySetConfigurator
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