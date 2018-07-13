using System.Collections.Generic;
using System.Reflection;

namespace Iql.Server
{
    public interface IAssemblyProvider
    {
        IEnumerable<Assembly> CandidateAssemblies { get; }
    }
}