using Iql.Queryable;
using Iql.Queryable.Operations;

namespace Iql.OData.Queryable
{
    public interface IODataQuery : IQueryResultBase
    {
        bool HasKey { get; set; }
        CompositeKey Key { get; set; }
        void AddQueryPart(ODataQueryPart queryPart, string part);
        string ToODataQuery(bool isNested = false);
    }
}