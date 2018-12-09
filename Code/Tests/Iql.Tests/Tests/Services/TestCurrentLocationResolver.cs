using System.Threading.Tasks;
using Iql.Entities.Services;

namespace Iql.Tests.Tests.Services
{
    public class TestCurrentLocationResolver : IqlCurrentLocationService
    {
        public static double? CurrentLatitude = null;
        public static double? CurrentLongitude = null;
        public override Task<IqlPointExpression> ResolveCurrentLocationAsync(IqlServiceProvider serviceProvider)
        {
            if (CurrentLongitude == null || CurrentLatitude == null)
            {
                return Task.FromResult<IqlPointExpression>(null);
            }
            return Task.FromResult(new IqlPointExpression(
                CurrentLongitude.Value,
                CurrentLatitude.Value));
        }
    }
}