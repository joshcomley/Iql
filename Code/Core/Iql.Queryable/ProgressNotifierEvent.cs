namespace Iql.Queryable
{
    public class ProgressNotifierEvent
    {
        public bool IsFinalNotification { get; }
        public double Progress { get; }
        public bool IsFailure => IsFinalNotification && Progress != 1;

        public ProgressNotifierEvent(double progress, bool isFinalNotification)
        {
            Progress = progress;
            IsFinalNotification = isFinalNotification;
        }
    }
}