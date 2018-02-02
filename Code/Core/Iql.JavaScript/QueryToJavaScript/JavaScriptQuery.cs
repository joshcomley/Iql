using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Iql.Queryable;
using Iql.Queryable.Data;
using Iql.Queryable.Data.DataStores.InMemory;
using Iql.Queryable.Data.EntityConfiguration.Relationships;
using Iql.Queryable.Data.Tracking;
using Iql.Queryable.Native;

namespace Iql.JavaScript.QueryToJavaScript
{
    public class JavaScriptQuery<T> : QueryResult<T>, IJavaScriptQueryResult
        where T : class
    {
        public JavaScriptQuery(IQueryable<T> queryable,
            IDataContext dataContext)
        {
            Queryable = queryable;
            DataContext = dataContext;
        }
        public StringBuilder Query { get; set; } = new StringBuilder();
        public string GetDataSetObjectName(Type type)
        {
            return "dataSet_" + type.Name;
        }

        public void ExpandOneToMany(
            IList source,
            IList target,
            string sourceProperty,
            string targetProperty,
            string sourceTargetKeyProperty,
            string targetKeyProperty)
        {
            var property = DataContext.EntityConfigurationContext.EntityType<T>()
                .FindProperty(sourceProperty);
            var targetType = property
                .ElementType;
            source.ExpandOneToMany(typeof(T), target, targetType, property.Relationship.Relationship);
        }

        public void ExpandOneToOne(
            IEnumerable source,
            IEnumerable target,
            string sourceProperty,
            string targetProperty,
            string sourceTargetKeyProperty,
            string targetKeyProperty)
        {
            source.ExpandOneToOne(target, sourceProperty, targetProperty, sourceTargetKeyProperty, targetKeyProperty);
        }

        private IQueryable<T> Queryable { get; }
        public IDataContext DataContext { get; }

        public bool HasKey { get; set; }
        public object Key { get; set; }
        public List<ExpandEntityType> Types { get; set; } = new List<ExpandEntityType>();

        public string ToJavaScriptQuery(bool returnValue = true)
        {
            var title = $"/* {typeof(T).Name} */\n";
            RegisterType(new ExpandEntityType(typeof(T)));
            var operations = Query.ToString();
            var types = ResolveTypes();
            var query = title + types;
            var varName = GetDataSetObjectName(typeof(T));
            if (!string.IsNullOrWhiteSpace(operations))
            {
                query += operations.Trim() + ";\n";
            }
            if (returnValue)
            {
                query += "" + varName + ";\n";
            }
            return query;
        }

        private string ResolveTypes()
        {
            var typeDefs = "";
            for (var i = 0; i < Types.Count; i++)
            {
                var queryable = Types[i].Queryable;
                if (queryable != null)
                {
                    var js = queryable.ToQueryWithAdapter(new JavaScriptQueryableAdapter(), DataContext)
                        .ToJavaScriptQuery(false);
                    typeDefs += js.Trim() + "\n\n";
                }
                else
                {
                    typeDefs +=
                        $"var {GetDataSetObjectName(Types[i].QueryableType)} = this.{nameof(DataSet)}(\'{Types[i].QueryableType.Name}\');\n\n";
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
            Query.Append(
                $".filter(function({filterExpression.RootVariableName}) {{ return {filterExpression.Expression}}} )");
        }

        public override List<T> ToList()
        {
            // JavaScript implementation
            var str = ToJavaScriptQuery();
            //return eval(this.toJavaScriptQuery());
            var list = (List<T>) JavaScript.Eval(str);
            var clone = list.CloneAs(DataContext, typeof(T), RelationshipCloneMode.Full);
            return clone;
        }

        public object DataSet(string name)
        {
            var sourceSet = DataContext.GetConfiguration<InMemoryDataStoreConfiguration>()
                .GetSourceByTypeName(name);
            foreach (var configuration in DataContext.EntityConfigurationContext.AllConfigurations())
            {
                if (configuration.Type.Name == name)
                {
                    var cloneSet = sourceSet.CloneAs(DataContext, configuration.Type, RelationshipCloneMode.Full);
                    return cloneSet;
                }
            }
            throw new Exception($"Unable to find entity type \"{name}\"");
        }
   }
}