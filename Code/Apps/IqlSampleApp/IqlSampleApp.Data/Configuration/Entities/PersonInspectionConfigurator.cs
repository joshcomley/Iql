using Brandless.AspNetCore.OData.Extensions.Configuration;
using IqlSampleApp.Data.Entities;
using Microsoft.OData.ModelBuilder;

namespace IqlSampleApp.Data.Configuration.Entities
{
    public class PersonInspectionConfigurator : IODataEntitySetConfigurator
    {
        public void Configure(ODataModelBuilder builder)
        {
            builder
                .EntityType<PersonInspection>()
                .HasRequired(
                    si => si.SiteInspection,
                    (personInspection, siteInspection) => personInspection.SiteInspectionId == siteInspection.Id,
                    siteInspection => siteInspection.PersonInspections
                );
        }
    }
}