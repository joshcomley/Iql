using System;
using Microsoft.AspNet.OData.Builder;
using Brandless.AspNetCore.OData.Extensions.Configuration;
using Microsoft.OData.Edm;
using Tunnel.App.Data.Entities;

namespace Tunnel.App.Web.OData.Configuration.Entities
{
    public class ReportReceiverEmailAddressConfigurator : IODataEntitySetConfigurator
    {
        public void Configure(ODataModelBuilder builder)
        {
            //builder.EntityType<ReportReceiverEmailAddress>()
            //    .HasRequired(s => s.Site,
            //        client => client.AdditionalSendReportsTo);
        }
    }
}