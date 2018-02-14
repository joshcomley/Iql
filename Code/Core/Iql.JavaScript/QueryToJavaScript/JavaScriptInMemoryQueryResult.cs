using System;
using System.Collections;
using System.Collections.Generic;
using Iql.Queryable;
using Iql.Queryable.Data.DataStores.InMemory;

namespace Iql.JavaScript.QueryToJavaScript
{
    public class JavaScriptInMemoryQueryResult : QueryResult<JavaScriptInMemoryQueryResult>, IInMemoryResult
    {
        public InMemoryQueryResult GetResults()
        {
            throw new NotImplementedException();
        }
    }
}