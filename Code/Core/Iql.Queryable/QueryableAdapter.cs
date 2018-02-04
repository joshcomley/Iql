using System;
using System.Collections.Generic;
using Iql.Queryable.Data;
using Iql.Queryable.Operations;
using Iql.Queryable.Operations.Applicators;

namespace Iql.Queryable
{
    public abstract class QueryableAdapter<TQueryResult, TQueryAdapter>
        : IQueryableAdapter<TQueryResult, TQueryAdapter>
        where TQueryResult : IQueryResultBase
        where TQueryAdapter : IQueryableAdapter<TQueryResult, TQueryAdapter>
    {
        private readonly Dictionary<Type, Func<IQueryOperationApplicatorBase>> _applicators =
            new Dictionary<Type, Func<IQueryOperationApplicatorBase>>();

        //DataContext<TQueryResult, QueryableAdapter<TQueryData, TQueryResult>, any> context;
        //DataContext<TQueryResult, QueryableAdapter<TQueryData, TQueryResult>> context;
        IQueryResultBase IQueryableAdapterBase.NewQueryData<TEntity>(IQueryable<TEntity> queryable)
        {
            return NewQueryData(queryable);
        }

        public IDataContext Context { get; set; }

        public void Begin(IDataContext dataContext)
        {
        }
        // public generateQuery(): TQueryResult {
        //     throw new Error("Method not implemented.");
        // }

        public abstract TQueryResult ToQueryResult<TEntity>(IQueryable<TEntity> queryable, TQueryResult data)
            where TEntity : class;

        public abstract TQueryResult NewQueryData<TEntity>(IQueryable<TEntity> queryable) where TEntity : class;

        public virtual IQueryOperationApplicatorBase ResolveApplicator<TOperation>(TOperation operation)
            where TOperation : IQueryOperation
        {
            var type = operation.GetType();
            return ResolveApplicatorInternal(type);
        }

        private IQueryOperationApplicatorBase ResolveApplicatorInternal(Type type)
        {
            foreach (var applicator in _applicators)
            {
                if (applicator.Key.IsAssignableFrom(type))
                {
                    return applicator.Value();
                }
            }
            return null;
        }

        //public void registerApplicator<TOperation>(Func<IQueryOperationApplicator<TOperation, TQueryResult>> resolve) where TOperation : IQueryOperationBase
        public virtual void RegisterApplicator<TOperation>(
            Func<IQueryOperationApplicator<TOperation, TQueryResult, TQueryAdapter>> resolve)
            where TOperation : IQueryOperation
        {
            _applicators[typeof(TOperation)] = resolve;
        }
    }
}