using System;
using Iql.Queryable;
using Iql.Queryable.Data.DataStores.InMemory;
using Iql.Queryable.QueryApplicator;

namespace Iql.JavaScript.QueryableApplicator
{
    public class JavaScriptInMemoryQueryResult : QueryResult<JavaScriptInMemoryQueryResult>, IInMemoryResult
    {
        public InMemoryQueryResult GetResults()
        {
            throw new NotImplementedException();
        }
    }
}