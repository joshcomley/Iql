using Iql.Server;
using Iql.Server.Serialization.Serialization;
using IqlSampleApp.Data.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace IqlSampleApp.Data.Controllers
{
    public class HomeController : ControllerBase
    {
        private readonly IEntityConfigurationProvider _entityConfigurationProvider;

        public HomeController(IEntityConfigurationProvider entityConfigurationProvider)
        {
            _entityConfigurationProvider = entityConfigurationProvider;
        }

        public string IqlJson { get; set; }

        [HttpGet("~/iql")]
        public IActionResult Iql()
        {
            var entityConfigurationBuilder = _entityConfigurationProvider.Get<IIqlSampleAppService>();
            IqlJson = IqlJson ?? entityConfigurationBuilder.ToJson().Value;
            return Content(IqlJson, "application/json");
        }

        [HttpGet("~/unql")]
        public IActionResult Unql()
        {
            var entityConfigurationBuilder = _entityConfigurationProvider.Get<IIqlSampleAppService>();
            IqlJson = IqlJson ?? entityConfigurationBuilder.ToJson().Value;
            return Content(IqlJson, "application/json");
        }
    }
}