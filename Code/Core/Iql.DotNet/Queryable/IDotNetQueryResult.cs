using System;
using System.Collections;
using System.Collections.Generic;
using Iql.Queryable;
using Iql.Queryable.Data.DataStores.InMemory;
using Iql.Queryable.Native;
using Iql.Queryable.Operations;

namespace Iql.DotNet.Queryable
{
    public interface IDotNetQueryResult : IInMemoryResult
    {
        RelationshipExpander RelationshipExpander { get; }
        List<Func<IEnumerable, IEnumerable>> Actions { get; }
        bool HasKey { get; set; }
        CompositeKey Key { get; set; }
        string GetDataSetObjectName(Type type);
        IList DataSetByType(Type type);
        void AddMatches(Type type, IList matches);
    }
}