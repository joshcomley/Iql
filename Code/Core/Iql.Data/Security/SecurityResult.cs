using Iql.Entities.Permissions;

namespace Iql.Data.Security
{
    public class SecurityResult
    {
        public IqlUserPermission Permissions { get; }

        public bool CanUpdate => Check(IqlUserPermission.Update);
        public bool CanDelete => Check(IqlUserPermission.Delete);
        public bool CanCreate => Check(IqlUserPermission.Create);
        public bool CanRead => Check(IqlUserPermission.Read);

        private bool Check(IqlUserPermission permissions)
        {
            return Permissions == IqlUserPermission.Unset || Permissions.HasFlag(permissions);
        }

        public SecurityResult(IqlUserPermission permissions)
        {
            Permissions = permissions;
        }
    }
}