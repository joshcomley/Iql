using System.Collections;
using Iql.Queryable.Operations.Applicators;

namespace Iql.Queryable
{
    public interface IQueryResultBase
    {
        IQueryResultBase ParentResult { get; set; }
        IQueryOperationContextBase Context { get; set; }
        IList ToList();
    }
}