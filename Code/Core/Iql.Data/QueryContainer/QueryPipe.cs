using System;
using System.Threading.Tasks;
using Iql.Data.Context;
using Iql.Data.Lists;
using Iql.Events;
using Newtonsoft.Json;

namespace Iql.Data.QueryContainer
{
    public class QueryPipe<T> : IDisposable, IQueryPipe where T : class
    {
        private DbQueryable<T> _sourceQuery;
        private string _lastIql;

        public QueryPipe(DbQueryable<T> sourceQuery)
        {
            SourceQuery = sourceQuery;
        }
        private EventEmitter<QueryPipeChangedEvent<T>> _sourceQueryChanged;

        public EventEmitter<QueryPipeChangedEvent<T>> SourceQueryChanged => _sourceQueryChanged = _sourceQueryChanged ?? new EventEmitter<QueryPipeChangedEvent<T>>();
        private AsyncEventEmitter<QueryPipeChangedEvent<T>> _resultsLoaded;

        public AsyncEventEmitter<QueryPipeChangedEvent<T>> ResultsLoaded => _resultsLoaded = _resultsLoaded ?? new AsyncEventEmitter<QueryPipeChangedEvent<T>>();
        private AsyncEventEmitter<QueryPipeChangedEvent<T>> _resultsLoadingChanged;

        public AsyncEventEmitter<QueryPipeChangedEvent<T>> ResultsLoadingChanged => _resultsLoadingChanged = _resultsLoadingChanged ?? new AsyncEventEmitter<QueryPipeChangedEvent<T>>();
        private AsyncEventEmitter<QueryPipeEvent<T>> _pipe;

        public AsyncEventEmitter<QueryPipeEvent<T>> Pipe => _pipe = _pipe ?? new AsyncEventEmitter<QueryPipeEvent<T>>();
        private AsyncEventEmitter<QueryPipeInspectorEvent<T>> _queryBuilt;

        public AsyncEventEmitter<QueryPipeInspectorEvent<T>> QueryBuilt => _queryBuilt = _queryBuilt ?? new AsyncEventEmitter<QueryPipeInspectorEvent<T>>();
        private AsyncEventEmitter<QueryPipeChangedEvent<T>> _queryBuildingChanged;

        public AsyncEventEmitter<QueryPipeChangedEvent<T>> QueryBuildingChanged => _queryBuildingChanged = _queryBuildingChanged ?? new AsyncEventEmitter<QueryPipeChangedEvent<T>>();

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
            set => SourceQuery = (DbQueryable<T>)value;
        }

        IDbList IQueryPipe.Results => Results;

        private string _lastReloadId = null;
        /// <summary>
        /// Refreshed the results, asynchronously.
        /// </summary>
        /// <param name="force">Force a request, even if the query has not changed since the last refresh.</param>
        /// <returns></returns>
        public async Task<bool> RefreshResultsAsync(bool force = false)
        {
            _lastReloadId = Guid.NewGuid().ToString();
            var lastReloadId = _lastReloadId;
            // Build the query
            QueryBuilding = true;
            await _queryBuildingChanged.EmitIfExistsAsync(() => new QueryPipeChangedEvent<T>(this));
            if (lastReloadId != _lastReloadId)
            {
                return false;
            }
            var pipe = new QueryPipeEvent<T>(this);
            await _pipe.EmitIfExistsAsync(() => pipe);
            if (lastReloadId != _lastReloadId)
            {
                return false;
            }
            QueryBuilding = false;
            await _queryBuildingChanged.EmitIfExistsAsync(() => new QueryPipeChangedEvent<T>(this));
            if (lastReloadId != _lastReloadId)
            {
                return false;
            }

            // Broadcast the final query
            await _queryBuilt.EmitIfExistsAsync(() => new QueryPipeInspectorEvent<T>(pipe.Query));
            if (lastReloadId != _lastReloadId)
            {
                return false;
            }

            // Load the results
            var iql = await pipe.Query.ToIqlAsync();
            if (lastReloadId != _lastReloadId)
            {
                return false;
            }
            var iqljson = JsonConvert.SerializeObject(iql);
            var canUpdate = force || iqljson != _lastIql;
            if (canUpdate)
            {
                ResultsLoading = true;
                await EmitEventAsync(ResultsLoadingChanged);
                if (lastReloadId != _lastReloadId)
                {
                    return false;
                }
                _lastIql = iqljson;
                Results = await pipe.Query.ToListAsync();
                if (lastReloadId != _lastReloadId)
                {
                    return false;
                }
                ResultsLoading = false;
                await EmitEventAsync(ResultsLoadingChanged);
                if (lastReloadId != _lastReloadId)
                {
                    return false;
                }

                // Broadcast the final results
                await _resultsLoaded.EmitIfExistsAsync(() => new QueryPipeChangedEvent<T>(this));
            }

            return canUpdate;
        }

        public bool ResultsLoading { get; private set; }
        public bool QueryBuilding { get; private set; }

        private void EmitEvent(EventEmitter<QueryPipeChangedEvent<T>> emitter)
        {
            emitter.EmitIfExists(() => new QueryPipeChangedEvent<T>(this));
        }

        private async Task EmitEventAsync(AsyncEventEmitter<QueryPipeChangedEvent<T>> emitter)
        {
            await emitter.EmitIfExistsAsync(() => new QueryPipeChangedEvent<T>(this));
        }
    }
}
