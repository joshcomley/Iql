using Iql.Parsing;
using Iql.Parsing.Extensions;

namespace Iql.Queryable.Operations
{
    public class QueryOperation : IQueryOperation
    {
        private static readonly string GuidKey = "cae13e2b-9580-4050-b956-ee38eb5c2c42";
        private string Guid { get; } = GuidKey;

        public EvaluateContext EvaluateContext { get; set; }

        public static bool IsQueryOperation(object obj)
        {
            return obj.HasPropertyValue(nameof(Guid), GuidKey);
        }
    }


    //    public interface IQueryableAdapterBase<TQueryData, TQueryResult>
    //        where TQueryResult : IQueryResultBase
    //    {
    //        //    generateQuery(): TQueryResult;
    //        DataContext<TQueryResult, IQueryableAdapterBase<TQueryData, TQueryResult>, any> context;
    //        toQueryResult<TEntity>(queryable: Queryable<TEntity>, data: TQueryData): TQueryResult;
    //        newQueryData<TEntity>(queryable: Queryable<TEntity>): TQueryData;
    //        resolveApplicator<TEntity, TOperation extends IQueryOperationBase, TQueryResult>(operation: TOperation): IQueryOperationApplicator<TEntity, TOperation, TQueryResult>;
    //        registerApplicator<TOperation>(ctor: { new <T>(): TOperation
    //        }, resolve: <T, TQueryResult>() => IQueryOperationApplicator<T, TOperation, TQueryResult>): void;
    //    begin(dataContext: DataContext<any, any, any>);
    //}
}