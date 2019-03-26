using System.Collections.Generic;
using System.Threading.Tasks;
using Iql.Entities.Permissions;

namespace Iql.Entities
{
    public interface IUserPermission
    {
        UserPermissionsManager Permissions { get; }
        List<IqlUserPermissionRule> PermissionRules { get; }
    }
}