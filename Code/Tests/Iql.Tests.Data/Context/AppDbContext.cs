
using System;
using Iql.Conversion;
using Iql.Data.DataStores;
using Iql.Data.DataStores.InMemory;
using Iql.Data.Queryable;
using Iql.Entities;
using Iql.Entities.SpecialTypes;
using Iql.OData;
using Iql.Tests.Data.Context;
using Iql.Tests.Data.Context.Custom;
using Iql.Tests.Tests.OData;
using IqlSampleApp.ApiContext.Base;
using Iql.Tests.Data.Services;
#if TypeScript
using Iql.JavaScript.JavaScriptExpressionToIql;
#else
using Iql.DotNet;
#endif
using IqlSampleApp.Data.Entities;

namespace Iql.Tests.Context
{
    public class AppDbContext : IqlSampleAppDataContextBase
    {
        public virtual bool UseStaticInMemoryData { get; } = true;
        static AppDbContext()
        {
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

        public MyCustomReportsSet MyCustomReports { get; set; }
        public static InMemoryDataStoreConfiguration InMemoryDataStoreConfiguration { get; set; }

        public AppDbContext(IDataStore dataStore = null) :
            base(dataStore)
        {
            DataStore = DataStore ?? new InMemoryDataStore("InMemory");
            Initialise();
        }

        public InMemoryDataStore InMemoryDataStore => DataStore as InMemoryDataStore;
        protected virtual void Initialise()
        {
            if (InMemoryDataStoreConfiguration == null)
            {
                InMemoryDataStoreConfiguration = new InMemoryDataStoreConfiguration(EntityConfigurationContext);
                InMemoryDataStoreConfiguration.RegisterSource(() => InMemoryDb.UserSites);
                InMemoryDataStoreConfiguration.RegisterSource(() => InMemoryDb.UserSettings);
                InMemoryDataStoreConfiguration.RegisterSource(() => InMemoryDb.ApplicationLogs);
                InMemoryDataStoreConfiguration.RegisterSource(() => InMemoryDb.SiteDocuments);
                InMemoryDataStoreConfiguration.RegisterSource(() => InMemoryDb.PersonReports);
                InMemoryDataStoreConfiguration.RegisterSource(() => InMemoryDb.ReportDefaultRecommendations);
                InMemoryDataStoreConfiguration.RegisterSource(() => InMemoryDb.ReportRecommendations);
                InMemoryDataStoreConfiguration.RegisterSource(() => InMemoryDb.ReportReceiverEmailAddresses);
                InMemoryDataStoreConfiguration.RegisterSource(() => InMemoryDb.ReportTypes);
                InMemoryDataStoreConfiguration.RegisterSource(() => InMemoryDb.RiskAssessmentAnswers);
                InMemoryDataStoreConfiguration.RegisterSource(() => InMemoryDb.RiskAssessmentQuestions);
                InMemoryDataStoreConfiguration.RegisterSource(() => InMemoryDb.ReportActionsTaken);
                InMemoryDataStoreConfiguration.RegisterSource(() => InMemoryDb.Users);
                InMemoryDataStoreConfiguration.RegisterSource(() => InMemoryDb.DocumentCategories);
                InMemoryDataStoreConfiguration.RegisterSource(() => InMemoryDb.MyCustomReports);
                InMemoryDataStoreConfiguration.RegisterSource(() => InMemoryDb.ClientTypes);
                InMemoryDataStoreConfiguration.RegisterSource(() => InMemoryDb.ClientCategories);
                InMemoryDataStoreConfiguration.RegisterSource(() => InMemoryDb.ClientCategoriesPivot);
                InMemoryDataStoreConfiguration.RegisterSource(() => InMemoryDb.ClientTypes);
                InMemoryDataStoreConfiguration.RegisterSource(() => InMemoryDb.Clients);
                InMemoryDataStoreConfiguration.RegisterSource(() => InMemoryDb.Sites);
                InMemoryDataStoreConfiguration.RegisterSource(() => InMemoryDb.SiteAreas);
                InMemoryDataStoreConfiguration.RegisterSource(() => InMemoryDb.PersonLoadings);
                InMemoryDataStoreConfiguration.RegisterSource(() => InMemoryDb.Projects);
                InMemoryDataStoreConfiguration.RegisterSource(() => InMemoryDb.People);
                InMemoryDataStoreConfiguration.RegisterSource(() => InMemoryDb.PeopleTypes);
                InMemoryDataStoreConfiguration.RegisterSource(() => InMemoryDb.PeopleTypeMap);
                InMemoryDataStoreConfiguration.RegisterSource(() => InMemoryDb.PersonInspections);
                InMemoryDataStoreConfiguration.RegisterSource(() => InMemoryDb.ReportCategories);
                InMemoryDataStoreConfiguration.RegisterSource(() => InMemoryDb.SiteInspections);
                InMemoryDataStoreConfiguration.RegisterSource(() => InMemoryDb.RiskAssessments);
                InMemoryDataStoreConfiguration.RegisterSource(() => InMemoryDb.RiskAssessmentSolutions);
            }
            if (UseStaticInMemoryData && DataStore is InMemoryDataStore)
            {
                (DataStore as InMemoryDataStore).Configuration = InMemoryDataStoreConfiguration;
            }

            ServiceProvider.Register<TestNowService>();
            // var defaultQueries = new EntityDefaultQueryConfiguration();
            // defaultQueries.ConfigureDefaultGetOperations<ClientType>(_ => _.Expand(c => c.Clients));
            // RegisterConfiguration(defaultQueries);
            ODataConfiguration.ApiUriBase = () => @"http://localhost:28000/odata";
            ODataConfiguration.HttpProvider = new ODataFakeHttpProvider();
            if (DataStore is ODataDataStore)
            {
                (DataStore as ODataDataStore).Configuration = ODataConfiguration;
            }
        }

        private void ConfigureCustomReports()
        {
            var customReportLocalConfiguration = EntityConfigurationContext.EntityType<MyCustomReport>();
            customReportLocalConfiguration.SetName = "MyCustomReports";
            customReportLocalConfiguration
                .DefineProperty(_ => _.MyId, false, IqlType.Guid)
                .DefineProperty(_ => _.MyUserId, true, IqlType.String)
                .DefineProperty(_ => _.MyName, false, IqlType.String)
                .DefineProperty(_ => _.MyEntityType, true, IqlType.String)
                .DefineProperty(_ => _.MyIql, true, IqlType.String)
                .DefineProperty(_ => _.MyFields, true, IqlType.String)
                .DefineProperty(_ => _.MySort, true, IqlType.String)
                .DefineProperty(_ => _.MySortDescending, true, IqlType.String)
                .DefineProperty(_ => _.MySearch, true, IqlType.String)
                .HasKey(_ => _.MyId);
            ;
            EntityConfigurationContext.CustomReportsDefinition = CustomReportsDefinition.Define(
                EntityConfigurationContext.EntityType<MyCustomReport>(),
                _ => _.MyId,
                _ => _.MyUserId,
                _ => _.MyName,
                _ => _.MyEntityType,
                _ => _.MyIql,
                _ => _.MyFields,
                _ => _.MySort,
                _ => _.MySortDescending,
                _ => _.MySearch);
        }

        protected override void InitializeProperties()
        {
            base.InitializeProperties();
            MyCustomReports = AsCustomDbSet<MyCustomReport, Guid, MyCustomReportsSet>();
        }

        public static InMemoryDataBase InMemoryDb { get; set; }

        public override void Configure(EntityConfigurationBuilder builder)
        {
            base.Configure(builder);
            builder.EntityType<Person>()
                .DefineDisplayFormatter(entity => entity.Title + " - " + entity.Type.CreatedByUser.Client.Name + " (" + entity.Id + ")", "ReportLong");
            ConfigureCustomReports();
        }

        public override void ConfigureInstance()
        {
            base.ConfigureInstance();
            var defaultQueries = new EntityDefaultQueryConfiguration();
            defaultQueries.AlwaysIncludeCount = false;
            defaultQueries.ConfigureDefaultGetOperations<ClientType>(_ => _.Expand(c => c.Clients));
            RegisterConfiguration(defaultQueries);
        }
    }
}
