using Iql.Queryable.Data.Queryable;
using Iql.Queryable.QueryApplicator.Applicators;

namespace Iql.Queryable.Data.DataStores.InMemory.QueryApplicator
{
    public class IqlQueryResult : IQueryResultBase
    {
        //public operations = new Array<IqlExpression>();

        //public virtual List<T> ToList()
        //{
        //}

        public IQueryResultBase ParentResult { get; set; }
        public IQueryOperationContextBase Context { get; set; }
    }
}