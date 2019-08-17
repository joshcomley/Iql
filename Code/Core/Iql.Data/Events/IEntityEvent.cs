using Iql.Data.Tracking.State;

namespace Iql.Data.Events
{
    public interface IEntityEvent<T> : IEntityEventBase
        where T : class
    {
        new IEntityState<T> Entity { get; }
    }
}