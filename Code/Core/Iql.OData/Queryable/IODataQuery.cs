using System.Collections.Generic;
using Iql.Queryable;
using Iql.Queryable.Operations;

namespace Iql.OData.Queryable
{
    public interface IODataQuery : IQueryResultBase
    {
        bool HasKey { get; set; }
        object Key { get; set; }
        void AddQueryPart(ODataQueryPart queryPart, string part);
        string ToODataQuery();
    }

    public enum ODataQueryPart
    {
        Filter,
        Expand,
        OrderBy
    }
}