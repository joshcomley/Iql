namespace IqlSampleApp.Data.Entities.Bases
{
    public class DbObjectBase<TUser, TKey> : DbObjectRoot<TUser>, IDbObject<TKey>
    {
        public TKey Id { get; set; }
    }
}