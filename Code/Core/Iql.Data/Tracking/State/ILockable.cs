namespace Iql.Data.Tracking.State
{
    public interface ILockable
    {
        ILockable Parent { get; }
        bool IsLocked { get; }
        void Lock();
        void Unlock();
    }
}