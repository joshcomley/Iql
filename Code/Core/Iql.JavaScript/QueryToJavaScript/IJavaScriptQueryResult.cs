using System;
using System.Text;
using Iql.Queryable;

namespace Iql.JavaScript.QueryToJavaScript
{
    public interface IJavaScriptQueryResult : IQueryResultBase
    {
        StringBuilder Query { get; set; }
        string GetDataSetObjectName(Type type);
        bool HasKey { get; set; }
        object Key { get; set; }
        string ToJavaScriptQuery(bool returnValue = true);
        void RegisterType(ExpandEntityType type);
        void AppendWhere(JavaScriptExpression filterExpression);
    }
}