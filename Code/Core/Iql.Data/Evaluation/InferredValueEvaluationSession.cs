using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Iql.Data.Context;
using Iql.Entities;
using Iql.Entities.Extensions;
using Iql.Entities.InferredValues;
using Iql.Entities.Services;
using Iql.Extensions;

namespace Iql.Data.Evaluation
{
    public class InferredValueEvaluationSession
    {
        private static MethodInfo NewInferredValueContextTypedMethod { get; }

        static InferredValueEvaluationSession()
        {
            NewInferredValueContextTypedMethod =
                typeof(InferredValueEvaluationSession).GetMethod(nameof(NewInferredValueContextTyped),
                    BindingFlags.NonPublic | BindingFlags.Static);
        }

        public InferredValueEvaluationSession()
        {
            EvaluationSession = new EvaluationSession();
        }

        public EvaluationSession EvaluationSession { get; set; }
        
        public async Task<InferredValuesResult> TrySetInferredValuesAsync(
            IDataContext dataContext,
            object entity,
            bool isInitialize = false)
        {
            var changes = await TryGetInferredValuesAsync(dataContext, entity, isInitialize);
            changes?.ApplyChanges();
            return changes;
        }

        public async Task<InferredValuesResult> TryGetInferredValuesAsync(
            IDataContext dataContext,
            object entity,
            bool isInitialize = false)
        {
            var config = dataContext.EntityConfigurationContext.GetEntityByType(entity.GetType());
            var oldEntity = dataContext.GetEntityState(entity)?.EntityBeforeChanges();
            return await TryGetInferredValuesCustomAsync(
                config,
                oldEntity,
                entity,
                isInitialize,
                dataContext,
                dataContext);
        }

        public async Task<InferredValuesResult> TrySetInferredValuesCustomAsync(
            IEntityConfiguration config,
            object oldEntity,
            object entity,
            bool isInitialize,
            IIqlDataEvaluator dataEvaluator,
            IServiceProviderProvider serviceProviderProvider = null)
        {
            var result = await TryGetInferredValuesCustomAsync(config, oldEntity, entity, isInitialize, dataEvaluator, serviceProviderProvider);
            result.ApplyChanges();
            return result;
        }

        public async Task<InferredValuesResult> TryGetInferredValuesCustomAsync(
            IEntityConfiguration config,
            object oldEntity,
            object entity,
            bool isInitialize,
            IIqlDataEvaluator dataEvaluator,
            IServiceProviderProvider serviceProviderProvider)
        {
            serviceProviderProvider = serviceProviderProvider ?? config.Builder;
            var changes = new List<InferredValueChanges>();
            for (var i = 0; i < config.Properties.Count; i++)
            {
                var property = config.Properties[i];
                var inferredValueChanges = await TryGetInferredValueCustomAsync(
                    property,
                    oldEntity,
                    entity,
                    isInitialize,
                    dataEvaluator,
                    serviceProviderProvider);
                changes.Add(inferredValueChanges);
            }

            return new InferredValuesResult(oldEntity, entity, changes.ToArray());
        }

