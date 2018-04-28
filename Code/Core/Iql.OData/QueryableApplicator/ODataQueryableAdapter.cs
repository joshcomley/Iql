using System;
using Iql.OData.IqlToODataExpression;
using Iql.OData.QueryableApplicator.Applicators;
#if TypeScript
using Iql.Parsing;
#endif
using Iql.Queryable;
using Iql.Queryable.Data.EntityConfiguration;
using Iql.Queryable.Data.Queryable;
using Iql.Queryable.Operations;

namespace Iql.OData.QueryableApplicator
{
    public class ODataQueryableAdapter : QueryableAdapter<IODataQuery, ODataQueryableAdapter>
    {
        public ODataQueryableAdapter()
        {
            // The order of these is important
            RegisterApplicator(() => new OrderByOperationApplicatorOData());
            RegisterApplicator(() => new ReverseOperationApplicatorOData());
            RegisterApplicator(() => new WhereOperationApplicatorOData());
            //RegisterApplicator(() => new ExpandCountOperationApplicatorOData());
            RegisterApplicator(() => new ExpandOperationApplicatorOData());
            RegisterApplicator(() => new IncludeCountApplicatorOData());
            RegisterApplicator(() => new WithKeyOperationApplicatorOData());
            RegisterApplicator(() => new SkipOperationApplicatorOData());
            RegisterApplicator(() => new TakeOperationApplicatorOData());
            //new QueryParser().parseRoot(queryFilter, new QueryExpressionAdapterOData())({})
        }

        public static string GetExpression(
            IqlExpression expression,
            IEntityConfigurationBuilder entityConfigurationContext,
            Type rootEntityType
#if TypeScript
            , EvaluateContext evaluateContext
#endif
        )
        {
            return new ODataIqlParserInstance(
                    new ODataIqlExpressionAdapter(),
                    rootEntityType)
                .Parse(expression
#if TypeScript
                , evaluateContext
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