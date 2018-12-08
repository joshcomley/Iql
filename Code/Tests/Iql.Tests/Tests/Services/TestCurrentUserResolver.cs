using System;
using System.Threading.Tasks;
using Iql.Entities.Services;

namespace Iql.Tests.Tests.Services
{
    public class TestCurrentUserResolver : IqlCurrentUserService
    {
        public static string TestCurrentUserId { get; } = "testuserid";

        public override Task<object> ResolveCurrentUserAsync(IqlServiceProvider serviceProvider)
        {
            throw new NotImplementedException();
        }

        public override Task<object> ResolveCurrentUserIdAsync(IqlServiceProvider serviceProvider)
        {
            return Task.FromResult<object>(TestCurrentUserId);
        }
    }
}