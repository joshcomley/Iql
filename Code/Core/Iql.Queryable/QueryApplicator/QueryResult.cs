using Iql.Queryable.Data.Queryable;
using Iql.Queryable.QueryApplicator.Applicators;

namespace Iql.Queryable.QueryApplicator
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