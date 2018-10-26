using Brandless.AspNetCore.OData.Extensions.Configuration;
using IqlSampleApp.Data.Entities;
using Microsoft.AspNet.OData.Builder;

namespace IqlSampleApp.Data.Configuration.Entities
{
    public class ReportReceiverEmailAddressConfigurator : IODataEntitySetConfigurator
    {
        public void Configure(ODataModelBuilder builder)
        {
            builder.EntityType<ReportReceiverEmailAddress>()
                .HasRequired(s => s.Site,
                    (address, site) => address.SiteId == site.Id,
                    client => client.AdditionalSendReportsTo);
        }
    }
}