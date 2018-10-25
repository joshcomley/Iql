using System;
using Microsoft.AspNet.OData.Builder;
using Brandless.AspNetCore.OData.Extensions.Configuration;
using Microsoft.OData.Edm;
using Tunnel.App.Data.Entities;

namespace Tunnel.App.Web.OData.Configuration.Entities
{
    public class SiteInspectionConfigurator : IODataEntitySetConfigurator
    {
        public void Configure(ODataModelBuilder builder)
        {
            builder
                .EntityType<SiteInspection>()
                .HasRequired(s => s.Site, (inspection, site) => inspection.SiteId == site.Id,
                    site => site.SiteInspections);
        }
    }
}