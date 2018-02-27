using Iql.OData.Parsers;
using Iql.OData.Queryable.Applicators;
using Iql.Queryable;
using Iql.Queryable.Data.EntityConfiguration;
using Iql.Queryable.Operations;

namespace Iql.OData.Queryable
{
    public class ODataQueryableAdapter : QueryableAdapter<IODataQuery, ODataQueryableAdapter>
    {
        public ODataQueryableAdapter()
        {
            // The order of these is important
            RegisterApplicator(() => new OrderByOperationApplicatorOData());
            RegisterApplicator(() => new ReverseOperationApplicatorOData());
            RegisterApplicator(() => new WhereOperationApplicatorOData());
            RegisterApplicator(() => new ExpandCountOperationApplicatorOData());
            RegisterApplicator(() => new ExpandOperationApplicatorOData());
            RegisterApplicator(() => new IncludeCountApplicatorOData());
            RegisterApplicator(() => new WithKeyOperationApplicatorOData());
            RegisterApplicator(() => new SkipOperationApplicatorOData());
            RegisterApplicator(() => new TakeOperationApplicatorOData());
            //new QueryParser().parseRoot(queryFilter, new QueryExpressionAdapterOData())({})
        }

        public static string GetExpression(
            IExpressionQueryOperation operation,
            EntityConfigurationBuilder entityConfigurationContext
        )
        {
            return new ODataIqlParserInstance(
                    new ODataIqlExpressionAdapter())
                .Parse(operation.Expression
#if TypeScript
                , operation.EvaluateContext
#endif
                ).ToCodeString();
        }

        // public generateQuery(): ODataQuery<T> {
        //     throw new Exception("Not implemented.");
        // }
        public override IODataQuery ToQueryResult<TEntity>(IQueryable<TEntity> queryable, IODataQuery data)
        {
            return data;
        }

        public override IODataQuery NewQueryData<TEntity>(IQueryable<TEntity> queryable)
        {
            return new ODataQuery<TEntity>(queryable, Context);
        }
    }
}