using Brandless.AspNetCore.OData.Extensions.Configuration;
using IqlSampleApp.Data.Entities;
using Microsoft.OData.ModelBuilder;

namespace IqlSampleApp.Data.Configuration.Entities
{
    public class SiteDocumentConfigurator : IODataEntitySetConfigurator
    {
        public void Configure(ODataModelBuilder builder)
        {
            builder
                .EntityType<SiteDocument>()
                .HasRequired(d => d.Category, (document, category) => document.CategoryId == category.Id,
                    category => category.Documents);
            builder
                .EntityType<SiteDocument>()
                .HasRequired(d => d.Site, (document, site) => document.SiteId == site.Id, site => site.Documents);
        }
    }
}