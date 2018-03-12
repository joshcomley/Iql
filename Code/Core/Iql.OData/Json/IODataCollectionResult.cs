using System.Collections;

namespace Iql.OData.Json
{
    public interface IODataCollectionResult
    {
        int? TotalCount { get; set; }
        IEnumerable Items { get; set; }
    }
}