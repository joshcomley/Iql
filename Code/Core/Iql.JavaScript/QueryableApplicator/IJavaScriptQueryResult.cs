using System;
using System.Text;
using Iql.Queryable.Data.DataStores.InMemory;
using Iql.Queryable.Data.EntityConfiguration;

namespace Iql.JavaScript.QueryableApplicator
{
    public interface IJavaScriptQueryResult : IInMemoryResult
    {
        StringBuilder Query { get; set; }
        string GetDataSetObjectName(Type type);
        bool HasKey { get; set; }
        object Key { get; set; }
        string ToJavaScriptQuery(bool returnValue = true);
        void RegisterType(ExpandEntityType type);
        Guid RegisterProperty(IProperty property);
        void AppendWhere(JavaScriptExpression filterExpression);
    }
}