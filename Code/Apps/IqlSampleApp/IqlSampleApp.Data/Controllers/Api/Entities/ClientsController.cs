using System.Threading.Tasks;
using Tunnel.App.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNet.OData.Routing;

namespace Tunnel.App.Web.Controllers.Api.Entities
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
