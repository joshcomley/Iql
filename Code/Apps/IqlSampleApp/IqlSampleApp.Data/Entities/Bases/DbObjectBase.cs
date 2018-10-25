namespace Brandless.Data.Entities
{
    public class DbObjectBase<TUser, TKey> : DbObjectRoot<TUser>, IDbObject<TKey>
    {
        public TKey Id { get; set; }
    }
}