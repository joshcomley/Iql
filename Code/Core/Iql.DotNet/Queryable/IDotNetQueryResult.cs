using System;
using System.Collections;
using System.Collections.Generic;
using Iql.Queryable.Data.DataStores.InMemory;
using Iql.Queryable.Native;

namespace Iql.DotNet.Queryable
{
    public interface IDotNetQueryResult : IInMemoryResult
    {
        RelationshipExpander RelationshipExpander { get; }
        List<Func<IEnumerable, IEnumerable>> Actions { get; }
        IList DataSetByType(Type type);
        void AddMatches(Type type, IList matches);
    }
}