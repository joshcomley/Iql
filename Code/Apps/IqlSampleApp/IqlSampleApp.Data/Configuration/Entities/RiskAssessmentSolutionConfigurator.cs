using Brandless.AspNetCore.OData.Extensions.Configuration;
using IqlSampleApp.Data.Entities;
using Microsoft.AspNet.OData.Builder;

namespace IqlSampleApp.Data.Configuration.Entities
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