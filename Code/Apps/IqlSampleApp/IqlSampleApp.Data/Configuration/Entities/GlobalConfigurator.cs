using Brandless.AspNetCore.OData.Extensions.Configuration;
using Iql.Server;
using IqlSampleApp.Data.Contracts;
using Microsoft.OData.ModelBuilder;

namespace IqlSampleApp.Data.Configuration.Entities
{
    public class AllConfigurator2 : AllConfigurator<IIqlSampleAppService>
    {

    }
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