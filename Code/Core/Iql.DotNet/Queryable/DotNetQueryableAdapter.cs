using Iql.DotNet.Queryable.Applicators;
using Iql.Queryable;

namespace Iql.DotNet.Queryable
{
    public class DotNetQueryableAdapter : QueryableAdapter<IDotNetQueryResult>
    {
        public DotNetQueryableAdapter()
        {
            RegisterApplicator(() => new DotNetOrderByOperationApplicator());
            //this.RegisterApplicator(() => new ReverseOperationApplicatorJavaScript());
            //this.RegisterApplicator(() => new WhereOperationApplicatorJavaScript());
            //this.RegisterApplicator(() => new ExpandOperationApplicatorJavaScript());
            //new QueryParser().parseRoot(queryFilter, new QueryExpressionAdapterJavaScript())({})
        }

        public override IDotNetQueryResult ToQueryResult<TEntity>(IQueryable<TEntity> queryable,
            IDotNetQueryResult data)
        {
            return data;
        }

        public override IDotNetQueryResult NewQueryData<TEntity>(IQueryable<TEntity> queryable)
        {
            return new DotNetQuery<TEntity>(Context);
        }
    }
}