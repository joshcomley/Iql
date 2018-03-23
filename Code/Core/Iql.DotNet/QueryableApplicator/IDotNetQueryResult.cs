using System;
using System.Collections;
using System.Collections.Generic;
using Iql.Queryable.Data.DataStores.InMemory;
using Iql.Queryable.Data.Relationships;

namespace Iql.DotNet.QueryableApplicator
{
    public interface IDotNetQueryResult : IInMemoryResult
    {
        RelationshipExpander RelationshipExpander { get; }
        List<Func<IEnumerable, IEnumerable>> Actions { get; }
        IList DataSetByType(Type type);
        void AddMatches(Type type, IList matches);
    }
}