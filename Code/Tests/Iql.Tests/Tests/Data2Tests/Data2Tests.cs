// using System;
// using System.Threading.Tasks;
// using Iql.Data.Http;
// using Iql.OData;
// using Microsoft.VisualStudio.TestTools.UnitTesting;
//
// namespace Iql.Tests.Tests.Data2Tests;
//
// [TestClass]
// public class Data2Tests
// {
//     public class ODataFakeHttpProvider : IHttpProvider
//     {
//         public Task<IHttpResult> Get(string uri, IHttpRequest payload = null)
//         {
//             return Task.FromResult<IHttpResult>(HttpResult.FromString(""));
//         }
//
//         public Task<IHttpResult> Post(string uri, IHttpRequest payload = null)
//         {
//             throw new NotImplementedException();
//         }
//
//         public Task<IHttpResult> Put(string uri, IHttpRequest payload = null)
//         {
//             throw new NotImplementedException();
//         }
//
//         public Task<IHttpResult> Delete(string uri, IHttpRequest payload = null)
//         {
//             throw new NotImplementedException();
//         }
//     }
//
//     [TestMethod]
//     public async Task PropertyStateCrudEventsTest()
//     {
//         var oDataDataStore = new ODataDataStore();
//         var db = new AppDbContext2(oDataDataStore);
//         oDataDataStore.Configuration = new ODataConfiguration(() => db.EntityConfigurationContext);
//         oDataDataStore.Configuration.HttpProvider = new ODataFakeHttpProvider();
//     }
// }