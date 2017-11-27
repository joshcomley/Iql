using System;
using System.Collections.Generic;
using System.Text;
using Iql.Queryable;
using Iql.Queryable.Data;
using Iql.Queryable.Data.DataStores.InMemory;
using Iql.Queryable.Data.EntityConfiguration.Relationships;
using Iql.Queryable.Data.Tracking;

namespace Iql.JavaScript.QueryToJavaScript
{
    public class JavaScriptQuery<T> : QueryResult<T>, IJavaScriptQueryResult
        where T : class
    {
        public JavaScriptQuery(IQueryable<T> queryable,
            IDataContext context)
        {
            Queryable = queryable;
            Context = context;
        }
        public StringBuilder Query { get; set; }
        public string GetDataSetObjectName(Type type)
        {
            return "dataSet_" + type.Name;
        }

        private IQueryable<T> Queryable { get; }
        public IDataContext Context { get; }

        public bool HasKey { get; set; }
        public object Key { get; set; }
        public List<ExpandEntityType> Types { get; set; } = new List<ExpandEntityType>();

        public string ToJavaScriptQuery(bool returnValue = true)
        {
            var title = "/* " + typeof(T).Name + " */\n";
            var operations = Query.ToString();
            var query = title + ResolveTypes() + operations;
            var varName = GetDataSetObjectName(typeof(T));
            if (!string.IsNullOrWhiteSpace(operations))
            {
                query += varName + " = " + varName + operations.Trim() + ";\n";
            }
            if (returnValue)
            {
                query += "" + varName + ";\n";
            }
            return query + "";
        }

        private string ResolveTypes()
        {
            var typeDefs = "";
            for (var i = 0; i < Types.Count; i++)
            {
                var queryable = Types[i].Queryable;
                if (queryable != null)
                {
                    var js = queryable.ToQueryWithAdapter(new JavaScriptQueryableAdapter(), Context)
                        .ToJavaScriptQuery(false);
                    typeDefs += js.Trim() + "\n\n";
                }
                else
                {
                    typeDefs += "var " + GetDataSetObjectName(Types[i].QueryableType) + " = this." +
                                nameof(DataSet) + "('" + Types[i].QueryableType.Name + "');\n\n";
                }
            }
            return typeDefs;
        }

        public void RegisterType(ExpandEntityType type)
        {
            if (!Types.Contains(type))
            {
                Types.Add(type);
            }
        }

        public void AppendWhere(JavaScriptExpression filterExpression)
        {
            Query.AppendLine(
                ".filter(function(" + filterExpression.RootVariableName + ") { return " + filterExpression.Expression + "} )");
        }

        public override List<T> ToList()
        {
            // JavaScript implementation
            var str = ToJavaScriptQuery();
            //return eval(this.toJavaScriptQuery());
            return (List<T>) JavaScript.Eval(str);
        }

        public object DataSet(string name)
        {
            var sourceSet = Context.GetConfiguration<InMemoryDataStoreConfiguration>()
                .GetSourceByTypeName(name);
            var cloneSet = sourceSet.CloneAs(Context, typeof(T));
            return cloneSet;
        }
   }
}