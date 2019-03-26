namespace Iql.Entities.Permissions
{
    public class IqlUserPermissionContext<TUser>
        where TUser : class
    {
        public bool IsNew { get; set; }
        public IPropertyGroup PropertyGroup { get; set; }
        public TUser User { get; set; }

        public IqlUserPermissionContext(bool isNew, IPropertyGroup propertyGroup, TUser user)
        {
            IsNew = isNew;
            PropertyGroup = propertyGroup;
            User = user;
        }
    }
}
