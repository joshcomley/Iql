using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Iql.Data.Context;
using Iql.Entities;
using Iql.Entities.Extensions;
using Iql.Entities.InferredValues;
using Iql.Entities.Relationships;
using Iql.Entities.Services;
using Iql.Extensions;
using Iql.Parsing.Types;

namespace Iql.Data.Evaluation
{
    public class InferredValueEvaluationSession : IEvaluationSessionContainer
    {
        private static MethodInfo NewInferredValueContextTypedMethod { get; }

        static InferredValueEvaluationSession()
        {
            NewInferredValueContextTypedMethod =
                typeof(InferredValueEvaluationSession).GetMethod(nameof(NewInferredValueContextTyped),
                    BindingFlags.NonPublic | BindingFlags.Static);
        }

        public InferredValueEvaluationSession(IEvaluationSessionContainer evaluationSession = null)
        {
            Session = evaluationSession?.Session ?? new EvaluationSession();
        }

        public IEvaluationSession Session { get; set; }

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
            bool isInitialize = false,
            bool? trackResults = null)
        {
            var config = dataContext.EntityConfigurationContext.GetEntityByType(entity.GetType());
            var oldEntity = dataContext.GetEntityState(entity)?.EntityBeforeChanges();
            return await TryGetInferredValuesCustomAsync(
                config,
                oldEntity,
                entity,
                isInitialize,
                dataContext,
                dataContext,
                trackResults);
        }

        public async Task<InferredValuesResult> TrySetInferredValuesCustomAsync(
            IEntityConfiguration config,
            object oldEntity,
            object entity,
            bool isInitialize,
            IIqlDataEvaluator dataEvaluator,
            IServiceProviderProvider serviceProviderProvider = null)
        {
            var result = await TryGetInferredValuesCustomAsync(config, oldEntity, entity, isInitialize, dataEvaluator, serviceProviderProvider, true);
            result.ApplyChanges();
            return result;
        }

        public async Task<InferredValuesResult> TryGetInferredValuesCustomAsync(
            IEntityConfiguration config,
            object oldEntity,
            object entity,
            bool isInitialize,
            IIqlDataEvaluator dataEvaluator,
            IServiceProviderProvider serviceProviderProvider,
            bool? trackResults = null)
        {
            trackResults = trackResults == null ? dataEvaluator.IsTracked(entity) : trackResults.Value;
            serviceProviderProvider = serviceProviderProvider ?? config.Builder;
            var changes = new List<InferredValueChanges>();
            var alreadyInferredLookup = new Dictionary<string, InferredValueChanges>();
            for (var i = 0; i < config.Properties.Count; i++)
            {
                var property = config.Properties[i];
                if(property.Name == "Client")
                {
                    int a = 0;
                }
                var inferredValueChanges = await TryGetInferredValueCustomInternalAsync(
                    alreadyInferredLookup, 
                    property,
                    oldEntity,
                    entity,
                    isInitialize,
                    dataEvaluator,
                    serviceProviderProvider,
                    trackResults);
                changes.Add(inferredValueChanges);
            }

            return new InferredValuesResult(oldEntity, entity, changes.ToArray());
        }

        public async Task<bool> IsReadOnlyWithDataContextAsync(
            IPropertyContainer property,
            object entity,
            IDataContext dataContext,
            bool isInitialize = false)
        {
            return await IsReadOnlyAsync(
                property,
                entity,
                dataContext,
                dataContext,
                dataContext.EntityConfigurationContext,
                isInitialize);
        }

        public async Task<bool> IsReadOnlyAsync(
            IPropertyContainer property,
            object entity,
            IIqlDataEvaluator dataContext,
            IServiceProviderProvider serviceProviderProvider,
            ITypeResolver typeResolver,
            bool isInitialize)
        {
            var sourceProperty = property;
            var basicProperty = property as IProperty;
            if (basicProperty != null &&
                basicProperty.Relationship != null &&
                basicProperty.Relationship.ThisIsTarget)
            {
                sourceProperty = basicProperty.Relationship.OtherEnd.Property;
            }
            else
            {
                var collectionRelationship = property as ITargetRelationshipSourceDetail;
                if (collectionRelationship != null)
                {
                    sourceProperty = collectionRelationship.Relationship.Source.Property;
                }
            }

            var sourcePropertyAsGroup = sourceProperty as IPropertyGroup;
            var propertyAsGroup = property as IPropertyGroup;
            if ((sourcePropertyAsGroup != null && !sourcePropertyAsGroup.CanWrite) ||
                (propertyAsGroup != null && propertyAsGroup.EditKind != PropertyEditKind.Edit))
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

                        var conditionResult = await Session.EvaluateIqlCustomAsync(
                            inferredValueConfiguration.InferredWithConditionIql,
                            NewInferredValueContext(entity, entity, isInitialize, property.EntityConfiguration.Type),
                            null,
                            serviceProviderProvider,
                            dataContext, 
                            typeResolver);

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
            IServiceProviderProvider serviceProviderProvider = null,
            bool? trackResults = null)
        {
            var alreadyInferredLookup = new Dictionary<string, InferredValueChanges>();
            return await TryGetInferredValueCustomInternalAsync(
                alreadyInferredLookup,
                property,
                oldEntity,
                entity,
                isInitialize,
                dataEvaluator,
                serviceProviderProvider,
                trackResults);
        }

