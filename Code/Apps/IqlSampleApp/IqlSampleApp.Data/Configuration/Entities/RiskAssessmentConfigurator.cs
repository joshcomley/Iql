using System;
using Microsoft.AspNet.OData.Builder;
using Brandless.AspNetCore.OData.Extensions.Configuration;
using Microsoft.OData.Edm;
using Tunnel.App.Data.Entities;

namespace Tunnel.App.Web.OData.Configuration.Entities
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
                    siteInspection => siteInspection.RiskAssessment
                );
        }
    }
}