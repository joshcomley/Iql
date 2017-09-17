using System.Collections.Generic;

namespace Iql.Queryable
{
    public interface IQueryResult<T> : IQueryResultBase
    {
        new List<T> ToList();
    }
}