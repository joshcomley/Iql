using System;
using System.Threading.Tasks;

namespace Iql.Entities.Geography
{
    public interface IGeographyResolver
    {
        Func<object, Task<Geography>> ResolveAsync { get; }
    }
}