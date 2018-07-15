using System.Threading.Tasks;
using Iql.Data.Context;
using Iql.Entities;
using Iql.Extensions;

namespace Iql.Data.Extensions
{
    public static class PropertyPathExtensions
    {
        public static async Task<object> EvaluateAsync(this IqlPropertyPath propertyPath, object entity, IDataContext dataContext)
        {
            if (entity == null)
            {
                return null;
            }
            var result = entity;
            foreach (var part in propertyPath.PropertyPath)
            {
                var parent = result;
                result = result.GetPropertyValueByName(part.PropertyName);
                if (result == null)
                {
                    if (part.Property.Kind.HasFlag(PropertyKind.Relationship) &&
                        dataContext.DataStore.Tracking.IsTracked(parent))
                    {
                        await dataContext.LoadRelationshipPropertyAsync(parent, part.Property);
                        result = parent.GetPropertyValueByName(part.PropertyName);
                        if (result == null)
                        {
                            return null;
                        }
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            return result;
        }

        public static async Task<T> EvaluateAsAsync<T>(this IqlPropertyPath propertyPath, object entity, IDataContext dataContext)
        {
            return (T)await propertyPath.EvaluateAsync(entity, dataContext);
        }
    }
}