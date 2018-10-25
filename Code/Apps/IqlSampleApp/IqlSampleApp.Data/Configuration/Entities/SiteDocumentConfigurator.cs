using System;
using Microsoft.AspNet.OData.Builder;
using Brandless.AspNetCore.OData.Extensions.Configuration;
using Microsoft.OData.Edm;
using Tunnel.App.Data.Entities;

namespace Tunnel.App.Web.OData.Configuration.Entities
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