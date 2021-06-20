using Brandless.AspNetCore.OData.Extensions.Configuration;
using IqlSampleApp.Data.Entities;
using Microsoft.OData.ModelBuilder;

namespace IqlSampleApp.Data.Configuration.Entities
{
    public class RiskAssessmentConfigurator : IODataEntitySetConfigurator
    {
        public void Configure(ODataModelBuilder builder)
        {
            builder
                .EntityType<RiskAssessment>()
                .HasRequired(
                    si => si.SiteInspection,
                    (riskAssessment, siteInspection) => riskAssessment.SiteInspectionId == siteInspection.Id,
                    siteInspection => siteInspection.RiskAssessments
                );
        }
    }
}