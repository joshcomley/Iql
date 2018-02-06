using System;
using System.Collections;
using System.Collections.Generic;

namespace Iql.Queryable.Data.DataStores.InMemory
{
    public interface IInMemoryResult : IQueryResultBase
    {
        Dictionary<Type, IList> GetResults();
    }
}