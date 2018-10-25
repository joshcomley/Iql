using System.Threading.Tasks;
using Iql.Server;
using Iql.Server.Serialization.Serialization;
using Microsoft.AspNetCore.Mvc;
using Tunnel.App.Data.Models.Contracts;

namespace Tunnel.App.Data.Controllers
{
    public class HomeController : Controller
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
            var entityConfigurationBuilder = _entityConfigurationProvider.Get<ITunnelService>();
            IqlJson = IqlJson ?? entityConfigurationBuilder.ToJson();
            return Content(IqlJson, "application/json");
        }
    }
}