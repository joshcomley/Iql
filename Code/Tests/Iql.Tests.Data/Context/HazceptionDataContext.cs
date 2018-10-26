using Hazception.ApiContext.Base;
using Iql.Conversion;
using Iql.Data.DataStores;
using Iql.Data.DataStores.InMemory;
using Iql.Entities;
#if TypeScript
using Iql.JavaScript.JavaScriptExpressionToIql;
#else
using Iql.DotNet;
#endif
using Iql.Queryable;
using Iql.Tests.Data.Context;
using IqlSampleApp.Data.Entities;

namespace Iql.Tests.Context
{
    public class HazceptionDataContext : HazceptionDataContextBase
    {
        public static InMemoryDataStoreConfiguration InMemoryDataStoreConfiguration { get; set; }
        static HazceptionDataContext()
        {
            if (IqlExpressionConversion.DefaultExpressionConverter == null)
            {
#if TypeScript
                IqlExpressionConversion.DefaultExpressionConverter = () => new JavaScriptExpressionConverter();
#else
                IqlExpressionConversion.DefaultExpressionConverter = () => new DotNetExpressionConverter();
#endif
            }
        }

        public static HazceptionInMemoryDataBase InMemoryDb { get; set; }

        public HazceptionDataContext(IDataStore dataStore = null) :
            base(dataStore ?? new InMemoryDataStore())
        {
            if (InMemoryDataStoreConfiguration == null)
            {
                InMemoryDataStoreConfiguration = new InMemoryDataStoreConfiguration(EntityConfigurationContext);
                InMemoryDataStoreConfiguration.RegisterSource(() => InMemoryDb.ClientTypes);
                InMemoryDataStoreConfiguration.RegisterSource(() => InMemoryDb.Clients);
                InMemoryDataStoreConfiguration.RegisterSource(() => InMemoryDb.Users);
                InMemoryDataStoreConfiguration.RegisterSource(() => InMemoryDb.Hazards);
                InMemoryDataStoreConfiguration.RegisterSource(() => InMemoryDb.Videos);
                InMemoryDataStoreConfiguration.RegisterSource(() => InMemoryDb.Exams);
                InMemoryDataStoreConfiguration.RegisterSource(() => InMemoryDb.ExamManagers);
                InMemoryDataStoreConfiguration.RegisterSource(() => InMemoryDb.ExamCandidates);
                InMemoryDataStoreConfiguration.RegisterSource(() => InMemoryDb.ExamCandidateResults);
                InMemoryDataStoreConfiguration.RegisterSource(() => InMemoryDb.ExamResults);
                var inMemoryDb = new HazceptionDataStore().GetData();
                InMemoryDb = inMemoryDb;
            }
            ODataConfiguration.ApiUriBase = @"http://localhost:58000/odata";
            RegisterConfiguration(InMemoryDataStoreConfiguration);
            this.ODataConfiguration.HttpProvider = new ODataFakeHttpProvider();
        }
    }
}
