using System.Collections;
using System.Collections.Generic;

namespace Iql.Queryable
{
    public abstract class QueryResult<T> : IQueryResult<T>
    {
        public abstract List<T> ToList();

        IList IQueryResultBase.ToList()
        {
            return ToList();
        }
    }
}