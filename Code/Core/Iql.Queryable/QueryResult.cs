using Iql.Queryable.Operations.Applicators;

namespace Iql.Queryable
{
    public abstract class QueryResult<TQueryResult> : IQueryResultBase
        where TQueryResult : IQueryResultBase
    {
        public TQueryResult ParentResult { get; set; }
        IQueryResultBase IQueryResultBase.ParentResult
        {
            get => ParentResult;
            set => ParentResult = (TQueryResult) value;
        }

        public IQueryOperationContextBase Context { get; set; }
    }
}