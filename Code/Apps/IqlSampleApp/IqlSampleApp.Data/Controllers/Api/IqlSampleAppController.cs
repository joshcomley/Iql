using Tunnel.App.Data;
using Tunnel.App.Data.Entities;
using Brandless.AspNetCore.OData.Extensions.Controllers;

namespace Tunnel.App.Web.Controllers.Api
{
    public abstract class IqlSampleAppController<TModel> :
        ODataCrudController<ApplicationDbContext, ApplicationDbContext, ApplicationUser, TModel>
        where TModel : class
    {

    }
}
