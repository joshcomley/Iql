using System;
using System.Collections;

namespace Iql.Queryable
{
    public class IqlQueryResult : IQueryResultBase
    {
        //public operations = new Array<IqlExpression>();

        //public virtual List<T> ToList()
        //{
        //}

        IList IQueryResultBase.ToList()
        {
            throw new NotImplementedException();
            //return ToList();
        }
    }
}