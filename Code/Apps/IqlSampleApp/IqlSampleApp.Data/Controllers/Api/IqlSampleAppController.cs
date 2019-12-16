using System.Threading.Tasks;
using Brandless.AspNetCore.OData.Extensions.Controllers;
using Iql.Server.OData.Net;
using IqlSampleApp.Data.Contracts;
using IqlSampleApp.Data.Entities;
using Microsoft.AspNet.OData;

namespace IqlSampleApp.Data.Controllers.Api
{
    [EnableQuery]
    public abstract class IqlSampleAppController<TModel> :
        IqlODataController<IIqlSampleAppService, ApplicationDbContext, ApplicationDbContext, ApplicationUser, TModel>
        where TModel : class
    {
        protected override Task EnqueueThumbnailRequest(string sourceUrl, string targetUrl)
        {
            throw new System.NotImplementedException();
        }
    }
}
