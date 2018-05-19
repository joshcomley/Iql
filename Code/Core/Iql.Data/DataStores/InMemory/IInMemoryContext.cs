using System;
using System.Collections;

namespace Iql.Queryable.Data.DataStores.InMemory
{
    public interface IInMemoryContext
    {
        IEnumerable SourceList { get; set; }
        IEnumerable ResolveSource(Type entityType);
        void AddMatches(Type type, IList matches);
    }
}