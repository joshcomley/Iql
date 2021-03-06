﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using Iql.Data.Context;
using Iql.Data.Evaluation;
using Iql.Entities;
using Iql.Entities.Extensions;
using Iql.Entities.Relationships;
using Iql.Entities.Rules.Relationship;
using Iql.Extensions;

namespace Iql.Data.Extensions
{
    public static class RelationshipMappingExtensions
    {
        static RelationshipMappingExtensions()
        {
            EvaluateExpressionTypedAsyncInternalMethod = typeof(RelationshipMappingExtensions).GetMethod(
                nameof(EvaluateExpressionTypedAsyncInternal),
                BindingFlags.NonPublic | BindingFlags.Static);
        }

        private static MethodInfo EvaluateExpressionTypedAsyncInternalMethod { get; }

        public static async Task<object> CreateEntityForRelationshipAsync(
            this IRelationshipDetail relationship,
            IDataContext dataContext,
            object parent)
        {
            var set = dataContext.GetDbSetByEntityType(relationship.Type);
            if (!dataContext.IsTracked(parent))
            {
                set.Add(parent);
            }
            // TODO: Should support creating entities from parent entities that are new
            var allMappings = new List<IMappingBase>();
            allMappings.AddRange(relationship.RelationshipMappings);
            allMappings.AddRange(relationship.ValueMappings);
            var type = relationship.OtherSide.EntityConfiguration.Type;
            var entity = Activator.CreateInstance(type);
            for (var i = 0; i < allMappings.Count; i++)
            {
                var mapping = allMappings[i];
                var result = await EvaluateMappingAsync(
                    mapping,
                    entity,
                    dataContext,
                    parent);
                if (result.Result == null)
                {
                    if (result.Paths.Length == 1)
                    {
                        var last = result.Paths[0];
                        var entityProperty = last.Source.Property.EntityProperty();
                        IRelationshipFilterContext relationshipFilterContext = last.Parent as IRelationshipFilterContext;
                        if (last.Success && 
                            last.Value == null &&
                            relationshipFilterContext != null &&
                            relationshipFilterContext.Owner != null &&
                            entityProperty != null &&
                            entityProperty.Relationship != null)
                        {
                            var parentConstraints = entityProperty.Relationship.ThisEnd.GetCompositeKey(relationshipFilterContext.Owner);
                            var ourConstraints = (mapping.Property as IRelationshipDetail).Constraints;
                            for (var j = 0; j < parentConstraints.Keys.Length; j++)
                            {
                                var constraint = parentConstraints.Keys[j];
                                entity.SetPropertyValueByName(((IMetadata) ourConstraints[j]).Name, constraint.Value);
                            }
                        }
                    }
                }
                else
                {
                    mapping.SetValue(entity, result.Result);
                }
                mapping.SetValue(entity, result.Result);
            }

            dataContext.GetDbSetByEntityType(type).Add(entity);

            if (relationship.RelationshipSide == RelationshipSide.Target)
            {
                relationship.OtherSide.Property.SetValue(entity, parent);
            }
            else
            {
                relationship.Property.SetValue(parent, entity);
            }

            return entity;
        }

        public static async Task<IqlExpressonEvaluationResult> EvaluateMappingAsync(
            this IMappingBase mapping,
            object entity,
            IDataContext dataContext,
            object parent)
        {
            var parentType = mapping.Container.EntityConfiguration.Type;
            Type childType = null;
            var isRelationship = false;
            if (mapping.Property is IProperty)
            {
                childType = (mapping.Property as IProperty).TypeDefinition.Type;
            }
            else
            {
                childType = (mapping.Property as IRelationshipDetail).OtherSide.EntityConfiguration.Type;
                isRelationship = true;
            }
            var result = await (Task<IqlExpressonEvaluationResult>)EvaluateExpressionTypedAsyncInternalMethod.InvokeGeneric(
                null,
                new object[]
                {
                    mapping.Expression,
                    entity,
                    dataContext,
                    parent
                },
                parentType,
                childType);
            return result;
        }

        public static async Task<T> EvaluateExpressionTypedAsync<TParent, T>(
            this IqlExpression expression,
            object entity,
            IDataContext dataContext,
            TParent parent)
        {
            var result = await EvaluateExpressionTypedAsyncInternal<TParent, T>(
                expression,
                entity,
                dataContext,
                parent);
            return (T)result.Result;
        }

        private static async Task<IqlExpressonEvaluationResult> EvaluateExpressionTypedAsyncInternal<TParent, T>(
            this IqlExpression expression,
            object entity,
            IDataContext dataContext,
            TParent parent)
        {
            var ctx = new RelationshipFilterContext<TParent>();
            ctx.Owner = parent;
            var result = await new EvaluationSession().EvaluateIqlPathAsync(
                expression,
                ctx,
                dataContext,
                typeof(RelationshipFilterContext<TParent>),
                null,
                dataContext.EntityConfigurationContext,
                true);
            if (result.Result is LambdaExpression)
            {
                var exp = result.Result as LambdaExpression;
                result.Result = exp.Compile().DynamicInvoke(new object[] { entity });
                if (result.Result is IqlPropertyPathEvaluationResult)
                {
                    result.Result = (result.Result as IqlPropertyPathEvaluationResult).Value;
                }
            }
            return result;
        }
    }
}