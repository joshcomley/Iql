using System.Collections.Generic;

namespace Iql.Entities.Extensions
{
    public static class PermissionsExtensions
    {
        public static List<IqlUserPermissionRule> EnsureHasBuilder(this List<IqlUserPermissionRule> rules,
            IEntityConfigurationBuilder builder)
        {
            for (var i = 0; i < rules.Count; i++)
            {
                var item = rules[i];
                item.EntityConfigurationBuilder = builder;
            }

            return rules;
        }
    }
}