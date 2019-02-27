using Brandless.AspNetCore.OData.Extensions.Configuration;
using Iql.Entities;
using Iql.Server;
using IqlSampleApp.Data.Entities;
using Microsoft.AspNet.OData.Builder;

namespace IqlSampleApp.Data.Configuration.Entities
{
    public class ProjectConfigurator : IODataEntitySetConfigurator
    {
        public void Configure(ODataModelBuilder builder)
        {
        }
    }
}
