using Iql.OData.Parsers;
using Iql.OData.Queryable.Applicators;
using Iql.Parsing;
using Iql.Queryable;
using Iql.Queryable.Data.EntityConfiguration;
using Iql.Queryable.Operations;

namespace Iql.OData.Queryable
{
    public class ODataQueryableAdapter : QueryableAdapter<IODataQuery>
    {
        public ODataQueryableAdapter()
        {
            RegisterApplicator(() => new OrderByOperationApplicatorOData());
            RegisterApplicator(() => new ReverseOperationApplicatorOData());
            RegisterApplicator(() => new WhereOperationApplicatorOData());
            RegisterApplicator(() => new ExpandOperationApplicatorOData());
            RegisterApplicator(() => new WithKeyOperationApplicatorOData());
            //new QueryParser().parseRoot(queryFilter, new QueryExpressionAdapterOData())({})
        }

        public static string GetExpression(
            IExpressionQueryOperation operation,
            EntityConfigurationBuilder entityConfigurationContext
        )
        {
            return new ActionParserInstance<ODataIqlData, ODataIqlExpressionAdapter>(
                    new ODataIqlExpressionAdapter())
                .Parse(operation.Expression, operation.EvaluateContext);
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