using System;
using System.Collections;
using System.Collections.Generic;
using Iql.Queryable;
using Iql.Queryable.Operations;

namespace Iql.DotNet.Queryable
{
    public interface IDotNetQueryResult : IQueryResultBase
    {
        List<Func<IEnumerable, IEnumerable>> Actions { get; }
        bool HasKey { get; set; }
        CompositeKey Key { get; set; }
        string GetDataSetObjectName(Type type);
        IList DataSetByType(Type type);
    }
}