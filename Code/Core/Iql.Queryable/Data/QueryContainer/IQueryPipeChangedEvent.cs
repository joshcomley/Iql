using Iql.Queryable.Data.Lists;

namespace Iql.Queryable.Data.QueryContainer
{
    public interface IQueryPipeChangedEvent
    {
        IQueryPipe QueryPipe { get; set; }
    }
    public interface IQueryPipeEvent : IQueryPipeChangedEvent
    {
        IDbQueryable Query { get; set; }
    }
    public interface IQueryPipeInspectorEvent
    {
        IDbQueryable Query { get; }
    }
}