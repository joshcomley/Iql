using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Iql.Server
{
    public class DefaultAssemblyProvider : IAssemblyProvider
    {
        private readonly Lazy<IEnumerable<Assembly>> _candidateAssemblies;

        public DefaultAssemblyProvider()
        {
            _candidateAssemblies = new Lazy<IEnumerable<Assembly>>(GetCandidateAssemblies);
        }

        public IEnumerable<Assembly> CandidateAssemblies => _candidateAssemblies.Value;

        private static IEnumerable<Assembly> GetCandidateAssemblies()
        {
            var entryAssemblies = Assembly.GetEntryAssembly().GetReferencedAssemblies().Select(Assembly.Load).ToList();
            var callingAssemblies = Assembly.GetCallingAssembly().GetReferencedAssemblies().Select(Assembly.Load).ToList();
            return entryAssemblies.Concat(callingAssemblies).Distinct();
        }
    }
}