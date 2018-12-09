using System.Threading.Tasks;

namespace Iql.Entities.Services
{
    public abstract class IqlCurrentLocationService
    {
        public abstract Task<IqlPointExpression> ResolveCurrentLocationAsync(IqlServiceProvider serviceProvider);
    }
}