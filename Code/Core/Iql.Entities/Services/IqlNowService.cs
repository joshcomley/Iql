using System;
using System.Threading.Tasks;

namespace Iql.Entities.Services
{
    public abstract class IqlNowService
    {
        public abstract Task<DateTimeOffset> ResolveNowAsync(IqlServiceProvider serviceProvider);
    }
}