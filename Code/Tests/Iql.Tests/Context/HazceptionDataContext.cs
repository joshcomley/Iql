using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Hazception.ApiContext.Base;
using Iql.JavaScript.JavaScriptExpressionToIql.Expressions.JavaScript;
#if TypeScript
using Iql.JavaScript.QueryToJavaScript;
using Iql.JavaScript.JavaScriptExpressionToIql.Expressions.JavaScript;
#else
using Iql.DotNet.Queryable;
using Iql.DotNet;
using System.Net.Http;
#endif
using Iql.OData.Data;
using Iql.Queryable;
using Iql.Queryable.Data.DataStores;
using Iql.Queryable.Data.DataStores.InMemory;
using Iql.Queryable.Data.Http;

namespace Iql.Tests.Context
{
    public class HazceptionDataContext : HazceptionDataContextBase
    {
        public static InMemoryDataStoreConfiguration InMemoryDataStoreConfiguration { get; set; }
        static HazceptionDataContext()
        {
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

        public HazceptionDataContext() :
#if TypeScript
            base(new InMemoryDataStore(new JavaScriptQueryableAdapter()))
#else
            //base(new InMemoryDataStore(new JavaScriptQueryableAdapter()))
            base(new InMemoryDataStore(new DotNetQueryableAdapter()))
#endif
        {
            ODataConfiguration.ApiUriBase = @"http://localhost:58000/odata";
            RegisterConfiguration(InMemoryDataStoreConfiguration);
            this.ODataConfiguration.HttpProvider = new ODataHttpProvider();
            if (IqlQueryableAdapter.ExpressionConverter == null)
            {
                IqlQueryableAdapter.ExpressionConverter = () => new JavaScriptExpressionToIqlConverter();
            }
        }
    }

    public class ODataHttpProvider : IHttpProvider
    {
        public async Task<IHttpResult> Get(string uri, IHttpRequest payload = null)
        {
#if TypeScript
            throw new NotImplementedException();
#else
            var result = await new HttpClient().GetAsync(uri);
            return new HttpResult(await result.Content.ReadAsStringAsync(), result.IsSuccessStatusCode);
#endif
        }

        public Task<IHttpResult> Post(string uri, IHttpRequest payload = null)
        {
            throw new NotImplementedException();
        }

        public Task<IHttpResult> Put(string uri, IHttpRequest payload = null)
        {
            throw new NotImplementedException();
        }

        public Task<IHttpResult> Delete(string uri, IHttpRequest payload = null)
        {
            throw new NotImplementedException();
        }
    }
}
