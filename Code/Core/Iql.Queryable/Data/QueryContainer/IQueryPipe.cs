using System.Threading.Tasks;
using Iql.Queryable.Data.Lists;
using Iql.Queryable.Events;

namespace Iql.Queryable.Data.QueryContainer
{
    public interface IQueryPipe
    {
        bool DisableAutoEvents { get; set; }
        bool ResultsLoading { get; set; }

        IDbQueryable Query { get; set; }
        IDbList Results { get; set; }

        IEventSubscriber<IQueryPipeChangedEvent> QueryChanged { get; }
        IEventSubscriber<IQueryPipeChangedEvent> ResultsChanged { get; }
        IEventSubscriber<IQueryPipeChangedEvent> ResultsLoadingChanged { get; }
        IEventSubscriber<IQueryPipeChangedEvent> Pipe { get; }

        void NotifyQueryableChanged();
        void NotifyResultsChanged();
        void NotifyResultsLoadingChanged();

        Task RefreshResultsAsync();

        void Dispose();
    }
}