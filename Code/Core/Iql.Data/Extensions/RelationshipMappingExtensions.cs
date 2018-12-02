using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using Iql.Data.Context;
using Iql.Entities;
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
                if (result.Value == null)
                {
                    if (result.Results.Length == 1)
                    {
                        var last = result.Results[0];
                        if (last.Success && last.Value == null && last.Parent != null &&
                            last.Source.Property.Relationship != null)
                        {
                            var parentConstraints = last.Source.Property.Relationship.ThisEnd.GetCompositeKey(last.Parent);
                            var ourConstraints = (mapping.Property as IRelationshipDetail).Constraints;
                            for (var j = 0; j < parentConstraints.Keys.Length; j++)
                            {
                                var constraint = parentConstraints.Keys[j];
                                entity.SetPropertyValueByName(ourConstraints[j].Name, constraint.Value);
                            }
                        }
                    }
                }
                else
                {
                    mapping.SetValue(entity, result.Value);
                }
                mapping.SetValue(entity, result.Value);
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
            if (result == null && isRelationship)
            {
                int a = 0;
            }
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
            return (T)result.Value;
        }

        private static async Task<IqlExpressonEvaluationResult> EvaluateExpressionTypedAsyncInternal<TParent, T>(
            this IqlExpression expression,
            object entity,
            IDataContext dataContext,
            TParent parent)
        {
            var ctx = new RelationshipFilterContext<TParent>();
            ctx.Owner = parent;
            var result = await expression.EvaluateIqlPathAsync(
                ctx,
                dataContext,
                typeof(T));
            if (result.Value is LambdaExpression)
            {
                var exp = result.Value as LambdaExpression;
                result.Value = exp.Compile().DynamicInvoke(new object[] { entity });
                if (result.Value is IqlPropertyPathEvaluationResult)
                {
                    result.Value = (result.Value as IqlPropertyPathEvaluationResult).Value;
                }
            }
            return result;
        }
    }
}