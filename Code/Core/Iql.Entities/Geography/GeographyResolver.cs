using System;
using System.Threading.Tasks;

namespace Iql.Entities.Geography
{
    public class GeographyResolver<T> : IGeographyResolver
    {
        private readonly Func<object, Task<Geography>> _untypedResolver;
        public GeographyResolver(Func<T, Task<Geography>> resolver = null)
        {
            _untypedResolver = _ => ResolveAsync((T)_);
        }
        public Func<T, Task<Geography>> ResolveAsync { get; set; }
        Func<object, Task<Geography>> IGeographyResolver.ResolveAsync => _untypedResolver;
    }
}