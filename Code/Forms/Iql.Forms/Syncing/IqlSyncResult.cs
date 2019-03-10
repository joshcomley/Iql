namespace Iql.Forms.Syncing
{

    public class IqlSyncResult
    {
        public bool Success { get; set; }
        public float TimeTakenInSeconds { get; set; }

        public IqlSyncResult(bool success, float timeTakenInSeconds)
        {
            Success = success;
            TimeTakenInSeconds = timeTakenInSeconds;
        }
    }
}