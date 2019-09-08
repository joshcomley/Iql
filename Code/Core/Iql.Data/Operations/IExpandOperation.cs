using System.Collections.Generic;
using Iql.Queryable;
using Iql.Queryable.Operations;

namespace Iql.Data.Operations
{
    public interface IExpandOperation : IExpressionQueryOperation
    {
        bool CountOnly { get; set; }
        // List<ExpandDetail> ExpandDetails { get; set; }
        // IQueryableBase ApplyQuery(IQueryableBase queryable);
    }
}