        private async Task<InferredValueChanges> TryGetInferredValueCustomInternalAsync(
            Dictionary<string, InferredValueChanges> alreadyInferredLookup,
            IProperty property,
            object oldEntity,
            object entity,
            bool isInitialize,
            IIqlDataEvaluator dataEvaluator,
            IServiceProviderProvider serviceProviderProvider = null,
            bool? trackResults = null
            )
        {
            alreadyInferredLookup = alreadyInferredLookup ?? new Dictionary<string, InferredValueChanges>();
            if (alreadyInferredLookup.ContainsKey(property.PropertyName))
            {
                return alreadyInferredLookup[property.PropertyName];
            }
            trackResults = trackResults == null ? dataEvaluator.IsTracked(entity) : trackResults.Value;
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
            Func<object, string, Task<object>> propertyValueResolverAsync = async (obj, propertyName) =>
            {
                if (obj != entity || propertyName == property.PropertyName)
                {
                    return obj.GetPropertyValueByName(propertyName);
                }
                var prop = property.EntityConfiguration.FindProperty(propertyName);
                if (prop == null)
                {
                    return obj.GetPropertyValueByName(propertyName);
                }

                if (!alreadyInferredLookup.ContainsKey(propertyName))
                {
                    var propChanges = await TryGetInferredValueCustomInternalAsync(
                        alreadyInferredLookup,
                        prop,
                        oldEntity,
                        entity,
                        isInitialize,
                        dataEvaluator,
                        serviceProviderProvider,
                        trackResults);
                    if (!alreadyInferredLookup.ContainsKey(propertyName))
                    {
                        alreadyInferredLookup.Add(propertyName, propChanges);
                    }
                }

                var c = alreadyInferredLookup[propertyName];
                if (!c.Success || c.Changes == null ||
                    c.Changes.Length == 0)
                {
                    return obj.GetPropertyValueByName(propertyName);
                }
                return c.Changes[c.Changes.Length - 1].NewValue;
            };
            if (property.HasInferredWith)
            {
                for (var i = 0; i < property.InferredValueConfigurations.Count; i++)
                {
                    var inferredWith = property.InferredValueConfigurations[i];
                    if (inferredWith.HasCondition)
                    {
                        var conditionResult = await Session
                            .EvaluateIqlCustomAsync(
                                inferredWith.InferredWithConditionIql,
                                NewInferredValueContext(oldEntity, entity, isInitialize, property.EntityConfiguration.Type),
                                null,
                                serviceProviderProvider,
                                dataEvaluator,
                                property.EntityConfiguration.Builder,
                                typeof(InferredValueContext<>).MakeGenericType(property.EntityConfiguration.Type), 
                                false,
                                trackResults,
                                propertyValueResolverAsync);

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
                    if(property.Name == "Client")
                    {
                        int a = 0;
                    }
                    var result = await Session.EvaluateIqlCustomAsync(
                        inferredWithIql,
                        NewInferredValueContext(oldEntity, entity, isInitialize, property.EntityConfiguration.Type),
                        null,
                        serviceProviderProvider,
                        dataEvaluator,
                        property.EntityConfiguration.Builder, typeof(InferredValueContext<>).MakeGenericType(property.EntityConfiguration.Type),
                        false,
                        trackResults,
                        propertyValueResolverAsync);

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
                        object relatedEntity = null;
                        if (value != null)
                        {
                            var entityClone = entity.Clone(property.EntityConfiguration.Builder,
                                property.Relationship.ThisEnd.Type);
                            inferredValueChange.ApplyChange(entityClone);
                            var compositeKey = property.Relationship.ThisEnd.GetCompositeKey(
                                entityClone,
                                true);
                            var cached = Session.GetCachedEntity(property.Relationship.OtherEnd.EntityConfiguration, compositeKey);
                            if (cached != null && cached.Exists)
                            {
                                relatedEntity = cached.Entity;
                            }
                            else
                            {
                                relatedEntity = await dataEvaluator.GetEntityByKeyAsync(
                                   property.Relationship.OtherEnd.EntityConfiguration,
                                   compositeKey,
                                   new string[] { },
                                   true);
                            }

                            if (Session.CacheMode != EvaluationCacheMode.None)
                            {
                                Session.SetCachedEntity(property.Relationship.OtherEnd.EntityConfiguration, compositeKey, relatedEntity);
                            }
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


            var inferredValueChanges = new InferredValueChanges(oldEntity, entity, property, changes.ToArray());
            if (!alreadyInferredLookup.ContainsKey(property.Name))
            {
                alreadyInferredLookup.Add(property.PropertyName, inferredValueChanges);
            }
            return inferredValueChanges;
        }

        private static object NewInferredValueContext(object oldEntity, object entity, bool isInitialize, Type entityType)
        {
            return NewInferredValueContextTypedMethod.InvokeGeneric(null, new[] { oldEntity, entity, isInitialize }, entityType);
        }

        private static object NewInferredValueContextTyped<T>(T oldEntity, T entity, bool isInitialize)
            where T : class
        {
            return new InferredValueContext<T>(oldEntity, entity, isInitialize);
        }
    }
}
