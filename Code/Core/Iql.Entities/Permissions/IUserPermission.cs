using System.Collections.Generic;
using System.Threading.Tasks;
using Iql.Entities.Permissions;

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
        public IEntityConfigurationBuilder Builder { get; }

        public UserPermissionsCollection(IEntityConfigurationBuilder builder)
        {
            Builder = builder;
        }
    }
}