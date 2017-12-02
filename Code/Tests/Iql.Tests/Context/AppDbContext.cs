using Iql.JavaScript.QueryToJavaScript;
using Iql.Queryable;
#if TypeScript
using Iql.JavaScript.QueryToJavaScript;
using Iql.JavaScript.JavaScriptExpressionToIql.Expressions.JavaScript;
#else
using Iql.DotNet.Queryable;
using Iql.DotNet;
#endif
using Iql.Queryable.Data.DataStores.InMemory;

namespace Iql.Tests.Context
{
    public class AppDbContext : ISiteDataContextBase
    {
        static AppDbContext()
        {
#if TypeScript
            IqlQueryableAdapter.ExpressionConverter = () => new JavaScriptExpressionToIqlConverter();
#else
            IqlQueryableAdapter.ExpressionConverter = () => new ExpressionToIqlConverter();
#endif
            InMemoryDataStoreConfiguration = new InMemoryDataStoreConfiguration();
            var inMemoryDb = new InMemoryDataBase();
            InMemoryDb = inMemoryDb;
        }

        public static InMemoryDataStoreConfiguration InMemoryDataStoreConfiguration { get; set; }

        public AppDbContext() :
#if TypeScript
            base(new InMemoryDataStore(new JavaScriptQueryableAdapter()))
#else
            //base(new InMemoryDataStore(new JavaScriptQueryableAdapter()))
            base(new InMemoryDataStore(new DotNetQueryableAdapter()))
#endif
        {
            RegisterConfiguration(InMemoryDataStoreConfiguration);
            InMemoryDataStoreConfiguration.RegisterSource(() => InMemoryDb.ClientTypes);
            InMemoryDataStoreConfiguration.RegisterSource(() => InMemoryDb.Clients);
        }

        public static InMemoryDataBase InMemoryDb { get; set; }
    }
}
