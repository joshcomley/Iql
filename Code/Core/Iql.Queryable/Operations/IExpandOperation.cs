using System;
using System.Collections.Generic;
using Iql.Queryable.Expressions.QueryExpressions;

namespace Iql.Queryable.Operations
{
    public interface IExpandOperation : IExpressionQueryOperation
    {
        List<ExpandDetail> ExpandDetails { get; set; }
        IQueryableBase ApplyQuery(IQueryableBase queryable);
    }
}