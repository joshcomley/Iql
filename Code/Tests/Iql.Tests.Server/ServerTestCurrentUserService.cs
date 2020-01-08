using System.Collections.Generic;
using System.Threading.Tasks;
using Iql.Data.Evaluation;
using Iql.Entities;
using Iql.Entities.Services;
using Iql.Parsing.Evaluation;
using Iql.Server.OData.Net;
using IqlSampleApp.Data.Entities;

namespace Iql.Tests.Server
{
    public class ServerTestCurrentUserService : IqlHttpCurrentUserService<ApplicationUser>
    {
        private static readonly Dictionary<object, ApplicationUser> Users = new Dictionary<object, ApplicationUser>();
        public static void RegisterUser(object id, ApplicationUser user)
        {
            if (!Users.ContainsKey(id))
            {
                Users.Add(id, user);
            }
            else
            {
                Users[id] = user;
            }
        }

        public ServerTestCurrentUserService(IqlHttpServiceProviderContext context) : base(context)
        {
        }

        protected override Task<object> ResolveUserByIdAsync(IIqlDataEvaluator evaluator, EntityConfiguration<ApplicationUser> entityConfiguration,
            object userId)
        {
            return Task.FromResult<object>(Users.ContainsKey(userId) ? Users[userId] : null);
        }

        public override Task<IqlObjectEvaluationResult> ResolveCurrentUserIdAsync(IqlServiceProvider serviceProvider)
        {
            return Task.FromResult(new IqlObjectEvaluationResult(true, "testuser"));
        }
    }
}