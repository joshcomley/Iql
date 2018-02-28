using Iql.Queryable;
using Iql.Queryable.Data;
using Iql.Queryable.Data.DataStores;
#if TypeScript
using Iql.JavaScript.JavaScriptExpressionToIql;
using Iql.JavaScript.QueryToJavaScript;
#else
using Iql.DotNet;
using Iql.DotNet.QueryableApplicator;
#endif
using Iql.Queryable.Data.DataStores.InMemory;
using Iql.Queryable.Data.EntityConfiguration;
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
            base(new InMemoryDataStore(new JavaScriptQueryableAdapter()))
#else
            //base(new InMemoryDataStore(new JavaScriptQueryableAdapter()))
            base(dataStore ?? new InMemoryDataStore(new DotNetQueryableAdapter()))
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
            ODataConfiguration.ApiUriBase = @"http://localhost:28000/odata";
            ODataConfiguration.HttpProvider = new ODataFakeHttpProvider();
        }

        public static InMemoryDataBase InMemoryDb { get; set; }

        public override void Configure(EntityConfigurationBuilder builder)
        {
            base.Configure(builder);
            builder.EntityType<ApplicationUser>().PrimaryKeyIsGeneratedRemotely();
            builder.EntityType<Client>().PrimaryKeyIsGeneratedRemotely();
            builder.EntityType<ClientType>().PrimaryKeyIsGeneratedRemotely();
            builder.EntityType<DocumentCategory>().PrimaryKeyIsGeneratedRemotely();
            builder.EntityType<SiteDocument>().PrimaryKeyIsGeneratedRemotely();
            builder.EntityType<ReportActionsTaken>().PrimaryKeyIsGeneratedRemotely();
            builder.EntityType<ReportCategory>().PrimaryKeyIsGeneratedRemotely();
            builder.EntityType<ReportDefaultRecommendation>().PrimaryKeyIsGeneratedRemotely();
            builder.EntityType<ReportRecommendation>().PrimaryKeyIsGeneratedRemotely();
            builder.EntityType<ReportType>().PrimaryKeyIsGeneratedRemotely();
            builder.EntityType<Project>().PrimaryKeyIsGeneratedRemotely();
            builder.EntityType<ReportReceiverEmailAddress>().PrimaryKeyIsGeneratedRemotely();
            builder.EntityType<RiskAssessment>().PrimaryKeyIsGeneratedRemotely();
            builder.EntityType<RiskAssessmentSolution>().PrimaryKeyIsGeneratedRemotely();
            builder.EntityType<RiskAssessmentAnswer>().PrimaryKeyIsGeneratedRemotely();
            builder.EntityType<RiskAssessmentQuestion>().PrimaryKeyIsGeneratedRemotely();
            builder.EntityType<Person>().PrimaryKeyIsGeneratedRemotely();
            builder.EntityType<PersonInspection>().PrimaryKeyIsGeneratedRemotely();
            builder.EntityType<PersonLoading>().PrimaryKeyIsGeneratedRemotely();
            builder.EntityType<PersonType>().PrimaryKeyIsGeneratedRemotely();
            builder.EntityType<PersonTypeMap>().PrimaryKeyIsGeneratedRemotely();
            builder.EntityType<PersonReport>().PrimaryKeyIsGeneratedRemotely();
            builder.EntityType<Site>().PrimaryKeyIsGeneratedRemotely();
            builder.EntityType<SiteInspection>().PrimaryKeyIsGeneratedRemotely();
            builder.EntityType<UserSite>().PrimaryKeyIsGeneratedRemotely();
        }
    }
}
