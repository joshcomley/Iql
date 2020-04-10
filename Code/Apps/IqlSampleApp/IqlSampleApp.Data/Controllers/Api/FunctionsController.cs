using System.Threading.Tasks;
using Brandless.AspNetCore.OData.Extensions.Configuration;
using Brandless.AspNetCore.OData.Extensions.Controllers;
using IqlSampleApp.Data.Contracts;
using IqlSampleApp.Data.Entities;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace IqlSampleApp.Data.Controllers.Api
{
    [EnableQuery]
    [Route("odata")]
    public class FunctionsController : ControllerBase
    {
        [HttpPost("ValidateField")]
        public virtual Task<IActionResult> ValidateField([FromBody] JObject validation)
        {
            return this.ValidateFieldInService<IIqlSampleAppService>(validation);
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
