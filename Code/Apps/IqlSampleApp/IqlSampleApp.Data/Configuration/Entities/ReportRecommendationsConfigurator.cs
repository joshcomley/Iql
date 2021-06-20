using Brandless.AspNetCore.OData.Extensions.Configuration;
using IqlSampleApp.Data.Entities;
using Microsoft.OData.ModelBuilder;

namespace IqlSampleApp.Data.Configuration.Entities
{
    public class ReportRecommendationsConfigurator : IODataEntitySetConfigurator
    {
        public void Configure(ODataModelBuilder builder)
        {
            builder
                .EntityType<ReportRecommendation>()
                .HasRequired(d => d.PersonReport, (recommendation, report) => recommendation.ReportId == report.Id,
                    report => report.Recommendations);
            builder
                .EntityType<ReportRecommendation>()
                .HasRequired(d => d.Recommendation,
                    (recommendation, defaultRecommendation) => recommendation.RecommendationId ==
                                                               defaultRecommendation.Id,
                    recommendation => recommendation.Recommendations);
        }
    }
}