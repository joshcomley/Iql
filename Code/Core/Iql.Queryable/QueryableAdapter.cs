using System;
using System.Collections.Generic;
using Iql.Queryable.Data;
using Iql.Queryable.Operations;
using Iql.Queryable.Operations.Applicators;

namespace Iql.Queryable
{
    public abstract class QueryableAdapter<TQueryResult>
        : IQueryableAdapter<TQueryResult>
        where TQueryResult : IQueryResultBase
    {
        private readonly Dictionary<string, Func<IQueryOperationApplicatorBase>> _applicators =
            new Dictionary<string, Func<IQueryOperationApplicatorBase>>();

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
            var name = type.Name;
            return ResolveApplicatorInternal(name);
        }

        private IQueryOperationApplicatorBase ResolveApplicatorInternal(string name)
        {
            while (true)
            {
                var index = name.IndexOf("`", StringComparison.Ordinal);
                if (index != -1)
                {
                    name = name.Substring(0, index);
                }
                if (!_applicators.ContainsKey(name))
                {
                    if (name.StartsWith("I"))
                    {
                        return null;
                    }
                    // This is a big hack until we support interfaces in TypeScript
                    name = "I" + name;
                    continue;
                }
                var pre = _applicators[name]();
                return pre;
            }
        }

        //public void registerApplicator<TOperation>(Func<IQueryOperationApplicator<TOperation, TQueryResult>> resolve) where TOperation : IQueryOperationBase
        public virtual void RegisterApplicator<TOperation>(
            Func<IQueryOperationApplicator<TOperation, TQueryResult>> resolve)
            where TOperation : IQueryOperation
        {
            var name = typeof(TOperation).Name;
            _applicators[name] = resolve;
        }
    }
}