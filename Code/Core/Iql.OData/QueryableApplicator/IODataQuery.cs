using Iql.Queryable;
using Iql.Queryable.Data;
using Iql.Queryable.Data.EntityConfiguration;
using Iql.Queryable.Data.Queryable;
using Iql.Queryable.Operations;

namespace Iql.OData.QueryableApplicator
{
    public interface IODataQuery : IQueryResultBase
    {
        bool IncludeCount { get; set; }
        bool HasKey { get; set; }
        CompositeKey Key { get; set; }
        int TotalSkip { get; set; }
        int TotalTake { get; set; }
        void AddQueryPart(ODataQueryPart queryPart, string part);
        void SetQueryPart(ODataQueryPart queryPart, string part);
        string ToODataQuery(bool isNested = false);
    }
}