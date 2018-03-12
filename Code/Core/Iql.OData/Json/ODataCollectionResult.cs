using System.Collections;
using System.Collections.Generic;

namespace Iql.OData.Json
{
    public class ODataCollectionResult<T> : IODataCollectionResult
    {
        public int? TotalCount { get; set; }
        public IEnumerable<T> Items { get; set; }

        IEnumerable IODataCollectionResult.Items
        {
            get { return Items; }
            set { Items = (IEnumerable<T>) value; }
        }

        public ODataCollectionResult(IEnumerable<T> items, int? totalCount = null)
        {
            TotalCount = totalCount;
            Items = items;
        }
    }
}