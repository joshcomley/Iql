using System;
using System.Collections.Generic;
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
            Expands = new List<JavaScriptExpand>();
            Filters = new List<JavaScriptExpression>();
            OrderBys = new List<JavaScriptOrderByExpression>();
        }

        private IQueryable<T> Queryable { get; }
        public IDataContext Context { get; }
        public bool HasKey { get; set; }
        public object Key { get; set; }
        public List<JavaScriptExpand> Expands { get; set; } = new List<JavaScriptExpand>();
        public List<JavaScriptExpression> Filters { get; set; } = new List<JavaScriptExpression>();
        public List<JavaScriptOrderByExpression> OrderBys { get; set; } = new List<JavaScriptOrderByExpression>();

        public string ToJavaScriptQuery(bool returnValue = true)
        {
            var title = "/* " + typeof(T).Name + " */\n";
            var query = "";
            var types = new List<ExpandEntityType>();
            types.Add(new ExpandEntityType(typeof(T)));

            for (var i = 0; i < Expands.Count; i++)
            {
                var expand = Expands[i].Operation;
                for (var j = 0; j < expand.ExpandDetails.Count; j++)
                {
                    var detail = expand.ExpandDetails[j];
                    var sourceType = detail.Relationship.SourceType;
                    var targetType = detail.Relationship.TargetType;
                    types.Add(new ExpandEntityType(detail.Relationship.SourceType, detail.SourceQueryable));
                    types.Add(new ExpandEntityType(detail.Relationship.TargetType, detail.TargetQueryable));
                    var expandMethodName = "";
                    switch (detail.Relationship.Type)
                    {
                        case RelationshipType.OneToOne:
                            expandMethodName = nameof(ListExpandExtensions.ExpandOneToOne);
                            break;
                        case RelationshipType.OneToMany:
                            expandMethodName = nameof(ListExpandExtensions.ExpandOneToMany);
                            break;
                        case RelationshipType.ManyToMany:
                            expandMethodName = nameof(ListExpandExtensions.ExpandManyToMany);
                            break;
                    }
                    query += "this." + expandMethodName + "(";
                    query += GetDataSetObjectName(sourceType) + ",";
                    query += GetDataSetObjectName(targetType) + ",";
                    if (detail.Relationship.Type == RelationshipType.ManyToMany)
                    {
                        var manyToMany = detail.Relationship as IManyToManyRelationship;
                        types.Add(new ExpandEntityType(manyToMany.PivotType));
                        query += GetDataSetObjectName(manyToMany.PivotType) + ",";
                        query += "'" + manyToMany.PivotSourceKeyProperty.PropertyName + "',";
                        query += "'" + manyToMany.PivotTargetKeyProperty.PropertyName + "',";
                    }
                    query += "'" + detail.Relationship.SourceProperty.PropertyName + "',";
                    query += "'" + detail.Relationship.TargetProperty.PropertyName + "',";
                    query += "'" + detail.Relationship.SourceKeyProperty.PropertyName + "',";
                    query += "'" + detail.Relationship.TargetKeyProperty.PropertyName + "'";
                    query += ");\n";
                }
            }
            var typeDefs = "";
            var typeDefLog = new Dictionary<string, bool>();
            for (var i = 0; i < types.Count; i++)
            {
                if (!typeDefLog.ContainsKey(types[i].QueryableType.Name))
                {
                    var queryable = types[i].Queryable;
                    if (queryable != null)
                    {
                        var js = queryable.ToQueryWithAdapter(new JavaScriptQueryableAdapter(), Context)
                            .ToJavaScriptQuery(false);
                        typeDefs += js.Trim() + "\n\n";
                    }
                    else
                    {
                        typeDefs += "var " + GetDataSetObjectName(types[i].QueryableType) + " = this." +
                                    nameof(DataSet) + "('" + types[i].QueryableType.Name + "');\n\n";
                    }
                    typeDefLog[types[i].QueryableType.Name] = true;
                }
            }
            query = title + typeDefs + query;
            var varName = GetDataSetObjectName(typeof(T));
            var operations = "";
            //var queryParts = new List<string>();
            // if (this.orderBys.length) {
            //     query = "var by = " + by.toString() + "\n" + query;
            // }
            for (var i = 0; i < Filters.Count; i++)
            {
                var filter = Filters[i];
                var entityName = filter.RootVariableName;
                operations += ".filter(function(" + entityName + ") { return " + Filters[i].Expression + "} )\n";
            }
            var sort = ResolveSort(0, false);
            if (!string.IsNullOrWhiteSpace(sort))
            {
                operations += ".sort(" + sort + ")\n";
            }
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

        public override List<T> ToList()
        {
            // JavaScript implementation
            var str = ToJavaScriptQuery();
            //return eval(this.toJavaScriptQuery());
            return (List<T>) JavaScript.Eval(str);
        }

        public object DataSet(string name)
        {
            var sourceSet = Context.GetConfiguration<JavaScriptQueryConfiguration>()
                .GetSourceByName(name);
            var cloneSet = sourceSet.Clone();
            return cloneSet;
        }


        //private static string GetDataSetObjectName<TEntity>() => GetDataSetObjectName(typeof(TEntity));
        private static string GetDataSetObjectName(Type type)
        {
            return "dataSet_" + type.Name;
        }

        protected Func<object, object, int> OrderBy(string path, bool reverse, Func<object, object> primer,
            Func<object, object, int> then = null)
        {
            Func<object, string, object> get = (obj, path_) =>
            {
                if (!string.IsNullOrWhiteSpace(path))
                {
                    var pathParts = path_.Split('.');
                    var len = pathParts.Length - 1;
                    for (var i = 0; i < len; i++)
                    {
                        obj = obj.GetPropertyValue(pathParts[i]);
                    }
                    ;
                    return obj.GetPropertyValue(pathParts[len]);
                }
                return obj;
            };
            Func<object, object> prime = obj => { return primer != null ? primer(get(obj, path)) : get(obj, path); };

            Func<object, object, int> result = (a, b) =>
            {
                var A = prime(a) as IComparable;
                var B = prime(b) as IComparable;
                var compare = A.CompareTo(B);
                return (
                           compare != 0 ? compare : then?.Invoke(a, b) ?? 0
                       ) * (reverse ? -1 : 1);
            };
            return result;
        }

        private string ResolveSort(int index, bool lastDescending)
        {
            if (index >= OrderBys.Count)
            {
                return null;
            }
            var filter = OrderBys[index];
            if (filter.Descending)
            {
                lastDescending = !lastDescending;
            }
            var sort = "this." + nameof(OrderBy) + "('" + filter.Expression + "', " +
                       (lastDescending ? "true" : "false") + ", null";
            if (index < OrderBys.Count - 1)
            {
                sort += ",";
                sort += ResolveSort(++index, lastDescending);
            }
            sort += ")";
            return sort;
        }
    }
}