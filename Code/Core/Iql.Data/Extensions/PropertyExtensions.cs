using System.Threading.Tasks;
using Iql.Data.Context;
using Iql.Entities;
using Iql.Entities.Extensions;
using Iql.Entities.InferredValues;
using Iql.Extensions;

namespace Iql.Data.Extensions
{
    public static class PropertyExtensions
    {
        public static async Task<bool> TrySetInferredValuesAsync(this IDataContext dataContext, object entity)
        {
            var config = dataContext.EntityConfigurationContext.GetEntityByType(entity.GetType());
            var success = true;
            for (var i = 0; i < config.Properties.Count; i++)
            {
                var propety = config.Properties[i];
                var propertySuccess = await propety.TrySetInferredValueAsync(entity, dataContext);
                if (!propertySuccess)
                {
                    success = false;
                }
            }

            return success;
        }

        public static async Task<bool> TrySetInferredValueAsync(
            this IProperty property,
            object entity,
            IDataContext dataContext)
        {
            if (property.HasInferredWith)
            {
                for (var i = 0; i < property.InferredValueConfigurations.Count; i++)
                {
                    var inferredWith = property.InferredValueConfigurations[i];
                    if (inferredWith.HasCondition)
                    {
                        var conditionResult = await inferredWith.InferredWithConditionIql.EvaluateIqlAsync(entity, dataContext);
                        if (!Equals(conditionResult.Result, true))
                        {
                            return true;
                        }
                    }
                    if (inferredWith.ForNewOnly && dataContext.IsEntityNew(entity, property.EntityConfiguration.Type) == false)
                    {
                        return true;
                    }

                    if (inferredWith.Mode == InferredValueMode.IfNull && property.GetValue(entity) != null)
                    {
                        return true;
                    }

                    if (inferredWith.Mode == InferredValueMode.IfNullOrEmpty &&
                        property.GetValue(entity) != null &&
                        !property.GetValue(entity).IsDefaultValue(property.TypeDefinition))
                    {
                        return true;
                    }

                    var inferredWithIql = inferredWith.InferredWithIql;
                    var result = await inferredWithIql.EvaluateIqlAsync(entity, dataContext);
                    if (!result.Success)
                    {
                        return false;
                    }
                    var value = result.Result;
                    if (property.TypeDefinition.ToIqlType() == IqlType.String &&
                        value != null && !(value is string))
                    {
                        value = value.ToString();
                    }
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
                        for (var j = 0; j < compositeKey.Keys.Length; j++)
                        {
                            var key = compositeKey.Keys[j];
                            entity.SetPropertyValueByName(key.Name, key.Value);
                        }
                    }
                }
            }

            return true;
        }
    }
}