using System;
using System.Threading.Tasks;
using Iql.Data.Evaluation;
using Iql.Entities;
using Iql.Entities.Services;
using Microsoft.AspNetCore.Http;

namespace Iql.Server.OData.Net
{
    public class IqlHttpServiceProviderContext
    {
        public HttpContext HttpContext { get; set; }
        public IEntityConfigurationBuilder EntityConfigurationBuilder { get; }
        public Func<string, Task<object>> ResolveUserIdByName { get; set; }
        public IIqlDataEvaluator DataEvaluator { get; set; }
        public IqlCurrentUserService CurrentUserService { get; set; }

        public IqlHttpServiceProviderContext(
            HttpContext httpContext,
            IEntityConfigurationBuilder entityConfigurationBuilder,
            Func<string, Task<object>> resolveUserIdByName,
            IIqlDataEvaluator dataEvaluator,
            IqlCurrentUserService currentUserService)
        {
            HttpContext = httpContext;
            EntityConfigurationBuilder = entityConfigurationBuilder;
            ResolveUserIdByName = resolveUserIdByName;
            DataEvaluator = dataEvaluator;
            CurrentUserService = currentUserService;
        }
    }
}