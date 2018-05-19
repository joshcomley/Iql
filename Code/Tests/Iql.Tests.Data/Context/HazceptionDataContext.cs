﻿using Hazception.ApiContext.Base;
using Iql.Conversion;
using Iql.Data.DataStores;
using Iql.Data.DataStores.InMemory;
#if TypeScript
using Iql.JavaScript.JavaScriptExpressionToIql;
#else
using Iql.DotNet;
#endif
using Iql.Queryable;
using Iql.Tests.Data.Context;

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
            base(dataStore ?? new InMemoryDataStore())
        {
            ODataConfiguration.ApiUriBase = @"http://localhost:58000/odata";
            RegisterConfiguration(InMemoryDataStoreConfiguration);
            this.ODataConfiguration.HttpProvider = new ODataFakeHttpProvider();
        }
    }
}
