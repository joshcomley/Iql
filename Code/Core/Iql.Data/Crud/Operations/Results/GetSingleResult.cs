using Iql.Data.Tracking.State;

namespace Iql.Data.Crud.Operations.Results
{
    public class GetSingleResult<T> : DataResult<T, T>, IGetSingleResult
        where T : class
    {
        public EntityState<T> EntityState { get; }

        object IGetSingleResult.Entity => Data;

        public GetSingleResult(T data, EntityState<T> entityState, GetDataOperation<T> operation, bool success) 
            : base(data, operation, success)
        {
            EntityState = entityState;
        }

        IEntityStateBase IGetSingleResult.EntityState => EntityState;
    }
}