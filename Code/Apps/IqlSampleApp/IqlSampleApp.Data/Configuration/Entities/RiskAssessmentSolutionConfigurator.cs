using System;
using Microsoft.AspNet.OData.Builder;
using Brandless.AspNetCore.OData.Extensions.Configuration;
using Microsoft.OData.Edm;
using Tunnel.App.Data.Entities;

namespace Tunnel.App.Web.OData.Configuration.Entities
{
    public class RiskAssessmentSolutionConfigurator : IODataEntitySetConfigurator
    {
        public void Configure(ODataModelBuilder builder)
        {
            builder
                .EntityType<RiskAssessmentSolution>()
                .HasRequired(
                    d => d.RiskAssessment,
                    (assessment, solution) => assessment.RiskAssessmentId == solution.Id,
                    riskAssessment => riskAssessment.RiskAssessmentSolution
                );
        }
    }
}