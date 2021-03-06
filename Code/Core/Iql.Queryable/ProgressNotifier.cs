using Iql.Events;

namespace Iql.Queryable
{
    public class ProgressNotifier
    {
        private EventEmitter<ProgressNotifierEvent> _onProgress;
        public EventEmitter<ProgressNotifierEvent> OnProgress => _onProgress = _onProgress ?? new EventEmitter<ProgressNotifierEvent>();
        public void NotifyProgress(double progress, bool isFinalNotification)
        {
            CurrentProgress = progress;
            _onProgress.EmitIfExists(() => new ProgressNotifierEvent(progress, isFinalNotification));
        }

        public double CurrentProgress { get; private set; }
    }
}
