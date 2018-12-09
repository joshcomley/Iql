using System;
using System.Threading.Tasks;
using Iql.Data.Context;
using Iql.Data.IqlToIql;
using Iql.Entities;
using Iql.Extensions;

namespace Iql.Data.Extensions
{
    public static class PropertyExtensions
    {
        public static async Task TrySetInferredValuesAsync(
            object entity,
            IDataContext dataContext)
        {
            var config = dataContext.EntityConfigurationContext.GetEntityByType(entity.GetType());
            for (var i = 0; i < config.Properties.Count; i++)
            {
                var propety = config.Properties[i];
                await propety.TrySetInferredValueAsync(entity, dataContext);
            }
        }

        public static async Task TrySetInferredValueAsync(
            this IProperty property,
            object entity,
            IDataContext dataContext)
        {
            if (property.HasInferredWith)
            {
                if (property.InferredWithForNewOnly && dataContext.IsEntityNew(entity, property.EntityConfiguration.Type) == false)
                {
                    return;
                }

                if (property.InferredWithForNullOnly && property.GetValue(entity) != null)
                {
                    return;
                }

                var inferredWithIql = property.InferredWithIql;
                var value = await inferredWithIql.EvaluateIqlAsync(entity, dataContext);
                property.SetValue(entity, value);
                if (property.Kind.HasFlag(PropertyKind.RelationshipKey))
                {
                    if (value != null)
                    {
                        var compositeKey = property.Relationship.ThisEnd.GetCompositeKey(
                            entity,
                            true);
                        var dbSet = dataContext.GetDbSetByEntityType(
                            property.Relationship.OtherEnd.Type);
                        var relatedEntity = await dbSet.GetWithKeyAsync(compositeKey);
                        property.Relationship.ThisEnd.Property.SetValue(entity, relatedEntity);
                    }
                    else
                    {
                        property.Relationship.ThisEnd.Property.SetValue(entity, null);
                    }
                }
                if (property.Kind.HasFlag(PropertyKind.Relationship))
                {
                    var compositeKey = property.Relationship.OtherEnd.GetCompositeKey(
                        value,
                        true);
                    for (var i = 0; i < compositeKey.Keys.Length; i++)
                    {
                        var key = compositeKey.Keys[i];
                        entity.SetPropertyValueByName(key.Name, key.Value);
                    }
                }
            }
        }
    }
}