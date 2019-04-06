using System.Collections.Generic;
using System.Linq;

namespace Iql.Entities
{
    public interface IUserPermissionContainer
    {
        UserPermissionsManager Permissions { get; }
        List<IqlUserPermissionRule> PermissionRules { get; }
    }
    public interface IUserPermission
    {
        UserPermissionsCollection Permissions { get; }
    }

    public class UserPermissionsCollection
    {
        public IEntityConfigurationContainer Builder { get; }
        public List<string> Keys { get; set; } = new List<string>();

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