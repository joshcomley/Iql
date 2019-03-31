using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Iql.Conversion;
using Iql.Data.Evaluation;
using Iql.Entities.Permissions;
using Iql.Entities.Services;
using Iql.Parsing.Types;

#if TypeScript
using Iql.Parsing;
#endif

namespace Iql.Entities
{
    public class UserPermissionsManager
    {
        public IUserPermission Container { get; }
        public IEntityConfigurationBuilder EntityConfigurationBuilder { get; }

        public IqlUserPermissionRule DefineUserPermissionRule<TUser>(
            Expression<Func<IqlUserPermissionContext<TUser>, IqlUserPermission>> rule, string key = null
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

        public IqlUserPermissionRule DefineEntityUserPermissionRule<TEntity, TUser>(
            Expression<Func<IqlEntityUserPermissionContext<TEntity, TUser>, IqlUserPermission>> rule, string key = null
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
            if (Container.PermissionRules == null)
            {
                return null;
            }

            return Container.PermissionRules.FirstOrDefault(_ => _.Key == key);
        }

        public UserPermissionsManager(IUserPermission container, IEntityConfigurationBuilder entityConfigurationBuilder)
        {
            Container = container;
            EntityConfigurationBuilder = entityConfigurationBuilder;
        }
    }
}