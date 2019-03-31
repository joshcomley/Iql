using System;
using System.Threading.Tasks;

namespace Iql.Entities.Services
{
    public abstract class IqlNewGuidService
    {
        public abstract Task<Guid> ResolveNewGuidAsync(IqlServiceProvider serviceProvider);
    }
}