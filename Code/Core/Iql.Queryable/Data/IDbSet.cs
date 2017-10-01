using System.Threading.Tasks;
using Iql.Queryable.Operations;

namespace Iql.Queryable.Data
{
    public interface IDbSet : IQueryableBase
    {
        Task<object> WithKey(object key);
    }
}