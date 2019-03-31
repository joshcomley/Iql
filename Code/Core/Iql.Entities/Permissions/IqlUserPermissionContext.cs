namespace Iql.Entities.Permissions
{
    public class IqlUserPermissionContext<TUser>
        where TUser : class
    {
        public TUser User { get; set; }

        public IqlUserPermissionContext(TUser user)
        {
            User = user;
        }
    }
}
