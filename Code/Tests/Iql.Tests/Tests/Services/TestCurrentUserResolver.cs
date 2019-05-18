using System;
using System.Threading.Tasks;
using Iql.Entities.Services;
using Iql.Parsing.Evaluation;
using IqlSampleApp.Data.Entities;

namespace Iql.Tests.Tests.Services
{
    public class TestCurrentUserResolver : IqlCurrentUserService
    {
        public static string TestCurrentUserId { get; } = "testuserid";

        public override Task<IqlObjectEvaluationResult> ResolveCurrentUserAsync(IqlServiceProvider serviceProvider)
        {
            return Task.FromResult(new IqlObjectEvaluationResult(true, new ApplicationUser()
            {
                ClientId = 77
            }));
        }

        public override Task<IqlObjectEvaluationResult> ResolveCurrentUserIdAsync(IqlServiceProvider serviceProvider)
        {
            return Task.FromResult(new IqlObjectEvaluationResult(
                true, TestCurrentUserId));
        }
    }
}