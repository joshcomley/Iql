using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Iql.Data.Context;
using Iql.Data.Evaluation;
using Iql.Entities;
using Iql.Entities.Extensions;
using Iql.Entities.InferredValues;
using Iql.Entities.Relationships;
using Iql.Extensions;
using Iql.Parsing.Evaluation;
using Iql.Entities.Services;

namespace Iql.Data.Extensions
{
    public static class PropertyExtensions
    {
        private static MethodInfo NewInferredValueContextTypedMethod { get; }

        static PropertyExtensions()
        {
            NewInferredValueContextTypedMethod =
                typeof(PropertyExtensions).GetMethod(nameof(NewInferredValueContextTyped),
                    BindingFlags.NonPublic | BindingFlags.Static);
        }

        public static async Task<bool> IsReadOnlyAsync(
            this ISimpleProperty property,
            object owner,
            IDataContext dataContext)
        {
            if (property.IsReadOnly)
            {
                return true;
            }

            if (property is RelationshipDetailBase relationshipDetailBase)
            {
                if (relationshipDetailBase.IsReadOnly)
                {
                    return true;
                }
            }

            //if (simpleProperty != null &&
            //    simpleProperty.Relationship != null &&
            //    simpleProperty.Relationship.ThisIsTarget &&
            //    await simpleProperty.Relationship.OtherEnd.Property.IsReadOnlyAsync(entity, dataContext))
            //{
            //    continue;
            //}
            
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
                            PropertyExtensions.NewInferredValueContext(owner, owner, property.EntityConfiguration.Type),
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

        public static async Task<bool> TrySetInferredValueAsync(
            this IProperty property,
            object oldEntity,
            object entity,
            IDataContext dataContext,
            IServiceProviderProvider serviceProviderProvider = null)
        {
            var changes = await property.TryGetInferredValueCustomAsync(
                oldEntity,
                entity, 
                new DefaultEvaluator(dataContext),
                serviceProviderProvider);
            changes.ApplyChanges();
            return changes.Success;
        }

        public static async Task<InferredValueChanges> TryGetInferredValueCustomAsync(
            this IProperty property, 
            object oldEntity,
            object entity, 
            IIqlCustomEvaluator customEvaluator,
            IServiceProviderProvider serviceProviderProvider = null)
        {
            //Func<InferredValueChanges> noChangeResult = () => new InferredValueChanges(
            //    false,
            //    true, 
            //    property, 
            //    oldEntity, 
            //    entity,
            //    property.GetValue(entity), 
            //    property.GetValue(entity));
            Func<bool, InferredValueChanges> noChangeResult = success => new InferredValueChanges(
                success, 
                oldEntity, 
                entity, 
                property, 
                null);
            Func<IProperty, object, InferredValueChange> getPropertyChange = (prop, newValue) =>
                new InferredValueChange(true, true, prop, oldEntity, entity, prop.GetValue(entity), newValue);
            var changes = new List<InferredValueChange>();
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
                                NewInferredValueContext(oldEntity, entity, property.EntityConfiguration.Type),
                                customEvaluator,
                                property.EntityConfiguration.Type);

                        if (!Equals(conditionResult.Result, true))
                        {
                            return noChangeResult(true);
                        }
                    }

                    if (inferredWith.ForNewOnly && customEvaluator.IsEntityNew(property.EntityConfiguration, entity) == false)
                    {
                        return noChangeResult(true);
                    }

                    if (inferredWith.Mode == InferredValueMode.IfNull && property.GetValue(entity) != null)
                    {
                        return noChangeResult(true);
                    }

                    if (inferredWith.Mode == InferredValueMode.IfNullOrEmpty &&
                        property.GetValue(entity) != null &&
                        !property.GetValue(entity).IsDefaultValue(property.TypeDefinition))
                    {
                        return noChangeResult(true);
                    }

                    var inferredWithIql = inferredWith.InferredWithIql;
                    var result = await inferredWithIql.EvaluateIqlCustomAsync(
                        property.EntityConfiguration.Builder,
                        serviceProviderProvider,
                        NewInferredValueContext(oldEntity, entity, property.EntityConfiguration.Type), 
                        customEvaluator,
                        property.EntityConfiguration.Type);

                    if (!result.Success)
                    {
                        return noChangeResult(false);
                    }

                    var value = result.Result;
                    if (property.TypeDefinition.ToIqlType() == IqlType.String &&
                        value != null && !(value is string))
                    {
                        value = value.ToString();
                    }

                    changes.Add(getPropertyChange(property, value));
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
                            changes.Add(getPropertyChange(property.Relationship.ThisEnd.Property, relatedEntity));
                        }
                        else
                        {
                            changes.Add(getPropertyChange(property.Relationship.ThisEnd.Property, null));
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
                            changes.Add(getPropertyChange(property.EntityConfiguration.FindProperty(key.Name), key.Value));
                        }
                    }
                }
            }

            return new InferredValueChanges(true, oldEntity, entity, property, changes.ToArray());
        }

        private static object NewInferredValueContext(object oldEntity, object entity, Type entityType)
        {
            return NewInferredValueContextTypedMethod.InvokeGeneric(null, new[] { oldEntity, entity }, entityType);
        }

        private static object NewInferredValueContextTyped<T>(T oldEntity, T entity)
            where T : class
        {
            return new InferredValueContext<T>(oldEntity, entity);
        }
    }
}