using Brandless.AspNetCore.OData.Extensions.Configuration;
using Iql.Entities;
using Iql.Server;
using IqlSampleApp.Data.Entities;
using Microsoft.OData.ModelBuilder;

namespace IqlSampleApp.Data.Configuration.Entities
{
    public class SiteInspectionIqlConfigurator : IIqlEntitySetConfigurator
    {
        public void Configure(IEntityConfigurationBuilder builder)
        {
            var inspections = builder.EntityType<SiteInspection>();
            inspections.Configure(i =>
            {
                i.DisplayFormatting.Set(_ => _.Site.Name + " - " + _.EndTime + " (" + (_.CreatedByUser == null ? "no creator" : _.CreatedByUser.FullName) + ")");
            });
        }
    }
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