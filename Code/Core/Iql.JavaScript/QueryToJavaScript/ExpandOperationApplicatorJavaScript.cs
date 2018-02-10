using System;
using System.Collections.Generic;
using System.Linq;
using Iql.Queryable.Data.EntityConfiguration.Relationships;
using Iql.Queryable.Native;
using Iql.Queryable.Operations;
using Iql.Queryable.Operations.Applicators;

namespace Iql.JavaScript.QueryToJavaScript
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
                var sourceType = detail.Relationship.Source.Type;
                var targetType = detail.Relationship.Target.Type;
                types.Add(new ExpandEntityType(detail.Relationship.Source.Type, detail.SourceQueryable));
                types.Add(new ExpandEntityType(detail.Relationship.Target.Type, detail.TargetQueryable));
                var expandMethodName = nameof(JavaScriptQuery<object>.Expand);
                switch (detail.Relationship.Kind)
                {
                    case RelationshipKind.ManyToMany:
                        throw new NotSupportedException("Expanding many to many relationships are not yet supported.");
                }
                query += "\nthis." + expandMethodName + "(";
                query += javaScriptQueryResult.GetDataSetObjectName(sourceType) + ",";
                query += javaScriptQueryResult.GetDataSetObjectName(targetType) + ",";
                if (detail.Relationship.Kind == RelationshipKind.ManyToMany)
                {
                    var manyToMany = detail.Relationship as IManyToManyRelationship;
                    types.Add(new ExpandEntityType(manyToMany.PivotType));
                    query += javaScriptQueryResult.GetDataSetObjectName(manyToMany.PivotType) + ",";
                    query += "'" + manyToMany.PivotSourceKeyProperty.PropertyName + "',";
                    query += "'" + manyToMany.PivotTargetKeyProperty.PropertyName + "',";
                }
                query += "'" + detail.Relationship.Source.Property.Name + "',";
                query += "'" + detail.Relationship.Target.Property.Name + "',";
                // TODO: Support multiple constraints
                var constraint = detail.Relationship.Constraints.First();
                query += "'" + constraint.SourceKeyProperty.Name + "',";
                query += "'" + constraint.TargetKeyProperty.Name + "'";
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