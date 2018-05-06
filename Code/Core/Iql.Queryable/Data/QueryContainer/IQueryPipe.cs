using System.Threading.Tasks;
using Iql.Queryable.Data.Lists;
using Iql.Queryable.Events;

namespace Iql.Queryable.Data.QueryContainer
{
    public interface IQueryPipe
    {
        bool DisableAutoEvents { get; set; }
        bool ResultsLoading { get; }

        IDbQueryable SourceQuery { get; set; }
        IDbList Results { get; }

        IEventSubscriber<IQueryPipeChangedEvent> SourceQueryChanged { get; }
        IAsyncEventSubscriber<IQueryPipeChangedEvent> ResultsLoadingChanged { get; }
        IAsyncEventSubscriber<IQueryPipeChangedEvent> ResultsLoaded { get; }
        IAsyncEventSubscriber<IQueryPipeEvent> Pipe { get; }
        IAsyncEventSubscriber<IQueryPipeChangedEvent> QueryBuildingChanged { get; }
        IAsyncEventSubscriber<IQueryPipeInspectorEvent> QueryBuilt { get; }

        Task RefreshResultsAsync();

        void Dispose();
    }
}