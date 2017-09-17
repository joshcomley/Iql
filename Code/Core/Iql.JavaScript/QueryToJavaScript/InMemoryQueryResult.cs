using System;
using System.Collections.Generic;
using Iql.Queryable;

namespace Iql.JavaScript.QueryToJavaScript
{
    public class InMemoryQueryResult<T> : QueryResult<T>
    {
        public override List<T> ToList()
        {
            throw new NotImplementedException();
        }
    }
}