using Brandless.AspNetCore.OData.Extensions.Controllers;
using IqlSampleApp.Data.Entities;

namespace IqlSampleApp.Data.Controllers.Api
{
    public abstract class IqlSampleAppController<TModel> :
        ODataCrudController<ApplicationDbContext, ApplicationDbContext, ApplicationUser, TModel>
        where TModel : class
    {

    }
}
