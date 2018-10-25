using System;
using Brandless.AspNetCore.OData.Extensions.Configuration;
using Microsoft.AspNet.OData.Builder;
using Microsoft.OData.Edm;
using Tunnel.App.Web.Controllers.Api;

namespace Tunnel.App.Web.OData.Configuration.Entities
{
    public class GlobalConfigurator : IODataEntitySetConfigurator
    {
        public void Configure(ODataModelBuilder builder)
        {
            //var sayHi = builder.Function(nameof(FunctionsController.SayHi));
            //sayHi
            //    .Parameter(typeof(string), "name");
            //sayHi.Returns<string>();

            //var sendHi = builder.Action(nameof(FunctionsController.SendHi));
            //sendHi
            //    .Parameter(typeof(string), "name");
            //sendHi.Returns<string>();
        }
    }
}