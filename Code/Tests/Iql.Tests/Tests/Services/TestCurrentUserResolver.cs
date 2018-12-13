using System;
using System.Threading.Tasks;
using Iql.Entities.Services;
using Iql.Parsing.Evaluation;

namespace Iql.Tests.Tests.Services
{
    public class TestCurrentUserResolver : IqlCurrentUserService
    {
        public static string TestCurrentUserId { get; } = "testuserid";

        public override Task<IqlObjectEvaluationResult> ResolveCurrentUserAsync(IqlServiceProvider serviceProvider)
        {
            throw new NotImplementedException();
        }

        public override Task<IqlObjectEvaluationResult> ResolveCurrentUserIdAsync(IqlServiceProvider serviceProvider)
        {
            return Task.FromResult(new IqlObjectEvaluationResult(
                true, TestCurrentUserId));
        }
    }
}