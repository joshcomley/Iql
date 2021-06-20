using Brandless.AspNetCore.OData.Extensions.Configuration;
using IqlSampleApp.Data.Entities;
using Microsoft.OData.ModelBuilder;

namespace IqlSampleApp.Data.Configuration.Entities
{
    public class ReportTypeConfigurator : IODataEntitySetConfigurator
    {
        public void Configure(ODataModelBuilder builder)
        {
            builder
                .EntityType<ReportType>()
                .HasRequired(d => d.Category, (type, category) => type.CategoryId == category.Id,
                    category => category.ReportTypes);
        }
    }
}