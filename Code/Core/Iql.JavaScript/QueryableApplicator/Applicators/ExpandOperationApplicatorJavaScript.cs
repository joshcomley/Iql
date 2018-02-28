using System;
using System.Collections.Generic;
using Iql.Queryable.Data.EntityConfiguration.Relationships;
using Iql.Queryable.Extensions;
using Iql.Queryable.Operations;
using Iql.Queryable.QueryApplicator.Applicators;

namespace Iql.JavaScript.QueryableApplicator.Applicators
{
    public class ExpandOperationApplicatorJavaScript
        : QueryOperationApplicator<IExpandOperation, IJavaScriptQueryResult, JavaScriptQueryableAdapter>
    {
        public override void Apply<TEntity>(
            IQueryOperationContext<IExpandOperation, TEntity, IJavaScriptQueryResult, JavaScriptQueryableAdapter> context)
        {
            var expand = context.Operation;
            var javaScriptQueryResult = context.Data;
            ApplyExpand<TEntity>(expand, javaScriptQueryResult);
        }

        internal static void ApplyExpand<TEntity>(IExpandOperation expand, IJavaScriptQueryResult javaScriptQueryResult)
        {
            var query = "";
            var types = new List<ExpandEntityType>();
            types.Add(new ExpandEntityType(typeof(TEntity)));
            for (var j = 0; j < expand.ExpandDetails.Count; j++)
            {
                var detail = expand.ExpandDetails[j];
                var thisEnd = detail.IsTarget
                    ? detail.Relationship.Target
                    : detail.Relationship.Source;
                var property = javaScriptQueryResult.GetRoot().RegisterProperty(thisEnd.Property);
                types.Add(new ExpandEntityType(detail.SourceQueryable.ItemType, detail.SourceQueryable));
                types.Add(new ExpandEntityType(detail.TargetQueryable.ItemType, detail.TargetQueryable));
                var expandMethodName = nameof(JavaScriptQuery<object>.Expand);
                switch (detail.Relationship.Kind)
                {
                    case RelationshipKind.ManyToMany:
                        throw new NotSupportedException("Expanding many to many relationships are not yet supported.");
                }
                query += $"\nthis.{expandMethodName}(";
                query += $"{javaScriptQueryResult.GetDataSetObjectName(detail.Relationship.Source.Type)},";
                query += $"{javaScriptQueryResult.GetDataSetObjectName(detail.Relationship.Target.Type)},";
                //if (detail.Relationship.Kind == RelationshipKind.ManyToMany)
                //{
                //    var manyToMany = detail.Relationship as IManyToManyRelationship;
                //    types.Add(new ExpandEntityType(manyToMany.PivotType));
                //    query += javaScriptQueryResult.GetDataSetObjectName(manyToMany.PivotType) + ",";
                //    query += "'" + manyToMany.PivotSourceKeyProperty.PropertyName + "',";
                //    query += "'" + manyToMany.PivotTargetKeyProperty.PropertyName + "',";
                //}
                query += $"\'{property}\'";
                //query += "'" + detail.Relationship.Target.Property.Name + "',";
                //// TODO: Support multiple constraints
                //var constraint = detail.Relationship.Constraints.First();
                //query += "'" + constraint.SourceKeyProperty.Name + "',";
                //query += "'" + constraint.TargetKeyProperty.Name + "'";
                query += ");\n";
            }
            foreach (var type in types)
            {
                javaScriptQueryResult.RegisterType(type);
            }
            javaScriptQueryResult.Query.AppendLine(query);
        }
    }
}