namespace Iql.Entities.Permissions
{
    public class IqlEntityUserPermissionContext<TEntity, TUser> : IqlUserPermissionContext<TUser>
        where TEntity : class
        where TUser : class
    {
        public TEntity Entity { get; set; }

        public IqlEntityUserPermissionContext(bool isNew, IPropertyGroup propertyGroup, TUser user, TEntity entity) : base(isNew, propertyGroup, user)
        {
            Entity = entity;
        }
    }
}