using System.Threading.Tasks;
using Iql.Queryable.Data.Queryable;

namespace Iql.Queryable.Data.Lists
{
    public interface IDbQueryable : IQueryableBase
    {
        void DeleteEntity(object entity);
        void AddEntity(object entity);
        Task<object> WithKey(object key);
    }
}