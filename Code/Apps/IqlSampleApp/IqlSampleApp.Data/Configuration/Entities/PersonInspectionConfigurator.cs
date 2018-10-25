using System;
using Microsoft.AspNet.OData.Builder;
using Brandless.AspNetCore.OData.Extensions.Configuration;
using Microsoft.OData.Edm;
using Tunnel.App.Data.Entities;

namespace Tunnel.App.Web.OData.Configuration.Entities
{
    public class PersonInspectionConfigurator : IODataEntitySetConfigurator
    {
        public void Configure(ODataModelBuilder builder)
        {
            builder
                .EntityType<PersonInspection>()
                .HasRequired(
                    si => si.SiteInspection,
                    (personInspection, siteInspection) => personInspection.SiteInspectionId == siteInspection.Id,
                    siteInspection => siteInspection.PersonInspections
                );
        }
    }
}