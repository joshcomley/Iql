using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Iql.Entities.Permissions;
using Iql.Parsing;
using Iql.Serialization;

namespace Iql.Entities
{
    public interface IUserPermissionContainer
    {
        UserPermissionsManager PermissionManager { get; }
        List<IqlUserPermissionRule> PermissionRules { get; }
    }

    public interface IUserPermission : IEntityConfigurationItem
    {
        UserPermissionsCollection Permissions { get; }

        IUserPermission ParentPermissions { get; }
        // IEntityConfiguration EntityConfiguration { get; set; }
    }

    public class UserPermissionsCollection
    {
        public IEntityConfiguration EntityConfiguration { get; }
        public IEntityConfigurationContainer Builder { get; }
        private bool _keysInitialized;
        private List<string> _keys;

        public List<string> Keys
        {
            get
            {
                if (!_keysInitialized)
                {
                    _keysInitialized = true;
                    _keys = new List<string>();
                }

                return _keys;
            }
            set
            {
                _keysInitialized = true;
                _keys = value;
            }
        }

        public UserPermissionsCollection(
            IEntityConfiguration? entityConfiguration = null)
        {
            EntityConfiguration = entityConfiguration;
            Builder = entityConfiguration?.Builder;
        }

        public UserPermissionsCollection UseRule(
            string key
        )
        {
            if (!Keys.Contains(key))
            {
                Keys.Add(key);
            }

            return this;
        }

        public UserPermissionsCollection RemoveRule(string key)
        {
            while (Keys.Contains(key))
            {
                Keys.Remove(key);
            }

            return this;
        }

        public IqlUserPermissionRule[] Rules
        {
            get
            {
                var rules = new List<IqlUserPermissionRule>();
                for (var i = 0; i < Keys.Count; i++)
                {
                    var key = Keys[i];
                    var rule = Builder.PermissionRules.SingleOrDefault(_ => _.Key == key);
                    if (rule != null)
                    {
                        rules.Add(rule);
                    }
                }

                return rules.ToArray();
            }
        }
    }

    public static class UserPermissionsCollectionExtensions
    {
        public static IqlUserPermissionRule DefineAndUseUserPermissionRule<TUser>(
            this UserPermissionsCollection permissions,
            Expression<Func<IqlUserPermissionContext<TUser>, IqlUserPermission>> rule,
            string? key = null,
            IqlUserPermissionRulePrecedenceDirection? precedence = null
#if TypeScript
        , EvaluateContext evaluateContext = null
#endif
        ) where TUser : class
        {
            if (key == null)
            {
                var iql = rule.ToIqlLambdaExpression(
                    permissions.EntityConfiguration
                );
                var json = IqlJsonSerializer.Serialize(iql);
                key = Md5.Hash(json);
            }

            permissions.UseRule(key!);
            return permissions.Builder.PermissionManager.DefineUserPermissionRule(
                key,
                rule,
                precedence
#if TypeScript
        , evaluateContext
#endif
            );
        }

        public static IqlUserPermissionRule DefineAndUseEntityUserPermissionRule<TEntity, TUser>(
            this UserPermissionsCollection permissions,
            Expression<Func<IqlEntityUserPermissionContext<TEntity, TUser>, IqlUserPermission>> rule,
            string? key = null,
            IqlUserPermissionRulePrecedenceDirection? precedence = null
#if TypeScript
        , EvaluateContext evaluateContext = null
#endif
        ) where TUser : class where TEntity : class
        {
            if (key == null)
            {
                var iql = rule.ToIqlPropertyExpression(
                    permissions.EntityConfiguration
                );
                key = Md5.Hash(IqlJsonSerializer.Serialize(iql));
            }

            permissions.UseRule(key!);
            return permissions.Builder.PermissionManager.DefineEntityUserPermissionRule(
                key,
                rule,
                precedence
#if TypeScript
        , evaluateContext
#endif
            );
        }
    }
}