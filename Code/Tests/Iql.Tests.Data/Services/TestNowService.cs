using System;
using System.Threading.Tasks;
using Iql.Entities.Services;

namespace Iql.Tests.Data.Services
{
    public class TestNowService : IqlNowService
    {
        public override Task<DateTimeOffset> ResolveNowAsync(IqlServiceProvider serviceProvider)
        {
            return Task.FromResult(new DateTimeOffset(new DateTime(2019, 1, 2, 3, 4, 5)));
        }
    }
}