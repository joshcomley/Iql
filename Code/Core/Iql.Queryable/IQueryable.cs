using Iql.Queryable.Data;

namespace Iql.Queryable
{
    public interface IQueryable<out T> : IDbSet
    {
    }
}