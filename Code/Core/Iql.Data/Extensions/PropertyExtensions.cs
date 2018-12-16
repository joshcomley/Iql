using System.Threading.Tasks;
using Iql.Data.Context;
using Iql.Data.Evaluation;
using Iql.Entities;
using Iql.Entities.Extensions;
using Iql.Entities.InferredValues;
using Iql.Extensions;
using Iql.Parsing.Evaluation;
using Iql.Entities.Services;

namespace Iql.Data.Extensions
{
    public static class PropertyExtensions
    {
        public static async Task<bool> IsReadOnlyAsync(
            this ISimpleProperty property,
            object owner,
            IDataContext dataContext)
        {
            if (property.IsReadOnly)
            {
                return true;
            }

            if (property.Name.Contains("Reports"))
            {
                int a = 0;
            }

            if (property is IProperty)
            {
                var p = property as IProperty;
                if (p.InferredValueConfigurations != null)
                {
                    for (var i = 0; i < p.InferredValueConfigurations.Count; i++)
                    {
                        var inferredValueConfiguration = p.InferredValueConfigurations[i];
                        if (inferredValueConfiguration.CanOverride)
                        {
                            continue;
                        }

                        if (!inferredValueConfiguration.HasCondition)
                        {
                            return true;
                        }

                        var conditionResult = await inferredValueConfiguration.InferredWithConditionIql.EvaluateIqlAsync(
                            owner,
                            dataContext);

                        if (conditionResult.Success && Equals(conditionResult.Result, true))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public static Task<bool> TrySetInferredValueAsync(
            this IProperty property,
            object entity,
            IDataContext dataContext,
            IServiceProviderProvider serviceProviderProvider = null)
        {
            return property.TrySetInferredValueCustomAsync(
                entity, 
                new DefaultEvaluator(dataContext),
                serviceProviderProvider);
        }

        public static async Task<bool> TrySetInferredValueCustomAsync(
            this IProperty property, 
            object entity, 
            IIqlCustomEvaluator customEvaluator,
            IServiceProviderProvider serviceProviderProvider = null)
        {
            if (serviceProviderProvider == null && customEvaluator is DefaultEvaluator)
            {
                serviceProviderProvider = (customEvaluator as DefaultEvaluator).DataContext;
            }
            if (property.HasInferredWith)
            {
                for (var i = 0; i < property.InferredValueConfigurations.Count; i++)
                {
                    var inferredWith = property.InferredValueConfigurations[i];
                    if (inferredWith.HasCondition)
                    {
                        IqlObjectEvaluationResult conditionResult = null;

                        conditionResult = await inferredWith
                            .InferredWithConditionIql
                            .EvaluateIqlCustomAsync(
                                property.EntityConfiguration.Builder,
                                serviceProviderProvider,
                                entity,
                                customEvaluator,
                                property.EntityConfiguration.Type);

                        if (!Equals(conditionResult.Result, true))
                        {
                            return true;
                        }
                    }

                    if (inferredWith.ForNewOnly && customEvaluator.IsEntityNew(property.EntityConfiguration, entity) == false)
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
                    var result = await inferredWithIql.EvaluateIqlCustomAsync(
                        property.EntityConfiguration.Builder,
                        serviceProviderProvider,
                        entity,
                        customEvaluator,
                        property.EntityConfiguration.Type);

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
                            var relatedEntity = await customEvaluator.GetEntityByKeyAsync(
                                property.Relationship.OtherEnd.EntityConfiguration,
                                compositeKey);
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