using Iql.Entities.Services;

namespace Iql.Tests.Server
{
    public class TestServiceProvider : IServiceProviderProvider
    {
        public IqlServiceProvider ServiceProvider { get; } = new IqlServiceProvider();
    }
}