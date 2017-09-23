using System.Collections.Generic;
using Iql.Queryable;

namespace Iql.JavaScript.QueryToJavaScript
{
    public interface IJavaScriptQueryResult : IQueryResultBase
    {
        bool HasKey { get; set; }
        object Key { get; set; }
        List<JavaScriptExpand> Expands { get; set; }
        List<JavaScriptExpression> Filters { get; set; }
        List<JavaScriptOrderByExpression> OrderBys { get; set; }
        string ToJavaScriptQuery(bool returnValue = true);
    }
}