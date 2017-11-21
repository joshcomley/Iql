using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        bool IsIdMatch(object left, object right);
        Task<T> RefreshEntity<T>(T entity)
            where T : class
            ;

        T EnsureTypedEntity<T>(object entity);
        object EnsureTypedEntityByType(object entity, Type type);
        IList<T> EnsureTypedList<T>(IEnumerable responseData, bool forceNotNull = false);
        IList EnsureTypedListByType(IEnumerable responseData, Type type, bool forceNotNull = false);
    }
}