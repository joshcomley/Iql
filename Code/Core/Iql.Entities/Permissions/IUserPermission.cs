using System.Collections.Generic;
using System.Linq;

namespace Iql.Entities
{
    public interface IUserPermissionContainer
    {
        UserPermissionsManager PermissionManager { get; }
        List<IqlUserPermissionRule> PermissionRules { get; }
    }

    public interface IUserPermission
    {
        UserPermissionsCollection Permissions { get; }
        IUserPermission ParentPermissions { get; }
    }

    public class UserPermissionsCollection
    {
        public IEntityConfigurationContainer Builder { get; }
        private bool _keysInitialized;
        private List<string> _keys;
        public List<string> Keys { get { if(!_keysInitialized) { _keysInitialized = true; _keys = new List<string>(); } return _keys; } set { _keysInitialized = true; _keys = value; } }

        public UserPermissionsCollection(IEntityConfigurationContainer builder = null)
        {
            Builder = builder;
        }

        public UserPermissionsCollection()
        {

        }

        public UserPermissionsCollection UseRule(string key)
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
}