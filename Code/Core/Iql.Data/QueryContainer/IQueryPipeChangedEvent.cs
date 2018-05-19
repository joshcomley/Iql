using Iql.Data.Lists;

namespace Iql.Data.QueryContainer
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