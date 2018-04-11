using Iql.JavaScript.QueryableApplicator;
using Iql.Queryable;
using Iql.Queryable.Data;
using Iql.Queryable.Data.DataStores;
#if TypeScript
using Iql.JavaScript.JavaScriptExpressionToIql;
#else
using Iql.DotNet;
using Iql.DotNet.QueryableApplicator;
#endif
using Iql.Queryable.Data.DataStores.InMemory;
using Iql.Queryable.Data.DataStores.InMemory.QueryApplicator;
using Iql.Queryable.Data.EntityConfiguration;
using Iql.Queryable.Data.Queryable;
using Tunnel.ApiContext.Base;
using Tunnel.App.Data.Entities;

namespace Iql.Tests.Context
{
    public class AppDbContext : TunnelDataContextBase
    {
        static AppDbContext()
        {
            InMemoryDataStoreConfiguration = new InMemoryDataStoreConfiguration();
            var inMemoryDb = new InMemoryDataBase();
            InMemoryDb = inMemoryDb;
            if (IqlQueryableAdapter.ExpressionConverter == null)
            {
                IqlQueryableAdapter.ExpressionConverter = () =>
#if TypeScript
                    new JavaScriptExpressionConverter();
#else
                    new DotNetExpressionConverter();
#endif
            }
        }

        public static InMemoryDataStoreConfiguration InMemoryDataStoreConfiguration { get; set; }

        public AppDbContext(IDataStore dataStore = null) :
#if TypeScript
            base(dataStore ?? new InMemoryDataStore(new JavaScriptQueryableAdapter()))
#else
            //base(new InMemoryDataStore(new JavaScriptQueryableAdapter()))
            base(dataStore ?? new InMemoryDataStore(new DotNetQueryableAdapter()))
#endif
        {
            RegisterConfiguration(InMemoryDataStoreConfiguration);
            InMemoryDataStoreConfiguration.RegisterSource(() => InMemoryDb.Users);
            InMemoryDataStoreConfiguration.RegisterSource(() => InMemoryDb.ClientTypes);
            InMemoryDataStoreConfiguration.RegisterSource(() => InMemoryDb.Clients);
            InMemoryDataStoreConfiguration.RegisterSource(() => InMemoryDb.Sites);
            InMemoryDataStoreConfiguration.RegisterSource(() => InMemoryDb.People);
            InMemoryDataStoreConfiguration.RegisterSource(() => InMemoryDb.PeopleTypes);
            InMemoryDataStoreConfiguration.RegisterSource(() => InMemoryDb.PeopleTypeMap);
            InMemoryDataStoreConfiguration.RegisterSource(() => InMemoryDb.PersonInspections);
            InMemoryDataStoreConfiguration.RegisterSource(() => InMemoryDb.ReportCategories);
            InMemoryDataStoreConfiguration.RegisterSource(() => InMemoryDb.SiteInspections);
            InMemoryDataStoreConfiguration.RegisterSource(() => InMemoryDb.RiskAssessments);
            InMemoryDataStoreConfiguration.RegisterSource(() => InMemoryDb.RiskAssessmentSolutions);
            var defaultQueries = new EntityDefaultQueryConfiguration();
            defaultQueries.ConfigureDefaultGetOperations(() => ClientTypes.Expand(c => c.Clients));
            RegisterConfiguration(defaultQueries);
            ODataConfiguration.ApiUriBase = @"http://localhost:28000/odata";
            ODataConfiguration.HttpProvider = new ODataFakeHttpProvider();
        }

        public static InMemoryDataBase InMemoryDb { get; set; }

        public override void Configure(EntityConfigurationBuilder builder)
        {
            base.Configure(builder);
        }
    }
}
