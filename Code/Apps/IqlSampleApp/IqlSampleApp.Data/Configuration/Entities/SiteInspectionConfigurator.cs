using Brandless.AspNetCore.OData.Extensions.Configuration;
using IqlSampleApp.Data.Entities;
using Microsoft.AspNet.OData.Builder;

namespace IqlSampleApp.Data.Configuration.Entities
{
    public class SiteInspectionConfigurator : IODataEntitySetConfigurator
    {
        public void Configure(ODataModelBuilder builder)
        {
            builder
                .EntityType<SiteInspection>()
                .HasRequired(s => s.Site, (inspection, site) => inspection.SiteId == site.Id,
                    site => site.SiteInspections);
        }
    }
}