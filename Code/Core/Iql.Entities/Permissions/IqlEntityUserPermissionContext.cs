namespace Iql.Entities.Permissions
{
    public class IqlEntityUserPermissionContext<TEntity, TUser> : IqlUserPermissionContext<TUser>
        where TEntity : class
        where TUser : class
    {
        public bool IsEntityNew { get; set; }
        public TEntity Entity { get; set; }

        public IqlEntityUserPermissionContext(bool isEntityNew, TUser user, TEntity entity) : base(user)
        {
            Entity = entity;
            IsEntityNew = isEntityNew;
        }
    }
}