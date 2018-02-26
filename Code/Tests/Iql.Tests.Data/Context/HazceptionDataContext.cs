using System.Collections.Generic;
using System.Text;
using Hazception.ApiContext.Base;
using Iql.JavaScript.JavaScriptExpressionToIql.Expressions.JavaScript;
#if TypeScript
using Iql.JavaScript.QueryToJavaScript;
using Iql.JavaScript.JavaScriptExpressionToIql.Expressions.JavaScript;
#else
using Iql.DotNet.Queryable;
using Iql.DotNet;
#endif
using Iql.OData.Data;
using Iql.Queryable;
using Iql.Queryable.Data.DataStores;
using Iql.Queryable.Data.DataStores.InMemory;

namespace Iql.Tests.Context
{
    public class HazceptionDataContext : HazceptionDataContextBase
    {
        public static InMemoryDataStoreConfiguration InMemoryDataStoreConfiguration { get; set; }
        static HazceptionDataContext()
        {
            if (IqlQueryableAdapter.ExpressionConverter == null)
            {
#if TypeScript
                IqlQueryableAdapter.ExpressionConverter = () => new JavaScriptExpressionConverter();
#else
                IqlQueryableAdapter.ExpressionConverter = () => new DotNetExpressionConverter();
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
            base(dataStore ?? new InMemoryDataStore(new DotNetQueryableAdapter()))
#endif
        {
            ODataConfiguration.ApiUriBase = @"http://localhost:58000/odata";
            RegisterConfiguration(InMemoryDataStoreConfiguration);
            this.ODataConfiguration.HttpProvider = new ODataFakeHttpProvider();
        }
    }
}
