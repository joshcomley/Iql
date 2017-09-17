using System.Collections.Generic;
using Iql.Queryable;
using Iql.Queryable.Operations;

namespace Iql.OData.Queryable
{
    public interface IODataQuery : IQueryResultBase
    {
        List<string> Filters { get; set; }
        List<string> OrderBys { get; set; }
        List<IExpandOperation> Expands { get; set; }
        string ToODataQuery();
    }
}