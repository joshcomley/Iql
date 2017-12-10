namespace Iql.Queryable.Data.Crud.State
{
    public interface IEntityState<out T> : IEntityStateBase
    {
        new T Entity { get; }
    }
}