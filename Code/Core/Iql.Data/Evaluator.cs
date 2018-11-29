using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Iql.Conversion;
using Iql.Data.Context;
using Iql.Data.Extensions;
using Iql.Entities;
using Iql.Extensions;

namespace Iql.Data
{
    public class ExpressionEvaluator
    {
        public static object EvaluateExpression<T>(
            Expression<Func<T, object>> expression,
            T entity,
            EntityConfigurationBuilder builder = null)
            where T : class
        {
            return EvaluateLambda(expression, entity, builder, typeof(T));
        }

        public static object EvaluateLambda(
            LambdaExpression expression,
            object entity,
            EntityConfigurationBuilder builder = null,
            Type entityType = null)
        {
            var iql = IqlConverter.Instance.ConvertLambdaExpressionToIqlByType(expression, entityType).Expression;
            ProcessIqlExpression(iql, entityType, builder, out var propertyExpressions, out var lookup);
            foreach (var item in lookup.Keys.ToArray())
            {
                lookup[item] = item.Evaluate(entity) ?? ResolveNull(item, propertyExpressions.First(_ => _.Expression == item.Expression));
            }
            return Finalise(entity, lookup, iql, propertyExpressions);
        }

        public static Task<object> EvaluateExpressionAsync<T>(
            Expression<Func<T, object>> expression,
            T entity,
            IDataContext dataContext)
            where T : class
        {
            return EvaluateLambdaAsync(expression, entity, dataContext);
        }

        public static async Task<object> EvaluateLambdaAsync(
            LambdaExpression expression,
            object entity,
            IDataContext dataContext,
            Type entityType = null)
        {
            entityType = entityType ?? entity.GetType();
            var iql = IqlConverter.Instance.ConvertLambdaExpressionToIqlByType(expression, entityType).Expression;
            return await EvaluateIqlAsync(iql, entity, dataContext, entityType);
        }

        public static Task<object> EvaluateIqlAsync(
            IqlExpression expression,
            object entity,
            IDataContext dataContext,
            Type entityType = null)
        {
            return EvaluateIqlCustomAsync(
                expression,
                dataContext?.EntityConfigurationContext ?? DataContext.FindBuilderForEntityType(entityType),
                entity,
                async (e, type, propertyPath, flattenedExpression) => await propertyPath.EvaluateAsync(e, dataContext) ??
                                                 ResolveNull(propertyPath, flattenedExpression));
        }

        private static object ResolveNull(IqlPropertyPath propertyPath, IqlFlattenedExpression flattenedExpression)
        {
            if (IsStringConcatenation(flattenedExpression))
            {
                return "";
            }

            return null;
        }

        private static bool IsStringConcatenation(IqlFlattenedExpression flattenedExpression)
        {
            if (flattenedExpression.Expression.Kind == IqlExpressionKind.Condition)
            {
                return false;
            }

            if (flattenedExpression.Expression.Kind == IqlExpressionKind.Add)
            {
                var add = flattenedExpression.Expression as IqlAddExpression;
                if (add.Left.ReturnType == IqlType.String || add.Right.ReturnType == IqlType.String)
                {
                    return true;
                }

                var ancestors = flattenedExpression.Ancestors.ToList();
                ancestors.Add(flattenedExpression);
                return IsStringConcatenation(
                           new IqlFlattenedExpression(add.Left, ancestors.ToArray())) ||
                       IsStringConcatenation(
                           new IqlFlattenedExpression(add.Right, ancestors.ToArray()));
            }

            if (flattenedExpression.Ancestors.Any())
            {
                return IsStringConcatenation(flattenedExpression.Ancestors[flattenedExpression.Ancestors.Length - 1]);
            }
            return false;
        }

        public static async Task<object> EvaluateIqlCustomAsync(
            IqlExpression expression,
            EntityConfigurationBuilder builder,
            object entity,
            Func<object, Type, IqlPropertyPath, IqlFlattenedExpression, Task<object>> evaluatorAsync,
            Type entityType = null)
        {
            entityType = entityType ?? entity.GetType();
            var iql = ProcessIqlExpression(
                expression,
                entityType,
                builder,
                out var propertyExpressions,
                out var lookup);
            foreach (var item in lookup.Keys.ToArray())
            {
                lookup[item] = await evaluatorAsync(entity, entityType, item,
                    propertyExpressions.First(_ => _.Expression == item.Expression));
            }
            return Finalise(entity, lookup, iql, propertyExpressions);
        }

        //private static IqlExpression ProcessLambdaExpression(LambdaExpression expression, object entity, Type entityType,
        //    IDataContext dataContext, out IqlPropertyExpression[] propertyExpressions, out Dictionary<IqlPropertyPath, object> lookup)
        //{
        //    entityType = entityType ?? entity.GetType();
        //    var iql = IqlConverter.Instance.ConvertLambdaExpressionToIqlByType(expression, entityType).Expression;
        //    return ProcessIqlExpression(iql, entityType, dataContext, out propertyExpressions, out lookup);
        //}

        private static IqlExpression ProcessIqlExpression(
            IqlExpression iql,
            Type entityType,
            EntityConfigurationBuilder builder,
            out IqlFlattenedExpression[] propertyExpressions, out Dictionary<IqlPropertyPath, object> lookup)
        {
            builder = builder ?? DataContext.FindBuilderForEntityType(entityType);
            propertyExpressions = iql.TopLevelPropertyExpressions();
            lookup = new Dictionary<IqlPropertyPath, object>();
            for (var i = 0; i < propertyExpressions.Length; i++)
            {
                var propertyExpression = propertyExpressions[i];
                var path = IqlPropertyPath.FromPropertyExpression(
                    builder
                    .GetEntityByType(entityType),
                    propertyExpression.Expression as IqlPropertyExpression);
                lookup.Add(path, null);
            }
            return iql;
        }

        private static object Finalise(object entity, Dictionary<IqlPropertyPath, object> lookup, IqlExpression iql,
            IqlFlattenedExpression[] propertyExpressions)
        {
            var expressionResultLookup = new Dictionary<IqlExpression, object>();
            foreach (var item in lookup)
            {
                expressionResultLookup.Add(item.Key.Expression, item.Value);
            }

            var processedIql = iql.ReplaceWith((context, iqlExpression) =>
            {
                if (propertyExpressions.Any(_ => _.Expression == iqlExpression))
                {
                    return new IqlLiteralExpression(expressionResultLookup[iqlExpression]);
                }

                return iqlExpression;
            });
            var processedLambda = IqlConverter.Instance.ConvertIqlToLambdaExpression(processedIql);
            var compiledLambda = processedLambda.Compile();
            var result = compiledLambda.DynamicInvoke(new object[] { entity });
            return result;
        }
    }
}