using System;
using System.Threading.Tasks;

namespace Iql.Queryable.Data
{
    public interface IDbSet : IQueryableBase
    {
        void AddEntity(object entity);
        Task<object> WithKey(object key);
    }
}