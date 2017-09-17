using System;
using Iql.Parsing;
using Iql.Queryable.Data.DataStores;
using Iql.Queryable.Data.EntityConfiguration;

namespace Iql.Queryable.Data
{
    public interface IDataContext
    {
        EvaluateContext EvaluateContext { get; set; }
        IDataStore DataStore { get; }
        EntityConfigurationBuilder EntityConfigurationContext { get; set; }

        void RegisterConfiguration<T>(T configuration)
            where T : class;

        T GetConfiguration<T>()
            where T : class;

        IDbSet AsDbSetByType(Type entityType);
        DbSet<T, TKey> AsDbSet<T, TKey>() where T : class;
    }
}