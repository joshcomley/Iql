
using Iql.Conversion;
using Iql.Data.DataStores;
using Iql.Data.DataStores.InMemory;
using Iql.Data.Queryable;
using Iql.Entities;
using Iql.Tests.Data.Context;
#if TypeScript
using Iql.JavaScript.JavaScriptExpressionToIql;
#else
using Iql.DotNet;
#endif
using Tunnel.ApiContext.Base;

namespace Iql.Tests.Context
{
    public class AppDbContext : TunnelDataContextBase
    {
        static AppDbContext()
        {
            InMemoryDataStoreConfiguration = new InMemoryDataStoreConfiguration();
            var inMemoryDb = new InMemoryDataBase();
            InMemoryDb = inMemoryDb;
            if (IqlExpressionConversion.DefaultExpressionConverter == null)
            {
                IqlExpressionConversion.DefaultExpressionConverter = () =>
#if TypeScript
                    new JavaScriptExpressionConverter();
#else
                    new DotNetExpressionConverter();
#endif
            }
        }

        public static InMemoryDataStoreConfiguration InMemoryDataStoreConfiguration { get; set; }

        public AppDbContext(IDataStore dataStore = null) :
            base(dataStore ?? new InMemoryDataStore())
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
