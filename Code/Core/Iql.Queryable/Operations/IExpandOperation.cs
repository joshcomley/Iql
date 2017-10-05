using System;
using System.Collections.Generic;

namespace Iql.Queryable.Operations
{
    public interface IExpandOperation : IExpressionQueryOperation
    {
        List<ExpandDetail> ExpandDetails { get; set; }
        IQueryableBase ApplyQuery(IQueryableBase queryable);
    }
}