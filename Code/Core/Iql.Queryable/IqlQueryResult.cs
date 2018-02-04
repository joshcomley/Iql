using System;
using System.Collections;
using Iql.Queryable.Operations.Applicators;

namespace Iql.Queryable
{
    public class IqlQueryResult : IQueryResultBase
    {
        //public operations = new Array<IqlExpression>();

        //public virtual List<T> ToList()
        //{
        //}

        public IQueryResultBase ParentResult { get; set; }
        public IQueryOperationContextBase Context { get; set; }

        IList IQueryResultBase.ToList()
        {
            throw new NotImplementedException();
            //return ToList();
        }
    }
}