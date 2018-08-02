using Iql.Data.Context;
using Iql.Entities;
using Iql.Entities.Relationships;

namespace Iql.Data.Queryable
{
    public static class QueryBuilder
    {
        public static DbQueryable<T> BuildQueryFromPropertyGroup<T>(this IPropertyGroup propertyGroup, IDataContext dataContext)
            where T : class
        {
            var query = dataContext.GetDbQueryable<T>();
            return propertyGroup.ApplyExpandsToQuery(query);
        }

        public static DbQueryable<T> ApplyExpandsToQuery<T>(this IPropertyGroup propertyGroup, DbQueryable<T> query)
            where T : class
        {
            if (propertyGroup is IRelationshipDetail)
            {
                var relationship = propertyGroup as IRelationshipDetail;
                query = query.ExpandRelationship(relationship.Property.Name);
            }
            else if (propertyGroup is PropertyPath)
            {
                var propertyPath = propertyGroup as PropertyPath;
                query = query.ExpandRelationship(propertyPath.Path);
            }
            else if (propertyGroup is PropertyCollection)
            {
                foreach (var property in propertyGroup.GetGroupProperties())
                {
                    query = property.ApplyExpandsToQuery(query);
                }
            }

            return query;
        }
    }
}