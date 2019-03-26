using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Iql.Entities.Functions;
using Iql.Entities.Permissions;

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
            Container.PermissionRules.Add(new IqlUserPermissionRule(key, null, false));
            return this;
        }

        public UserPermissionsManager DefineEntityUserPermission<TEntity, TUser>(
            Expression<Func<IqlEntityUserPermissionContext<TEntity, TUser>, IqlUserPermission>> rule, string key = null)
            where TEntity : class
            where TUser : class
        {
            Container.PermissionRules.Add(new IqlUserPermissionRule(key, null, true));
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
    public class IqlUserPermissionRule
    {
        public string Key { get; set; }
        public IqlLambdaExpression Rule { get; set; }
        public bool AcceptsEntity { get; set; }

        public IqlUserPermissionRule(string key, IqlLambdaExpression rule, bool acceptsEntity)
        {
            Key = key;
            Rule = rule;
            AcceptsEntity = acceptsEntity;
        }
    }
}