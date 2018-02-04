namespace Iql.Queryable.Operations.Applicators
{
    public abstract class QueryOperationApplicator<TOperation, TQueryResult, TQueryAdapter>
        : IQueryOperationApplicator<TOperation, TQueryResult, TQueryAdapter>
        where TOperation : IQueryOperation
        where TQueryResult : IQueryResultBase
        where TQueryAdapter : IQueryableAdapterBase
    {
        //public void apply(QueryOperationContext<TEntity> context)
        //{
        //}

        // public resolveExpression(){
        //     let result = new QueryParser().parseRoot<TEntity, ODataParseResult, TApplicator, TQueryResult>(operation.getFilter(), new QueryExpressionAdapterOData<T>());
        //     let filter = result.expression(<TEntity>{});
        // }
        public abstract void Apply<TEntity>(IQueryOperationContext<TOperation, TEntity, TQueryResult, TQueryAdapter> context)
            where TEntity : class;
    }
}