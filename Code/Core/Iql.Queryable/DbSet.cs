using System;
using System.Threading.Tasks;
using Iql.Parsing;
using Iql.Queryable.Data;
using Iql.Queryable.Data.Crud.Operations.Results;
using Iql.Queryable.Data.DataStores;
using Iql.Queryable.Data.EntityConfiguration;
using Iql.Queryable.Operations;

namespace Iql.Queryable
{
    public class DbSet<T, TKey> : DbQueryable<T>, IDbSetOperations<T, TKey> where T : class
    {
        public DbSet(EntityConfigurationBuilder configuration, Func<IDataStore> dataStoreGetter,
            EvaluateContext evaluateContext = null, IDataContext dataContext = null) : base(
                configuration, dataStoreGetter, evaluateContext, dataContext)
        {
            Configuration = configuration;
            DataContext = dataContext;
            DataStoreGetter = dataStoreGetter;
        }

        public async Task<T> WithKey(TKey key)
        {
            return (await WithKeyWithResponse(key)).Data;
        }

        public async Task<GetSingleResult<T>> WithKeyWithResponse(TKey key)
        {
            return await Then(new WithKeyOperation(key)).SingleOrDefault();
        }
    }
}