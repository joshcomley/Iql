using Hazception.ApiContext.Base;
#if TypeScript
using Iql.JavaScript.JavaScriptExpressionToIql;
#else
using Iql.DotNet;
#endif
using Iql.Queryable;
using Iql.Queryable.Data.DataStores;
using Iql.Queryable.Data.DataStores.InMemory;
using Iql.Queryable.Expressions;

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
            InMemoryDataStoreConfiguration = new InMemoryDataStoreConfiguration();
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

        public static HazceptionInMemoryDataBase InMemoryDb { get; set; }

        public HazceptionDataContext(IDataStore dataStore = null) :
#if TypeScript
            base(dataStore ?? new InMemoryDataStore(new JavaScriptQueryableAdapter()))
#else
            //base(new InMemoryDataStore(new JavaScriptQueryableAdapter()))
            base(dataStore ?? new InMemoryDataStore())
#endif
        {
            ODataConfiguration.ApiUriBase = @"http://localhost:58000/odata";
            RegisterConfiguration(InMemoryDataStoreConfiguration);
            this.ODataConfiguration.HttpProvider = new ODataFakeHttpProvider();
        }
    }
}
