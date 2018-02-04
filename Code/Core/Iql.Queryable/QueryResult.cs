using System.Collections;
using System.Collections.Generic;
using Iql.Queryable.Operations.Applicators;

namespace Iql.Queryable
{
    public abstract class QueryResult<T, TQueryResult> : IQueryResult<T>
        where TQueryResult : IQueryResultBase
    {
        public TQueryResult ParentResult { get; set; }
        IQueryResultBase IQueryResultBase.ParentResult
        {
            get => ParentResult;
            set => ParentResult = (TQueryResult) value;
        }

        public IQueryOperationContextBase Context { get; set; }
        public abstract List<T> ToList();

        IList IQueryResultBase.ToList()
        {
            return ToList();
        }
    }
}