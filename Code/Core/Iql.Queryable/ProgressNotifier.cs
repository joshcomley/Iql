using Iql.Entities.Events;

namespace Iql.Queryable
{
    public class ProgressNotifier
    {
        public EventEmitter<ProgressNotifierEvent> OnProgress { get; } = new EventEmitter<ProgressNotifierEvent>();
        public void NotifyProgress(double progress, bool isFinalNotification)
        {
            CurrentProgress = progress;
            OnProgress.Emit(() => new ProgressNotifierEvent(progress, isFinalNotification));
        }

        public double CurrentProgress { get; private set; }
    }
}