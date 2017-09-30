using System.Collections.Generic;
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
            var query = "";
            var types = new List<ExpandEntityType>();
            types.Add(new ExpandEntityType(typeof(TEntity)));
            var expand = context.Operation;
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
                query += context.Data.GetDataSetObjectName(sourceType) + ",";
                query += context.Data.GetDataSetObjectName(targetType) + ",";
                if (detail.Relationship.Type == RelationshipType.ManyToMany)
                {
                    var manyToMany = detail.Relationship as IManyToManyRelationship;
                    types.Add(new ExpandEntityType(manyToMany.PivotType));
                    query += context.Data.GetDataSetObjectName(manyToMany.PivotType) + ",";
                    query += "'" + manyToMany.PivotSourceKeyProperty.PropertyName + "',";
                    query += "'" + manyToMany.PivotTargetKeyProperty.PropertyName + "',";
                }
                query += "'" + detail.Relationship.SourceProperty.PropertyName + "',";
                query += "'" + detail.Relationship.TargetProperty.PropertyName + "',";
                query += "'" + detail.Relationship.SourceKeyProperty.PropertyName + "',";
                query += "'" + detail.Relationship.TargetKeyProperty.PropertyName + "'";
                query += ");\n";
            }
            context.Data.Query.AppendLine(query);
        }
    }
}