using System.Threading.Tasks;
using IqlSampleApp.Data.Entities;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;

namespace IqlSampleApp.Data.Controllers.Api.Entities
{
    public class ClientsController : IqlSampleAppController<Client>
    {
        #region Imports

        [ODataRoute("Clients/Tunnel.All")]
        public async Task<IActionResult> All()
        {
            return Ok(await Get());
        }

        #endregion Imports
    }
}
