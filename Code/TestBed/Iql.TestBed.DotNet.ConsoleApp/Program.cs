using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Iql.TestBed.DotNet.ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await DotNet.Main.Run();
            //new DbMe<Person>().Expand(p => p.Mother, p => p.Expand(p2 => p2.Mother, null).Nothing().Nothing().Nothing());
        }
    }


    //public class Person
    //{
    //    public Person Mother { get; set; }
    //}

    //public class Expander<TDbRoot, TDbExpand, T, TTarget>
    //{
    //    private readonly TDbRoot _root;

    //    public Expander(TDbRoot root)
    //    {
    //        _root = root;
    //    }

    //    public TDbRoot Expand(
    //        Expression<Func<T, TTarget>> target,
    //        Expression<Func<TDbExpand, TDbExpand>> filter)
    //    {
    //        return _root;
    //    }
    //}
    //public interface IDbMe<T>
    //{
    //    Expander<IDbMe<T>, IDbMe<TTarget>, T, TTarget> Expand<TTarget>();

    //    DbMe<T> Expand<TTarget>(
    //        Expression<Func<T, TTarget>> target,
    //        Expression<Func<IDbMe<TTarget>, IDbMe<TTarget>>> filter);
    //    DbMe<T> Hey();
    //    DbMe<T> Nothing();
    //}

    //public class DbMe<T> : IDbMe<T>// : Me<T, DbMe<T>>
    //{
    //    //public Expander<IDbMe<T>, IDbMe<TTarget>, T, TTarget> Expand<TTarget>()
    //    //{
    //    //    throw new NotImplementedException();
    //    //}
    //    public Expander<DbMe<T>, DbMe<TTarget>, T, TTarget> Expand<TTarget>()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public DbMe<T> Expand<TTarget>(Expression<Func<T, TTarget>> target, Expression<Func<IDbMe<TTarget>, IDbMe<TTarget>>> filter)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public DbMe<T> Hey()
    //    {
    //        return this;
    //    }

    //    public DbMe<T> Nothing()
    //    {
    //        return this;
    //    }
    //    public DbMe<T> Expand<TTarget>(
    //        Expression<Func<T, TTarget>> target,
    //        Expression<Func<DbMe<TTarget>, DbMe<TTarget>>> filter)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}
