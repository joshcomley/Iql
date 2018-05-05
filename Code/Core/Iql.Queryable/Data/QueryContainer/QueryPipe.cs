using System;
using System.Threading.Tasks;
using Iql.Queryable.Data.Lists;
using Iql.Queryable.Data.Queryable;
using Iql.Queryable.Events;

namespace Iql.Queryable.Data.QueryContainer
{
    public class QueryPipe<T> : IDisposable, IQueryPipe where T : class
    {
        private DbQueryable<T> _query;
        private DbList<T> _results;
        private bool _resultsLoading;

        public QueryPipe(DbQueryable<T> query)
        {
            Query = query;
        }

        public EventEmitter<QueryPipeChangedEvent<T>> QueryChanged { get; } =
            new EventEmitter<QueryPipeChangedEvent<T>>();

        public EventEmitter<QueryPipeChangedEvent<T>> ResultsChanged { get; } =
            new EventEmitter<QueryPipeChangedEvent<T>>();

        public EventEmitter<QueryPipeChangedEvent<T>> ResultsLoadingChanged { get; } =
            new EventEmitter<QueryPipeChangedEvent<T>>();

        public AsyncEventEmitter<QueryPipeChangedEvent<T>> Pipe { get; } =
            new AsyncEventEmitter<QueryPipeChangedEvent<T>>();

        public AsyncEventEmitter<QueryPipeInspectorEvent<T>> Inspector { get; } =
            new AsyncEventEmitter<QueryPipeInspectorEvent<T>>();

        public DbQueryable<T> Query
        {
            get => _query;
            set
            {
                var hasChanged = value != _query;
                _query = value;
                if (hasChanged && !DisableAutoEvents) NotifyQueryableChanged();
            }
        }

        public DbList<T> Results
        {
            get => _results;
            set
            {
                var hasChanged = value != _results;
                _results = value;
                if (hasChanged && !DisableAutoEvents) NotifyResultsChanged();
            }
        }

        public void Dispose()
        {
            QueryChanged.UnsubscribeAll();
            ResultsChanged.UnsubscribeAll();
            ResultsLoadingChanged.UnsubscribeAll();
            Pipe.UnsubscribeAll();
        }

        IEventSubscriber<IQueryPipeChangedEvent> IQueryPipe.QueryChanged => QueryChanged;
        IEventSubscriber<IQueryPipeChangedEvent> IQueryPipe.ResultsChanged => ResultsChanged;
        IEventSubscriber<IQueryPipeChangedEvent> IQueryPipe.ResultsLoadingChanged => ResultsLoadingChanged;
        IAsyncEventSubscriber<IQueryPipeChangedEvent> IQueryPipe.Pipe => Pipe;
        IAsyncEventSubscriber<IQueryPipeInspectorEvent> IQueryPipe.Inspector => Inspector;

        public bool DisableAutoEvents { get; set; }

        public void NotifyQueryableChanged()
        {
            EmitEvent(QueryChanged);
        }

        public void NotifyResultsChanged()
        {
            EmitEvent(ResultsChanged);
        }

        public void NotifyResultsLoadingChanged()
        {
            EmitEvent(ResultsLoadingChanged);
        }

        IDbQueryable IQueryPipe.Query
        {
            get => Query;
            set => Query = (DbQueryable<T>) value;
        }

        IDbList IQueryPipe.Results
        {
            get => Results;
            set => Results = (DbList<T>) value;
        }

        public async Task RefreshResultsAsync()
        {
            ResultsLoading = true;
            await Pipe.EmitAsync(() => new QueryPipeEvent<T>(this));
            await Inspector.EmitAsync(() => new QueryPipeInspectorEvent<T>(Query));
            Results = await Query.ToListAsync();
            ResultsLoading = false;
        }

        public bool ResultsLoading
        {
            get => _resultsLoading;
            set
            {
                var hasChanged = value != _resultsLoading;
                _resultsLoading = value;
                if (hasChanged && !DisableAutoEvents) NotifyResultsLoadingChanged();
            }
        }

        private void EmitEvent(EventEmitter<QueryPipeChangedEvent<T>> emitter)
        {
            emitter.Emit(() => new QueryPipeChangedEvent<T>(this));
        }

        private async Task EmitEventAsync(AsyncEventEmitter<QueryPipeChangedEvent<T>> emitter)
        {
            await emitter.EmitAsync(() => new QueryPipeChangedEvent<T>(this));
        }
    }
}