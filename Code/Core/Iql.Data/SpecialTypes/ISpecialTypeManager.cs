using Iql.Data.Context;
using Iql.Data.Lists;

namespace Iql.Data.SpecialTypes
{
    public interface ISpecialTypeManager<T, TMapType, TKey>
       where TMapType : class
    {
        IDataContext DataContext { get; set; }
        T Definition { get; set; }
        DbSet<TMapType, TKey> Set { get; }
    }
}