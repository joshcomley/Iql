using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Iql.Data.Context;
using Iql.Data.Evaluation;
using Iql.Data.Rendering;
using Iql.Entities;
using Iql.Entities.Extensions;
using Iql.Entities.InferredValues;
using Iql.Entities.PropertyGroups.Files;
using Iql.Entities.Relationships;
using Iql.Extensions;
using Iql.Parsing.Evaluation;
using Iql.Entities.Services;

namespace Iql.Data.Extensions
{
    public static class IqlDataPropertyExtensions
    {
        private static MethodInfo NewInferredValueContextTypedMethod { get; }

        static IqlDataPropertyExtensions()
        {
            NewInferredValueContextTypedMethod =
                typeof(IqlDataPropertyExtensions).GetMethod(nameof(NewInferredValueContextTyped),
                    BindingFlags.NonPublic | BindingFlags.Static);
        }

        public static string ResolveKind(this IPropertyContainer property)
        {
            var kind = PropertyRenderingKind.Unknown;
            var prop = property as PropertyBase;
            var rel = property as RelationshipDetailBase;
            if (prop != null)
            {
                switch (prop.TypeDefinition.Kind)
                {
                    case IqlType.Enum:
                        kind = PropertyRenderingKind.Enum;
                        break;
                    case IqlType.Guid:
                        kind = PropertyRenderingKind.Guid;
                        break;
                    case IqlType.String:
                        kind = PropertyRenderingKind.String;
                        break;
                    case IqlType.TimeSpan:
                        kind = PropertyRenderingKind.TimeSpan;
                        break;
                    case IqlType.Date:
                        kind = PropertyRenderingKind.Date;
                        break;
                    case IqlType.Boolean:
                        kind = PropertyRenderingKind.Boolean;
                        break;
                    case IqlType.Integer:
                    case IqlType.Decimal:
                        kind = PropertyRenderingKind.Number;
                        break;
                    case IqlType.GeometryPolygon:
                    case IqlType.GeographyPolygon:
                        kind = PropertyRenderingKind.GeoPolygon;
                        break;
                    case IqlType.GeometryPoint:
                    case IqlType.GeographyPoint:
                        kind = PropertyRenderingKind.GeoPoint;
                        break;
                }
                //if (prop.Kind.HasFlag(PropertyKind.Key))
                //{
                //    kind = PropertyRenderingKind.Key;
                //}
                //else if (prop.Kind.HasFlag(PropertyKind.RelationshipKey))
                //{
                //    kind = PropertyRenderingKind.RelationshipKey;
                //}
                //else if (prop.Kind == PropertyKind.Relationship)
                //{
                //    kind = PropertyRenderingKind.Relationship;
                //}
            }
            else if (rel != null && rel.RelationshipSide == RelationshipSide.Target)
            {
                kind = PropertyRenderingKind.RelationshipTarget;
            }
            else if (rel != null && rel.RelationshipSide == RelationshipSide.Source)
            {
                kind = PropertyRenderingKind.RelationshipSource;
            }
            else if (property is IFile)
            {
                kind = PropertyRenderingKind.File;
            }
            else
            {
                kind = PropertyRenderingKind.Group;
            }

            return kind;
        }

        public static async Task<bool> IsReadOnlyAsync(
            this IPropertyGroup property,
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

                        var conditionResult = await inferredValueConfiguration.InferredWithConditionIql.EvaluateIqlAsync(
                            IqlDataPropertyExtensions.NewInferredValueContext(entity, entity, property.EntityConfiguration.Type),
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
            bool isInitialize,
            IDataContext dataContext,
            IServiceProviderProvider serviceProviderProvider = null)
        {
            var changes = await property.TryGetInferredValueCustomAsync(
                oldEntity,
                entity,
                isInitialize,
                dataContext,
                serviceProviderProvider);
            changes.ApplyChanges();
            return changes.Success;
        }

        public static async Task<InferredValueChanges> TryGetInferredValueCustomAsync(
            this IProperty property, 
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
                        var conditionResult = await inferredWith
                            .InferredWithConditionIql
                            .EvaluateIqlCustomAsync(
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
                    var result = await inferredWithIql.EvaluateIqlCustomAsync(
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
                                new string[]{});
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