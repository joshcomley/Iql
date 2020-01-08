using Iql.Data.Evaluation;
using Iql.Entities.Services;
using Iql.Server.OData.Net;

namespace Iql.Tests.Server
{
    public class TestServiceProvider : IServiceProviderProvider
    {
        public IqlServiceProvider ServiceProvider { get; } = new IqlServiceProvider();

        public TestServiceProvider(IIqlDataEvaluator dataEvaluator)
        {
        }
    }
}