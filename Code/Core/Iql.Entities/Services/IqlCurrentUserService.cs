using System.Threading.Tasks;

namespace Iql.Entities.Services
{
    public abstract class IqlCurrentUserService
    {
        public abstract Task<object> ResolveCurrentUserAsync(IqlServiceProvider serviceProvider);
        public abstract Task<object> ResolveCurrentUserIdAsync(IqlServiceProvider serviceProvider);
    }
}