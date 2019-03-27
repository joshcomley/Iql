using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Iql.Conversion;
using Iql.DotNet.Serialization;
using Iql.Entities.Permissions;
using Newtonsoft.Json;

namespace Iql.Entities
{
    public class UserPermissionsManager
    {
        public IUserPermission Container { get; }
        public IEntityConfiguration EntityConfiguration { get; }

        public UserPermissionsManager DefineUserPermission<TUser>(
            Expression<Func<IqlUserPermissionContext<TUser>, IqlUserPermission>> rule, string key = null)
            where TUser : class
        {
            var result = IqlConverter.Instance.ConvertLambdaExpressionToIqlByType(rule, typeof(IqlUserPermissionContext<TUser>));
            var lambdaExpression = result.Expression as IqlLambdaExpression;
            Container.PermissionRules.Add(new IqlUserPermissionRule(key, lambdaExpression, false));
            return this;
        }

        public UserPermissionsManager DefineEntityUserPermission<TEntity, TUser>(
            Expression<Func<IqlEntityUserPermissionContext<TEntity, TUser>, IqlUserPermission>> rule, string key = null)
            where TEntity : class
            where TUser : class
        {
            var result = IqlConverter.Instance.ConvertLambdaExpressionToIqlByType(rule, typeof(IqlEntityUserPermissionContext<TEntity, TUser>));
            var lambdaExpression = result.Expression as IqlLambdaExpression;
            Container.PermissionRules.Add(new IqlUserPermissionRule(key, lambdaExpression, true));
            return this;
        }

        public Task<IqlUserPermission> GetUserPermissionAsync(object entity = null)
        {
            return Task.FromResult(IqlUserPermission.ReadAndEdit);
        }

        public UserPermissionsManager(IUserPermission container, IEntityConfiguration entityConfiguration)
        {
            Container = container;
            EntityConfiguration = entityConfiguration;
        }
    }
}