        public async Task<bool> IsReadOnlyAsync(
            IPropertyGroup property,
            object entity,
            IDataContext dataContext)
        {
            if (!property.CanWrite || property.EditKind != PropertyEditKind.Edit)
            {
                return true;
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

                        var conditionResult = await EvaluationSession.EvaluateIqlAsync(
                            inferredValueConfiguration.InferredWithConditionIql,
                            NewInferredValueContext(entity, entity, property.EntityConfiguration.Type),
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

        public async Task<bool> TrySetInferredValueAsync(
            IProperty property,
            object oldEntity,
            object entity,
            bool isInitialize,
            IDataContext dataContext,
            IServiceProviderProvider serviceProviderProvider = null)
        {
            var changes = await TryGetInferredValueCustomAsync(
                property,
                oldEntity,
                entity,
                isInitialize,
                dataContext,
                serviceProviderProvider);
            changes.ApplyChanges();
            return changes.Success;
        }

        public async Task<InferredValueChanges> TryGetInferredValueCustomAsync(
            IProperty property,
            object oldEntity,
            object entity,
            bool isInitialize,
            IIqlDataEvaluator dataEvaluator,
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
            Func<IProperty, object, InferredValueChange> getPropertyChange = (prop, newValue) =>
                new InferredValueChange(true, true, prop, oldEntity, entity, prop.GetValue(entity), newValue);
            var changes = new List<InferredValueChange>();
            if (serviceProviderProvider == null && dataEvaluator is IServiceProviderProvider)
            {
                serviceProviderProvider = dataEvaluator as IServiceProviderProvider;
            }
            if (property.HasInferredWith)
            {
                for (var i = 0; i < property.InferredValueConfigurations.Count; i++)
                {
                    var inferredWith = property.InferredValueConfigurations[i];
                    if (inferredWith.HasCondition)
                    {
                        var conditionResult = await EvaluationSession
                            .EvaluateIqlCustomAsync(
                                inferredWith.InferredWithConditionIql,
                                serviceProviderProvider,
                                NewInferredValueContext(oldEntity, entity, property.EntityConfiguration.Type),
                                dataEvaluator,
                                property.EntityConfiguration.Builder,
                                typeof(InferredValueContext<>).MakeGenericType(property.EntityConfiguration.Type),
                                false,
                                false);

                        if (!Equals(conditionResult.Result, true))
                        {
                            continue;
                        }
                    }

                    if (inferredWith.Kind == InferredValueKind.InitializeOnly && !isInitialize)
                    {
                        continue;
                    }

                    if (inferredWith.ForNewOnly
                        && dataEvaluator.EntityStatus(entity, property.EntityConfiguration) == IqlEntityStatus.Existing
                        )
                    {
                        continue;
                    }

                    var originalValue = property.GetValue(entity);
                    if (inferredWith.Kind == InferredValueKind.IfNull && originalValue != null)
                    {
                        continue;
                    }

                    if (inferredWith.Kind == InferredValueKind.IfNullOrEmpty &&
                        originalValue != null &&
                        !originalValue.IsDefaultValue(property.TypeDefinition))
                    {
                        continue;
                    }

                    var inferredWithIql = inferredWith.InferredWithIql;
                    var result = await EvaluationSession.EvaluateIqlCustomAsync(
                        inferredWithIql,
                        serviceProviderProvider,
                        NewInferredValueContext(oldEntity, entity, property.EntityConfiguration.Type),
                        dataEvaluator,
                        property.EntityConfiguration.Builder,
                        typeof(InferredValueContext<>).MakeGenericType(property.EntityConfiguration.Type));

                    if (!result.Success)
                    {
                        changes.Add(new InferredValueChange(
                            false,
                            false,
                            property,
                            oldEntity,
                            entity,
                            originalValue,
                            property.GetValue(entity)));
                        continue;
                    }

                    var value = result.Result;
                    if (property.TypeDefinition.ToIqlType() == IqlType.String &&
                        value != null && !(value is string))
                    {
                        value = value.ToString();
                    }

                    var inferredValueChange = getPropertyChange(property, value);
                    changes.Add(inferredValueChange);
                    if (property.Kind.HasFlag(PropertyKind.RelationshipKey))
                    {
                        if (value != null)
                        {
                            var entityClone = entity.Clone(property.EntityConfiguration.Builder,
                                property.Relationship.ThisEnd.Type);
                            inferredValueChange.ApplyChange(entityClone);
                            var compositeKey = property.Relationship.ThisEnd.GetCompositeKey(
                                entityClone,
                                true);
                            var relatedEntity = await dataEvaluator.GetEntityByKeyAsync(
                                property.Relationship.OtherEnd.EntityConfiguration,
                                compositeKey,
                                new string[] { });
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

            return new InferredValueChanges(oldEntity, entity, property, changes.ToArray());
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
