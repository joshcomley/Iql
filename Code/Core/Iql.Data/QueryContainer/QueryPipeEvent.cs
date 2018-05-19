using Iql.Data.Context;
using Iql.Data.Lists;

namespace Iql.Data.QueryContainer
{
    public class QueryPipeEvent<T> : QueryPipeChangedEvent<T>, IQueryPipeEvent
        where T : class
    {
        public QueryPipeEvent(QueryPipe<T> queryPipe) : base(queryPipe)
        {
            Query = queryPipe.SourceQuery.Copy();
        }

        public DbQueryable<T> Query { get; set; }

        IDbQueryable IQueryPipeEvent.Query
        {
            get => Query;
            set => Query = (DbQueryable<T>)value;
        }
    }

    public class QueryPipeInspectorEvent<T> : IQueryPipeInspectorEvent
        where T : class
    {
        private readonly DbQueryable<T> _query;

        public QueryPipeInspectorEvent(DbQueryable<T> query)
        {
            _query = query;
        }

        public DbQueryable<T> Query
        {
            get => _query;
        }

        IDbQueryable IQueryPipeInspectorEvent.Query
        {
            get => Query;
        }
    }
}