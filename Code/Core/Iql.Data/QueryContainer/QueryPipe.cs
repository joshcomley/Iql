using System;
using System.Threading.Tasks;
using Iql.Queryable.Data.Lists;
using Iql.Queryable.Data.Queryable;
using Iql.Queryable.Events;

namespace Iql.Queryable.Data.QueryContainer
{
    public class QueryPipe<T> : IDisposable, IQueryPipe where T : class
    {
        private DbQueryable<T> _sourceQuery;

        public QueryPipe(DbQueryable<T> sourceQuery)
        {
            SourceQuery = sourceQuery;
        }

        public EventEmitter<QueryPipeChangedEvent<T>> SourceQueryChanged { get; } =
            new EventEmitter<QueryPipeChangedEvent<T>>();

        public AsyncEventEmitter<QueryPipeChangedEvent<T>> ResultsLoaded { get; } =
            new AsyncEventEmitter<QueryPipeChangedEvent<T>>();

        public AsyncEventEmitter<QueryPipeChangedEvent<T>> ResultsLoadingChanged { get; } =
            new AsyncEventEmitter<QueryPipeChangedEvent<T>>();

        public AsyncEventEmitter<QueryPipeEvent<T>> Pipe { get; } =
            new AsyncEventEmitter<QueryPipeEvent<T>>();

        public AsyncEventEmitter<QueryPipeInspectorEvent<T>> QueryBuilt { get; } =
            new AsyncEventEmitter<QueryPipeInspectorEvent<T>>();

        public AsyncEventEmitter<QueryPipeChangedEvent<T>> QueryBuildingChanged { get; } =
            new AsyncEventEmitter<QueryPipeChangedEvent<T>>();

        public DbQueryable<T> SourceQuery
        {
            get => _sourceQuery;
            private set
            {
                var hasChanged = value != _sourceQuery;
                _sourceQuery = value;
                if (hasChanged)
                {
                    EmitEvent(SourceQueryChanged);
                }
            }
        }

        public DbList<T> Results { get; private set; }

        public void Dispose()
        {
            SourceQueryChanged.UnsubscribeAll();
            ResultsLoaded.UnsubscribeAll();
            ResultsLoadingChanged.UnsubscribeAll();
            Pipe.UnsubscribeAll();
            QueryBuilt.UnsubscribeAll();
            QueryBuildingChanged.UnsubscribeAll();
        }

        IEventSubscriber<IQueryPipeChangedEvent> IQueryPipe.SourceQueryChanged => SourceQueryChanged;
        IAsyncEventSubscriber<IQueryPipeChangedEvent> IQueryPipe.ResultsLoadingChanged => ResultsLoadingChanged;
        IAsyncEventSubscriber<IQueryPipeChangedEvent> IQueryPipe.ResultsLoaded => ResultsLoaded;
        IAsyncEventSubscriber<IQueryPipeEvent> IQueryPipe.Pipe => Pipe;
        IAsyncEventSubscriber<IQueryPipeChangedEvent> IQueryPipe.QueryBuildingChanged => QueryBuildingChanged;
        IAsyncEventSubscriber<IQueryPipeInspectorEvent> IQueryPipe.QueryBuilt => QueryBuilt;

        public bool DisableAutoEvents { get; set; }

        IDbQueryable IQueryPipe.SourceQuery
        {
            get => SourceQuery;
            set => SourceQuery = (DbQueryable<T>) value;
        }

        IDbList IQueryPipe.Results => Results;

        public async Task RefreshResultsAsync()
        {
            // Build the query
            QueryBuilding = true;
            await QueryBuildingChanged.EmitAsync(() => new QueryPipeChangedEvent<T>(this));
            var pipe = new QueryPipeEvent<T>(this);
            await Pipe.EmitAsync(() => pipe);
            QueryBuilding = false;
            await QueryBuildingChanged.EmitAsync(() => new QueryPipeChangedEvent<T>(this));

            // Broadcast the final query
            await QueryBuilt.EmitAsync(() => new QueryPipeInspectorEvent<T>(pipe.Query));

            // Load the results
            ResultsLoading = true;
            await EmitEventAsync(ResultsLoadingChanged);
            Results = await pipe.Query.ToListAsync();
            ResultsLoading = false;
            await EmitEventAsync(ResultsLoadingChanged);

            // Broadcast the final results
            await ResultsLoaded.EmitAsync(() => new QueryPipeChangedEvent<T>(this));
        }

        public bool ResultsLoading { get; private set; }
        public bool QueryBuilding { get; private set; }

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