using System;
using System.Linq;
using System.Linq.Expressions;
using Iql.Conversion;
using Iql.Entities.Permissions;
using Iql.Extensions;

#if TypeScript
using Iql.Parsing;
#endif

namespace Iql.Entities
{
    public class UserPermissionsManager
    {
        public IUserPermissionContainer Container { get; }
        public IEntityConfigurationBuilder EntityConfigurationBuilder { get; }

        public IqlUserPermissionRule DefineUserPermissionRule<TUser>(string key,
            Expression<Func<IqlUserPermissionContext<TUser>, IqlUserPermission>> rule
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
            )
            where TUser : class
        {
            var result = IqlConverter.Instance.ConvertLambdaExpressionToIqlByType(
                rule,
                EntityConfigurationBuilder,
                typeof(IqlUserPermissionContext<TUser>)
#if TypeScript
            , evaluateContext
#endif
                );
            var lambdaExpression = result.Expression as IqlLambdaExpression;
            var configuredRule = new IqlUserPermissionRule(
                EntityConfigurationBuilder,
                key,
                lambdaExpression,
                EntityConfigurationBuilder.EntityType<TUser>().Name
            );
            Container.PermissionRules.Add(configuredRule);
            return configuredRule;
        }

        public IqlUserPermissionRule DefineEntityUserPermissionRule<TEntity, TUser>(string key,
            Expression<Func<IqlEntityUserPermissionContext<TEntity, TUser>, IqlUserPermission>> rule
#if TypeScript
            , EvaluateContext evaluateContext = null
#endif
            )
            where TEntity : class
            where TUser : class
        {
            var result = IqlConverter.Instance.ConvertLambdaExpressionToIqlByType(
                rule,
                EntityConfigurationBuilder,
                typeof(IqlEntityUserPermissionContext<TEntity, TUser>)
#if TypeScript
            , evaluateContext
#endif
                );
            var flattened = result.Expression.Flatten()
                .Where(_ => _.Expression.Kind == IqlExpressionKind.Variable ||
                            _.Expression.Kind == IqlExpressionKind.RootReference)
                .ToArray();
            for (var i = 0; i < flattened.Length; i++)
            {
                var exp = flattened[i];
                var variableExpression = exp.Expression as IqlVariableExpression;
                if (variableExpression != null && variableExpression.EntityTypeName ==
                    typeof(IqlEntityUserPermissionContext<TEntity, TUser>).GetFullName())
                {
                    variableExpression.EntityTypeName =
                        $"{nameof(IqlEntityUserPermissionContext<TEntity, TUser>)}<{nameof(TEntity)}, {typeof(TUser).Name}>";
                }
            }

            var lambdaExpression = result.Expression as IqlLambdaExpression;
            var configuredRule = new IqlUserPermissionRule(
                EntityConfigurationBuilder,
                key,
                lambdaExpression,
                EntityConfigurationBuilder.EntityType<TUser>().Name,
                EntityConfigurationBuilder.EntityType<TEntity>().Name
            );
            Container.PermissionRules.Add(configuredRule);
            return configuredRule;
        }

        public IqlUserPermissionRule FindRule(string key)
        {
            return Container.PermissionRules?.FirstOrDefault(_ => _.Key == key);
        }

        public UserPermissionsManager(IUserPermissionContainer container, IEntityConfigurationBuilder entityConfigurationBuilder)
        {
            Container = container;
            EntityConfigurationBuilder = entityConfigurationBuilder;
        }
    }
}