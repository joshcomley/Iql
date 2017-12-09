using Iql.JavaScript.QueryToJavaScript;
using Iql.Queryable;
using Iql.Queryable.Data;
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
    public class AppDbContext : TunnelDataContextBase
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
            InMemoryDataStoreConfiguration.RegisterSource(() => InMemoryDb.Sites);
            InMemoryDataStoreConfiguration.RegisterSource(() => InMemoryDb.People);
            InMemoryDataStoreConfiguration.RegisterSource(() => InMemoryDb.PeopleTypes);
            InMemoryDataStoreConfiguration.RegisterSource(() => InMemoryDb.PeopleTypeMap);
            InMemoryDataStoreConfiguration.RegisterSource(() => InMemoryDb.SiteInspections);
            InMemoryDataStoreConfiguration.RegisterSource(() => InMemoryDb.RiskAssessments);
            InMemoryDataStoreConfiguration.RegisterSource(() => InMemoryDb.RiskAssessmentSolutions);
            var defaultQueries = new EntityDefaultQueryConfiguration();
            defaultQueries.ConfigureDefaultGetOperations(() => ClientTypes.Expand(c => c.Clients));
            RegisterConfiguration(defaultQueries);
        }

        public static InMemoryDataBase InMemoryDb { get; set; }
    }
}
