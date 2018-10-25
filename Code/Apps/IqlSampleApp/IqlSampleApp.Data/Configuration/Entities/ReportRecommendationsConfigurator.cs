using System;
using Microsoft.AspNet.OData.Builder;
using Brandless.AspNetCore.OData.Extensions.Configuration;
using Microsoft.OData.Edm;
using Tunnel.App.Data.Entities;

namespace Tunnel.App.Web.OData.Configuration.Entities
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