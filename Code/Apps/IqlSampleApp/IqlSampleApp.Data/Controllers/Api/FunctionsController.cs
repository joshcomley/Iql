using System.Threading.Tasks;
using Brandless.AspNetCore.OData.Extensions.Configuration;
using Brandless.AspNetCore.OData.Extensions.Controllers;
using Tunnel.App.Data.Models.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNet.OData;
using Newtonsoft.Json.Linq;
using Tunnel.App.Data.Entities;

namespace Tunnel.App.Web.Controllers.Api
{
    [EnableQuery]
    [Route("odata")]
    public class FunctionsController : Controller
    {
        [HttpPost("ValidateField")]
        public virtual Task<IActionResult> ValidateField([FromBody] JObject validation)
        {
            return this.ValidateFieldInService<ITunnelService>(validation);
        }

        [HttpGet(nameof(SayHi))]
        [ODataFunction(ForType = typeof(ClientType))]
        public async Task<string> SayHi(int key, string name)
        {
            return "Hello, " + name;
        }

        [HttpPost(nameof(SendHi))]
        [ODataAction]
        public async Task<string> SendHi(string name)
        {
            return "Hello, " + name;
        }
    }
}
