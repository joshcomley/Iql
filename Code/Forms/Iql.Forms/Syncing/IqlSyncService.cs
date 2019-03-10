using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Iql.Data.Lists;
using Iql.Events;

namespace Iql.Forms.Syncing
{
    public class IqlSyncService
    {
        private static int _syncCount;
        public string Status { get; set; }
        public EventEmitter<IqlSyncStartEvent> OnStart { get; } = new EventEmitter<IqlSyncStartEvent>();
        public EventEmitter<IqlSyncCompleteEvent> OnComplete { get; } = new EventEmitter<IqlSyncCompleteEvent>();
        public EventEmitter<IqlSyncSetCompleteEvent> OnSetComplete { get; } = new EventEmitter<IqlSyncSetCompleteEvent>();
        public EventEmitter<IqlSyncStatusChangeEvent> OnStatusChange { get; } = new EventEmitter<IqlSyncStatusChangeEvent>();
        public EventEmitter<IqlSyncProgressEvent> OnProgress { get; } = new EventEmitter<IqlSyncProgressEvent>();
        public static EventEmitter<IqlIsSyncingChangeEvent> GlobalIsSyncingChanged { get; } = new EventEmitter<IqlIsSyncingChangeEvent>();
        public static EventEmitter<IqlSyncStartEvent> GlobalOnStart { get; } = new EventEmitter<IqlSyncStartEvent>();
        public static EventEmitter<IqlSyncCompleteEvent> GlobalOnComplete { get; } = new EventEmitter<IqlSyncCompleteEvent>();
        public static EventEmitter<IqlSyncSetCompleteEvent> GlobalOnSetComplete { get; } = new EventEmitter<IqlSyncSetCompleteEvent>();
        public static EventEmitter<IqlSyncStatusChangeEvent> GlobalOnStatusChange { get; } = new EventEmitter<IqlSyncStatusChangeEvent>();
        public static EventEmitter<IqlSyncProgressEvent> GlobalOnProgress { get; } = new EventEmitter<IqlSyncProgressEvent>();

        private static int SyncCount
        {
            get => _syncCount;
            set
            {
                var oldIsAnySyncing = IsAnySyncing;
                _syncCount = value;
                var newIsAnySyncing = IsAnySyncing;
                if (oldIsAnySyncing != newIsAnySyncing)
                {
                    GlobalIsSyncingChanged.Emit(() => new IqlIsSyncingChangeEvent(newIsAnySyncing));
                }
            }
        }

        public static bool IsAnySyncing => SyncCount > 0;
        public bool IsSyncing { get; protected set; }

        public virtual async Task<IqlSyncResult> SyncAsync(IEnumerable<IDbQueryable> sets)
        {
            if (IsSyncing)
            {
                return null;
            }
            IsSyncing = true;
            SyncCount++;
            var setsArr = sets.ToArray();
            OnStart.Emit(() => new IqlSyncStartEvent(this, setsArr));
            GlobalOnStart.Emit(() => new IqlSyncStartEvent(this, setsArr));
            var startTime = DateTimeOffset.Now;
            var success = true;
            for (var i = 0; i < setsArr.Length; i++)
            {
                Status = $"Syncing {setsArr[i].EntityConfiguration.SetFriendlyName}...";
                OnStatusChange.Emit(() => new IqlSyncStatusChangeEvent(this, Status));
                GlobalOnStatusChange.Emit(() => new IqlSyncStatusChangeEvent(this, Status));
                var data = await setsArr[i].AllPagesToListAsync();
                if (!data.Success)
                {
                    success = false;
                    break;
                }

                var progressEvent = new IqlSyncProgressEvent(this, (i + 1) / setsArr.Length, i + 1 == setsArr.Length);
                var setCompleteEvent = new IqlSyncSetCompleteEvent(this, setsArr[i].EntityConfiguration, data.Count);
                OnProgress.Emit(() => progressEvent);
                GlobalOnProgress.Emit(() => progressEvent);
                OnSetComplete.Emit(() => setCompleteEvent);
                GlobalOnSetComplete.Emit(() => setCompleteEvent);
            }
            Status = success ? "Syncing complete" : "Syncing failed";
            OnStatusChange.Emit(() => new IqlSyncStatusChangeEvent(this, Status));
            GlobalOnStatusChange.Emit(() => new IqlSyncStatusChangeEvent(this, Status));
            var endTime = DateTimeOffset.Now;
            var dif = startTime.Ticks - endTime.Ticks;
            var timeTakenInSeconds = Math.Abs(dif / 1000);
            IsSyncing = false;
            SyncCount--;
            OnComplete.Emit(() => new IqlSyncCompleteEvent(this, setsArr));
            GlobalOnComplete.Emit(() => new IqlSyncCompleteEvent(this, setsArr));
            return new IqlSyncResult(success, timeTakenInSeconds);
        }
    }
}