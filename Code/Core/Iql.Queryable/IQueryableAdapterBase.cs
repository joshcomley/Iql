using Iql.Queryable.Data;
using Iql.Queryable.Operations;
using Iql.Queryable.Operations.Applicators;

namespace Iql.Queryable
{
    public interface IQueryableAdapterBase
    {
        IDataContext Context { get; set; }
        IQueryResultBase NewQueryData<TEntity>(IQueryable<TEntity> queryable) where TEntity : class, IEntity;

        IQueryOperationApplicatorBase ResolveApplicator<TOperation>(TOperation operation)
            where TOperation : IQueryOperation;

        void Begin(IDataContext dataContext);
    }
}