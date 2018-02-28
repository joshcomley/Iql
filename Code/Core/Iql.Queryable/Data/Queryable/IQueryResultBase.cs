using Iql.Queryable.QueryApplicator.Applicators;

namespace Iql.Queryable.Data.Queryable
{
    public interface IQueryResultBase
    {
        IQueryResultBase ParentResult { get; set; }
        IQueryOperationContextBase Context { get; set; }
    }
}