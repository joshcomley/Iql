namespace Iql.Queryable.Data.QueryContainer
{
    public class QueryPipeChangedEvent<T> : IQueryPipeChangedEvent
        where T : class
    {
        public QueryPipe<T> QueryPipe { get; set; }

        IQueryPipe IQueryPipeChangedEvent.QueryPipe
        {
            get => QueryPipe;
            set => QueryPipe = (QueryPipe<T>)value;
        }

        public QueryPipeChangedEvent(QueryPipe<T> queryPipe)
        {
            QueryPipe = queryPipe;
        }
    }
}