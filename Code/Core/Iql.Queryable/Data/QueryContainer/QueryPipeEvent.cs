using Iql.Queryable.Data.Lists;
using Iql.Queryable.Data.Queryable;

namespace Iql.Queryable.Data.QueryContainer
{
    public class QueryPipeEvent<T> : QueryPipeChangedEvent<T>, IQueryPipeEvent
        where T : class
    {
        public QueryPipeEvent(QueryPipe<T> queryPipe) : base(queryPipe)
        {
        }

        public DbQueryable<T> Query
        {
            get => QueryPipe.Query;
            set => QueryPipe.Query = value;
        }

        IDbQueryable IQueryPipeEvent.Query
        {
            get => Query;
            set => Query = (DbQueryable<T>) value;
        }
    }
}