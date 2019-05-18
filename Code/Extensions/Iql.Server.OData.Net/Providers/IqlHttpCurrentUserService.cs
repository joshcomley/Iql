using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Iql.Data.Evaluation;
using Iql.Entities;
using Iql.Entities.Services;
using Iql.Parsing.Evaluation;

namespace Iql.Server.OData.Net
{
    public class IqlHttpCurrentUserService<TUser> : IqlCurrentUserService
        where TUser : class
    {
        public IqlHttpServiceProviderContext Context { get; }

        public IqlHttpCurrentUserService(IqlHttpServiceProviderContext context)
        {
            Context = context;
        }

        public override async Task<IqlObjectEvaluationResult> ResolveCurrentUserAsync(IqlServiceProvider serviceProvider)
        {
            var evaluator = serviceProvider.Resolve<IIqlDataEvaluator>();
            var userIdResult = await ResolveCurrentUserIdAsync(serviceProvider);
            if (!Equals(null, userIdResult) && !Equals(null, userIdResult.Result))
            {
                var entityConfiguration = Context.EntityConfigurationBuilder.EntityType<TUser>();
                var user = await ResolveUserByIdAsync(evaluator, entityConfiguration, userIdResult.Result);
                return new IqlObjectEvaluationResult(user != null, user);
            }
            return new IqlObjectEvaluationResult(false, null);
        }

        protected virtual async Task<object> ResolveUserByIdAsync(IIqlDataEvaluator evaluator, EntityConfiguration<TUser> entityConfiguration,
            object userId)
        {
            var user = await evaluator.GetEntityByKeyAsync(entityConfiguration,
                CompositeKey.Ensure(userId, entityConfiguration), new string[] { }, false);
            return user;
        }

        public override async Task<IqlObjectEvaluationResult> ResolveCurrentUserIdAsync(IqlServiceProvider serviceProvider)
        {
            if (!Context.HttpContext.User.Identity.IsAuthenticated)
            {
                return new IqlObjectEvaluationResult(true, null);
            }

            if (Context.HttpContext.User.Identity is ClaimsIdentity claims)
            {
                var id = claims.Claims.SingleOrDefault(_ => _.Type == "sub");
                if (id != null)
                {
                    return new IqlObjectEvaluationResult(true, id.Value);
                }
            }
            var name = Context.HttpContext.User.Identity.Name;
            if (Context.ResolveUserIdByName != null)
            {
                var id = await Context.ResolveUserIdByName(name);
                return new IqlObjectEvaluationResult(true, id);
            }
            return new IqlObjectEvaluationResult(false, null);
        }
    }
}