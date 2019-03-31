using System;
using System.Threading.Tasks;

namespace Iql.Entities.Services
{
    public static class IqlServiceProviderExtensions
    {
        public static async Task<DateTimeOffset?> ResolveNowAsync(this IqlServiceProvider serviceProvider, bool force = false)
        {
            var nowService = serviceProvider.Resolve<IqlNowService>();
            if (nowService != null)
            {
                return await nowService.ResolveNowAsync(serviceProvider);
            }
            else if(force)
            {
                return DateTimeOffset.Now;
            }
            return null;
        }

        public static async Task<Guid?> ResolveNewGuidAsync(this IqlServiceProvider serviceProvider, bool force = false)
        {
            var newGuidService = serviceProvider.Resolve<IqlNewGuidService>();
            if (newGuidService != null)
            {
                return await newGuidService.ResolveNewGuidAsync(serviceProvider);
            }
            else if(force)
            {
                return IqlNewGuidExpression.NewGuid();
            }
            return null;
        }
    }
}