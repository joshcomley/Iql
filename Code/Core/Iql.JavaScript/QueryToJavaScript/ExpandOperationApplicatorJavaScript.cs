using System.Collections.Generic;
using System.Linq;
using Iql.Queryable.Data.EntityConfiguration.Relationships;
using Iql.Queryable.Operations;
using Iql.Queryable.Operations.Applicators;

namespace Iql.JavaScript.QueryToJavaScript
{
    public class ExpandOperationApplicatorJavaScript
        : QueryOperationApplicator<IExpandOperation, IJavaScriptQueryResult>
    {
        public override void Apply<TEntity>(
            IQueryOperationContext<IExpandOperation, TEntity, IJavaScriptQueryResult> context)
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
                query += "\nthis." + expandMethodName + "(";
                query += javaScriptQueryResult.GetDataSetObjectName(sourceType) + ",";
                query += javaScriptQueryResult.GetDataSetObjectName(targetType) + ",";
                if (detail.Relationship.Type == RelationshipType.ManyToMany)
                {
                    var manyToMany = detail.Relationship as IManyToManyRelationship;
                    types.Add(new ExpandEntityType(manyToMany.PivotType));
                    query += javaScriptQueryResult.GetDataSetObjectName(manyToMany.PivotType) + ",";
                    query += "'" + manyToMany.PivotSourceKeyProperty.PropertyName + "',";
                    query += "'" + manyToMany.PivotTargetKeyProperty.PropertyName + "',";
                }
                query += "'" + detail.Relationship.Source.Property.PropertyName + "',";
                query += "'" + detail.Relationship.Target.Property.PropertyName + "',";
                // TODO: Support multiple constraints
                var constraint = detail.Relationship.Constraints.First();
                query += "'" + constraint.SourceKeyProperty.PropertyName + "',";
                query += "'" + constraint.TargetKeyProperty.PropertyName + "'";
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