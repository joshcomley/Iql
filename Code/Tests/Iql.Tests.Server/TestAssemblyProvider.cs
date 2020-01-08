using System.Collections.Generic;
using System.Reflection;
using Iql.Server;
using IqlSampleApp.Data.Contracts;

namespace Iql.Tests.Server
{
    public class TestAssemblyProvider : IAssemblyProvider
    {
        public IEnumerable<Assembly> CandidateAssemblies => new[] {typeof(IIqlSampleAppService).Assembly};
    }
}