using System;
using Tunnel.App.Data.Entities;
using Microsoft.AspNet.OData.Builder;
using Brandless.AspNetCore.OData.Extensions.Configuration;
using Microsoft.OData.Edm;

namespace Tunnel.App.Web.OData.Configuration.Entities
{
    public class ProjectConfigurator : IODataEntitySetConfigurator
    {
        public void Configure(ODataModelBuilder builder)
        {
        }
    }
}
