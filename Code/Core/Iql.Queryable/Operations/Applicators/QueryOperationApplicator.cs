namespace Iql.Queryable.Operations.Applicators
{
    public abstract class QueryOperationApplicator<TOperation, TQueryResult>
        : IQueryOperationApplicator<TOperation, TQueryResult>
        where TOperation : IQueryOperation
        where TQueryResult : IQueryResultBase
    {
        //public void apply(QueryOperationContext<TEntity> context)
        //{
        //}

        // public resolveExpression(){
        //     let result = new QueryParser().parseRoot<TEntity, ODataParseResult, TApplicator, TQueryResult>(operation.getFilter(), new QueryExpressionAdapterOData<T>());
        //     let filter = result.expression(<TEntity>{});
        // }
        public abstract void Apply<TEntity>(IQueryOperationContext<TOperation, TEntity, TQueryResult> context)
            where TEntity : class;
    }
}