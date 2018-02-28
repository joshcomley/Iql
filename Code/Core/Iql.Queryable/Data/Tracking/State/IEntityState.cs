namespace Iql.Queryable.Data.Tracking.State
{
    public interface IEntityState<out T> : IEntityStateBase
    {
        new T Entity { get; }
    }
}