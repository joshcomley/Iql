using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Iql.Queryable;
using Iql.Queryable.Data;
using Iql.Queryable.Data.Context;
using Iql.Queryable.Data.DataStores.InMemory;
using Iql.Queryable.Data.EntityConfiguration;
using Iql.Queryable.Data.Queryable;
using Iql.Queryable.Data.Relationships;
using Iql.Queryable.Extensions;

namespace Iql.JavaScript.QueryableApplicator
{
    public class JavaScriptQuery<T> : InMemoryResult<IJavaScriptQueryResult>, IJavaScriptQueryResult
        where T : class
    {
        public JavaScriptQuery(
            IQueryable<T> queryable,
            IDataContext dataContext) : base(typeof(T), dataContext)
        {
            Queryable = queryable;
        }
        public StringBuilder Query { get; set; } = new StringBuilder();
        public string GetDataSetObjectName(Type type)
        {
            return $"dataSet_{type.Name}";
        }

        public void Expand(
            IList source,
            IList target,
            Guid propertyGuid,
            bool assign)
        {
            var property = this.GetRoot().Properties[propertyGuid];

            var allMatches = new RelationshipExpander()
                .FindMatches(
                    source,
                    target,
                    property.Relationship.Relationship,
                    assign);

            var ourMatches =
                property.Relationship.ThisIsTarget
                    ? allMatches.SourceMatches
                    : allMatches.TargetMatches;

            this.GetRoot().AddMatches(
                property.Relationship.ThisEnd.Property.TypeDefinition.ElementType,
                ourMatches);
        }

        private IQueryable<T> Queryable { get; }

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
                    var js = queryable.ToQueryWithAdapter<IJavaScriptQueryResult, JavaScriptQueryableAdapter>(
                            new JavaScriptQueryableAdapter(), DataContext, null, this)
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

        protected readonly Dictionary<Guid, IProperty> Properties = new Dictionary<Guid, IProperty>();
        public Guid RegisterProperty(IProperty property)
        {
            var root = this.GetRoot();
            if (root == this)
            {
                var guid = Guid.NewGuid();
                Properties.Add(guid, property);
                return guid;
            }

            return root.RegisterProperty(property);
        }

        public void AppendWhere(JavaScriptExpression filterExpression)
        {
            Query.Append(
                $".filter(function({filterExpression.RootVariableName}) {{ return {filterExpression.Expression}}} )");
        }

        public object DataSet(string name)
        {
            var sourceSet = Configuration.GetSourceByTypeName(name);
            foreach (var configuration in DataContext.EntityConfigurationContext.AllConfigurations())
            {
                if (configuration.Type.Name == name)
                {
                    //var cloneSet = sourceSet.CloneAs(DataContext, configuration.Type, RelationshipCloneMode.Full);
                    return sourceSet;
                }
            }
            throw new Exception($"Unable to find entity type \"{name}\"");
        }

        public override List<TEntity> ApplyOperations<TEntity>()
        {
            var str = ToJavaScriptQuery();
            //return eval(this.toJavaScriptQuery());
            var list = (List<TEntity>)JavaScript.Eval(str);
            return list;
            //var clone = list.CloneAs(DataContext, typeof(TEntity), RelationshipCloneMode.Full);
            //return clone;
        }
    }
}