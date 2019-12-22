using Iql.Data.Context;
using Iql.Entities;
using Iql.Entities.Relationships;

namespace Iql.Data.Queryable
{
    public static class QueryBuilder
    {
        public static DbQueryable<T> BuildQueryFromPropertyGroup<T>(
            this IPropertyGroup propertyGroup, 
            IDataContext dataContext,
            bool fullCollectionExpand = false)
            where T : class
        {
            var query = dataContext.GetDbQueryable<T>();
            return propertyGroup.ApplyExpandsToQuery(query, fullCollectionExpand);
        }

        public static DbQueryable<T> ApplyExpandsToQuery<T>(
            this IPropertyGroup propertyGroup, 
            DbQueryable<T> query,
            bool fullCollectionExpand)
            where T : class
        {
            if (propertyGroup is IRelationshipDetail)
            {
                var relationship = propertyGroup as IRelationshipDetail;
                if (relationship.IsCollection && !fullCollectionExpand && relationship.CountProperty != null)
                {
                    query = query.ExpandRelationship(((IMetadata) relationship.CountProperty).Name);
                }
                else
                {
                    query = query.ExpandRelationship(((IMetadata) relationship.Property).Name);
                }
            }
            else if (propertyGroup is PropertyPath)
            {
                var propertyPath = propertyGroup as PropertyPath;
                query = query.ExpandRelationship(propertyPath.Path);
            }
            else if (propertyGroup is PropertyCollection)
            {
                var groups = propertyGroup.GetGroupProperties();
                for (var i = 0; i < groups.Length; i++)
                {
                    var property = groups[i];
                    query = property.ApplyExpandsToQuery(query, fullCollectionExpand);
                }
            }

            return query;
        }
    }
}