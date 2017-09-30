using Iql.Queryable;

namespace Iql.OData.Queryable
{
    public interface IODataQuery : IQueryResultBase
    {
        bool HasKey { get; set; }
        object Key { get; set; }
        void AddQueryPart(ODataQueryPart queryPart, string part);
        string ToODataQuery(bool isNested = false);
    }
}