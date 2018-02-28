using Iql.Queryable.Data.Context;
using Iql.Queryable.Operations;
using Iql.Queryable.QueryApplicator.Applicators;

namespace Iql.Queryable.Data.Queryable
{
    public interface IQueryableAdapterBase
    {
        IDataContext Context { get; set; }
        IQueryResultBase NewQueryData<TEntity>(IQueryable<TEntity> queryable) where TEntity : class;

        IQueryOperationApplicatorBase ResolveApplicator<TOperation>(TOperation operation)
            where TOperation : IQueryOperation;

        void Begin(IDataContext dataContext);
    }
}